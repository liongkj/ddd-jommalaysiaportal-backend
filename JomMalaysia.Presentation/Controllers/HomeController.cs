using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using RestSharp;
using JomMalaysia.Framework.Configuration;
using System.Security.Claims;
using JomMalaysia.Presentation.Manager;

namespace JomMalaysia.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAppSetting _appSetting;
        private readonly IAuthorizationManagers _authorizationManagers;

        public HomeController(IAppSetting appSetting, IAuthorizationManagers authorizationManagers)
        {
            _appSetting = appSetting;
            _authorizationManagers = authorizationManagers;
        }


        public IActionResult Index()
        {
            ViewData["AT"] = _authorizationManagers.accessToken;
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
