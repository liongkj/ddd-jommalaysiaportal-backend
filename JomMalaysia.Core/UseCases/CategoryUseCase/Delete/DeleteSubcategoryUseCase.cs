using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Domain.ValueObjects;
using System.Collections.Generic;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        private readonly ICategoryRepository _Category;
        private readonly IListingRepository _Listing;
        public DeleteSubcategoryUseCase(ICategoryRepository category, IListingRepository listing)
        {
            _Category = category;
            _Listing = listing;
        }

        public async Task<bool> Handle(DeleteSubcategoryRequest message, IOutputPort<DeleteCategoryResponse> outputPort)
        {

            //generate subcategory object
            var Subcategory = await _Category.FindByNameAsync(message.Category, message.Subcategory);
            var CategoryPath = new CategoryPath(message.Category, message.Subcategory);
            if (Subcategory.Category != null)
            {
                //check whether listing have this category
                //TODO wait vinnie listing done
                var GetListingWithCategoryResponse = await _Listing.GetAllListings(CategoryPath);
                if (GetListingWithCategoryResponse.Success)
                {
                    var ListingCounts = GetListingWithCategoryResponse.Listings.Count;
                    if (ListingCounts > 0)
                    {
                        outputPort.Handle(new DeleteCategoryResponse(new List<string> { "Failed to delete" }, false, $"There are still {ListingCounts} listing under this category. Please update the listing and try again"));
                        return false;
                    }
                }
                //if no then initiate delete subcategory repo
                var response = await _Category.DeleteAsync(Subcategory.Category.CategoryId);
                outputPort.Handle(response);
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