using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Delete
{
    public class DeleteListingRequest : IUseCaseRequest<DeleteListingResponse>
    {
        public string ListingId { get; set; }
        public ICollection<Listing> Listings { get; private set; }
        public Listing Listing { get; private set; }

        public DeleteListingRequest(string ListingId)
        {
            if (string.IsNullOrWhiteSpace(ListingId)) 
            {
                throw new System.ArgumentException("Delete Listing: Listing Id null", nameof(ListingId));
            }
            Listings = new Collection<Listing>();
            
            this.ListingId = ListingId;
        }
    }
}