using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases.GetNearbyListings
{
    public class GetNearbyListingRequest : IUseCaseRequest<ListingResponse>
    {
 public Coordinates Location { get; set; }
        public double Radius { get; set; } = 10.0;
        public string CategoryType { get; set; } = "all";

    }
}