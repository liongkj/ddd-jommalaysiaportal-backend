using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByNameRequest : IUseCaseRequest<GetCategoryResponse>
    {
        public string Name { get; set; }

        public GetCategoryByNameRequest(string name)
        {
            Name = name;
        }
    }
}