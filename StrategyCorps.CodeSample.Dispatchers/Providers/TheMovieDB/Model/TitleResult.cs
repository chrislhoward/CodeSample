using Newtonsoft.Json;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model
{
    // Definition is potentially used by multiple title results
    public class TitleResult
    {
        [JsonProperty("iso_3166_1", NullValueHandling = NullValueHandling.Ignore)]
        public string Iso_3166_1 { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
