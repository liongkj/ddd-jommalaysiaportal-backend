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
        private readonly IMongoDbContext _db;

        public CreateListingUseCase(IListingRepository listingRepository, ICategoryRepository categoryRepository,
        IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(CreateListingRequest message, IOutputPort<CreateListingResponse> outputPort)
        {
            List<string> Errors = new List<string>();
            //create listing

            // Listing NewListing = new EventListing(message.ListingName, message.Description, message.Category, message.Subcategory, message.ListingLocation, message.eventDate);

            var NewListing = ListingFactory.CreateListing(ListingTypeEnum.For(message.ListingType), message);
            
            try
            {
                if (NewListing is EventListing)
                {
                    NewListing = (EventListing)NewListing;
                }

            }
            catch (Exception e)
            {
                Errors.Add(e.ToString());
            }


            //find merchant and add to merchant
            var merchant = _merchantRepository.FindById(message.MerchantId).Merchant;
            //var verified = merchant.AddNewListing(NewListing);
            //find subcategory and add listing
            //var subcategories = _categoryRepository.GetAllSubcategory(NewListing.Category.CategoryId);
            // var subcategory = getSubcategory(subcategories.Subcategories, message.Subcategory.SubcategoryId, NewListing.ListingId);
            //if (subcategory == null)
            //{

            //}
            //else
            //{
            //start transaction
            await _db.StartSession();
            //add to listing collection
            //await _listingRepository.CreateListing(NewListing);
            //update category collection
            //_categoryRepository.UpdateSubcategoryListing(subcategory, NewListing, true);
            //update merchant collection
            //commit
            _db.Session.CommitTransaction();
            // }

            //validate listing





            var response = _listingRepository.CreateListing(NewListing);
            outputPort.Handle(response.Success ? new CreateListingResponse(response.Id, true) : new CreateListingResponse(response.Errors));
            return response.Success;
        }

    }
}
