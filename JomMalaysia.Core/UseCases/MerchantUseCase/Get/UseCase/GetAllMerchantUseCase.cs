using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.UseCase
{
    public class GetAllMerchantUseCase : IGetAllMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;
        
        private readonly IMapper _mapper;

        public GetAllMerchantUseCase(IMerchantRepository merchantRepository, IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
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
                var mapped = _mapper.Map<List<MerchantViewModel>>(response.Merchants);
                outputPort.Handle(new GetAllMerchantResponse(mapped,response.Success,response.Message));
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
