
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
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
