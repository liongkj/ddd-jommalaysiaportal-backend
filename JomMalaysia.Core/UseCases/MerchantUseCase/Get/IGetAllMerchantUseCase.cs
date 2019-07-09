﻿using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get
{
    public interface IGetAllMerchantUseCase : IUseCaseHandler<GetAllMerchantRequest, GetAllMerchantResponse>
    {

    }
}


//public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>
//{
//}