
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Core.Services.Merchants.UseCaseRequests
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