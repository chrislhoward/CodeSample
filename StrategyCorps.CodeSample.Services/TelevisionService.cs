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

        /// <summary>
        /// Gets television shows that meet the query criteria
        /// </summary>
        /// <param name="query">The criteria used to search for television shows</param>
        /// <returns cref="TelevisionSearchResponseDto"></returns>
        public TelevisionSearchResponseDto GetTelevisionShowsByQuery(string query)
        {
            return _entertainmentDispatcher.GetTelevisionShowsByQuery(query);
        }

        /// <summary>
        /// Gets television shows that are similar to the television show whose id is passed in.
        /// </summary>
        /// <param name="id">The id of the television show used to find similar television shows.</param>
        /// <returns cref="TelevisionSearchResponseDto"></returns>
        public TelevisionSearchResponseDto GetSimilarTelevisionShowsById(int id)
        {
            return _entertainmentDispatcher.GetSimilarTelevisionShowsById(id);
        }

        /// <summary>
        /// Gets alternative titles for the movie whose id is passed in.
        /// </summary>
        /// <param name="id">The id of the movie used to retrieve alternative titles.</param>
        /// <returns cref="AlternativeTitlesResponseDto"></returns>
        public AlternativeTitlesResponseDto GetAlternativeMovieTitlesById(int id)
        {
            return _entertainmentDispatcher.GetAlternativeMovieTitlesById(id);
        }
    }
}
