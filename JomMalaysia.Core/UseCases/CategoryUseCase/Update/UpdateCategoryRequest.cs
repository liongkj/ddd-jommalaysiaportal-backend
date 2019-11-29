using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Update
{
    public class UpdateCategoryRequest : IUseCaseRequest<UpdateCategoryResponse>
    {
        public string CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public string ParentCategory { get; set; }
        public string CategoryImageUrl { get; set; }
        public string CategoryThumbnailUrl { get; set; }

    }
}