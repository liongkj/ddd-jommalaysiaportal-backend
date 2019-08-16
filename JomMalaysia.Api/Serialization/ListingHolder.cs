using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Api.Serialization
{
    [JsonConverter(typeof(ListingConverter))]
    public class ListingHolder
    {
        public string ListingType { get; set; }
        public BaseListingHolder ConvertedListing{ get; set; }
    }
}
