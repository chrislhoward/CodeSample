using NLog;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services
{
    public class TelevisionService : ITelevisionService
    {
        private readonly ITelevisionDispatcher _televisionDispatcher;
        private readonly ILogger _logger;
        
        public TelevisionService(ITelevisionDispatcher televisionDispatcher, ILogger logger)
        {
            _televisionDispatcher = televisionDispatcher;
            _logger = logger;
        }

        public TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query)
        {
            return _televisionDispatcher.GetTelevisionShowsByQuery(query);
        }
    }
}
