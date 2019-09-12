using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
{
    public class CreateCategoryRequest : IUseCaseRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; }

        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public string ParentCategory { get; set; }

        public CreateCategoryRequest(string categoryName, string categoryNameMs, string categoryNameZh, string ParentCategory)
        {
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
            this.ParentCategory = ParentCategory;
        }
    }
}
