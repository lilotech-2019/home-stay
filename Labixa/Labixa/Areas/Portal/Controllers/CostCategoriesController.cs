using Labixa.Areas.Portal.ViewModels.CostCategory;
using Outsourcing.Core.Common;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Service;

namespace Labixa.Areas.Portal.Controllers
{
    [Authorize]
    public class CostCategoriesController : Controller
    {
        #region Field

        private readonly ICostCategoryService _costCategoryService;

        #endregion

        #region Ctor

        public CostCategoriesController(ICostCategoryService costCategoryService)
        {
            _costCategoryService = costCategoryService;
        }

        #endregion

        #region Index

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var costCategories = await _costCategoryService.FindAll().AsNoTracking().ToListAsync();
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
            CostCategory costCategory = _costCategoryService.FindById((int) id);
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
        [Authorize(Roles = Role.Admin)]
        public ActionResult Create()
        {
            ViewBag.CategoryParentId = new SelectList(_costCategoryService.FindSelectList(), "Id",
                "Name");
            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="costCategory"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CostCategory costCategory)
        {
            if (ModelState.IsValid)
            {
                costCategory.Slug = StringConvert.ConvertShortName(costCategory.Name);
                _costCategoryService.Create(costCategory);
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
        [Authorize(Roles = Role.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostCategory costCategory = _costCategoryService.FindById((int) id);
            ViewBag.CategoryParentId =
                new SelectList(_costCategoryService.FindSelectList(costCategory.CategoryParentId), "Id", "Name",
                    costCategory.CategoryParentId);
            return View(costCategory);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="costCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = Role.Admin)]
        public ActionResult Edit(CostCategory costCategory)
        {
            if (ModelState.IsValid)
            {
                costCategory.Slug = StringConvert.ConvertShortName(costCategory.Name);
                _costCategoryService.Edit(costCategory);
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
        [Authorize(Roles = Role.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var costCategory = _costCategoryService.FindById((int) id);
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
        [Authorize(Roles = Role.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var costCategory = _costCategoryService.FindById(id);
            if (costCategory == null)
            {
                return HttpNotFound();
            }

            _costCategoryService.Delete(costCategory);
            return RedirectToAction("Index");
        }

        #endregion

        public ActionResult CostCategorySubMenu()
        {
            var data = _costCategoryService.FindAll().AsNoTracking();
            var viewModel = new CostCategorySubMenuViewModel
            {
                Count = data.Count(),
                CostCategories = data
            };
            return PartialView("_CostCategorySubMenu", viewModel);
        }
    }
}