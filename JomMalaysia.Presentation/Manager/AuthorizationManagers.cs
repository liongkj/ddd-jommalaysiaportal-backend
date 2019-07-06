using JomMalaysia.Framework.Constant;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace JomMalaysia.Presentation.Manager
{
    public class AuthorizationManagers : IAuthorizationManagers
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationManagers(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string accessToken
        {
            get
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims;
                return claims.Where(c => c.Type == ConstantHelper.Claims.accessToken).Select(c => c.Value).FirstOrDefault();
            }
        }

        public string refreshToken
        {
            get
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims;
                return claims.Where(c => c.Type == ConstantHelper.Claims.refreshToken).Select(c => c.Value).FirstOrDefault();
            }
        }

        public UserInfoViewModel LoginInfo
        {
            get
            {
                UserInfoViewModel userInfo = new UserInfoViewModel();

                var claims = _httpContextAccessor.HttpContext.User.Claims;
                userInfo.userId = claims.Where(c => c.Type == ConstantHelper.Claims.userId).Select(c => c.Value).FirstOrDefault();
                userInfo.name = claims.Where(c => c.Type == ConstantHelper.Claims.name).Select(c => c.Value).FirstOrDefault();
                userInfo.scope = claims.Where(c => c.Type == ConstantHelper.Claims.scope).Select(c => c.Value).FirstOrDefault();
                userInfo.role = claims.Where(c => c.Type == ConstantHelper.Claims.role).Select(c => c.Value).FirstOrDefault();
                return userInfo;
            }
        }
    }
}
