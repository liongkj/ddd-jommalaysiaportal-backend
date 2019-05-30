using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace JomMalaysia.Core.Domain.Entities.Merchants
{
    public sealed class ListingCollection
    {
        private readonly IList<Guid> _listingIds;

        public ListingCollection()
        {
            _listingIds = new List<Guid>();
        }

        public IReadOnlyCollection<Guid> GetListingIds()
        {
            IReadOnlyCollection<Guid> listingIds = new ReadOnlyCollection<Guid>(_listingIds);
            return listingIds;
        }

        public void Add(Guid listingIds)
        {
            _listingIds.Add(listingIds);
        }
    }
}