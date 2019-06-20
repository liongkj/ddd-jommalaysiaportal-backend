using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

using JomMalaysia.Core.Interfaces.UseCases.Listings;
using JomMalaysia.Core.Services.Listings.UseCaseRequests;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class DeleteListingUseCase : IDeleteListingUseCase
    {
        private readonly IListingRepository _listing;
        public DeleteListingUseCase(IListingRepository listing)
        {
            _listing = listing;
        }

        public bool Handle(DeleteListingRequest message, IOutputPort<DeleteListingResponse> outputPort)
        {
            Listing merchant = (_listing.FindById(message.ListingId)).Listing;
            if (merchant == null)
            {
                outputPort.Handle(new DeleteListingResponse(message.ListingId,false,"Listing Not Found"));
                return false;
            }
            else
            {

                var response = _listing.Delete(message.ListingId);
                outputPort.Handle(new DeleteListingResponse(message.ListingId, true , merchant.ListingId+" deleted"));
                return response.Success;
            }
        }
    }
}