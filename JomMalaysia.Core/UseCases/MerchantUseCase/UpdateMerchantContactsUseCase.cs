using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.MerchantUseCase
{
    public class UpdateMerchantContactsUseCase : ICreateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public UpdateMerchantContactsUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public bool Handle(CreateMerchantRequest message, IOutputPort<CreateMerchantResponse> outputPort)
        {
            var response = _merchantRepository.CreateMerchant(new Merchant(message.CompanyName,message.CompanyRegistrationNumber,message.Address));

            outputPort.Handle(response.Success ? new CreateMerchantResponse(response.Id, true) : new CreateMerchantResponse(response.Errors));
            return response.Success;
        }

        
    }
}
