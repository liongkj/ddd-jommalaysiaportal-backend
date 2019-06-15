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
    public class UpdateMerchantUseCase : IUpdateMerchantUseCase
    {
        private readonly IMerchantRepository _merchantRepository;

        public UpdateMerchantUseCase(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        public bool Handle(UpdateMerchantRequest message, IOutputPort<UpdateMerchantResponse> outputPort)
        {
            var response = _merchantRepository.Update(message.MerchantId, new Merchant(message.CompanyName, message.CompanyRegistrationNumber, message.ContactName, message.Address, message.Phone, message.Fax, message.ContactEmail));

            outputPort.Handle(response.Success ? new UpdateMerchantResponse(response.Id, true) : new UpdateMerchantResponse(response.Errors));
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
