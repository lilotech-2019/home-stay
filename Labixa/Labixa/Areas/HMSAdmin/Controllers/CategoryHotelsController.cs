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
    public class CategoryHotelsController : Controller
    {
        #region Fields
        private readonly ICategoryHotelService _categoryHotelService;
        #endregion

        #region Ctor
        public CategoryHotelsController(ICategoryHotelService categoryHotelService)
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
            CategoryHotels categoryHotels = _categoryHotelService.FindById((int)id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new CategoryHotels { SharePercent = 0 });
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="categoryHotels"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryHotels categoryHotels)
        {
            if (ModelState.IsValid)
            {
                categoryHotels.Slug = StringConvert.ConvertShortName(categoryHotels.Name);
                _categoryHotelService.Create(categoryHotels);
                return RedirectToAction("Index");
            }

            return View(categoryHotels);
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
            CategoryHotels categoryHotels = _categoryHotelService.FindById((int)id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="categoryHotels"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryHotels categoryHotels)
        {
            if (ModelState.IsValid)
            {
                categoryHotels.Slug = StringConvert.ConvertShortName(categoryHotels.Name);
                _categoryHotelService.Edit(categoryHotels);
                return RedirectToAction("Index");
            }
            return View(categoryHotels);
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
            var categoryHotels = _categoryHotelService.FindById((int)id);
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
    }
}
