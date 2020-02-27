using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public static class UpdateOperation
    {
        public static async Task<bool> HandleListingUpdate(CoreListingRequest message, IOutputPort<CoreListingResponse> outputPort, 
            ICategoryRepository categoryRepository, IMongoDbContext transaction, IMerchantRepository merchantRepository, IListingRepository listingRepository, Listing oldListing = null)
        {
            #region Handle Find Merchant, Category, find related workflow and create new listing object
            var findMerchantResponse = await GetMerchant(message.MerchantId, merchantRepository);
            if (!findMerchantResponse.Success) //merchant not found
            {
                outputPort.Handle(new CoreListingResponse(findMerchantResponse.Errors, false, findMerchantResponse.Message));
                return false;
            }
            var newMerchant = findMerchantResponse.Merchant;

            var findCategoryResponse = await categoryRepository.FindByIdAsync(message.CategoryId).ConfigureAwait(false);
            if (!findCategoryResponse.Success)
            {
                outputPort.Handle(new CoreListingResponse(findCategoryResponse.Errors, false, findCategoryResponse.Message));
                return false;
            }

            var newListing = ListingFactory.CreateListing(message.CategoryType, message, findCategoryResponse.Data, newMerchant);
            #endregion
            if (newListing != null) //validate is Listing Type
            {
                newListing.ListingId = message.ListingId;
                newListing.PublishStatus = oldListing.PublishStatus;
                //start transaction
                using (var session = await transaction.StartSession())
                {
                    CoreListingResponse updateListingResponse;
                    try
                    {
                        session.StartTransaction();
                        //Handle Switch Ownership
                        var oldMerchant = await GetOldMerchant(message.ListingId, listingRepository, merchantRepository);
                        if (IsSwitchingOwnership(newMerchant, oldMerchant))
                        {
                            var toBeUpdateMerchantList = newListing.SwitchOwnershipFromTo(oldMerchant, newMerchant);

                            foreach (var m in toBeUpdateMerchantList)
                            {
                                var updateMerchantResponse = await merchantRepository.UpdateMerchantAsyncWithSession(m.MerchantId, m, session);
                                if (!updateMerchantResponse.Success)
                                {
                                    outputPort.Handle(new CoreListingResponse(updateMerchantResponse.Errors, false, updateMerchantResponse.Message));
                                    return false;
                                }

                            }
                        }//Handle Switch Ownership End
                        newListing.Updated();

                        updateListingResponse = await listingRepository.UpdateAsyncWithSession(newListing, session);
                    }
                    catch
                    {
                        await session.AbortTransactionAsync();
                        throw;
                    }
                    await session.CommitTransactionAsync();
                    outputPort.Handle(updateListingResponse);
                    return updateListingResponse.Success;
                }
            }
            outputPort.Handle(new CoreListingResponse(new List<string> { "Update Listing:Unknown Error" }));
            return false;
        }

        private static async Task<GetMerchantResponse> GetMerchant(string id, IMerchantRepository merchantRepository)
        {
            return await merchantRepository.FindByIdAsync(id).ConfigureAwait(false);
        }

        private static async Task<Merchant> GetOldMerchant(string id, IListingRepository listingRepository, IMerchantRepository merchantRepository)
        {
            var getListingResponse = await listingRepository.FindById(id);
            if (!getListingResponse.Success) return null;
            var merchantId = getListingResponse.Listing.Merchant.MerchantId;
            var merchant = (await GetMerchant(merchantId, merchantRepository)).Merchant;
            return merchant;
        }
        private static bool IsSwitchingOwnership(Merchant newMerchant, Merchant oldMerchant)
        {
            return oldMerchant.MerchantId != newMerchant.MerchantId;
        }
    }
}