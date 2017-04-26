using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Services
{
    public interface ITelevisionService
    {
        TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query);

        TelevisionSearchResponseDTO GetSimilarTelevisionShowsById(int id);
    }
}
