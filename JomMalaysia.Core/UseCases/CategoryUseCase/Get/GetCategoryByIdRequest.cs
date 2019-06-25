
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByIdRequest : IUseCaseRequest<GetCategoryResponse>
    {

        public string Id { get; }
        public GetCategoryByIdRequest(string Id)
        {
            this.Id = Id;

        }
    }
}
