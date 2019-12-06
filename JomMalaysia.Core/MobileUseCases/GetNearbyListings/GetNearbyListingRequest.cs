using System;
using JomMalaysia.Core.Domain.Entities;
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
            CategoryType = type;
        }

        public Coordinates Location { get;}
        public double Radius { get;  }
        public string CategoryType { get; }

    }
}