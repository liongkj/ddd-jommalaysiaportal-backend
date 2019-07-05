using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Factories
{
    public abstract class Factory
    {
        private List<Property> _properties = new List<Property>();
        
        public List<Property> Properties
        {
            get { return _properties; }
        }
        public abstract Listing CreateListing(string listingName, string description, Category category, Subcategory subcategory, Location listingLocation, ListingTypeEnum ListingType);


    }
}