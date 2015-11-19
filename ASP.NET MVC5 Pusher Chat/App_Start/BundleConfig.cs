using System.Web.Optimization;

namespace ASP.NET_MVC5_Pusher_Chat
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js"));



            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/site.css"));




            BundleTable.EnableOptimizations = true;
        }
    }
}
