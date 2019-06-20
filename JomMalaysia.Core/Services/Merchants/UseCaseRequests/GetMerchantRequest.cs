
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Core.Services.Merchants.UseCaseRequests
{
    public class GetMerchantRequest:IUseCaseRequest<GetMerchantResponse>
    {
        public string Id { get; }

        public GetMerchantRequest(string merchantId)
        {
            Id = merchantId;
        }
    }
}
