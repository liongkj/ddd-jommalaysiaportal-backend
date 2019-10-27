using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public class ListingStatusEnum : EnumerationBase
    {

        public static ListingStatusEnum Pending = new ListingStatusEnum(2, "Pending".ToLowerInvariant());
        public static ListingStatusEnum Published = new ListingStatusEnum(3, "Published".ToLowerInvariant());
        public static ListingStatusEnum Unpublished = new ListingStatusEnum(4, "Unpublished".ToLowerInvariant());

        public ListingStatusEnum(int id, string name) : base(id, name)
        {

        }

        public static ListingStatusEnum For(string enumstring)
        {

            return Parse<ListingStatusEnum>(enumstring);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}