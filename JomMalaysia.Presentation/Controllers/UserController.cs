using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Manager;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JomMalaysia.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthorizationManagers _authorizationManagers;

        public UserController(IAuthorizationManagers authorizationManagers)
        {
            _authorizationManagers = authorizationManagers;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var client = new RestClient("https://localhost:44368/api/User");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + _authorizationManagers.accessToken);
            IRestResponse response = client.Execute(request);
            
            return View();
        }
    }
}
