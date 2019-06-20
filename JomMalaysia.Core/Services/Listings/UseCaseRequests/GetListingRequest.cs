
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.Services.Listings.UseCaseRequests
{
    public class GetListingRequest:IUseCaseRequest<GetListingResponse>
    {
        public string Id { get; }

        public GetListingRequest(string ListingId)
        {
            Id = ListingId;
        }
    }
}
