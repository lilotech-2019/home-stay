using System.Web.Mvc;
using Outsourcing.Service;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        #region Field
        IBlogService _blogService;
        #endregion

        public DashboardController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {
            var test = _blogService.GetBlogs();
            return View();
        }


	}
}