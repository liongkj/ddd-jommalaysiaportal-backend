using System;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases.GetNearbyListings
{
    public class GetNearbyListingRequest : IUseCaseRequest<ListingResponse>
    {
        public GetNearbyListingRequest(string location, double radius, string type)
        {
            var coor = location.Split(',');

            Location = new Coordinates(coor[0], coor[1]);
            Radius = radius;
            Type = type;
        }

        public Coordinates Location { get; set; }
        public double Radius { get; set; }
        public string Type { get; set; }

    }
}