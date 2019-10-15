﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.MobileUseCases;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetNearbyListingUseCase : IGetNearbyListingUseCase
    {
        IGeospatialRepository _geospatialQuery;

        public GetNearbyListingUseCase(IGeospatialRepository geospatialQuery)
        {
            _geospatialQuery = geospatialQuery;
        }

        public async Task<bool> Handle(GetNearbyListingRequest message, IOutputPort<ListingResponse> outputPort)
        {
            var NearbyListingQuery = await _geospatialQuery.GetListingsWithinRadius(message.Location, message.Radius, message.Type);
            outputPort.Handle(NearbyListingQuery);
            return NearbyListingQuery.Success;
        }
    }
}
