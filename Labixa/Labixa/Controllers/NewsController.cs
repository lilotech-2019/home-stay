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
        private readonly IBlogCategoryService _blogCategoryService;
        public NewsController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            this._blogCategoryService = blogCategoryService;
            this._blogService = blogService;
        }
        //
        // GET: /Blog/
        //public ActionResult Index()
        //{
        //    var news = _blogService.GetBlogs();
        //    return View(model: news);
        //}
        //int? page = 1:


        //Index(int? page = 1): mac dinh page la trang dau tien, neu user bam page o ngoai view thi no tu gan page =xxx
//noi chung ky moi nhanh dc, 
        public ActionResult Index(int? page = 1)
        {
            var model = _blogService.GetBlogs().ToList();//em chi can get het data
            int pageNumber = (page ?? 1);// day la so thu tu page
            int pageSize = 4;//day la so bai viet tren 1 page
                             //model.ToPagedList(pageNumber, pageSize): em lay model. ToPagedList(PageNumper, pagesize) la no tu phan trang cho em
                             // khởi tạo BlogView vừa mới tạo
            BlogViewModel viewModel = new BlogViewModel();
            viewModel.RelatedBlogs = _blogService.Get3BlogNewsNewest();
            viewModel.ListBlogs = model.ToPagedList(pageNumber, pageSize);

            return View(viewModel);//no truyen kieu du lieu la IPagedList<Blog>
        }
        public ActionResult Detail(string slug)
        {
            BlogViewModel viewModel = new BlogViewModel();
            viewModel.RelatedBlogs = _blogService.Get3BlogNewsNewest();
            viewModel.listBlogNew = _blogService.GetBlogByUrlName(slug);
            return View(viewModel);
        }

        public ActionResult Event()
        {
            return View();

        }
        public ActionResult ActitityNews()
        {
            return View();

        }
        #region[Multi Language]
        public ActionResult SetCulture(string slug)
        {
            // Validate input
            slug = CultureHelper.GetImplementedCulture(slug);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = slug;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = slug;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index","Home");
        }
        #endregion
    }
}