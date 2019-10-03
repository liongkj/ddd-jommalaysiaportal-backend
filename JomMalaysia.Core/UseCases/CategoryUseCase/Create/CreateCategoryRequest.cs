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
        public string CategoryCode { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public string ParentCategory { get; set; }

        public CreateCategoryRequest(string categoryCode, string categoryName, string categoryNameMs, string categoryNameZh, string ParentCategory)
        {
            CategoryCode = categoryCode.Trim().ToUpper();
            CategoryName = categoryName.Trim().ToLower();
            CategoryNameMs = categoryNameMs.Trim().ToLower();
            CategoryNameZh = categoryNameZh.Trim().ToLower();
            this.ParentCategory = ParentCategory;
        }
    }
}
