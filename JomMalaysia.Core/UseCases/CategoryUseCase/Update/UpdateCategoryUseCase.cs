
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<bool> Handle(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {
            //TODO
            //check if any listing has this category

            var category = (await _CategoryRepository.FindByNameAsync(message.CategoryName)).Category;
            var subcategories = (await _CategoryRepository.GetAllCategoriesAsync(message.CategoryName)).Categories;
            if (category != null) //if category found
            {
                //find all subcategories with same name
                if (category.UpdateCategoryIsSuccess(message.Updated, true))
                {
                    var response = await TransactionHasNoError(category, subcategories);

                    outputPort.Handle(response);
                    return response.Success;
                }
            }
            outputPort.Handle(new UpdateCategoryResponse(new List<string> { "Category Not Found" }, false));
            return false;
        }

        private async Task<UpdateCategoryResponse> TransactionHasNoError(Category category, List<Category> subcategories)
        {
            using (var session = await _transaction.StartSession())
            {
                session.StartTransaction();

                var updatedSubcategories = category.UpdateSubcategories(subcategories, category);

                List<string> Errors = new List<string>();

                var update1 = _CategoryRepository.UpdateManyWithSession(updatedSubcategories, session);


                var update2 = _CategoryRepository.UpdateCategoryWithSession(category.CategoryId, category, session);

                //var update3 = _ListingRepository.UpdateListingWithSession

                if (Errors.Count == 0)
                {
                    session.CommitTransaction();
                    return new UpdateCategoryResponse(category.CategoryId, true, "update category committed successfully");

                }
                _transaction.Session.AbortTransaction();
                return new UpdateCategoryResponse(Errors);
            }
        }
    }
}
