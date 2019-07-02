
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.UseCase
{
    public class GetMerchantUseCase : IGetMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetMerchantUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public bool HandleAsync(GetMerchantRequest message, IOutputPort<GetMerchantResponse> outputPort)
        {
            if (message is null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }

            var response = _merchantRepository.FindById(message.Id);
            //if found
            if (!response.Success)
            {
                outputPort.Handle(new GetMerchantResponse(response.Errors));
            }
            //if merchant is null
            if (response.Merchant != null)
            {
                outputPort.Handle(new GetMerchantResponse(response.Merchant, true));
                return response.Success;
            }
            else
            //handle not found
            {
                outputPort.Handle(new GetMerchantResponse(response.Errors, false, "Merchant Deleted or Not Found"));
                return false;
            }
        }
    }
}
