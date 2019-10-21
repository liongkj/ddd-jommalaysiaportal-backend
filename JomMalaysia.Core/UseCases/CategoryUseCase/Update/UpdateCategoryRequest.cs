
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryRequest : IUseCaseRequest<UpdateCategoryResponse>
    {
        public string CategoryId { get; set; }

        public Category Updated { get; set; }

        public UpdateCategoryRequest(string categoryId, string categoryCode, string categoryName, string categoryNameMs, string categoryNameZh, Image image)
        {
            CategoryId = categoryId;
            var CategoryName = categoryName.Trim().ToLower();
            var CategoryCode = handleCode(categoryCode, CategoryName);
            var CategoryNameMs = categoryNameMs.Trim().ToLower();
            var CategoryNameZh = categoryNameZh.Trim().ToLower();
            Updated = new Category(CategoryCode, CategoryName, CategoryNameMs, CategoryNameZh, image);

        }

        private string handleCode(string categoryCode, string categoryName)
        {
            if (categoryCode == null)
            {
                var index = categoryName.Length >= 5 ? 5 : categoryName.Length;
                return categoryName.Substring(0, index).ToUpper();
            }
            else
            {
                return categoryCode.Trim().ToUpper();
            }
        }

    }
}