using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Framework.Helper;
using RestSharp;

namespace JomMalaysia.Framework.WebServices
{
    public class WebServiceExecutor : IWebServiceExecutor
    {
        public virtual IWebServiceResponse ExecuteRequest(string url, Method method, params object[] objects)
        {
            //Create Client
            IRestClient client = RestSharpFactory.ConstructClient(NetHelper.GetBaseUrl(url));
            //Create Request
            IRestRequest request = RestSharpFactory.ConstructRequest(NetHelper.GetUrlPath(url), method, objects);
            //wait response
            IRestResponse response = client.Execute(request);

            return new WebServiceResponse()
            {
                RawContent = response.Content,
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription
            };

        }

        public IWebServiceResponse<T> ExecuteRequest<T>(string url, Method method, params object[] objects) where T : new()
        {
            //Create Client
            IRestClient client = RestSharpFactory.ConstructClient(NetHelper.GetBaseUrl(url));
            //Create Request
            IRestRequest request = RestSharpFactory.ConstructRequest(NetHelper.GetUrlPath(url), method, objects);
            //wait response
            IRestResponse<T> response = client.Execute<T>(request);

            return new WebServiceResponse<T>()
            {
                RawContent = response.Content,
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription,
                Data = response.Data
            };

        }
    }
}
