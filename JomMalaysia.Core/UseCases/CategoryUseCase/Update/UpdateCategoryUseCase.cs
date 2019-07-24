
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMongoDbContext _transaction;

        public UpdateCategoryUseCase(ICategoryRepository CategoryRepository, IMongoDbContext transaction)
        {
            _CategoryRepository = CategoryRepository;
            _transaction = transaction;
        }
        public bool Handle(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {
            //TODO
            //check if any listing has this category
            
            var category = _CategoryRepository.FindByName(message.CategoryName).Category;
            var subcategories = _CategoryRepository.GetAllCategories(message.CategoryName).Categories;
            if (category != null) //if category found
            {
                //find all subcategories with same name
                if (category.UpdateCategoryIsSuccess(message.Updated,true))
                {
                    var Errors = TransactionHasNoError(category, subcategories);
                    if (Errors.Count == 0)
                    { 
                        _transaction.Session.CommitTransaction();
                        outputPort.Handle(new UpdateCategoryResponse(category.CategoryId, true,"update category committed successfully"));
                        return true;
                    }
                    _transaction.Session.AbortTransaction();
                    outputPort.Handle(new UpdateCategoryResponse(Errors, false));
                    return false;
                }
            }
            outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Category Not Found" }, false));
            return false;
        }

        private List<string> TransactionHasNoError(Category category, List<Category> subcategories)
        {
            _transaction.StartSession();
            _transaction.Session.StartTransaction();

            var updatedSubcategories = category.UpdateSubcategories(subcategories, category);

            List<string> Errors = new List<string>();

            var update1 = _CategoryRepository.UpdateManyWithSession(updatedSubcategories, _transaction.Session);
            if(update1.Errors!=null) Errors.AddRange(update1.Errors);

            var update2 = _CategoryRepository.UpdateCategoryWithSession(category.CategoryId, category, _transaction.Session);
            if (update2.Errors != null) Errors.AddRange(update2.Errors);
            //var update3 = _ListingRepository.UpdateListingWithSession
            return Errors;
        }
    }
}
