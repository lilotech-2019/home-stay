using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}