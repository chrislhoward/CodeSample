using System.Collections.Generic;
using Newtonsoft.Json;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model
{
    // JSON structure of Movie and TV alternative title results is identical
    public class AlternativeTitlesResponse
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("titles", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TitleResult> Titles { get; set; }
    }
}
