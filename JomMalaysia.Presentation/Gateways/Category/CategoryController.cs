using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Presentation.Gateways.Category
{
    public class CategoryControllerTest : Controller
    {
        private readonly ICategoryGateway _gateway;



        public CategoryControllerTest(ICategoryGateway gateway)
        {
            _gateway = gateway;
        }
        public IActionResult Index()
        {
            List<CategoryViewModel> vm = new List<CategoryViewModel>();

            try
            {
                vm = _gateway.GetCategories();
            }catch(Exception e)
            { }
            return View(vm);
        }
    }
}