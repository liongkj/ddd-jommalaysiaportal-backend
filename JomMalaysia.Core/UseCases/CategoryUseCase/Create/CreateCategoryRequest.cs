using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Create
{
    public class CreateCategoryRequest : IUseCaseRequest<CreateCategoryResponse>
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public string ParentCategory { get; set; }
        public string CategoryImageUrl { get; set; }
        public string CategoryThumbnailUrl { get; set; }
        public CategoryType CategoryType { get; set; }


    }
}
