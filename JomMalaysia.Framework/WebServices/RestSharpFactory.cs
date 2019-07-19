using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Framework.Helper;
using RestSharp;

namespace JomMalaysia.Framework.WebServices
{
    public class RestSharpFactory
    {
        public static JsonDotNetDeserializer Instance => new JsonDotNetDeserializer();

        public static IRestRequest ConstructRequest(string path, Method method, object[] objects)
        {
            IRestRequest request = new RestRequest(path, method, DataFormat.Json)
            {
                Timeout = TimeSpan.FromMinutes(60).Milliseconds
            };
            foreach (var obj in objects)
            {
                request.AddBody(obj);
            }
            return request;
        }

        /// <summary>
        /// Constructs a RestSharp client.
        /// </summary>
        /// <param name="baseUrl">Base URL of web service to connect. (Example: http://api.google.com)</param>
        /// <returns>A RestSharp client.</returns>
        public static IRestClient ConstructClient(string baseUrl)
        {
            var client = new RestClient(baseUrl);
            client.ClearHandlers();

            var handler = JsonDotNetDeserializer.Instance;
            client.AddHandler("application/json",()=> { return handler; }) ; // Use custom deserializer.
            client.AddHandler("text/json", () => { return handler; });
            client.AddHandler("text/x-json", () => { return handler; });
            client.AddHandler("*+json", () => { return handler; });
            client.Timeout = 300000; // 5 minutes
            return client;
        }
    }
}
