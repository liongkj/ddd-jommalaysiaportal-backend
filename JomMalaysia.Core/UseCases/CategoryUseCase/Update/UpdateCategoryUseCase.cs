
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IListingRepository _ListingRepository;
        private readonly IUpdateSubcategoryUseCase _updateSubcategoryUseCase;
        private readonly IMongoDbContext _transaction;

        public UpdateCategoryUseCase(ICategoryRepository CategoryRepository, IMongoDbContext transaction, IListingRepository listingRepository, IUpdateSubcategoryUseCase updateSubcategoryUseCase)
        {
            _CategoryRepository = CategoryRepository;
            _ListingRepository = listingRepository;
            _transaction = transaction;
            _updateSubcategoryUseCase = updateSubcategoryUseCase;
        }
        public async Task<bool> Handle(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {

            //check if any listing has this category
            var CategoryQuery = await _CategoryRepository.FindByIdAsync(message.CategoryId);
            var Category = CategoryQuery.Category;
            if (!CategoryQuery.Success)
            {
                outputPort.Handle(new UpdateCategoryResponse(CategoryQuery.Errors, false, CategoryQuery.Message));
                return false;
            }
            if (Category.IsCategory())
            {
                // var category = (await _CategoryRepository.FindByNameAsync(message.CategoryName)).Category;
                var GetAllCategoriesResponse = await _CategoryRepository.GetAllCategoriesAsync(Category.CategoryName);
                if (!GetAllCategoriesResponse.Success)
                {
                    outputPort.Handle(new UpdateCategoryResponse(GetAllCategoriesResponse.Errors, false, GetAllCategoriesResponse.Message));
                    return false;
                }
                var ToBeUpdateCategories = GetAllCategoriesResponse.Data.Where(x => !x.IsCategory()).ToList();
                var OldCategory = GetAllCategoriesResponse.Data.Where(x => x.IsCategory()).FirstOrDefault();

                var GetListingWithCategories = await _ListingRepository.GetAllListings(OldCategory.CategoryPath);
                if (!GetListingWithCategories.Success) //handle get listing
                {
                    outputPort.Handle(new UpdateCategoryResponse(GetListingWithCategories.Errors, false, GetListingWithCategories.Message));
                    return false;
                }
                List<Listing> ToBeUpdateListings = GetListingWithCategories.Listings;
                //TODO
                var UpdatedListings = new Dictionary<string, string>();
                //  = message.Updated.UpdateListings(ToBeUpdateListings);

                //start update operation
                List<Category> UpdatedCategories = OldCategory.UpdateCategory(message.Updated, ToBeUpdateCategories);

                var response = await TransactionHasNoError(UpdatedListings, UpdatedCategories);

                outputPort.Handle(response);
                return response.Success;
                // throw new NotImplementedException();
            }


            else
            {
                return await _updateSubcategoryUseCase.Handle(message, outputPort);
            }
        }

        private async Task<UpdateCategoryResponse> TransactionHasNoError(Dictionary<string, string> UpdatedListing, List<Category> updatedSubcategories)
        {
            using (var session = await _transaction.StartSession())
            {
                UpdateCategoryResponse response = new UpdateCategoryResponse(new List<string> { "Error updating Category" });
                try
                {
                    session.StartTransaction();

                    if (UpdatedListing.Count != 0)
                    {
                        var updateListngResponse = await _ListingRepository.UpdateCategoryAsyncWithSession(UpdatedListing, session);
                    }
                    response = await _CategoryRepository.UpdateManyWithSession(updatedSubcategories, session);
                }

                catch (Exception e)
                {
                    await session.AbortTransactionAsync();

                    return new UpdateCategoryResponse(new List<string> { e.Source }, false, e.Message);
                }

                session.CommitTransaction();
                return response;

            }

        }
    }
}
