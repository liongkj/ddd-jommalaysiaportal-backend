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

        public bool HandleAsync(DeleteCategoryRequest message, IOutputPort<DeleteCategoryResponse> outputPort)
        {
            Category category = (_Category.FindById(message.CategoryId)).Category;
            if (category == null)
            {
                outputPort.Handle(new DeleteCategoryResponse(message.CategoryId, false, "Category Not Found"));
                return false;
            }
            else
            {
                if (message.Subcategories.Count < 1) //if no subcategories
                {
                    var response = _Category.Delete(message.CategoryId);
                    outputPort.Handle(new DeleteCategoryResponse(message.CategoryId, true, category.CategoryId + " deleted"));
                    return response.Success;
                }
                else
                {
                    outputPort.Handle(new DeleteCategoryResponse(message.CategoryId, false, category.CategoryName + " still has subcategories"));
                    return false;
                }
            }
        }
    }
}