using System.Web.Mvc;

namespace StrategyCorps.CodeSample.WebApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FilterConfig
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable 1591
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
#pragma warning restore 1591
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
