using System.Web.Optimization;

namespace DateCalculator
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/DateCalculator.css",
              "~/Content/bootstrap.css",
              "~/Content/jQueryUI-1.10.1.css"
              ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-1.9.1.js",
                             "~/Scripts/jQueryUI-1.10.1.js",
                             "~/Scripts/datecalc.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}