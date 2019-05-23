using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StrategyCorps.CodeSample.WebApi
{
#pragma warning disable 1591
    public class WebApiApplication : System.Web.HttpApplication
#pragma warning restore 1591
    {
#pragma warning disable 1591
        protected void Application_Start()
#pragma warning restore 1591
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
