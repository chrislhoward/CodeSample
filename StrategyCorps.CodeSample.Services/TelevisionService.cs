using NLog;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services
{
    public class TelevisionService : ITelevisionService
    {
        private readonly IEntertainmentDispatcher _entertainmentDispatcher;
        private readonly ILogger _logger;
        
        public TelevisionService(IEntertainmentDispatcher entertainmentDispatcher, ILogger logger)
        {
            _entertainmentDispatcher = entertainmentDispatcher;
            _logger = logger;
        }

        public TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query)
        {
            return _entertainmentDispatcher.GetTelevisionShowsByQuery(query);
        }

        public TelevisionSearchResponseDTO GetSimilarTelevisionShowsById(int id)
        {
            return _entertainmentDispatcher.GetSimilarTelevisionShowsById(id);
        }
    }
}
