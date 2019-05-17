using System.Web.Optimization;

namespace Labixa
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Content/ckeditor/ckeditor.js",
                "~/Content/ckeditor/config.js"));

            bundles.Add(new StyleBundle("~/Content/css/").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));


            //CSS for  Ace admin
            bundles.Add(new StyleBundle("~/Style/Common").Include(
                "~/Content/admin/css/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/admin/css/ace.min.css",
                "~/Content/admin/css/ace-rtl.min.css",
                "~/Content/admin/css/ace-skins.min.css",
                "~/Content/admin/css/ace-fonts.css",
                "~/Content/admin/css/datepicker.css",
                "~/Content/admin/admincustom.css"
            ));

            //Scipts for  Ace admin
            bundles.Add(new ScriptBundle("~/Scripts/Common").Include(
                "~/Scripts/jquery-3.3.1.js",
                "~/Content/admin/js/bootstrap.min.js",
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Content/admin/js/date-time/bootstrap-datepicker.min.js",
                "~/Content/admin/js/ace-elements.min.js",
                "~/Content/admin/js/ace.min.js",
                "~/Content/admin/js/ace-extra.min.js",
                "~/Content/admin/vpn/adminvpn.js"
            ));
        }
    }
}