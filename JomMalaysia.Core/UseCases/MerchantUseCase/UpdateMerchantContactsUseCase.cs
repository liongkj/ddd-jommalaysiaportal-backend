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
    public class UpdateMerchantContactsUseCase : IUpdateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public UpdateMerchantContactsUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public bool Handle(UpdateMerchantRequest message, IOutputPort<UpdateMerchantResponse> outputPort)
        {
            //var response = _merchantRepository.UpdateMerchant(new Merchant(message.CompanyName,message.CompanyRegistrationNumber,message.Address));

            //outputPort.Handle(response.Success ? new UpdateMerchantResponse(response.Id, true) : new UpdateMerchantResponse(response.Errors));
            // return response.Success;
            throw new NotImplementedException();
        }

        
    }
}
