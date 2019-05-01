using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class CostsController : Controller
    {
        #region Field
        private readonly ICostsService _costService;
        private readonly IHotelService _hotelService;
        private readonly ICostCategoriesService _costCategoryService;
        #endregion

        #region Ctor
        public CostsController(ICostsService costService, IHotelService hotelService, ICostCategoriesService costCategoryService)
        {
            _costService = costService;
            _hotelService = hotelService;
            _costCategoryService = costCategoryService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? hotelId, int? costCategoryId)
        {
            var costs = _costService.FindAll();
            if (costCategoryId != null)
            {
                costs = costs.Where(w => w.CostCategoryId == costCategoryId);
                ViewBag.CostCategoryId = costCategoryId;
            }
            if (hotelId != null)
            {
                costs = costs.Where(w => w.HotelId == hotelId);
            }
            costs = costs.AsNoTracking();
            return View(costs);
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
            var cost = _costService.FindById((int)id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            return View(cost);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int? hotelId, int? costCategoryId)
        {
            if (hotelId != null)
            {
                ViewBag.HotelId = new SelectList(_hotelService.FindSelectList((int)hotelId), "Id", "Name", hotelId);
            }
            else
            {
                ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(null), "Id", "Name");
            }

            if (costCategoryId != null)
            {
                ViewBag.CostCategoryId = new SelectList(_costCategoryService.FindSelectList(costCategoryId), "Id", "Name", costCategoryId);
            }
            else
            {
                ViewBag.CostCategoryId = new SelectList(_costCategoryService.FindSelectList(null), "Id", "Name");
            }
            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cost cost)
        {
            if (ModelState.IsValid)
            {
                _costService.Create(cost);
                return RedirectToAction("Index", new { costCategoryId = cost.CostCategoryId, hotelId = cost.HotelId });
            }

            ViewBag.CostCategoryId = new SelectList(_costCategoryService.FindSelectList(cost.CostCategoryId), "Id", "Name", cost.CostCategoryId);
            return View(cost);
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
            Cost cost = _costService.FindById((int)id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostCategoryId = new SelectList(_costCategoryService.FindSelectList(cost.CostCategoryId), "Id", "Name", cost.CostCategoryId);
            ViewBag.HotelId = new SelectList(_hotelService.FindSelectList(cost.HotelId), "Id", "Name", cost.HotelId);
            return View(cost);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cost cost)
        {
            if (ModelState.IsValid)
            {
                _costService.Edit(cost);
                return RedirectToAction("Index", new { costCategoryId = cost.CostCategoryId, hotelId = cost.HotelId });
            }
            ViewBag.CostCategoryId = new SelectList(_costCategoryService.FindSelectList(cost.CostCategoryId), "Id", "Name", cost.CostCategoryId);
            return View(cost);
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
            var costCategory = _costService.FindById((int)id);
            if (costCategory == null)
            {
                return HttpNotFound();
            }
            return View(costCategory);
        }

        /// <summary>
        /// Delete - POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cost = _costService.FindById(id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            _costService.Delete(cost);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
