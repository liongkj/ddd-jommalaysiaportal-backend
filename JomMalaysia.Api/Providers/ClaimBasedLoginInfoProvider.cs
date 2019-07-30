using Auth0.AuthenticationApi;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Framework.Constant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JomMalaysia.Api.Providers
{
    public class ClaimBasedLoginInfoProvider : ILoginInfoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimBasedLoginInfoProvider(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginInfo GetLoginInfo()
        {
            IEnumerable<Claim> claims = _httpContextAccessor.HttpContext.User.Claims;
            LoginInfo userInfo = new LoginInfo();

            userInfo.userId = claims.Where(c => c.Type == ConstantHelper.Claims.userId).Select(c => c.Value).FirstOrDefault();
            userInfo.name = claims.Where(c => c.Type == ConstantHelper.Claims.name).Select(c => c.Value).FirstOrDefault();
            userInfo.scope = claims.Where(c => c.Type == ConstantHelper.Claims.permission).Select(c => c.Value).ToList();

            return userInfo;
        }
    }
}
