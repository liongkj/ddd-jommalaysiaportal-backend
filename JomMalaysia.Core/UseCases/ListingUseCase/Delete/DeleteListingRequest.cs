using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Delete
{
    public class DeleteListingRequest : IUseCaseRequest<DeleteListingResponse>
    {
        public string ListingId { get; set; }
        public DeleteListingRequest(string ListingId)
        {
            if (string.IsNullOrEmpty(ListingId))
            {
                throw new System.ArgumentException("message", nameof(ListingId));
            }

            this.ListingId = ListingId;
        }
    }
}