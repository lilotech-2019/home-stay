using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;

namespace Labixa.Areas.Portal.Controllers
{
    public class BlogsController : Controller
    {
        #region Fields

        private readonly IBlogService _blogsService;
        private readonly IBlogCategoryService _blogCategoryService;

        #endregion


        #region Ctor

        public BlogsController(IBlogService blogsService, IBlogCategoryService blogCategoryService)
        {
            _blogsService = blogsService;
            _blogCategoryService = blogCategoryService;
        }

        #endregion

        #region Index

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var blogs = await _blogsService.FindAll().AsNoTracking().ToListAsync();
            return View(blogs);
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
            Blog blog = _blogsService.FindById((int)id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.BlogCategoryId = new SelectList(_blogCategoryService.FindSelectList(), "Id", "Name");
            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Slug = StringConvert.ConvertShortName(blog.Title);
                _blogsService.Create(blog);
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId = new SelectList(_blogCategoryService.FindSelectList(), "Id", "Name");
            return View(blog);
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
            var blog = _blogsService.FindById((int)id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogCategoryId =
                new SelectList(_blogCategoryService.FindSelectList(blog.BlogCategoryId), "Id", "Name", blog.BlogCategoryId);
            return View(blog);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Slug = StringConvert.ConvertShortName(blog.Title);
                _blogsService.Edit(blog);
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId =
                new SelectList(_blogCategoryService.FindSelectList(), "Id", "Name", blog.BlogCategoryId);

            return View(blog);
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
            var blogs = _blogsService.FindById((int)id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
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
            var blogs = _blogsService.FindById(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            _blogsService.Delete(blogs);
            return RedirectToAction("Index");
        }

        #endregion
    }
}