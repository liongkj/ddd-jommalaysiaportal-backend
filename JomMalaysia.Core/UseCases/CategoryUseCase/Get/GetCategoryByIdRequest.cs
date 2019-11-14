
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByIdRequest : IUseCaseRequest<GetCategoryResponse>
    {

        public string CategoryId { get; set; }

    }
}
