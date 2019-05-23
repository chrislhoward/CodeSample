using System.Configuration;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB
{
    public class TheMovieDbDispatcherBase
    {
        protected static string TheMovieDbApiKey => ConfigurationManager.AppSettings.Get("TheMovieDbApiKey");
        protected static string TheMovieDbBaseUrl => ConfigurationManager.AppSettings.Get("TheMovieDbBaseUrl");
    }
}