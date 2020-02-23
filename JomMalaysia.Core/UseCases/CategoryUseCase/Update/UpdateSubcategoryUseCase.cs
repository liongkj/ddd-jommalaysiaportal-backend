
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.CategoryUseCase.Update;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateSubcategoryUseCase : IUpdateSubcategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IListingRepository _ListingRepository;
        private readonly IMongoDbContext _transaction;

        public UpdateSubcategoryUseCase(ICategoryRepository CategoryRepository, IMongoDbContext transaction, IListingRepository listingRepository)
        {
            _CategoryRepository = CategoryRepository;
            _ListingRepository = listingRepository;
            _transaction = transaction;
        }
        public async Task<bool> Handle(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {

            Category updated = new Category(message.CategoryCode, message.CategoryName, message.CategoryNameMs, message.CategoryNameZh, new Image(message.CategoryImageUrl));
            //check if any listing has this category -currently no need
            var getCategoryResponse = await _CategoryRepository.FindByIdAsync(message.CategoryId);


            var ToBeUpdateSubcategory = getCategoryResponse.Data;

            //fetch listing with this subcategory
            var GetListingWithThisSubcategory = await _ListingRepository.GetAllListings(ToBeUpdateSubcategory.CategoryPath, true);
            var UpdatedListings = new Dictionary<string, CategoryPath>();
            List<Listing> ToBeUpdateListings = null;
            if (GetListingWithThisSubcategory.Success)
            {
                ToBeUpdateListings = GetListingWithThisSubcategory.Listings;

                //TODO
                if (ToBeUpdateListings.Count > 0)
                    UpdatedListings = ToBeUpdateSubcategory.UpdateListings(ToBeUpdateListings, false);
                //update operation end
            }

            //retrieve data end

            //Update Operation Start
            ToBeUpdateSubcategory.UpdateCategory(updated, null, false);



            UpdateCategoryResponse updateCategoryResponse;
            CoreListingResponse updateListingResponse;
            using (var session = await _transaction.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    //start update operation
                    if (ToBeUpdateListings != null && ToBeUpdateListings.Count > 0)
                    {

                        updateListingResponse = await _ListingRepository.UpdateCategoryAsyncWithSession(UpdatedListings, session);
                    }

                    updateCategoryResponse = await _CategoryRepository.UpdateCategoryWithSession(ToBeUpdateSubcategory.CategoryId, ToBeUpdateSubcategory, session);
                }

                catch
                {
                    await session.AbortTransactionAsync();

                    outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Update category operation failed" }));
                    return false;
                }
                await session.CommitTransactionAsync();
                outputPort.Handle(updateCategoryResponse);
                return true;
            }

            // throw new NotImplementedException();
        }
    }
}