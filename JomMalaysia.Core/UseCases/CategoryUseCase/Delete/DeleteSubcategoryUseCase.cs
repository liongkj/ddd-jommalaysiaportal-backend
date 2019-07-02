using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        private readonly ICategoryRepository _Category;
        public DeleteSubcategoryUseCase(ICategoryRepository Category)
        {
            _Category = Category;
        }

        public bool HandleAsync(DeleteSubcategoryRequest message, IOutputPort<DeleteSubcategoryResponse> outputPort)
        {
            Category category = (_Category.FindById(message.CategoryId)).Category;
            if (category == null)
            {
                outputPort.Handle(new DeleteSubcategoryResponse(message.CategoryId, false, "Category Not Found"));
                return false;
            }
            else
            {
                if (message.ListingId.Count < 1) //if no subcategories
                {
                    var response = _Category.Delete(message.CategoryId);
                    outputPort.Handle(new DeleteSubcategoryResponse(message.CategoryId, true, category.CategoryId + " deleted"));
                    return response.Success;
                }
                else
                {
                    outputPort.Handle(new DeleteSubcategoryResponse(message.CategoryId, false, category.CategoryName + " still has subcategories"));
                    return false;
                }
            }
        }
    }
}