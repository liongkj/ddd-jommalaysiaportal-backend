
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.Services.Listings.UseCaseRequests
{
    public class UpdateListingRequest : IUseCaseRequest<UpdateListingResponse>
    {
        public UpdateListingRequest(string listingId, Listing Updated)
        {
            ListingId = listingId;
            this.Updated = Updated;
        }
        public string ListingId { get; }
        public Listing Updated { get; }


    }
}