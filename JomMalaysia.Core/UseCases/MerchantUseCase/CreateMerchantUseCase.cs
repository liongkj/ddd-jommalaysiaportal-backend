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
    public class CreateMerchantUseCase : ICreateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public CreateMerchantUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public bool Handle(CreateMerchantRequest message, IOutputPort<CreateMerchantResponse> outputPort)
        {
            Merchant merchant;
            if (message.Contacts.Count() > 0)
            {
                merchant = new Merchant(message.CompanyName, message.CompanyRegistrationNumber, message.Address,message.Contacts);
            }
            else
            {
               merchant = new Merchant(message.CompanyName, message.CompanyRegistrationNumber, message.Address);
            }
            var response = _merchantRepository.CreateMerchant(merchant);
            outputPort.Handle(response.Success ? new CreateMerchantResponse(response.Id, true) : new CreateMerchantResponse(response.Errors));
            return response.Success;
        }

        //public async Task<bool> Handle(RegisterUserRequest message, IOutputPort<RegisterUserResponse> outputPort)
        //{
        //    var response = await _userRepository.Create(new User(message.FirstName, message.LastName, message.Email, message.UserName), message.Password);
        //    outputPort.Handle(response.Success ? new RegisterUserResponse(response.Id, true) : new RegisterUserResponse(response.Errors.Select(e => e.Description)));
        //    return response.Success;
        //}
    }
}
