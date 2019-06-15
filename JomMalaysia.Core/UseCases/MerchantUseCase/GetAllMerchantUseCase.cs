using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.MerchantUseCase
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
