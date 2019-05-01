using System.Web.Mvc;

namespace Labixa.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Portal";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
                "Portal_default_2",
                "Portal",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "",
                "Portal/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}