namespace StrategyCorps.CodeSample.WebApi.ViewModels
{
    /// <summary>
    ///  The result contained in the response of the television search request
    /// </summary>
    public class TitleResultViewModel
    {
        /// <summary>
        /// Title value
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Country code that the title was used in
        /// </summary>
        public string Iso_3166_1 { get; set; }

        /// <summary>
        /// Purpose of the title
        /// </summary>
        public string Type { get; set; }
    }
}
