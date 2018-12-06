using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CarModificationCenter.App_Start;

namespace CarModificationCenter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);  //run FilterConfig.cs on startup.]
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
