using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace JomMalaysia.Framework.Helper
{
    public class JsonDotNetDeserializer : IDeserializer
    {
        public static JsonDotNetDeserializer Instance => new JsonDotNetDeserializer();
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }


        public T Deserialize<T>(IRestResponse response)
        {
            if (response.ErrorException == null && response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(response.Content);
            else
                return default;
        }
    }
}
