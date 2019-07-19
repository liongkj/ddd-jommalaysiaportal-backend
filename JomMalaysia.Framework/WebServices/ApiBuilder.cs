using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Framework.Configuration;

namespace JomMalaysia.Framework.WebServices
{
    public class ApiBuilder : IApiBuilder
    {
        private readonly IAppSetting _appSetting;


        public ApiBuilder(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public string WebApiUrl => _appSetting.WebApiUrl;


        public string GetApi(string path, params string[] parameters)
        {
            StringBuilder apiString = new StringBuilder();
            apiString.Append(WebApiUrl);
            apiString.Append(path);
            foreach(var param in parameters)
            {
                apiString.Replace(($"{{param}}"),param);
            }
            return apiString.ToString();
        }
    }
}
