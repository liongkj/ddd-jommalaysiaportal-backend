using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities.Merchants;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Entities
{
    public class Merchant
    {
        public Guid SsmId { get; private set; }
        public Name Name { get; private set; }
        private ListingCollection _listings;
        public IReadOnlyCollection<Guid> Listings
        {
            get
            {
                IReadOnlyCollection<Guid> readOnly = _listings.GetListingIds();
                return readOnly;
            }
        }

        public void CreateListing (Guid listingId)
        {
            _listings.Add(listingId);
        }

        private Merchant()
        {
        }


        public static Merchant Load(Guid ssmId, string name, ListingCollection listings)
        {
            Merchant merchant = new Merchant();
            merchant.SsmId = ssmId;
            merchant.Name = name;
            merchant._listings = listings;
            return merchant;
        }
    }
}