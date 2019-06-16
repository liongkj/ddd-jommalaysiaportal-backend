
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class GetMerchantRequest:IUseCaseRequest<GetMerchantResponse>
    {
        public string MerchantId { get; set; }

        public GetMerchantRequest(string merchantId)
        {
            MerchantId = merchantId;
        }
    }
}
