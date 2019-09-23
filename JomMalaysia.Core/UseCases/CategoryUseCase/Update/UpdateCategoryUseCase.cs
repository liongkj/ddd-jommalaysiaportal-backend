
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IListingRepository _ListingRepository;
        private readonly IMongoDbContext _transaction;

        public UpdateCategoryUseCase(ICategoryRepository CategoryRepository, IMongoDbContext transaction, IListingRepository listingRepository)
        {
            _CategoryRepository = CategoryRepository;
            _ListingRepository = listingRepository;
            _transaction = transaction;
        }
        public async Task<bool> Handle(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {

            //check if any listing has this category

            // var category = (await _CategoryRepository.FindByNameAsync(message.CategoryName)).Category;
            var GetAllCategoriesResponse = await _CategoryRepository.GetAllCategoriesAsync(message.CategoryName);
            if (!GetAllCategoriesResponse.Success)
            {
                outputPort.Handle(new UpdateCategoryResponse(GetAllCategoriesResponse.Errors, false, GetAllCategoriesResponse.Message));
                return false;
            }
            var ToBeUpdateCategories = GetAllCategoriesResponse.Categories.Where(x => !x.IsCategory()).ToList();
            var OldCategory = GetAllCategoriesResponse.Categories.Where(x => x.IsCategory()).FirstOrDefault();

            var GetListingWithCategories = await _ListingRepository.GetAllListings(OldCategory.CategoryPath);
            if (!GetListingWithCategories.Success) //handle get listing
            {
                outputPort.Handle(new UpdateCategoryResponse(GetListingWithCategories.Errors, false, GetListingWithCategories.Message));
                return false;
            }
            List<Listing> ToBeUpdateListings = GetListingWithCategories.Listings;
            var UpdatedListings = message.Updated.UpdateListings(ToBeUpdateListings);

            //start update operation
            List<Category> UpdatedCategories = OldCategory.UpdateCategory(message.Updated, ToBeUpdateCategories);

            var response = await TransactionHasNoError(UpdatedListings, UpdatedCategories);

            outputPort.Handle(response);
            return response.Success;


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
