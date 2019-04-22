using Labixa.Helpers;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cultureName = "vi";
            // Validate culture name
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {                //cultureName = cultureCookie.Value;
                cultureName = "vi";
            }
            //else
            //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
            //            Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
            //            null;
            // Validate culture name
            CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }
}