using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public static class UpdateOperation
    {
        public static async Task<bool> HandleListingUpdate(CoreListingRequest message, IOutputPort<CoreListingResponse> outputPort, ICategoryRepository _categoryRepository, IMongoDbContext _transaction, IMerchantRepository _merchantRepository, IListingRepository _listingRepository)
        {
            #region Handle Find Merchant, Category, find related workflow and create new listing object
            var FindMerchantResponse = await GetMerchant(message.MerchantId, _merchantRepository);
            if (!FindMerchantResponse.Success) //merchant not found
            {
                outputPort.Handle(new CoreListingResponse(FindMerchantResponse.Errors, false, FindMerchantResponse.Message));
                return false;
            }
            var NewMerchant = FindMerchantResponse.Merchant;

            var FindCategoryResponse = await _categoryRepository.FindByIdAsync(message.CategoryId).ConfigureAwait(false);
            if (!FindCategoryResponse.Success)
            {
                outputPort.Handle(new CoreListingResponse(FindCategoryResponse.Errors, false, FindCategoryResponse.Message));
                return false;
            }

            var NewListing = ListingFactory.CreateListing(ListingTypeEnum.For(message.ListingType), message, FindCategoryResponse.Category, NewMerchant);
            #endregion
            if (NewListing is Listing && NewListing != null) //validate is Listing Type

            {
                NewListing.ListingId = message.ListingId;
                //start transaction
                using (var session = await _transaction.StartSession())
                {
                    CoreListingResponse updateListingResponse;
                    try
                    {
                        session.StartTransaction();
                        //Handle Switch Ownership
                        var OldMerchant = await GetOldMerchant(message.ListingId, _listingRepository, _merchantRepository);
                        if (IsSwitchingOwnership(NewMerchant, OldMerchant))
                        {
                            var ToBeUpdateMerchantList = NewListing.SwitchOwnershipFromTo(OldMerchant, NewMerchant);

                            foreach (var m in ToBeUpdateMerchantList)
                            {
                                var UpdateMerchantResponse = await _merchantRepository.UpdateMerchantAsyncWithSession(m.MerchantId, m, session);
                                if (!UpdateMerchantResponse.Success)
                                {
                                    outputPort.Handle(new CoreListingResponse(UpdateMerchantResponse.Errors, false, UpdateMerchantResponse.Message));
                                    return false;
                                }

                            }
                        }//Handle Switch Ownership End
                        NewListing.Updated();

                        updateListingResponse = await _listingRepository.UpdateAsyncWithSession(NewListing, session);
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

        private static async Task<GetMerchantResponse> GetMerchant(string id, IMerchantRepository _merchantRepository)
        {
            return await _merchantRepository.FindByIdAsync(id).ConfigureAwait(false);
        }

        private static async Task<Merchant> GetOldMerchant(string id, IListingRepository _listingRepository, IMerchantRepository _merchantRepository)
        {
            var GetListingResponse = await _listingRepository.FindById(id);
            if (GetListingResponse.Success)
            {
                var merchantId = GetListingResponse.Listing.Merchant.MerchantId;
                var merchant = (await GetMerchant(merchantId, _merchantRepository)).Merchant;
                return merchant;
            }
            return null;
        }
        private static bool IsSwitchingOwnership(Merchant newMerchant, Merchant oldMerchant)
        {
            return oldMerchant.MerchantId != newMerchant.MerchantId;
        }
    }
}