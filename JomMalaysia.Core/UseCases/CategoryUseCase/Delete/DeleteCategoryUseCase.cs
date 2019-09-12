using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepo;
        public DeleteCategoryUseCase(ICategoryRepository Category)
        {
            _CategoryRepo = Category;
        }

        public async Task<bool> Handle(DeleteCategoryRequest message, IOutputPort<DeleteCategoryResponse> outputPort)
        {
            try
            {
                var query = await _CategoryRepo.FindByNameAsync(message.Name);
                var category = query.Category;
                if (!query.Success)//found category
                {
                    outputPort.Handle(new DeleteCategoryResponse(message.Name, false, "Category Not Found"));
                    return false;
                }
                else
                {
                    //fetch subcategories
                    var Subcategories = await _CategoryRepo.GetAllCategoriesAsync(message.Name);
                    if (category.HasSubcategories(Subcategories.Categories))
                    {
                        outputPort.Handle(new DeleteCategoryResponse(category.CategoryName, false, category.CategoryName + " still has subcategories associated."));
                        return false;
                    }
                    else //no subcategories
                    {
                        var response = await _CategoryRepo.DeleteAsync(category.CategoryId);
                        outputPort.Handle(response);
                        return response.Success;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}