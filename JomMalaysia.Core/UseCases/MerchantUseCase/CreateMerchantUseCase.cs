using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases.Merchants;
using JomMalaysia.Core.Services.Merchants.UseCaseRequests;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.MerchantUseCase
{
    public class CreateMerchantUseCase : ICreateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public CreateMerchantUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public bool Handle(CreateMerchantRequest message, IOutputPort<CreateMerchantResponse> outputPort)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Merchant merchant = new Merchant(message.CompanyName, message.CompanyRegistrationNumber, message.Address);
            if (message.Contacts != null)
            {
                foreach (var c in message.Contacts)
                {
                    merchant.AddContact(c);
                }
            }
            var response = _merchantRepository.CreateMerchant(merchant);
            outputPort.Handle(response.Success ? new CreateMerchantResponse(response.Id, true) : new CreateMerchantResponse(response.Errors));
            return response.Success;
        }
    }
}
