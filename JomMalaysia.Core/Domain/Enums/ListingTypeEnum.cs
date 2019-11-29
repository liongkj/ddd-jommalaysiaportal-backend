using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public class ListingTypeEnum : EnumerationBase
    {
        public static  ListingTypeEnum PrivateSector = new ListingTypeEnum(1, nameof(PrivateSector).ToLowerInvariant());
        public static ListingTypeEnum ProfessionalService = new ListingTypeEnum(2, nameof(ProfessionalService).ToLowerInvariant());
        public static ListingTypeEnum GovernmentOrg = new ListingTypeEnum(3, nameof(GovernmentOrg).ToLowerInvariant());
        public static ListingTypeEnum NonProfitOrg = new ListingTypeEnum(4, nameof(NonProfitOrg).ToLowerInvariant());
        public static ListingTypeEnum Attraction = new ListingTypeEnum(4, nameof(Attraction).ToLowerInvariant());
        
        public ListingTypeEnum(int id, string name) : base(id, name)
        {

        }

        public static ListingTypeEnum For(string enumstring)
        {
            return Parse<ListingTypeEnum>(enumstring);
        }
    }
}