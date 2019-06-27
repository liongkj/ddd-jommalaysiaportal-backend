﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Interfaces.UseCases.Listings;
using JomMalaysia.Core.Services.Listings.UseCaseRequests;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class CreateListingUseCase : ICreateListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMongoDbConfiguration _db;

        public CreateListingUseCase(IListingRepository listingRepository, ICategoryRepository categoryRepository,
        IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(CreateListingRequest message, IOutputPort<CreateListingResponse> outputPort)
        {
            //create listing
            ListingFactory lf = new ListingFactory();
            Listing NewListing = lf.GetListing(message.ListingType.ToString());
            //find merchant and add to merchant
            var merchant = _merchantRepository.FindById(message.MerchantId).Merchant;
            merchant.AddListing(NewListing);


            var subcategory = _categoryRepository.GetAllSubcategory(message.Category.CategoryId);
            //validate listing
            var category = message.Category;

            //start transaction
            await _db.StartSession();
            //add to listing collection

            //add to category collection

            var response = _listingRepository.CreateListing(NewListing).Result;
            outputPort.Handle(response.Success ? new CreateListingResponse(response.Id, true) : new CreateListingResponse(response.Errors));
            return response.Success;
        }


    }
}
