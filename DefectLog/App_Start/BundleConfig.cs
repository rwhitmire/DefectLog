using System.Web.Optimization;

namespace DefectLog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/scripts/json2.js",
                "~/scripts/jquery-{version}.js",
                "~/scripts/underscore.js",
                "~/scripts/angular.js",
                "~/scripts/bootstrap.js",
                "~/scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app.js"));

            bundles.Add(new StyleBundle("~/content/css/css").Include(
                "~/content/css/bootstrap.css",
                "~/content/css/bootstrap-theme.css",
                "~/content/css/site.css",
                "~/content/css/bootstrap-responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));
        }
    }
}