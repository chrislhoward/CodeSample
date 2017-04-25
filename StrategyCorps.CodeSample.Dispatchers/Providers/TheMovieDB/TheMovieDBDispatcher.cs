using System;
using System.Net;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB
{
    public class TheMovieDbDispatcher : TheMovieDbDispatcherBase, ITelevisionDispatcher
    {
        private readonly ILogger _logger;
       // private readonly IRestClient _restClient;

        public TheMovieDbDispatcher(ILogger logger)
        {
            _logger = logger;
            //_restClient = restClient;
        }

        public TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query)
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
                var restClient = new RestClient();
                restClient.BaseUrl = new Uri(TheMovieDbBaseUrl);

                var response = restClient.Execute(request);
                return MapGetTelevisionShowsResponse(response);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }

        private TelevisionSearchResponseDTO MapGetTelevisionShowsResponse(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var televisionSearchResponse  = JsonConvert.DeserializeObject<TelevisionSearchResponse>(response.Content);
                    //TODO : map to correct response;
                return new TelevisionSearchResponseDTO();
                    
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