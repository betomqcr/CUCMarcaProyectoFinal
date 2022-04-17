using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CUCMarca.WebSite.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery/jquery-3.3.1.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/defaultjslibraries").Include(
                     "~/Scripts/jquery-3.3.1.min.js",
                     "~/Scripts/popper.min.js",
                     "~/Scripts/bootstrap.bundle.min.js",
                     "~/vendor/jquery-easing/jquery.easing.min.js",
                     "~/Scripts/sb-admin-2.min.js"));
            bundles.Add(new StyleBundle("~/Content/defaultcss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/fontawesome-all.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/Styles.css"));
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/vendor/datatables/dataTables.bootstrap4.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/datatablesScript").Include(
                "~/vendor/datatables/jquery.dataTables.js",
                "~/vendor/datatables/dataTables.bootstrap4.js",
                "~/vendor/datatables-demo.js"
                ));
            //BundleTable.EnableOptimizations = true;
        }

    }
}