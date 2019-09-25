using Auth0.AuthenticationApi;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Framework.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public User AuthenticatedUser()
        {
            var loginInfo = GetLoginInfo();
            User logged = new User
            {
                UserId = loginInfo.userId,
                Name = (Name)(loginInfo.name),



            };
            return logged;
        }

        public LoginInfo GetLoginInfo()
        {
            IEnumerable<Claim> claims = _httpContextAccessor.HttpContext.User.Claims;

            LoginInfo userInfo = new LoginInfo
            {
                userId = claims.Where(c => c.Type == ConstantHelper.Claims.userId).Select(c => c.Value).FirstOrDefault(),
                name = claims.Where(c => c.Type == ConstantHelper.Claims.name).Select(c => c.Value).FirstOrDefault(),
                scope = claims.Where(c => c.Type == ConstantHelper.Claims.permission).Select(c => c.Value).ToList()
            };

            return userInfo;
        }


    }
}
