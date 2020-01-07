using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Update
{
    public class UpdateMerchantUseCase : IUpdateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMongoDbContext _transaction;

        public UpdateMerchantUseCase(IMongoDbContext transaction, IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository; _transaction = transaction;
        }
        public async Task<bool> Handle(UpdateMerchantRequest message, IOutputPort<UpdateMerchantResponse> outputPort)
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
                using (var session = await _transaction.StartSession())
                {
                    //verify update??
                    var response = await _merchantRepository.UpdateMerchantAsyncWithSession(message.MerchantId, merchant, session);

                    outputPort.Handle(response);
                    return response.Success;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
