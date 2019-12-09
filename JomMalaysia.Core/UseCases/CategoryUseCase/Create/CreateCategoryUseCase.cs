using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Create
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Handle(CreateCategoryRequest message, IOutputPort<CreateCategoryResponse> outputPort)
        {
            Image Thumbnail;
            Thumbnail = string.IsNullOrEmpty(message.CategoryImageUrl) ?
                new Image() : //default image
                new Image(message.CategoryImageUrl);
            Category newCategory = new Category(message.CategoryType, message.CategoryCode, message.CategoryName, message.CategoryNameMs, message.CategoryNameZh, Thumbnail);
            if (message.ParentCategory != null) //create subcategory
            {
                var parentCategoryQuery = await _categoryRepository.FindByIdAsync(message.ParentCategory);
                if (!parentCategoryQuery.Success)
                {
                    outputPort.Handle(new CreateCategoryResponse(new List<string> { "Category not found." }));
                    return false;
                }
                newCategory.CreateCategoryPath(parentCategoryQuery.Data.CategoryName, message.CategoryName);
                newCategory.CategoryType = parentCategoryQuery.Data.CategoryType;
            }
            else //create parent category
            {
                newCategory.CreateCategoryPath(null, message.CategoryName);
            }
            //Get all check unique
            try
            {
                //find existing category name
                var queries = await _categoryRepository.GetAllCategoriesAsync().ConfigureAwait(false);


                if (newCategory.HasDuplicate(queries.Data))
                {//has duplicates
                    outputPort.Handle(new CreateCategoryResponse(new List<string> { "Duplicated Category Name" }));
                    return false;
                }
                else
                {//no duplicate, proceed to add to database
                    var response = await _categoryRepository.CreateCategoryAsync(newCategory);
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
