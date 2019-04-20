using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin
{
    public class HmsAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "HMSAdmin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HMSAdmin_default_2",
                "HMSAdmin",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "HMSAdmin_default",
                "HMSAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}