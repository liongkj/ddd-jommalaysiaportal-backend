using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Configuration
{
    public class AppSetting : IAppSetting
    {
        IConfiguration _IConfiguration;
        private static string _Auth0Domain;
        private static string _Auth0ClientId;
        private static string _Auth0ClientSecret;
        private static string _WebApiUrl;
        private static string _DbConnection;
        private static string _Scope;

        public AppSetting(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
            Initialize();
        }

        public string Auth0Domain => _Auth0Domain;

        public string Auth0ClientId => _Auth0ClientId;

        public string Auth0ClientSecret => _Auth0ClientSecret;

        public string WebApiUrl => _WebApiUrl;

        public string DBConnection => _DbConnection;

        public string Scope => _Scope;

        public void Initialize()
        {
            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:Domain")))
            {
                _Auth0Domain = _IConfiguration.GetValue<string>("Auth0:Domain");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:ClientId")))
            {
                _Auth0ClientId = _IConfiguration.GetValue<string>("Auth0:ClientId");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:ClientSecret")))
            {
                _Auth0ClientSecret = _IConfiguration.GetValue<string>("Auth0:ClientSecret");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:DBConnection")))
            {
                _DbConnection = _IConfiguration.GetValue<string>("Auth0:DBConnection");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("WebApiURl")))
            {
                _WebApiUrl = _IConfiguration.GetValue<string>("WebApiURl");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:Scope")))
            {
                _Scope = _IConfiguration.GetValue<string>("Auth0:Scope");
            }

        }
    }
}
