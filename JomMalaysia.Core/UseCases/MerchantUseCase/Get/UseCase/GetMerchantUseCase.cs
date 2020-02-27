
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.UseCase
{
    public class GetMerchantUseCase : IGetMerchantUseCase
    
    {
        private readonly IMerchantRepository _merchantRepository;

        private readonly IMapper _mapper;
        public GetMerchantUseCase(IMerchantRepository merchantRepository, IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetMerchantRequest message, IOutputPort<GetMerchantResponse> outputPort)
        {
            GetMerchantResponse response;
            try
            {
                response = await _merchantRepository.FindByIdAsync(message.Id).ConfigureAwait(false);
                var mapped = _mapper.Map<MerchantViewModel>(response.Merchant);
                outputPort.Handle(new GetMerchantResponse(mapped,response.Success,response.Message));
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
