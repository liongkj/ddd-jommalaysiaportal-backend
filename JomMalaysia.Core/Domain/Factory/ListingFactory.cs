using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Factory;
using JomMalaysia.Core.Domain.ValueObjects;

class ListingFactory : Factory
{

    public override Listing GetListing(string type)
    {
        switch (type.ToLowerInvariant())
        {
            case "event":
                return new EventListing();
            case "governemnt":
                return new GovernmentListing();
            case "private":
                return new PrivateListing();
            default: return null;
        }
    }
}