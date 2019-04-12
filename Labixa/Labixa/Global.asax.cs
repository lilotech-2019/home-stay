using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Outsourcing.Data;
using Outsourcing.Data.Migrations;
//using FluentValidation.Mvc;
namespace Labixa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

           // System.Data.Entity.Database.SetInitializer(new OutsourcingSampleData());//
            //System.Data.Entity.Database.SetInitializer<OutsourcingEntities>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Run();
            // Auto update db to latest.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(true);
            }
            //FluentValidationModelValidatorProvider.Configure();//cấu hình cho fluent validation
        }
    }
}
