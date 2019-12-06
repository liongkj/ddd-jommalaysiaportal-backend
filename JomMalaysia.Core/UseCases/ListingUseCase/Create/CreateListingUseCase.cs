using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
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
            var findMerchantResponse = await _merchantRepository.FindByIdAsync(message.MerchantId).ConfigureAwait(false);
            if (!findMerchantResponse.Success) //merchant not found
            {
                outputPort.Handle(new CoreListingResponse(findMerchantResponse.Errors, false, findMerchantResponse.Message));
                return false;
            }

            //verify is there this category
            Category category = null;
            if (!string.IsNullOrWhiteSpace(message.CategoryId))
            {
                var findCategoryResponse = await _categoryRepository.FindByIdAsync(message.CategoryId).ConfigureAwait(false);
                if (!findCategoryResponse.Success)
                {
                    outputPort.Handle(new CoreListingResponse(findCategoryResponse.Errors, false, findCategoryResponse.Message));
                    return false;
                }
                category = findCategoryResponse.Data;
                if (category.IsCategory())
                {
                    outputPort.Handle(new CoreListingResponse(new List<string> { "Bad Request" }, false, "Please select a valid subcategory"));
                    return false;
                }
            }
            
            //create listing factory pattern
            var newListing = ListingFactory.CreateListing(message.CategoryType, message, category, findMerchantResponse.Data);
            if (newListing != null) //validate is Listing Type
            {
                //start transaction
                using (var session = await _transaction.StartSession())
                {
                    try
                    {
                        var merchantUser = findMerchantResponse.Data;
                        session.StartTransaction();

                        //create Listing command
                        var listing = await _listingRepository.CreateListingAsync(newListing, session).ConfigureAwait(false);
                        newListing.ListingId = listing.Id;//retrieve the created listing Id and add into merchant
                        merchantUser.AddNewListing(newListing);
                        //update merchant command
                        var updateMerchantCommand = await _merchantRepository.UpdateMerchantAsyncWithSession(merchantUser.MerchantId, merchantUser, session).ConfigureAwait(false);
                        if (updateMerchantCommand.Success)
                        {
                            await session.CommitTransactionAsync();
                            outputPort.Handle(new CoreListingResponse(listing.Id + " Listing Created Successfully", true));
                            return true;
                        }
                        outputPort.Handle(new CoreListingResponse(updateMerchantCommand.Errors, updateMerchantCommand.Success, updateMerchantCommand.Message));
                        return false;
                        
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
