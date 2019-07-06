using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Authorization;

namespace JomMalaysia.Presentation.Controllers
{
    [Authorize("read:merchant")]
    public class MerchantController : Controller
    {
        private Merchant merchant;
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            List<Merchant> merchantList = new List<Merchant>();
            Merchant mct;
            for (int i = 0; i < 5; i++)
            {
                mct = new Merchant();
                mct.ssmID = "" + i;
                mct.companyName = "Test company";
                mct.companyAddress = " Test Company address";
                mct.merchantName = " test merchant name";
                mct.contactNumber = "01232423423";

                merchantList.Add(mct);

            }

            return View(merchantList);
        }

       // [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Merchant merchant)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine(merchant.ssmID);
            }

            // update student to the database

            return View();
        }

        public ActionResult Edit(Merchant std)
        {

            // update student to the database

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(Merchant merchant)
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