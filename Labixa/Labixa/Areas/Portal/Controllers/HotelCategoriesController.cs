using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Labixa.Areas.Portal.ViewModels.HotelCategory;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;

namespace Labixa.Areas.Portal.Controllers
{
    [RouteArea("Portal")]
    [RoutePrefix("HotelCategories")]
    [Authorize]
    public class HotelCategoriesController : Controller
    {
        #region Fields

        private readonly IHotelCategoryService _hotelCategoryService;

        #endregion

        #region Ctor

        public HotelCategoriesController(IHotelCategoryService hotelCategoryService)
        {
            _hotelCategoryService = hotelCategoryService;
        }

        #endregion

        #region Index

        /// <summary>
        /// Index - GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var categories = await _hotelCategoryService.FindAll().AsNoTracking().ToListAsync();
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
            HotelCategory hotelCategory = _hotelCategoryService.FindById((int) id);
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
        [ValidateInput(false)]
        public ActionResult Create(HotelCategory hotelCategory)
        {
            if (ModelState.IsValid)
            {
                hotelCategory.Slug = StringConvert.ConvertShortName(hotelCategory.Name);
                _hotelCategoryService.Create(hotelCategory);
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
            HotelCategory hotelCategory = _hotelCategoryService.FindById((int) id);
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
        [ValidateInput(false)]
        public ActionResult Edit(HotelCategory hotelCategory)
        {
            if (ModelState.IsValid)
            {
                hotelCategory.Slug = StringConvert.ConvertShortName(hotelCategory.Name);
                _hotelCategoryService.Edit(hotelCategory);
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
            var categoryHotels = _hotelCategoryService.FindById((int) id);
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
            var categoryHotels = _hotelCategoryService.FindById(id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            _hotelCategoryService.Delete(categoryHotels);
            return RedirectToAction("Index");
        }

        #endregion

        #region MyRegion

        #endregion

        public ActionResult HotelCategorySubMenu()
        {
            var data = _hotelCategoryService.FindAll().AsNoTracking();
            var viewModel = new HotelCategorySubMenuViewModel
            {
                Count = data.Count(),
                HotelCategories = data
            };
            return PartialView("_HotelCategorySubMenu", viewModel);
        }
    }
}