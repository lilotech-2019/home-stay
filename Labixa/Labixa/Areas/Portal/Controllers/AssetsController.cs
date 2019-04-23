﻿using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class AssetsController : Controller
    {
        #region Fields

        private readonly IAssetService _assertService;

        #endregion

        #region Ctor

        public AssetsController(IAssetService assertService)
        {
            _assertService = assertService;
        }

        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var assets = await _assertService.FindAll().AsNoTracking()
                            .ToListAsync();
            return View(assets);
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
            Asset asset = _assertService.FindById((int)id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }
        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assertService.Create(asset);
                return RedirectToAction("Index");
            }

            return View(asset);
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
            Asset asset = _assertService.FindById((int)id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assertService.Edit(asset);
                return RedirectToAction("Index");
            }
            return View(asset);
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
            Asset asset = _assertService.FindById((int)id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        /// <summary>
        /// DeleteConfirmed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asset asset = _assertService.FindById(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            _assertService.Delete(asset);
            return RedirectToAction("Index");
        }
        #endregion
    }
}