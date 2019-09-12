using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

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
        public async Task<bool> Handle(CreateListingRequest message, IOutputPort<CreateListingResponse> outputPort)
        {
            List<string> Errors = new List<string>();
            var response = new CreateListingResponse(Errors);


            //find merchant and add to merchant
            var merchant = await _merchantRepository.FindByIdAsync(message.MerchantId).ConfigureAwait(false);
            if (merchant.Merchant == null)
            {
                outputPort.Handle(new CreateListingResponse(message.MerchantId, false, $"{merchant.Errors}"));
                return false;
            }

            //verify is there this category
            var category = await _categoryRepository.FindByNameAsync(message.Category, message.Subcategory).ConfigureAwait(false);
            if (category.Category == null)
            {
                outputPort.Handle(new CreateListingResponse($"{message.Category} / {message.Subcategory} ", false, $"{category.Errors}"));
                return false;
            }

            //create listing factory pattern
            var NewListing = ListingFactory.CreateListing(ListingTypeEnum.For(message.ListingType), message, merchant.Merchant);
            if (NewListing is Listing)
            {
                //start transaction
                using (var session = await _transaction.StartSession())
                {

                    try
                    {
                        session.StartTransaction();
                        var listing = await _listingRepository.CreateListingAsync(NewListing, session).ConfigureAwait(false);
                        NewListing.ListingId = listing.Id;
                        merchant.Merchant.AddNewListing(NewListing);
                        await _merchantRepository.UpdateMerchant(merchant.Merchant.MerchantId, merchant.Merchant, session).ConfigureAwait(false);
                        if (Task.WhenAll().IsCompletedSuccessfully)
                        {
                            await session.CommitTransactionAsync();
                            response = new CreateListingResponse(listing.Id, true, $"{ GetType().Name } successful");
                        }

                    }
                    catch (Exception e)
                    {
                        response = new CreateListingResponse(
                            $"{GetType().Name} Transaction Error",
                            false,
                             e.ToString());
                        outputPort.Handle(response);
                        return false;
                    }

                    outputPort.Handle(response);
                    return true;
                }

            }
            else
            {
                outputPort.Handle(response);
                return false;
            }
        }

    }
}
