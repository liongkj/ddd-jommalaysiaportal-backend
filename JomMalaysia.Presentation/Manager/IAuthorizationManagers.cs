using JomMalaysia.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Manager
{
    public interface IAuthorizationManagers
    {
        string accessToken { get; }

        string refreshToken { get; }

        UserInfoViewModel LoginInfo { get; }
    }
}
