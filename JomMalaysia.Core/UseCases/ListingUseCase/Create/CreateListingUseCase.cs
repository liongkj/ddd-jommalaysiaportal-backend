using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingUseCase : ICreateListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateListingUseCase(IListingRepository listingRepository,ICategoryRepository categoryRepository)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(CreateListingRequest message, IOutputPort<CreateListingResponse> outputPort)
        {

            //TODO
            var subcategory = _categoryRepository.GetAllSubcategory(message.Category.CategoryId);

            //validate listing
            var category = message.Category;
            //create listing
            Listing NewListing = new Listing(message.MerchantId, message.ListingName, message.Description, message.Category, message.ListingLocation);
            
            //add to category collection

            var response = _listingRepository.CreateListing(NewListing).Result;
            outputPort.Handle(response.Success ? new CreateListingResponse(response.Id, true) : new CreateListingResponse(response.Errors));
            return response.Success;
        }

        
    }
}
