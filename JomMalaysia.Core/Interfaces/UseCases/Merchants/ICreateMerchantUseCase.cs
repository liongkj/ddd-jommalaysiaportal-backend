using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.Merchants.UseCaseRequests;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces.UseCases.Merchants
{
    public interface ICreateMerchantUseCase : IUseCaseHandler<CreateMerchantRequest, CreateMerchantResponse>
    {

    }
}


//public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>
//{
//}