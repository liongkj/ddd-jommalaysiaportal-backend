using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class DeleteMerchantRequest : IUseCaseRequest<DeleteMerchantResponse>
    {
        public string MerchantId { get; set; }
        public ICollection<Listing> Listings { get; set; }

        public DeleteMerchantRequest(string MerchantId, ICollection<Listing> Listings)
        {
            if (string.IsNullOrWhiteSpace(MerchantId))
            {
                throw new System.ArgumentException("Select a Merchant", nameof(MerchantId));
            }
            Listings = new Collection<Listing>();
            this.MerchantId = MerchantId;
        }
    }
}