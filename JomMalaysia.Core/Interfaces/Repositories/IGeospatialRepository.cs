using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces
{
    public interface IGeospatialRepository
    {

        Task<ListingResponse> GetListingsWithinRadius(Coordinates location, double radius, string type);
    }
}
