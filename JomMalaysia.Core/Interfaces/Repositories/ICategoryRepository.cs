﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using MongoDB.Driver;
using static JomMalaysia.Core.UseCases.ListingUseCase.Get.ListingViewModel;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<CreateCategoryResponse> CreateCategoryAsync(Category Category);
        //Task<CreateCategoryResponse> CreateCategory(Category Category,Category Subcategory);
        Task<GetAllCategoryResponse> GetAllCategoriesAsync(int PageSize = 20, int PageNumber = 1);
        Task<GetAllCategoryResponse> GetAllCategoriesAsync(string CategoryName);
        Task<DeleteCategoryResponse> DeleteAsync(string id);


        Task<GetCategoryResponse> FindByIdAsync(string id);

        Task<UpdateCategoryResponse> UpdateManyWithSession(List<Category> categories, IClientSessionHandle session);
        Task<UpdateCategoryResponse> UpdateCategoryWithSession(string id, Category Category, IClientSessionHandle session);

        //helper method

        Task<CategoryPathViewModel> FindByNameAsync(string cat, string sub);

    }
}
