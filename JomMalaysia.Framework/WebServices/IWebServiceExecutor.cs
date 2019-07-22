using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RestSharp;

namespace JomMalaysia.Framework.WebServices
{
    public interface IWebServiceExecutor
    {
        IWebServiceResponse<T> ExecuteRequest<T>(string url, Method method, params object[] objects) where T : new();

        IWebServiceResponse ExecuteRequest(string url, Method method, params object[] objects);
    }

    public enum HttpParameterType
    {
        RequestBody,
        QueryString,
        HttpHeader,
        GetOrPost,
        UrlSegment,
        Cookie
    };

    //public enum Method
    //{
    //    GET,
    //    POST,
    //    PUT,
    //    DELETE
    //};
}
