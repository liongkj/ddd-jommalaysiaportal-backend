using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Delete
{
    public class DeleteMerchantRequest : IUseCaseRequest<DeleteMerchantResponse>
    {
        public string MerchantId { get; set; }
        public ICollection<Listing> Listings { get; private set; }
        public Merchant Merchant { get; private set; }

        public DeleteMerchantRequest(string MerchantId)
        {
            if (string.IsNullOrWhiteSpace(MerchantId)) 
            {
                throw new System.ArgumentException("Delete Merchant: Listing Id null", nameof(MerchantId));
            }
            Listings = new Collection<Listing>();
            
            this.MerchantId = MerchantId;
        }
    }
}