
using System.Collections.Generic;
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
                if (category.UpdateNameIsSuccess(message.Updated))
                {
                    var updatedSubcategories = category.UpdateSubcategories(subcategories, category);
                    _transaction.StartSession();
                    _transaction.Session.StartTransaction();
                    var update1 = _CategoryRepository.UpdateManyWithSession(updatedSubcategories, _transaction.Session);
                    var update2 = _CategoryRepository.UpdateCategoryWithSession(category.CategoryId, category, _transaction.Session);
                    var success = update1.Success && update2.Success;
                    if (success)
                    {
                        _transaction.Session.CommitTransaction();
                        outputPort.Handle(success ? new UpdateCategoryResponse(update1.Message, true) : new UpdateCategoryResponse(update1.Errors));
                        return success;
                    }
                    _transaction.Session.AbortTransaction();
                    outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Update category transaction failed" }, false));
                    return false;
                }
            }
            outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Category Not Found" }, false)) ;
            return false;
        }


    }
}
