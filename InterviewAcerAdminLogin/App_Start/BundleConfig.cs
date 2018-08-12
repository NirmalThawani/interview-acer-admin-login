using System.Web;
using System.Web.Optimization;

namespace InterviewAcerAdminLogin
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
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsxlsx").Include("~/Scripts/xlsx.core.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/FileSaver").Include("~/Scripts/FileSaver.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tableexport").Include(
                     "~/Scripts/tableexport.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                      "~/Scripts/bootstrap-select.min.js"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/main.css",
                      "~/css/font-awesome.min.css",
                      //"~/css/bootstrap-select.min.css",
                      "~/css/tableexport.min.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-select").Include(
                  "~/css/bootstrap-select.min.css"));

            bundles.Add(new StyleBundle("~/bundles/datatablecss").Include(
         "~/css/dataTables.bootstrap.min.css",
         "~/css/buttons.dataTables.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/main").Include(
            "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
            "~/Scripts/jquery.dataTables.min.js",
            "~/Scripts/dataTables.bootstrap.min.js"));
        }
    }
}
