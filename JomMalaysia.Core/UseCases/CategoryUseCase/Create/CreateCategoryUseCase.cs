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
            NewCategory.CreateCategoryPath(message.ParentCategory, message.CategoryName);
            //Get all check unique
            try
            {
                //find existing category name
                var queries = await _CategoryRepository.GetAllCategoriesAsync().ConfigureAwait(false);


                if (NewCategory.HasDuplicate(queries.Categories))
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
