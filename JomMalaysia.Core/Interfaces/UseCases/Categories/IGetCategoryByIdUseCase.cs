using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces.UseCases.Categories
{
    public interface IGetCategoryByIdUseCase : IUseCaseHandler<GetCategoryByIdRequest, GetCategoryResponse>
    {

    }
}