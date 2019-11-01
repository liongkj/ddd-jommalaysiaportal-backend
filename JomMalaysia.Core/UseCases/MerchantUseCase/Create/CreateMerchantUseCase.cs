using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Exceptions;
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
            var add = message.Address;
            var CompanyRegistration = new CompanyRegistration(message.SsmId, message.CompanyRegistrationName, message.OldSsmId);
            try
            {
                await _merchantRepository.FindBySsmIdAsync(message.SsmId).ConfigureAwait(false);

                //create new merchant
                Merchant merchant = new Merchant(CompanyRegistration, new Address(add.Add1, add.Add2, add.City, add.State, add.PostalCode, add.Country));

                //add contacts to merchant
                if (message.Contacts != null)
                {
                    foreach (var c in message.Contacts)
                    {
                        Contact con = new Contact(c.Name, c.Email, c.Phone);
                        merchant.AddContact(con);
                    }
                }
                var response = await _merchantRepository.CreateMerchantAsync(merchant);
                outputPort.Handle(response.Success ? new CreateMerchantResponse(response.Id, true, "Merchant created.") : new CreateMerchantResponse(response.Errors));
                return response.Success;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
