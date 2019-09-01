using System;
using System.Threading.Tasks;
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
        public async Task<bool> Handle(CreateMerchantRequest message, IOutputPort<CreateMerchantResponse> outputPort)
        {

            //create new merchant
            Merchant merchant = new Merchant(message.CompanyName, message.CompanyRegistrationNumber, message.Address);

            //add contacts to merchant
            if (message.Contacts != null)
            {
                foreach (var c in message.Contacts)
                {
                    merchant.AddContact(c);
                }
            }
            var response = await _merchantRepository.CreateMerchant(merchant);
            outputPort.Handle(response.Success ? new CreateMerchantResponse(response.Id, true) : new CreateMerchantResponse(response.Errors));
            return response.Success;
        }
    }
}
