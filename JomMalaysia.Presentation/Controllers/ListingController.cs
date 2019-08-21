using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Gateways.Listing;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Presentation.Controllers
{
    public class ListingController : Controller
    {
        private readonly IListingGateway _gateway;

        private List<ListingViewModel> ListingList { get; set; }

        public ListingController(IListingGateway gateway)
        {
            _gateway = gateway;

            Refresh();
        }

        async void Refresh()
        {
            if (ListingList != null)
                ListingList = await GetListings();
            else
            {
                ListingList = new List<ListingViewModel>();
            }
        }
        // GET: Listing
        async Task<List<ListingViewModel>> GetListings()
        {
            if (ListingList.Count > 0)
            {
                return ListingList;
            }
            try
            {
                ListingList = await _gateway.GetListings().ConfigureAwait(false);
                return ListingList;
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
            var listings = await GetListings();
            
            return View(listings);
        }

        // GET: Listing/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Listing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Listing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Listing/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Listing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Listing/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Listing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}