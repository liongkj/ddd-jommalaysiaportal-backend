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
            var type = GetListingTypeHelper(list);
            if (list != null)
            {
                if (type.Equals(typeof(EventListing)))
                {
                    var i = mapper.Map<EventListing>(list);

                    return i;
                }

                if (type.Equals(typeof(LocalListing)))
                {
                    var i = mapper.Map<LocalListing>(list);
                    return i;
                }
                if (type.Equals(typeof(AdministrativeListing)))
                {
                    var i = mapper.Map<AdministrativeListing>(list);
                    return i;
                }
            }
            return null;
        }

        public static Type GetListingTypeHelper(IListingDto list)
        {
            if (list.ListingType == ListingTypeEnum.Event.ToString())
            {
                return typeof(EventListing);

            }
            if (list.ListingType == ListingTypeEnum.Local.ToString())
            {
                return typeof(LocalListing);
            }

            if (list.ListingType == ListingTypeEnum.Gover.ToString())
            {
                return typeof(AdministrativeListing);
            }
            throw new ArgumentException("Error taking listing info from database ");

        }
    }
}