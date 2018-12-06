using System.Web.Optimization;

namespace CarModificationCenter
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-cosmo.css",
                "~/Content/Site.css",
                "~/Content/font-awesome.css",
                "~/Content/jquery-ui.css",
                "~/Content/style.css"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/bootstrap.min.js",
                "~/Plugins/Isotope/isotope.pkgd.min.js",
                "~/Scripts/shop_custom.js",
                "~/Scripts/modernizr-2.6.2.js",
                "~/Scripts/JavaScript-Validator.js",
                "~/Scripts/site-responsive.js",
                "~/Scripts/minicart.js"
            ));
        }
    }
}