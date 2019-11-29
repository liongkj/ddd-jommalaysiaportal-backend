using System;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Entities.Listings.Attractions;
using JomMalaysia.Core.Domain.Entities.Listings.Governments;
using JomMalaysia.Core.Domain.Entities.Listings.Professionals;
using JomMalaysia.Core.Domain.Enums;

namespace JomMalaysia.Infrastructure.Helpers
{
    public static class ListingDtoParser
    {
        public static Listing Converted(IMapper mapper, IListingDto list)
        {
            var type = GetListingTypeHelper(list);
            if (list != null)
            {
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
            }
            return null;
        }

        private static Type GetListingTypeHelper(IListingDto list)
        {
            if (list.ListingType == ListingTypeEnum.Attraction.ToString())
            {
                return typeof(Attraction);
            }
            if (list.ListingType == ListingTypeEnum.ProfessionalService.ToString())
            {
                return typeof(ProfessionalService);
            }

            if (list.ListingType == ListingTypeEnum.GovernmentOrg.ToString())
            {
                return typeof(GovernmentOrg);
            }
            if (list.ListingType == ListingTypeEnum.NonProfitOrg.ToString())
            {
                return typeof(NonProfitOrg);
            }
            if (list.ListingType == ListingTypeEnum.PrivateSector.ToString())
            {
                return typeof(PrivateSector);
            }
            throw new ArgumentException("Error taking listing info from database ");

        }
    }
}