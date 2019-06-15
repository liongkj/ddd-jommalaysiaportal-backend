using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces
{
    public interface IUpdateMerchantUseCase : IUseCaseHandler<UpdateMerchantRequest, UpdateMerchantResponse>
    {

    }
}


//public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>
//{
//}