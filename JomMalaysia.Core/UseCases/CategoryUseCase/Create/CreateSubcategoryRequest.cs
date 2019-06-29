using System.Collections.Generic;

using JomMalaysia.Core.Interfaces;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
{
    public class CreateSubcategoryRequest : IUseCaseRequest<CreateSubcategoryResponse>
    {
        public string CategoryId { get; set; }

        public string SubcategoryName { get; set; }
        public string SubcategoryNameMs { get; set; }
        public string SubcategoryZh { get; set; }
        public ICollection<string> ListingId { get; private set; }


        public CreateSubcategoryRequest(string categoryId, string subCategoryName, string subCategoryNameMs, string subCategoryNameZh)
        {
            SubcategoryName = subCategoryName;
            SubcategoryNameMs = subCategoryNameMs;
            SubcategoryZh = subCategoryNameZh;
            CategoryId = categoryId;
        }
    }
}