using System;
using System.Collections.Generic;
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
            try
            {
                var response = await _merchantRepository.GetAllMerchantAsync().ConfigureAwait(false);
                if (!response.Success)
                {
                    outputPort.Handle(new GetAllMerchantResponse(response.Errors));
                    return response.Success;
                }
                outputPort.Handle(new GetAllMerchantResponse(response.Data, true));
                return response.Success;
            }
            catch (Exception e)
            {
                outputPort.Handle(new GetAllMerchantResponse(new List<string> { e.ToString() }));

                return false;
            }

        }
    }
}
