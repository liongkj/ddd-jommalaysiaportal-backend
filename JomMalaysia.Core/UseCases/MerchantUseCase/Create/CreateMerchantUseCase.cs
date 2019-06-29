using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Create
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
