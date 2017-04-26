using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Dispatchers
{
    public interface IEntertainmentDispatcher
    {
        TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query);

        TelevisionSearchResponseDTO GetSimilarTelevisionShowsById(int id);
    }
}