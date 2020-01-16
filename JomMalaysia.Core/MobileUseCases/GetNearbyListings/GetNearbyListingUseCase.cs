using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;

namespace JomMalaysia.Core.MobileUseCases.GetNearbyListings
{
    public class GetNearbyListingUseCase : IGetNearbyListingUseCase
    {
        private readonly IGeospatialRepository _geospatialQuery;

        public GetNearbyListingUseCase(IGeospatialRepository geospatialQuery)
        {
            _geospatialQuery = geospatialQuery;
        }

        public async Task<bool> Handle(GetNearbyListingRequest message, IOutputPort<ListingResponse> outputPort)
        {
            var NearbyListingQuery = await _geospatialQuery.GetListingsWithinRadius(message.Location, message.Radius, message.CategoryType);
            outputPort.Handle(new ListingResponse(NearbyListingQuery.Data,NearbyListingQuery.Success,NearbyListingQuery.Message));
            return NearbyListingQuery.Success;
        }
    }
}
