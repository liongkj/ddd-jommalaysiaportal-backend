using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JomMalaysia.Framework.Constant;
using JomMalaysia.Framework.Helper;
using Microsoft.AspNetCore.Http;
using RestSharp;


namespace JomMalaysia.Framework.WebServices
{
    public class RestSharpFactory
    {
        
        public static IRestRequest ConstructRequest(string path, Method method, object[] objects)
        {
            IRestRequest request = new RestRequest(path, method, DataFormat.Json)
            {
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Timeout = TimeSpan.FromMinutes(60).Milliseconds
            };
            //if (accesstoken != null) request.AddHeader("Authorization", $"Bearer {accesstoken}");
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
        public static IRestClient ConstructClient(string baseUrl, string accesstoken = null)
        {
            var client = new RestClient(baseUrl);

            client.ClearHandlers();
            if (accesstoken != null) client.AddDefaultHeader("Authorization", $"Bearer {accesstoken}");
            var handler = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", () => handler); // Use custom deserializer.
            client.AddHandler("text/json", () => handler);
            client.AddHandler("text/x-json", () => handler);
            client.AddHandler("text/javascript", () => handler);
            client.AddHandler("*+json", () => handler);
            client.Timeout = 300000; // 5 minutes
            return client;
        }
    }
}
