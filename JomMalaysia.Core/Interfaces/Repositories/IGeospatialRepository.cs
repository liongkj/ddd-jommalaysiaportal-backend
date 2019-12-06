using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.MobileUseCases;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IGeospatialRepository
    {

        Task<ListingResponse> GetListingsWithinRadius(Coordinates location, double radius, string type);
    }
}
