
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<bool> Handle(GetMerchantRequest message, IOutputPort<GetMerchantResponse> outputPort)
        {
            try
            {
                var response = await _merchantRepository.FindByIdAsync(message.Id).ConfigureAwait(false);
                outputPort.Handle(response);
                return response.Success;
            }
            catch (Exception e)
            {
                outputPort.Handle(new GetMerchantResponse(new List<string> { e.ToString() }));
                return false;
            }
            //if found
        }
    }
}
