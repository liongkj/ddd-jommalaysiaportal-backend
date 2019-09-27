using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Infrastructure.Auth0
{
    public class Auth0Setting : IAuth0Setting
    {
        IConfiguration _IConfiguration;
        private static string _Auth0Domain;
        private static string _Auth0ClientId;
        private static string _Auth0ClientSecret;
        private static string _WebApiUrl;
        private static string _DbConnection;
        private static string _Scope;
        private static string _Auth0UserManagementApi;
        private static string _Auth0SendResetPasswordEmailApi;
        private static string _RequestAccessTokenApi;
        private static string _AdditionalClaimsRoles;
        private static string _AuthorizationApi;
        public Auth0Setting(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
            Initialize();
        }
        public string Auth0UserManagementApi => _Auth0UserManagementApi;
        public string Auth0Domain => _Auth0Domain;

        public string Auth0ClientId => _Auth0ClientId;

        public string Auth0ClientSecret => _Auth0ClientSecret;

        public string WebApiUrl => _WebApiUrl;

        public string DBConnection => _DbConnection;

        public string Scope => _Scope;

        public string Auth0SendResetPasswordEmailApi => _Auth0SendResetPasswordEmailApi;

        public string RequestAccessTokenApi => _RequestAccessTokenApi;
        public string AdditionalClaimsRoles => _AdditionalClaimsRoles;

        public string AuthorizationApi => _AuthorizationApi;

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

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:ManagementApi:User")))
            {
                _Auth0UserManagementApi = _IConfiguration.GetValue<string>("Auth0:ManagementApi:User");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:ManagementApi:ResetPassword")))
            {
                _Auth0SendResetPasswordEmailApi = _IConfiguration.GetValue<string>("Auth0:ManagementApi:ResetPassword");
            }

            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:OAuth")))
            {
                _RequestAccessTokenApi = _IConfiguration.GetValue<string>("Auth0:OAuth");
            }
            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:AdditionalClaims:Roles")))
            {
                _AdditionalClaimsRoles = _IConfiguration.GetValue<string>("Auth0:AdditionalClaims:Roles");
            }
            if (!string.IsNullOrEmpty(_IConfiguration.GetValue<string>("Auth0:AuthorizationApi")))
            {
                _AuthorizationApi = _IConfiguration.GetValue<string>("Auth0:AuthorizationApi");
            }

        }
    }
}
