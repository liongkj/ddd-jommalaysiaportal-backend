using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Get
{
    public class GetAllCategoryRequest : IUseCaseRequest<GetAllCategoryResponse>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
