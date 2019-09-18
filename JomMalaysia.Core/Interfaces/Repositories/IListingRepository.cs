using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces
{
    public interface IListingRepository
    {

        Task<CoreListingResponse> CreateListingAsync(Listing listing, IClientSessionHandle session);
        Task<GetAllListingResponse> GetAllListings();
        Task<DeleteListingResponse> DeleteAsyncWithSession(string id, IClientSessionHandle session);

        GetListingResponse FindByName(string name);
        Task<GetListingResponse> FindById(string id);
        Task<CoreListingResponse> UpdateAsyncWithSession(Listing listing, IClientSessionHandle session = null);




    }
}
