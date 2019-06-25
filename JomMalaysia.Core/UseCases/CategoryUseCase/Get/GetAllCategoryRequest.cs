using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllCategoryRequest:IUseCaseRequest<GetAllCategoryResponse>
    {
       public ICollection<Category> Categories { get; set; }
        public ICollection<Subcategory> Subcategories { get; private set; }
        public string Id { get; set; }
        public GetAllCategoryRequest()
        {
            Categories = new Collection<Category>();
            Subcategories = new Collection<Subcategory>();
        }
    }
}
