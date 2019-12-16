using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Get
{
    public class CategoryViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public string CategoryType { get; set; }
        public CategoryPath CategoryPath { get; set; }
        public Image CategoryThumbnail { get; set; }
    }
}