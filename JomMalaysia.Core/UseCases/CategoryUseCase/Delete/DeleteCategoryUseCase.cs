using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoryRepository _Category;
        public DeleteCategoryUseCase(ICategoryRepository Category)
        {
            _Category = Category;
        }

        public bool Handle(DeleteCategoryRequest message, IOutputPort<DeleteCategoryResponse> outputPort)
        {
            Category category = (_Category.FindByName(message.Name)).Category;

            if (category == null)//null check
            {
                outputPort.Handle(new DeleteCategoryResponse(message.Name, false, "Category Not Found"));
                return false;
            }
            else
            {

                //fetch subcategories
                var Subcategories = _Category.GetAllCategories(message.Name).Categories;
                if (category.HasSubcategories(Subcategories))
                {
                    outputPort.Handle(new DeleteCategoryResponse(category.CategoryName, false, category.CategoryName + " still has subcategories associated."));
                    return false;
                }
                else //no subcategories
                {
                    var response = _Category.Delete(category.CategoryId);
                    outputPort.Handle(new DeleteCategoryResponse(response.Id, response.Success, category.CategoryName + " Deleted"));
                    return response.Success;


                }

            }
        }
    }
}