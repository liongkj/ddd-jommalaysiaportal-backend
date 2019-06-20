using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
{
    public class CreateCategoryRequest : IUseCaseRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; }
        public ICollection<Subcategory> Subcategories { get; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }

        public CreateCategoryRequest(string categoryName, string categoryNameMs, string categoryNameZh)
        {
            Subcategories = new Collection<Subcategory>();

            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
        }
    }
}
