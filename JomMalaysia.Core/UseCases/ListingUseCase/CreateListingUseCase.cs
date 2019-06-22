using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases.Listings;
using JomMalaysia.Core.Services.Listings.UseCaseRequests;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class CreateListingUseCase : ICreateListingUseCase
    {
        private readonly IListingRepository _listingRepository;

        public CreateListingUseCase(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        public bool Handle(CreateListingRequest message, IOutputPort<CreateListingResponse> outputPort)
        {


            //validate listing
            //create listing
            Listing listing = new Listing(message.MerchantId, message.ListingName, message.Description, message.Category, message.ListingLocation);
            //add to category collection

            var response = _listingRepository.CreateListing(listing);
            outputPort.Handle(response.Success ? new CreateListingResponse(response.Id, true) : new CreateListingResponse(response.Errors));
            return response.Success;
        }
    }
}
