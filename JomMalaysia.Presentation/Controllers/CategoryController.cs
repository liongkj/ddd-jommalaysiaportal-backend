using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private List<CategoryViewModel> CategoryList { get; set; }
        public CategoryController(ICategoryGateway gateway)
        {
            _gateway = gateway;

            Refresh();
        }

        async void Refresh()
        {
            if (CategoryList != null)
                CategoryList = await GetCategories();
            else
            {
                CategoryList = new List<CategoryViewModel>();
            }
        }


        async Task<List<CategoryViewModel>> GetCategories()
        {
            if (CategoryList.Count > 0)
            {
                return CategoryList;
            }
            try
            {
                CategoryList = await _gateway.GetCategories().ConfigureAwait(false);
                return CategoryList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await GetCategories());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                // remove subcategory if category.lstCategory[i].isDeleted = true


            }

            return View();

            // update student to the database
        }
        public ActionResult Edit(CategoryViewModel std)
        {

            // update student to the database

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(CategoryViewModel category)
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
