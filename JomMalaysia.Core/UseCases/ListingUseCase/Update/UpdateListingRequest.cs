
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Domain.Entities.Listings;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
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
        public ListingImages Images { get; set; }


    }
}