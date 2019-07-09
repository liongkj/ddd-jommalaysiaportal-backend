using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;

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

    }
}
