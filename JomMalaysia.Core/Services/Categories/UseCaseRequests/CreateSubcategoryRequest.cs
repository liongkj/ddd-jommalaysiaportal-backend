using System.Collections.Generic;
using System.Reflection.Metadata;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
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
            SubcategoryName = subCategoryNameMs;
            SubcategoryNameMs = subCategoryName;
            SubcategoryZh = subCategoryNameZh;
            CategoryId = categoryId;
        }
    }
}