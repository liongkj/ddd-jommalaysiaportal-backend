using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingUseCase : ICreateListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMongoDbContext _transaction;

        public CreateListingUseCase(IListingRepository listingRepository, ICategoryRepository categoryRepository,
        IMerchantRepository merchantRepository,
        IMongoDbContext transaction
       )
        {
            _merchantRepository = merchantRepository;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _transaction = transaction;
        }
        public async Task<bool> Handle(CoreListingRequest message, IOutputPort<CoreListingResponse> outputPort)
        {
            ///find merchant and add to merchant
            var FindMerchantResponse = await _merchantRepository.FindByIdAsync(message.MerchantId).ConfigureAwait(false);
            if (!FindMerchantResponse.Success) //merchant not found
            {
                outputPort.Handle(new CoreListingResponse(FindMerchantResponse.Errors, false, FindMerchantResponse.Message));
                return false;
            }

            //verify is there this category
            Category category = null;
            if (!string.IsNullOrWhiteSpace(message.CategoryId))
            {
                var FindCategoryResponse = await _categoryRepository.FindByIdAsync(message.CategoryId).ConfigureAwait(false);
                if (!FindCategoryResponse.Success)
                {
                    outputPort.Handle(new CoreListingResponse(FindCategoryResponse.Errors, false, FindCategoryResponse.Message));
                    return false;
                }
                category = FindCategoryResponse.Category;
                if (category.IsCategory())
                {
                    outputPort.Handle(new CoreListingResponse(new List<string> { "Bad Request" }, false, "Please select a valid subcategory"));
                    return false;
                }
            }

            var ListingType = ListingTypeEnum.For(message.ListingType);
            if (ListingType == null)
            {
                outputPort.Handle(new CoreListingResponse(new List<string> { "Bad Request" }, false, "Please select a valid listing type"));
                return false;
            }
            //create listing factory pattern
            var NewListing = ListingFactory.CreateListing(ListingType, message, category, FindMerchantResponse.Data);
            if (NewListing is Listing && NewListing != null) //validate is Listing Type
            {
                //start transaction
                using (var session = await _transaction.StartSession())
                {
                    try
                    {
                        var MerchantUser = FindMerchantResponse.Data;
                        session.StartTransaction();

                        //create Listing command
                        var listing = await _listingRepository.CreateListingAsync(NewListing, session).ConfigureAwait(false);
                        NewListing.ListingId = listing.Id;//retrieve the created listing Id and add into merchant
                        MerchantUser.AddNewListing(NewListing);
                        //update merchant command
                        var UpdateMerchantCommand = await _merchantRepository.UpdateMerchantAsyncWithSession(MerchantUser.MerchantId, MerchantUser, session).ConfigureAwait(false);
                        if (UpdateMerchantCommand.Success)
                        {
                            await session.CommitTransactionAsync();
                            outputPort.Handle(new CoreListingResponse(listing.Id + " Listing Created Successfully", true));
                            return true;
                        }
                        else
                        {
                            outputPort.Handle(new CoreListingResponse(UpdateMerchantCommand.Errors, UpdateMerchantCommand.Success, UpdateMerchantCommand.Message));
                            return false;
                        }
                        // outputPort.Handle(new CreateListingResponse(listing.Id, true, $"{ GetType().Name } failed"));
                        // return false;
                    }
                    catch (Exception e)
                    {
                        outputPort.Handle(new CoreListingResponse(
                            $"{GetType().Name} Transaction Error",
                            false,
                             e.ToString()));
                        return false;
                    }


                }

            }
            else
            {

                outputPort.Handle(new CoreListingResponse(new List<string> { "Listing Factory Exception" }, false, "Create Listing Operation Failed"));
                return false;
            }
        }

    }
}
