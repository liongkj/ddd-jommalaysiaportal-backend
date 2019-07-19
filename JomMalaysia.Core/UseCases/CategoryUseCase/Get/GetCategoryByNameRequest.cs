using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByNameRequest : IUseCaseRequest<GetCategoryResponse>
    {
        public string Name { get; set; }
        public string ParentCategory { get; set; }

        public GetCategoryByNameRequest(string name)
        {
            Name = name;
        }

        public GetCategoryByNameRequest(string ParentCategory, string name)
        {
            Name = name;
            this.ParentCategory = ParentCategory;
        }
    }
}