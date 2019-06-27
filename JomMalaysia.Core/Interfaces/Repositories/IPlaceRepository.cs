using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.PlaceUseCase.Create;

namespace JomMalaysia.Core.Interfaces
{
    public interface IPlaceRepository
    {
        // api/[GET]
        Task<IEnumerable<Place>> GetAllPlaces();
        // api/1/[GET]
        Task<Place> GetPlace(string id);
        // api/[POST]
        Task<CreatePlaceResponse> Create(Place place);
        // api/[PUT]
        Task<bool> Update(Place place);
        // api/1/[DELETE]
        Task<bool> DeleteAsync(string id);
        Task<string> GetNextId();
    }
}
