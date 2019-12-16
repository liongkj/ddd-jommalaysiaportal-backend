using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepo;
        private readonly IListingRepository _LisitngRepo;
        public DeleteCategoryUseCase(ICategoryRepository categoryRepository, IListingRepository listingRepository)
        {
            _CategoryRepo = categoryRepository;
            _LisitngRepo = listingRepository;
        }

        public async Task<bool> Handle(DeleteCategoryRequest message, IOutputPort<DeleteCategoryResponse> outputPort)
        {
            try
            {
                var query = await _CategoryRepo.FindByIdAsync(message.Id);
                var category = query.Data;
                if (!query.Success)//found category
                {
                    outputPort.Handle(new DeleteCategoryResponse(message.Id, false, "Category Not Found"));
                    return false;
                }
                //TODO if is subcategory
                if (!category.IsCategory())
                {
                    var AssociatedListings = await _LisitngRepo.GetAllListings(category.CategoryPath);
                    if (AssociatedListings.Listings.Count > 0)//found category
                    {
                        outputPort.Handle(new DeleteCategoryResponse(message.Id, false, "This category still has listing associated with it"));
                        return false;
                    }
                    else
                    {
                        var response = await _CategoryRepo.DeleteAsync(category.CategoryId);
                        outputPort.Handle(response);
                        return response.Success;
                    }
                }
                else
                {
                    //fetch subcategories
                    var Subcategories = await _CategoryRepo.GetAllCategoriesAsync(category.CategoryName);

                    if (Subcategories.Data.Count > 1 && category.HasSubcategories(Subcategories.Categories))
                    {
                        outputPort.Handle(new DeleteCategoryResponse(category.CategoryName, false, category.CategoryName + " still has subcategories associated."));
                        return false;
                    }
                    else //no subcategories
                    {
                        var response = await _CategoryRepo.DeleteAsync(category.CategoryId);
                        outputPort.Handle(response);
                        return response.Success;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}