using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
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
        IMongoDbContext transaction)
        {
            _merchantRepository = merchantRepository;
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _transaction = transaction;
            
        }
        public bool Handle(CreateListingRequest message, IOutputPort<CreateListingResponse> outputPort)
        {
            var Errors = new List<string>();
            //create listing

            // Listing NewListing = new EventListing(message.ListingName, message.Description, message.Category, message.Subcategory, message.ListingLocation, message.eventDate);
            var Merchant = _merchantRepository.FindById(message.MerchantId).Merchant;


            if (Merchant.AddNewListingIsSuccess(new Listing())) 
            {

            }

            var response = _listingRepository.CreateListing(el);
            outputPort.Handle(response.Success ? new CreateListingResponse(response.Id, true) : new CreateListingResponse(response.Errors));
            return response.Success;
        }
        
    }
}
