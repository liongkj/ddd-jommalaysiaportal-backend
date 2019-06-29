
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request
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
