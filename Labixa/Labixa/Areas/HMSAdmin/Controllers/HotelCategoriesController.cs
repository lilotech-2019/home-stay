using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.HMS;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class HotelCategoriesController : Controller
    {
        #region Fields

        private readonly ICategoryHotelService _categoryHotelService;

        #endregion

        #region Ctor

        public HotelCategoriesController(ICategoryHotelService categoryHotelService)
        {
            _categoryHotelService = categoryHotelService;
        }

        #endregion

        #region Index

        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryHotelService.FindAll().AsNoTracking().ToListAsync();
            return View(categories);
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
            HotelCategory hotelCategory = _categoryHotelService.FindById((int) id);
            if (hotelCategory == null)
            {
                return HttpNotFound();
            }
            return View(hotelCategory);
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

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="hotelCategory"></param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelCategory hotelCategory)
        {
            if (ModelState.IsValid)
            {
                hotelCategory.Slug = StringConvert.ConvertShortName(hotelCategory.Name);
                _categoryHotelService.Create(hotelCategory);
                return RedirectToAction("Index");
            }

            return View(hotelCategory);
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
            HotelCategory hotelCategory = _categoryHotelService.FindById((int) id);
            if (hotelCategory == null)
            {
                return HttpNotFound();
            }
            return View(hotelCategory);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="hotelCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HotelCategory hotelCategory)
        {
            if (ModelState.IsValid)
            {
                hotelCategory.Slug = StringConvert.ConvertShortName(hotelCategory.Name);
                _categoryHotelService.Edit(hotelCategory);
                return RedirectToAction("Index");
            }
            return View(hotelCategory);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoryHotels = _categoryHotelService.FindById((int) id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var categoryHotels = _categoryHotelService.FindById(id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            _categoryHotelService.Delete(categoryHotels);
            return RedirectToAction("Index");
        }

        #endregion

        #region MyRegion

        #endregion
    }
}