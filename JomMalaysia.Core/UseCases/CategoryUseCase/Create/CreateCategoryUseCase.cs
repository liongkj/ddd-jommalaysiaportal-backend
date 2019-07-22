using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CreateCategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public bool Handle(CreateCategoryRequest message, IOutputPort<CreateCategoryResponse> outputPort)
        {
            Category NewCategory = new Category(message.CategoryName, message.CategoryNameMs, message.CategoryNameZh);
            NewCategory.CreateCategoryPath(message.ParentCategory,message.CategoryName);
            //Get all check unique
            var categories = _CategoryRepository.GetAllCategories().Categories;
            if (NewCategory.HasDuplicate(categories))
            {
                //throw
                outputPort.Handle(new CreateCategoryResponse(message.CategoryName, false, "this category exists"));
                return false;
            }
            else
            {
                var response = (_CategoryRepository.CreateCategory(NewCategory)).Result;
                outputPort.Handle(response.Success ? new CreateCategoryResponse(response.Id, true) : new CreateCategoryResponse(response.Errors));
                return response.Success;
            }
        
            
        }
    }
}
