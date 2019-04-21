using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}