using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
{
    public class CreateCategoryRequest : IUseCaseRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; }
        
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public CategoryPath CategoryPath { get; set; }

        public CreateCategoryRequest(string categoryName, string categoryNameMs, string categoryNameZh, CategoryPath CategoryPath)
        {
            

            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
            this.CategoryPath = CategoryPath;
        }
    }
}
