using System.Collections.Generic;

namespace StrategyCorps.CodeSample.WebApi.ViewModels
{
    /// <summary>
    /// The response from the movie or television alternative titles request
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AlternativeTitlesResponseViewModel
    {
        /// <summary>
        /// The results of the television search request
        /// </summary>
        public IList<TitleResultViewModel> Titles { get; set; }

        /// <summary>
        /// The Id of the movie or television show
        /// </summary>
        public int Id { get; set; }
    }
}
