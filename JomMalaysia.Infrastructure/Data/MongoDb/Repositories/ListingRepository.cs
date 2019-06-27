//ToDO
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;

public class ListingRepository : IListingRepository
{
    public Task<CreateListingResponse> CreateListing(Listing listing)
    {
        throw new System.NotImplementedException();
    }

    public DeleteListingResponse Delete(string id)
    {
        throw new System.NotImplementedException();
    }

    public GetListingResponse FindById(string id)
    {
        throw new System.NotImplementedException();
    }

    public GetListingResponse FindByName(string name)
    {
        throw new System.NotImplementedException();
    }

    public Task<GetAllListingResponse> GetAllListings()
    {
        throw new System.NotImplementedException();
    }

    public UpdateListingResponse Update(string id, Listing listing)
    {
        throw new System.NotImplementedException();
    }
}