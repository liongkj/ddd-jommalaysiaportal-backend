using System;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Helpers
{
    public static class ListingDtoParser
    {
        public static Listing Converted(IMapper mapper, ListingDto list)
        {
            if (list != null)
            {
                if (GetListingTypeHelper(list).Equals(typeof(EventListing)))
                {
                    var i = mapper.Map<EventListing>(list);

                    return i;
                }

                if (GetListingTypeHelper(list).Equals(typeof(PrivateListing)))
                {
                    var i = mapper.Map<PrivateListing>(list);
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
                return typeof(PrivateListing);
            }
            throw new ArgumentException("Error taking listing info from database ");

        }
    }
}