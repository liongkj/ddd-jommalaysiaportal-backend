using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IGeospatialRepository
    {

        Task<GetAllListingResponse> ListingSearch(string query, string locale = "en");
        Task<GetAllListingResponse> GetListingsWithinRadius(Coordinates location, double radius, string type);
    }
}
