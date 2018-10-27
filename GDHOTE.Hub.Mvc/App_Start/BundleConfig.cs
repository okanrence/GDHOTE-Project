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
                        "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        //"~/Content/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",
                        //"~/Content/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",
                        //"~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                        //"~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                        //"~/Content/bower_components/moment/min/moment.min.js",
                        //"~/Content/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                        //"~/Content/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                        //"~/Content/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/dist/js/adminlte.min.js",
                        //"~/Content/dist/js/pages/dashboard.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/sweetalert.min.js",
                        //"~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/select2.full.js",
                        "~/Scripts/jquery-ui-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/Content/bower_components/Ionicons/css/ionicons.min.css",
                      //"~/Content/bower_components/morris.js/morris.css",
                      //"~/Content/bower_components/jvectormap/jquery-jvectormap.css",
                      //"~/Content/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      //"~/Content/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                      //"~/Content/bower_components/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                     "~/Content/sweetalert.min.css",
                      "~/Content/typeahead.css",
                      "~/Content/select2.css",
                      "~/Content/loader.css",
                       "~/Content/jquery-ui/jquery-ui.min.css",
                       "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/dist/css/skins/skin-blue.min.css"

                     /* "~/Content/site.css"*/)); 
        }
    }
}
