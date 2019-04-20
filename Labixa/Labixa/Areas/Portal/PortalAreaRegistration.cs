using System.Web.Mvc;

namespace Labixa.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Portal";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Portal_default",
                "Portal/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}