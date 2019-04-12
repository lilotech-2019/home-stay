using System.Web.Mvc;
using Resources;

namespace Labixa.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Admin/Setting/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageMang().SetLanguage(lang);
            return RedirectToAction("Create", "Blog");
        }
    }
}