using System.Web.Http;

namespace StrategyCorps.CodeSample.WebApi
{
#pragma warning disable 1591
    public static class WebApiConfig
#pragma warning restore 1591
    {
#pragma warning disable 1591
        public static void Register(HttpConfiguration config)
#pragma warning restore 1591
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
