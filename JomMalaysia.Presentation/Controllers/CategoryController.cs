using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Gateways.Category;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JomMalaysia.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryGateway _gateway;

        public CategoryController(ICategoryGateway gateway)
        {
            _gateway = gateway;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<CategoryViewModel> CategoryList = new List<CategoryViewModel>();

            try
            {
                CategoryList = _gateway.GetCategories();
            }
            catch (Exception e)
            { }
            return View(CategoryList);

           
        }
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // remove subcategory if category.lstCategory[i].isDeleted = true


            }

            // update student to the database

            return View();
        }

        public ActionResult Edit(Category std)
        {

            // update student to the database

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(Category category)
        {

            // delete student from the database whose id matches with specified id

            return RedirectToAction("Index");
        }

        public ActionResult Publish(int id)
        {
            // delete student from the database whose id matches with specified id

            return RedirectToAction("Index");
        }
    }
}
