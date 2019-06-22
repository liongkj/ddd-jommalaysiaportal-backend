
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Interfaces.UseCases.Categories;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.CategoryUseCase
{
    public class CreateSubcategoryUseCase : ICreateSubcategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CreateSubcategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public bool Handle(CreateSubcategoryRequest message, IOutputPort<CreateSubcategoryResponse> outputPort)
        {
            //find category

            Category parent = (_CategoryRepository.FindById(message.CategoryId)).Category;
            if (parent != null)
            {
                foreach (var s in parent.Subcategories)
                {
                    if ((s.SubcategoryName.ToString().ToLowerInvariant())
                    .Equals((message.SubcategoryName).ToLowerInvariant()))
                    {
                        outputPort.Handle(new CreateSubcategoryResponse(s.SubcategoryId, false, "Suncategory Exist"));
                        return false;
                    }
                }
                {
                    Subcategory NewSubcategory = parent.CreateSubCategory(message.SubcategoryName, message.SubcategoryNameMs, message.SubcategoryZh);

                    //call create sub category
                    var response = _CategoryRepository.CreateSubcategory(message.CategoryId, NewSubcategory);
                    outputPort.Handle(response.Success ? new CreateSubcategoryResponse(response.Id, true) : new CreateSubcategoryResponse(response.Errors));
                    return response.Success;
                }

            }
            else return false;
        }
    }
}