using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;

namespace JomMalaysia.Core.Interfaces
{
    public interface IListingRepository
    {
        CreateListingResponse CreateListing(Listing listing);
        Task<GetAllListingResponse> GetAllListings();
        DeleteListingResponse Delete(string id);
        GetListingResponse FindByName(string name);
        GetListingResponse FindById(string id);
        UpdateListingResponse Update(string id, Listing listing);

        

    }
}
