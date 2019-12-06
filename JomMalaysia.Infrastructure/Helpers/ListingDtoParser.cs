using System;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Entities.Listings.Attractions;
using JomMalaysia.Core.Domain.Entities.Listings.Governments;
using JomMalaysia.Core.Domain.Entities.Listings.Professionals;

namespace JomMalaysia.Infrastructure.Helpers
{
    public static class ListingDtoParser
    {
        public static Listing Converted(IMapper mapper, IListingDto list)
        {
            var type = GetListingTypeHelper(list);
            if (list == null) return null;
            if (type == typeof(PrivateSector))
            {
                var i = mapper.Map<PrivateSector>(list);

                return i;
            }

            if (type == typeof(ProfessionalService))
            {
                var i = mapper.Map<ProfessionalService>(list);
                return i;
            }
            if (type == typeof(GovernmentOrg))
            {
                var i = mapper.Map<GovernmentOrg>(list);
                return i;
            }
            if (type == typeof(NonProfitOrg))
            {
                var i = mapper.Map<NonProfitOrg>(list);
                return i;
            }
            if (type == typeof(Attraction))
            {
                var i = mapper.Map<Attraction>(list);
                return i;
            }
            return null;
        }

        private static Type GetListingTypeHelper(IListingDto list)
        {
            if (list.CategoryType == CategoryType.Attraction.ToString())
            {
                return typeof(Attraction);
            }
            if (list.CategoryType == CategoryType.Professional.ToString())
            {
                return typeof(ProfessionalService);
            }

            if (list.CategoryType == CategoryType.Government.ToString())
            {
                return typeof(GovernmentOrg);
            }
            if (list.CategoryType == CategoryType.Nonprofit.ToString())
            {
                return typeof(NonProfitOrg);
            }
            if (list.CategoryType == CategoryType.Private.ToString())
            {
                return typeof(PrivateSector);
            }
            throw new ArgumentException("Error taking listing info from database ");

        }
    }
}