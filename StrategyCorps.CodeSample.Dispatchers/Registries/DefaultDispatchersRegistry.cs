using RestSharp;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StructureMap;

namespace StrategyCorps.CodeSample.Dispatchers.Registries
{
    public class DefaultDispatchersRegistry : Registry
    {
        public DefaultDispatchersRegistry()
        {
            For<ITelevisionDispatcher>().Use<TheMovieDbDispatcher>();
            For<IRestClient>().Use<RestClient>();
        }
    }
}