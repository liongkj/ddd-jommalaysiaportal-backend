using System.Threading;
using System.Threading.Tasks;
using JomMalaysia.Framework.Configuration;
using Newtonsoft.Json;
using RestSharp;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Infrastructure.Auth0.Managers
{
    public class Auth0AccessTokenManager : IOAuthTokenManager
    {
        private readonly IAuth0Setting _appSetting;
        public Auth0AccessTokenManager(IAuth0Setting appSetting)
        {
            _appSetting = appSetting;
        }

        public async Task<string> GetAccessToken(string type = null)
        {
            if (type == "auth0")
            {
                var client = new RestClient(_appSetting.RequestAccessTokenApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"client_id\":\"" + _appSetting.Auth0ClientId + "\",\"client_secret\":\"" + _appSetting.Auth0ClientSecret + "\",\"audience\":\"https://jomn9.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);
                dynamic deserializedJson = JsonConvert.DeserializeObject(response.Content);

                return deserializedJson.access_token ?? null;
            }
            else
            {
                var client = new RestClient(_appSetting.RequestAccessTokenApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json",
                "{\"client_id\":\"" + _appSetting.AuthorizationClientId +
                "\",\"client_secret\":\"" + _appSetting.AuthorizationClientSecret +
                "\",\"audience\":\"" + _appSetting.AuthorizationAudience +
                "\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);

                IRestResponse response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);
                dynamic deserializedJson = JsonConvert.DeserializeObject(response.Content);

                return deserializedJson.access_token ?? null;
            }
        }
    }
}