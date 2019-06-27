using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
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
