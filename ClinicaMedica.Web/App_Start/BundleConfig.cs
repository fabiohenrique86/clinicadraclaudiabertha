using System.Web.Optimization;

namespace ClinicaMedica.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery.min.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/easing.min.js",
                "~/Scripts/scrollreveal.min.js",
                "~/Scripts/jquery.magnific-popup.min.js",
                "~/Scripts/creative.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/blockui.min.js",
                "~/Scripts/jqueryui.min.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/mask.js",
                "~/Scripts/jquery.fullcalendar.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/jquery.ui.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/magnific.popup.css",
                      "~/Content/creative.min.css",
                      "~/Content/fullcalendar.min.css",
                      "~/Content/fullcalendar.print.css"));
        }
    }
}
