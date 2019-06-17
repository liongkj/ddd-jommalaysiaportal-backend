using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class UpdateMerchantRequest : IUseCaseRequest<UpdateMerchantResponse>
    {
        public UpdateMerchantRequest(string merchantId, Merchant Updated)
        {
            MerchantId = merchantId;
            this.Updated = Updated;
        }
        public string MerchantId { get; }
        public Merchant Updated { get; }


    }
}