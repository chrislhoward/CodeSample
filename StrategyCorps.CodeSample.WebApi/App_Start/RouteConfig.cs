using System.Web.Mvc;
using System.Web.Routing;

namespace StrategyCorps.CodeSample.WebApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RouteConfig
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable 1591
        public static void RegisterRoutes(RouteCollection routes)
#pragma warning restore 1591
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
