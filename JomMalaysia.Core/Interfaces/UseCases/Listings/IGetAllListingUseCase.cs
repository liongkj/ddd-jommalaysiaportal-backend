﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.Listings.UseCaseRequests;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces.UseCases.Listings
{
    public interface IGetAllListingUseCase : IUseCaseHandlerAsync<GetAllListingRequest, GetAllListingResponse>
    {

    }
}


//public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>
//{
//}