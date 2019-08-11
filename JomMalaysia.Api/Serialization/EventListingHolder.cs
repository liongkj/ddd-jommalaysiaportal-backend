using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Api.Serialization
{
    public class EventListingHolder : BaseListingHolder
    {
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
    }
}
