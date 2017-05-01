using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services
{
    public class TelevisionService : ITelevisionService
    {
        private readonly IEntertainmentDispatcher _entertainmentDispatcher;
 
        public TelevisionService(IEntertainmentDispatcher entertainmentDispatcher)
        {
            _entertainmentDispatcher = entertainmentDispatcher;
        }

        public TelevisionSearchResponseDto GetTelevisionShowsByQuery(string query)
        {
            return _entertainmentDispatcher.GetTelevisionShowsByQuery(query);
        }

        public TelevisionSearchResponseDto GetSimilarTelevisionShowsById(int id)
        {
            return _entertainmentDispatcher.GetSimilarTelevisionShowsById(id);
        }
    }
}
