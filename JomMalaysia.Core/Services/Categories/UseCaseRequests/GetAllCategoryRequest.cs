using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
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
