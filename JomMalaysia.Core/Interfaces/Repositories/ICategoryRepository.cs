﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<CreateCategoryResponse> CreateCategoryAsync(Category Category);
        //Task<CreateCategoryResponse> CreateCategory(Category Category,Category Subcategory);
        Task<GetAllCategoryResponse> GetAllCategoriesAsync(int PageSize = 20, int PageNumber = 1);
        GetAllCategoryResponse GetAllCategories(string categoryName);
        GetCategoryResponse GetCategory(string name);
        DeleteCategoryResponse Delete(string id);

        GetCategoryResponse FindByName(string name);
        GetCategoryResponse FindByName(string cat, string sub);
        GetCategoryResponse FindById(string id);

        UpdateCategoryResponse UpdateManyWithSession(List<Category> categories, IClientSessionHandle session);
        UpdateCategoryResponse UpdateCategoryWithSession(string id, Category Category, IClientSessionHandle session);

    }
}
