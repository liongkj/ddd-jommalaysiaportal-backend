using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class ListingTypeEnum : EnumerationBase
    {
        public static ListingTypeEnum Private = new ListingTypeEnum(1, "Private".ToLowerInvariant());
        public static ListingTypeEnum Government = new ListingTypeEnum(2, "Government".ToLowerInvariant());
        public static ListingTypeEnum Event = new ListingTypeEnum(3, "Event".ToLowerInvariant());
        public static ListingTypeEnum Social = new ListingTypeEnum(4, "Social".ToLowerInvariant());

        public ListingTypeEnum(int id, string name) : base(id, name)
        {

        }
    }
}
