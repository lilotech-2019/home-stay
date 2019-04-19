﻿using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.HMS;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class HotelsController : Controller
    {
        #region Fields
        private readonly IHotelService _hotelService;
        private readonly ICategoryHotelService _categoryHotelService;
        #endregion

        #region Ctor
        public HotelsController(IHotelService hotelService, ICategoryHotelService categoryHotelService)
        {
            _hotelService = hotelService;
            _categoryHotelService = categoryHotelService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var hotels = await _hotelService.FindAll().AsNoTracking().ToListAsync();
            return View(hotels);
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
            Hotel hotel = _hotelService.FindById((int)id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.CategoryHotelId = new SelectList(_categoryHotelService.FindAll(), "Id", "Name");
            return View(new Hotel { SharePercent = 0 });
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotel.Slug = StringConvert.ConvertShortName(hotel.Name);
                _hotelService.Create(hotel);
                return RedirectToAction("Index");
            }

            return View(hotel);
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
            Hotel hotel = _hotelService.FindById((int)id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryHotelId = _categoryHotelService.FindSelectList();
            return View(hotel);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotel.Slug = StringConvert.ConvertShortName(hotel.Name);
                _hotelService.Edit(hotel);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryHotelId = _categoryHotelService.FindSelectList();
            return View(hotel);
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
            var categoryHotels = _hotelService.FindById((int)id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        // POST: /HMSAdmin/Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hotels = _hotelService.FindById(id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            _hotelService.Delete(hotels);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
