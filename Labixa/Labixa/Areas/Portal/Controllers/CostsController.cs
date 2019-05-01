using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class CostsController : Controller
    {
        #region Field
        private readonly ICostsService _costsnewService;
        private readonly ICostCategoriesService _costCategoryService;
        #endregion

        #region Ctor
        public CostsController(ICostsService costsnewService, ICostCategoriesService costCategoryService)
        {
            _costsnewService = costsnewService;
            _costCategoryService = costCategoryService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? costCategoryId)
        {
            var costs = _costsnewService.FindAll();
            if (costCategoryId != null)
            {
                costs = costs.Where(w => w.CostCategoryId == costCategoryId);
                ViewBag.CostCategoryId = costCategoryId;
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
            var cost = _costsnewService.FindById((int)id);
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
        public ActionResult Create(int? costCategoryId)
        {
            ViewBag.CostCategoryId = new SelectList(_costsnewService.FindSelectList(costCategoryId), "Id", "Name", costCategoryId);
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
                _costsnewService.Create(cost);
                return RedirectToAction("Index", new { costCategoryId = cost.CostCategoryId });
            }

            ViewBag.CostCategoryId = new SelectList(_costsnewService.FindSelectList(cost.Id), "Id", "Name", cost.Id);
            ViewBag.HotelId = new SelectList(_costsnewService.FindSelectList(cost.HotelId), "Id", "MetaKeywords", cost.HotelId);
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
            Cost cost = _costsnewService.FindById((int)id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostCategoryId = new SelectList(_costsnewService.FindSelectList(cost.CostCategoryId), "Id", "Name", cost.CostCategoryId);
            ViewBag.HotelId = new SelectList(_costsnewService.FindSelectList(cost.HotelId), "Id", "MetaKeywords", cost.HotelId);
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
                _costsnewService.Edit(cost);
                return RedirectToAction("Index", new { costCategoryId = cost.CostCategoryId });
            }
            ViewBag.CostCategoryId = new SelectList(_costsnewService.FindSelectList(cost.CostCategoryId), "Id", "Name", cost.CostCategoryId);
            ViewBag.HotelId = new SelectList(_costsnewService.FindSelectList(cost.HotelId), "Id", "MetaKeywords", cost.HotelId);
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
            var costCategory = _costsnewService.FindById((int)id);
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
            var cost = _costsnewService.FindById(id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            _costsnewService.Delete(cost);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
