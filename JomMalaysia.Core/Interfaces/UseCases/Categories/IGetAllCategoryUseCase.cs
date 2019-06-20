using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces.UseCases.Categories
{
    public interface IGetAllCategoryUseCase : IUseCaseHandlerAsync<GetAllCategoryRequest, GetAllCategoryResponse>
    {

    }
}


//public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>
//{
//}