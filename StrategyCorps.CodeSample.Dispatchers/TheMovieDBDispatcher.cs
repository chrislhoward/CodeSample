using System;
using System.Net;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;

namespace StrategyCorps.CodeSample.Dispatchers
{
    public class TheMovieDbDispatcher : TheMovieDbDispatcherBase, ITelevisionDispatcher
    {
        private readonly ILogger _logger;
        private readonly IRestClient _restClient;

        public TheMovieDbDispatcher(ILogger logger, IRestClient restClient)
        {
            _logger = logger;
            _restClient = restClient;
        }

        public string GetTelevisionShowsByQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) throw new Exception("search query is required");

            var queryString = $"api_key={TheMovieDBApiKey}&query={query}";

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"3/search/tv?{queryString}",
                RequestFormat = DataFormat.Json
            };

            try
            {
                var response = _restClient.Execute(request);
                return MapGetTelevisionShowsResponse(response);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }

        private string MapGetTelevisionShowsResponse(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<string>(response.Content);
                case HttpStatusCode.NotFound:
                    throw new Exception("Requested Resource is not found");
                case HttpStatusCode.BadRequest:
                default:
                    _logger.Error("Problem calling The Movie Db");
                    throw new Exception("Problem calling The Movie Db");
            }
        }
    }
}