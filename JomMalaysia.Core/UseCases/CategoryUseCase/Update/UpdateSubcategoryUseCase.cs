
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

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

            //retrieve data start
            //check if any listing has this category -currently no need
            var getCategoryResponse = await _CategoryRepository.FindByIdAsync(message.CategoryId);


            var ToBeUpdateSubcategory = getCategoryResponse.Data;

            //fetch listing with this subcategory
            var GetListingWithThisSubcategory = await _ListingRepository.GetAllListings(ToBeUpdateSubcategory.CategoryPath);
            var ToBeUpdateListings = GetListingWithThisSubcategory.Listings;
            //retrieve data end

            //Update Operation Start
            ToBeUpdateSubcategory.UpdateCategory(message.Updated, null, false);
            //TODO
            var UpdatedListings = ToBeUpdateSubcategory.UpdateListings(ToBeUpdateListings, false);
            //update operation end


            UpdateCategoryResponse updateCategoryResponse;
            CoreListingResponse updateListingResponse;
            using (var session = await _transaction.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    //start update operation
                    if (ToBeUpdateListings.Count > 0)
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