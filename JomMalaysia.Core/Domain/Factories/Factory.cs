using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Factories
{
    public abstract class Factory
    {
        
        
        public List<Property> Properties
        {
            get { return _properties; }
        }
        public abstract Listing CreateListing(string listingName, Category category,  Location listingLocation, ListingTypeEnum ListingType);


    }
}