using Outsourcing.Core.Common;
using Outsourcing.Data;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class BlogCategoriesController : Controller
    {
        #region Fields
        private readonly IBlogCategoryService _blogCategoriesService;
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        #endregion

        #region Ctor
        public BlogCategoriesController(IBlogCategoryService blogCategoriesService)
        {
            _blogCategoriesService = blogCategoriesService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var blogCategories = await _blogCategoriesService.FindAll().AsNoTracking().ToListAsync();
            return View(blogCategories);
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
            BlogCategories details = _blogCategoriesService.FindById((int)id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.CategoryParentId = new SelectList(_db.BlogCategories, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="blogCategories"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogCategories blogCategories)
        {
            if (ModelState.IsValid)
            {
                blogCategories.Slug = StringConvert.ConvertShortName(blogCategories.Name);
                _blogCategoriesService.Create(blogCategories);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryParentId = new SelectList(_db.BlogCategories, "Id", "Name", blogCategories.CategoryParentId);
            return View(blogCategories);
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
            BlogCategories hotels = _blogCategoriesService.FindById((int)id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryParentId = new SelectList(_db.BlogCategories, "Id", "Name");
            return View(hotels);
        }
        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="blogCategories"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogCategories blogCategories)
        {
            if (ModelState.IsValid)
            {
                blogCategories.Slug = StringConvert.ConvertShortName(blogCategories.Name);
                _blogCategoriesService.Edit(blogCategories);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryParentId = new SelectList(_db.BlogCategories, "Id", "Name");
            return View(blogCategories);
        }
        #endregion

        #region
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
            var categoryHotels = _blogCategoriesService.FindById((int)id);
            if (categoryHotels == null)
            {
                return HttpNotFound();
            }
            return View(categoryHotels);
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
            var blogCategories = _blogCategoriesService.FindById(id);
            if (blogCategories == null)
            {
                return HttpNotFound();
            }
            _blogCategoriesService.Delete(blogCategories);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
