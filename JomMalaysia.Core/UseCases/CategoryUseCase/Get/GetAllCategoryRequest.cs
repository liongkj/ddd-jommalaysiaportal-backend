using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllCategoryRequest:IUseCaseRequest<GetAllCategoryResponse>
    {
       public ICollection<Category> Categories { get; set; }
        
        public string Id { get; set; }
        public GetAllCategoryRequest()
        {
            Categories = new Collection<Category>();
           
        }
    }
}
