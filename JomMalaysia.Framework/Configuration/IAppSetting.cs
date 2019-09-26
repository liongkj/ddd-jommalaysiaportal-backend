using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Configuration
{
    public interface IAuth0Setting
    {
        void Initialize();

        string WebApiUrl { get; }

        string Auth0Domain { get; }

        string Auth0ClientId { get; }

        string Auth0ClientSecret { get; }

        string DBConnection { get; }

        string Scope { get; }
        string Auth0UserManagementApi { get; }
        string Auth0SendResetPasswordEmailApi { get; }
        string RequestAccessTokenApi { get; }
    }
}
