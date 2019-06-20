using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Interfaces.UseCases.Categories;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.CategoryUseCase
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

            if (message.Subcategories.Count < 1)
            {
                foreach (var sub in message.Subcategories)
                {
                    NewCategory.CreateSubCategory(sub.SubcategoryName, sub.SubcategoryNameMs, sub.SubcategoryNameZh);
                }
            }

            var response = _CategoryRepository.CreateCategory(NewCategory);
            outputPort.Handle(response.Success ? new CreateCategoryResponse(response.Id, true) : new CreateCategoryResponse(response.Errors));
            return response.Success;
        }
    }
}
