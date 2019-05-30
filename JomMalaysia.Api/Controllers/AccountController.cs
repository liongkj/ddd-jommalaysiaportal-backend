using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace api.Controllers
{
    public class AccountController : Controller
    {
        //private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly JWTSettings _options;
        //private readonly UserManager<IdentityUser> _userManager;

        //public AuthController(
        //      UserManager<IdentityUser> userManager,
        //      SignInManager<IdentityUser> signInManager,
        //      IOptions<JWTSettings> optionsAccessor)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _options = optionsAccessor.Value;
        //}

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the 
                // **Allowed Logout URLs** settings for the client.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        //    [Authorize]
        //    public IActionResult Claims()
        //    {
        //        return View();
        //    }

        //    public IActionResult AccessDenied()
        //    {
        //        return View();
        //    }

        //    private string GetIdToken(IdentityUser user)
        //    {
        //        var payload = new Dictionary<string, object>
        //  {
        //    { "id", user.Id },
        //    { "sub", user.Email },
        //    { "email", user.Email },
        //    { "emailConfirmed", user.EmailConfirmed },
        //  };
        //        return GetToken(payload);
        //    }

        //    private string GetAccessToken(string Email)
        //    {
        //        var payload = new Dictionary<string, object>
        //  {
        //    { "sub", Email },
        //    { "email", Email }
        //  };
        //        return GetToken(payload);
        //    }

        //    private string GetToken(Dictionary<string, object> payload)
        //    {
        //        var secret = _options.SecretKey;

        //        payload.Add("iss", _options.Issuer);
        //        payload.Add("aud", _options.Audience);
        //        payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
        //        payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
        //        payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
        //        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        //        IJsonSerializer serializer = new JsonNetSerializer();
        //        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        //        return encoder.Encode(payload, secret);
        //    }

        //    private JsonResult Errors(IdentityResult result)
        //    {
        //        var items = result.Errors
        //            .Select(x => x.Description)
        //            .ToArray();
        //        return new JsonResult(items) { StatusCode = 400 };
        //    }

        //    private JsonResult Error(string message)
        //    {
        //        return new JsonResult(message) { StatusCode = 400 };
        //    }

        //    private static double ConvertToUnixTimestamp(DateTime date)
        //    {
        //        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //        TimeSpan diff = date.ToUniversalTime() - origin;
        //        return Math.Floor(diff.TotalSeconds);
        //    }
        //}
    }
}