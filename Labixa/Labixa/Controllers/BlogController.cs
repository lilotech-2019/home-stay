using System.Web.Mvc;

namespace Labixa.Controllers
{
    public class BlogController : Controller
    {
       public ActionResult Index()
        {
            return View();
        }
	}
}