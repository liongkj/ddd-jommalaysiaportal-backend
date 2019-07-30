using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Helper
{
    public class Auth0Helper
    {
        public static void getAccessToken()
        {
            var client = new RestClient("https://jomn9.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"9mWFLX7PoR6sELJWiH4JSUzV913LTuN6\",\"client_secret\":\"Ef3xlYarfqeys8S9g_hmnCjbOfufK63wtMP8Jl2uJM2ZymKd_gd0EUpHNGoFqhV0\",\"audience\":\"https://jomn9.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

    }
}
