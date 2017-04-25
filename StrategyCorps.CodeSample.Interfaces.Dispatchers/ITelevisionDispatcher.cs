using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Dispatchers
{
    public interface ITelevisionDispatcher
    {
        TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query);
    }
}