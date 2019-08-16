using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Api.Serialization
{
    public class ListingConverter : JsonConverter
    {



        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            ListingHolder holder = new ListingHolder
            {
                ListingType = (string)jo["listingType"],

            };

            if (holder.ListingType == "event")
                holder.ConvertedListing = jo.ToObject<EventListingHolder>(serializer);
            else
                holder.ConvertedListing = jo.ToObject<EventListingHolder>(serializer);

            return holder;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BaseListingHolder));
        }


    }
}
