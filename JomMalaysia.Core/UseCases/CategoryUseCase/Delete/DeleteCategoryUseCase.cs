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
            if (category == null)//if cant find the category
            {
                outputPort.Handle(new DeleteCategoryResponse(message.Name, false, "Category Not Found"));
                return false;
            }
            else
            {
                //TODO
                //validate if has subcategories

                //if (message.Subcategories.Count < 1) //if no subcategories
                //{
                    var response = _Category.Delete(category.CategoryId);
                //    outputPort.Handle(new DeleteCategoryResponse(message.CategoryId, true, category.CategoryId + " deleted"));
                //    return response.Success;
                //}
                //else
                //{
                    outputPort.Handle(new DeleteCategoryResponse(response.Id, true, category.CategoryName + " Deleted"));
                    return true;
                //}
            }
        }
    }
}