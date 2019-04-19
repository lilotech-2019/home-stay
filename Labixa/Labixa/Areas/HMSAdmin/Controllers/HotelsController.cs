using Outsourcing.Core.Common;
using Outsourcing.Data;
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
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        #endregion

        #region Ctor
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
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
            Hotels hotels = _hotelService.FindById((int)id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            return View(hotels);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.CategoryHotelId = new SelectList(_db.CategoryHotels, "Id", "Name");
            return View(new Hotels { SharePercent = 0 });
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                hotels.Slug = StringConvert.ConvertShortName(hotels.Name);
                _hotelService.Create(hotels);
                return RedirectToAction("Index");
            }

            return View(hotels);
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotels hotels = _hotelService.FindById((int)id);
            if (hotels == null)
            {
                return HttpNotFound();
            }

            return View(hotels);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                hotels.Slug = StringConvert.ConvertShortName(hotels.Name);
                _hotelService.Edit(hotels);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryHotelId = new SelectList(_db.CategoryHotels, "Id", "Name");
            return View(hotels);
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
