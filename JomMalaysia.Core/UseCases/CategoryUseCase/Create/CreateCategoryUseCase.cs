using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<bool> Handle(CreateCategoryRequest message, IOutputPort<CreateCategoryResponse> outputPort)
        {
            Category NewCategory = new Category(message.CategoryCode, message.CategoryName, message.CategoryNameMs, message.CategoryNameZh);
            if (message.ParentCategory != null) //create subcategory
            {
                var ParentCategoryQuery = await _CategoryRepository.FindByIdAsync(message.ParentCategory);
                if (!ParentCategoryQuery.Success)
                {
                    outputPort.Handle(new CreateCategoryResponse(new List<string> { "Category not found." }));
                    return false;
                }
                NewCategory.CreateCategoryPath(ParentCategoryQuery.Category.CategoryName, message.CategoryName);
            }
            else
            {
                NewCategory.CreateCategoryPath(null, message.CategoryName);
            }
            //Get all check unique
            try
            {
                //find existing category name
                var queries = await _CategoryRepository.GetAllCategoriesAsync().ConfigureAwait(false);


                if (NewCategory.HasDuplicate(queries.Data))
                {//has duplicates
                    outputPort.Handle(new CreateCategoryResponse(new List<string> { "Duplicated Category Name" }));
                    return false;
                }
                else
                {//no duplicate, proceed to add to database
                    var response = await _CategoryRepository.CreateCategoryAsync(NewCategory);
                    outputPort.Handle(response);
                    return response.Success;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
