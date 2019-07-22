using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllSubcategoryRequest:IUseCaseRequest<GetAllCategoryResponse>
    {
      public string CategoryName { get; set; }

        public GetAllSubcategoryRequest(string CategoryName)
        {
            this.CategoryName = CategoryName;
        }
    }
}
