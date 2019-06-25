using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.UseCase
{
    public class GetAllMerchantUseCase : IGetAllMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetAllMerchantUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public async Task<bool> Handle(GetAllMerchantRequest message, IOutputPort<GetAllMerchantResponse> outputPort)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var response = await _merchantRepository.GetAllMerchants();
            if (!response.Success)
            {
                outputPort.Handle(new GetAllMerchantResponse(response.Errors));
            }
            outputPort.Handle(new GetAllMerchantResponse(response.Merchants, true));

            return response.Success;
            //throw new NotImplementedException();
            //TODO 

        }
    }
}
