using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Service;
using PagedList;
using Labixa.ViewModels;
using Labixa.Helpers;


namespace Labixa.Controllers
{
    public class NewsController : BaseHomeController
    {
        private readonly IBlogService _blogService;

        public NewsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public ActionResult Index(int? page = 1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            BlogViewModel viewModel = new BlogViewModel
            {
                RelatedBlogs = _blogService.FindAll().Take(5).OrderByDescending(w => w.Id)
            };
            var model = _blogService.FindAll().AsEnumerable().OrderBy(q => q.Status);
            viewModel.ListBlogs = model.ToPagedList(pageNumber, pageSize);
            return View(viewModel);
        }

        public ActionResult Detail(string slug)
        {
            var viewModel = new BlogViewModel
            {
                RelatedBlogs = _blogService.FindAll(),
                listBlogNew = _blogService.FindBySlug(slug)
            };
            return View(viewModel);
        }



        #region[Multi Language]

        public ActionResult SetCulture(string slug)
        {
            slug = CultureHelper.GetImplementedCulture(slug);
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = slug;
            else
            {
                cookie = new HttpCookie("_culture")
                {
                    Value = slug,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}