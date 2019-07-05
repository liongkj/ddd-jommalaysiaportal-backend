using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace JomMalaysia.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppSetting _appSetting;

        public AccountController(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AuthenticationApiClient client = new AuthenticationApiClient(new Uri($"https://{_appSetting.Auth0Domain}/"));

                    var result = await client.GetTokenAsync(new ResourceOwnerTokenRequest
                    {
                        ClientId = _appSetting.Auth0ClientId,
                        ClientSecret = _appSetting.Auth0ClientSecret,
                        Scope = "openid email profile offline_access",
                        Realm = _appSetting.DBConnection, // Specify the correct name of your DB connection
                        Username = vm.EmailAddress,
                        Password = vm.Password,
                        Audience = _appSetting.WebApiUrl
                    });

                    // Get user info from token
                    var user = await client.GetUserInfoAsync(result.AccessToken);

                    // Create claims principal
                    var claims = new List<Claim>
                    {
                        new Claim("access_token", result.AccessToken),
                        new Claim("refresh_token", result.RefreshToken),
                        new Claim(ClaimTypes.Expiration, result.ExpiresIn.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId),
                        new Claim(ClaimTypes.Name, user.FullName)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Sign user into cookie middleware
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToLocal(returnUrl);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}