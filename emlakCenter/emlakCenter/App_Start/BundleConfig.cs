using System.Web;
using System.Web.Optimization;

namespace emlakCenter
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryeval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/unob").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/site.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/adjs").Include(
                        "~/Scripts/adminJS.js"));

            bundles.Add(new ScriptBundle("~/bundles/date").Include(
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/Locales/bootstrap-datetimepicker.tr.js"
                        ));

            //admin js ++++

            bundles.Add(new ScriptBundle("~/bundles/admin/jquery").Include(
                        "~/Scripts/admin/jquery.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/admin/bootstrap").Include(
                        "~/Scripts/admin/bootstrap.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/admin/app").Include(
                        "~/Scripts/admin/app.js"
            ));
            //admin js ----


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datetimepicker.min.css"
                      ));


            // admin css ++++
            bundles.Add(new StyleBundle("~/Content/admin/css/bootstrap").Include(
                      "~/Content/admin/css/bootstrap.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/admin/css/font").Include(
                      "~/Content/admin/css/font-awesome.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/admin/css/ionicons").Include(
                      "~/Content/admin/css/ionicons.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/admin/css/AdminLTE").Include(
                      "~/Content/admin/css/AdminLTE.css"
                      ));

            // admin css ----

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
