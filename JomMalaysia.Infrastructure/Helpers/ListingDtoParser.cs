using System;
using AutoMapper;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using JomMalaysia.Core.Domain.Entities.Listings;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Helpers
{
    public static class ListingDtoParser
    {
        public static Listing Converted(IMapper mapper, IListingDto list)
        {
            if (list != null)
            {
                if (GetListingTypeHelper(list).Equals(typeof(EventListing)))
                {
                    var i = mapper.Map<EventListing>(list);

                    return i;
                }

                if (GetListingTypeHelper(list).Equals(typeof(LocalListing)))
                {
                    var i = mapper.Map<LocalListing>(list);
                    return i;
                }
            }
            return null;
        }

        public static Type GetListingTypeHelper(ListingDto list)
        {
            if (list.ListingType == ListingTypeEnum.Event.ToString())
            {
                return typeof(EventListing);

            }
            if (list.ListingType == ListingTypeEnum.Private.ToString())
            {
                return typeof(LocalListing);
            }
            throw new ArgumentException("Error taking listing info from database ");

        }
    }
}