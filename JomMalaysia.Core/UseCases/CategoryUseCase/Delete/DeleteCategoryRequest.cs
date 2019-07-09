using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryRequest : IUseCaseRequest<DeleteCategoryResponse>
    {
        public string CategoryId { get; set; }
       

        public DeleteCategoryRequest(string CategoryId)
        {
            if (string.IsNullOrWhiteSpace(CategoryId))
            {
                throw new System.ArgumentException("Delete Category: Category Id null", nameof(CategoryId));
            }
           

            this.CategoryId = CategoryId;
        }
    }
}