using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryRequest : IUseCaseRequest<DeleteCategoryResponse>
    {
        public string Name { get; set; }
       

        public DeleteCategoryRequest(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new System.ArgumentException("Delete Category: Category Id null", nameof(Name));
            }
           

            this.Name = Name;
        }
    }
}