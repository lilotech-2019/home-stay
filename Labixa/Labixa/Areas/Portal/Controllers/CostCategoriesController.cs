using Outsourcing.Core.Common;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class CostCategoriesController : Controller
    {
        #region Field
        private readonly ICostCategoriesService _costCategoriesService;
        #endregion

        #region Ctor
        public CostCategoriesController(ICostCategoriesService costCategoryService)
        {
            _costCategoriesService = costCategoryService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var costCategories = await _costCategoriesService.FindAll().AsNoTracking().ToListAsync();
            return View(costCategories);
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
            CostCategory costCategory = _costCategoriesService.FindById((int)id);
            if (costCategory == null)
            {
                return HttpNotFound();
            }
            return View(costCategory);
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
        /// <param name="costCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CostCategory costCategory)
        {
            if (ModelState.IsValid)
            {
                costCategory.Slug = StringConvert.ConvertShortName(costCategory.Slug);
                _costCategoriesService.Create(costCategory);
                return RedirectToAction("Index");
            }

            return View(costCategory);
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
            CostCategory costCategory = _costCategoriesService.FindById((int)id);
            if (costCategory == null)
            {
                return HttpNotFound();
            }
            return View(costCategory);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="costCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CostCategory costCategory)
        {
            if (ModelState.IsValid)
            {
                costCategory.Slug = StringConvert.ConvertShortName(costCategory.Slug);
                return RedirectToAction("Index");
            }
            return View(costCategory);
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
            var costCategory = _costCategoriesService.FindById((int)id);
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
            var costCategory = _costCategoriesService.FindById((int)id);
            if (costCategory == null)
            {
                return HttpNotFound();
            }

            _costCategoriesService.Delete(costCategory);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
