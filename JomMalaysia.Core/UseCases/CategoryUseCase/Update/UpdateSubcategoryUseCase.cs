
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateSubcategoryUseCase :  IUpdateSubcategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMongoDbContext _transaction;

        public UpdateSubcategoryUseCase(ICategoryRepository CategoryRepository, IMongoDbContext transaction)
        {
            _CategoryRepository = CategoryRepository;
            _transaction = transaction;
        }
        public bool Handle(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {
            //TODO
            //check if any listing has this category
            var subcategory = _CategoryRepository.FindByName(message.ParentCategory,message.CategoryName).Category;
            //var listings = _Listing.FindListingsWithCategory()
            if (subcategory != null) //if category found
            {
                //find all subcategories with same name
                if (subcategory.UpdateCategoryIsSuccess(message.Updated,false))
                {
                    if (TransactionHasNoError(subcategory))
                    { 
                        _transaction.Session.CommitTransaction();
                        outputPort.Handle(new UpdateCategoryResponse(subcategory.CategoryId, true,"update transaction committed successfully"));
                        return true;
                    }
                    _transaction.Session.AbortTransaction();
                    outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Update category transaction failed" }, false));
                    return false;
                }
            }
            outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Category Not Found" }, false));
            return false;
        }

        private bool TransactionHasNoError(Category category)
        {
            _transaction.StartSession();
            _transaction.Session.StartTransaction();

            //TODO Change to listing
            //var updatedSubcategories = category.UpdateSubcategories(subcategories, category);

            //var update1 = _CategoryRepository.UpdateManyWithSession(updatedSubcategories, _transaction.Session);
            var update2 = _CategoryRepository.UpdateCategoryWithSession(category.CategoryId, category, _transaction.Session);
            //var update3 = _ListingRepository.UpdateListingWithSession
            return  update2.Success;
        }
    }
}
