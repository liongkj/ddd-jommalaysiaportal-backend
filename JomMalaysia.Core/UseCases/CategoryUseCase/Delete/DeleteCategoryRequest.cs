using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryRequest : IUseCaseRequest<DeleteCategoryResponse>
    {
        public string CategoryId { get; set; }
        public ICollection<Subcategory> Subcategories { get; private set; }

        public DeleteCategoryRequest(string CategoryId)
        {
            if (string.IsNullOrWhiteSpace(CategoryId))
            {
                throw new System.ArgumentException("Delete Category: Category Id null", nameof(CategoryId));
            }
            Subcategories = new Collection<Subcategory>();

            this.CategoryId = CategoryId;
        }
    }
}