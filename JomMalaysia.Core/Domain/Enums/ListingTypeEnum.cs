using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public class ListingTypeEnum : EnumerationBase
    {
        public static ListingTypeEnum Local = new ListingTypeEnum(1, nameof(Local).ToLowerInvariant());
        public static ListingTypeEnum Civic = new ListingTypeEnum(2, nameof(Civic).ToLowerInvariant());
        public static ListingTypeEnum Event = new ListingTypeEnum(3, nameof(Event).ToLowerInvariant());
        public static ListingTypeEnum Gover = new ListingTypeEnum(4, nameof(Gover).ToLowerInvariant());

        public ListingTypeEnum(int id, string name) : base(id, name)
        {

        }

        public static ListingTypeEnum For(string enumstring)
        {

            return Parse<ListingTypeEnum>(enumstring);
        }
    }
}