using System.Web;
using System.Web.Optimization;

namespace GDHOTE.Hub.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/moment.js",
                       "~/Scripts/bootstrap-datepicker.js",
                       "~/Scripts/sweetalert.min.js",
                       "~/Scripts/jquery.unobtrusive-ajax.js",
                       "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/jquery-ui-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/sweetalert.min.css",
                      "~/Content/typeahead.css",
                      "~/Content/jquery-ui/jquery-ui.min.css",
                      "~/Content/site.css")); 
        }
    }
}
