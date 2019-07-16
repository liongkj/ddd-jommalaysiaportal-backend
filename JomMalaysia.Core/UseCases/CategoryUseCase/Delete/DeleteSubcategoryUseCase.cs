using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        private readonly ICategoryRepository _Category;
        private readonly IListingRepository _Listing;
        public DeleteSubcategoryUseCase(ICategoryRepository category,IListingRepository listing)
        {
            _Category = category;
            _Listing = listing;
        }

        public bool Handle(DeleteSubcategoryRequest message, IOutputPort<DeleteCategoryResponse> outputPort)
        {

            //generate subcategory object
            var Subcategory = _Category.FindByName(message.Category, message.Subcategory).Category;

            if (Subcategory != null)
            {
                //check whether listing have this category
                //TODO wait vinnie listing done
                //if no then initiate delete subcategory repo
                var response = _Category.Delete(Subcategory.CategoryId);
                outputPort.Handle(new DeleteCategoryResponse(response.Id, response.Success));
                return response.Success;
            }
            else //subcategory not found
            {
                outputPort.Handle(new DeleteCategoryResponse(new string[] { "subcategory not found" }, false));
                return false;
            }
        }
    }
}