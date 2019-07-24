using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JomMalaysia.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> categoryList = new List<Category>();
            Category cat;
            for (int i = 0; i < 5; i++)
            {
                cat = new Category();
                cat.categoryId = "" + i;
                cat.categoryName = "Test NAME";
                cat.categoryNameMs = " Test Category MS NAME";
                cat.categoryNameZh = " test category ZH name";

                categoryList.Add(cat);

            }

        
            return View(categoryList);
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
