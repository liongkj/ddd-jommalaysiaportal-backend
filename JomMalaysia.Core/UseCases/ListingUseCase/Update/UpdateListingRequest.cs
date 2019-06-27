
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;

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


    }
}