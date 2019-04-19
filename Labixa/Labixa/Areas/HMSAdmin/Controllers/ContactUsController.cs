﻿using Outsourcing.Data.Models;
using Outsourcing.Service;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class ContactUsController : Controller
    {
        #region Fields
        private readonly IVendorService _vendorService;
        #endregion

        #region Ctor
        public ContactUsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var vendors = await _vendorService.FindAll().AsNoTracking().ToListAsync();
            return View(vendors);
        }
        #endregion

        #region Details
        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendors vendors = _vendorService.FindById((int)id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            return View(vendors);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new Vendors { Percent = 0 });
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="vendors"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendors vendors)
        {
            if (ModelState.IsValid)
            {
                _vendorService.Create(vendors);
                return RedirectToAction("Index");
            }

            return View(vendors);
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit - GET 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendors vendors = _vendorService.FindById((int)id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            return View(vendors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendors vendors)
        {
            if (ModelState.IsValid)
            {
                _vendorService.Edit(vendors);
                return RedirectToAction("Index");
            }
            return View(vendors);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vendors = _vendorService.FindById((int)id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            return View(vendors);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var vendors = _vendorService.FindById(id);
            if (vendors == null)
            {
                return HttpNotFound();
            }
            _vendorService.Delete(vendors);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
