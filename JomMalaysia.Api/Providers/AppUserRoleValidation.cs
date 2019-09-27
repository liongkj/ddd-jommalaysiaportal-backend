using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Framework.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JomMalaysia.Api.Providers
{

    public class AppUserRoleValidation : JwtBearerEvents, ILoginInfoProvider
    {
        private readonly IAuth0Setting _auth0Setting;
        private readonly IUserRepository _userRepository;
        private User AppUser { get; set; }

        public AppUserRoleValidation(IAuth0Setting auth0Setting, IUserRepository userRepository)
        {
            _auth0Setting = auth0Setting;
            _userRepository = userRepository;
        }

        public User AuthenticatedUser()
        {
            return AppUser;
        }

        public LoginInfo GetLoginInfo()
        {
            throw new System.NotImplementedException();
        }

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            var accessToken = context.SecurityToken as JwtSecurityToken;
            var role = context.Principal.Claims.Where(x => x.Type == _auth0Setting.AdditionalClaimsRoles).FirstOrDefault();
            var userid = accessToken.Subject;


            var user = await _userRepository.GetUser(userid.ToString());
            if (user.Success)
            {
                AppUser = user.User;

            }
            else
            {
                throw new Exception(user.Error);
            }

        }
    }
}