using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteSubcategoryRequest : IUseCaseRequest<DeleteCategoryResponse>
    {
        public string Category { get; set; }
        public string Subcategory { get; set; }


        public DeleteSubcategoryRequest(string category,string subcategory)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new System.ArgumentException("Delete Category: Category name cannot be null", nameof(category));
            }

            if (subcategory == null)
            {
                throw new System.ArgumentNullException(nameof(subcategory));
            }

            Category = category;
            Subcategory = subcategory;
        }
    }
}