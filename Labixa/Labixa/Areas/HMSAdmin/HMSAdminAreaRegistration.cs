using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin
{
    public class HMSAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HMSAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HMSAdmin_default",
                "HMSAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}