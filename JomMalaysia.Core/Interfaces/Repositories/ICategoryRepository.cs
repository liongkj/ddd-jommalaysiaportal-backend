using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;


namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        //category
        CreateCategoryResponse CreateCategory(Category Category);
        Task<GetAllCategoryResponse> GetAllCategories();
        DeleteCategoryResponse Delete(string id);
        GetCategoryResponse FindByName(string name);
        GetCategoryResponse FindById(string id);
        UpdateCategoryResponse Update(string id, Category Category);
        //subcategory
        CreateSubcategoryResponse CreateSubcategory(string categoryid, Subcategory subcategory);
    }
}
