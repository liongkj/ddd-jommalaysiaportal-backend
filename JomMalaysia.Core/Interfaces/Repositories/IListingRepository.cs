using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IListingRepository
    {

        Task<CoreListingResponse> CreateListingAsync(Listing listing, IClientSessionHandle session);
        Task<GetAllListingResponse> GetAllListings(CategoryPath cp = null, string type = "all", bool groupBySub = false, string publishStatus = "published", string selectedCity = "", bool isFeatured = false);
        Task<GetAllListingResponse> GetAllListings(CategoryPath cp = null, bool isSameSubcategory = true);
        Task<DeleteListingResponse> DeleteAsyncWithSession(string id, IClientSessionHandle session);

        Task<GetListingResponse> FindByName(string name);
        Task<GetListingResponse> FindById(string id);
        Task<CoreListingResponse> UpdateAsyncWithSession(Listing listing, IClientSessionHandle session = null);
        Task<CoreListingResponse> UpdateCategoryAsyncWithSession(Dictionary<string, CategoryPath> toBeUpdateListings, IClientSessionHandle session); //optimize performance
    }
}
