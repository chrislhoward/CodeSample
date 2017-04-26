using System;
using System.Net;
using AutoMapper;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB
{
    public class TheMovieDbDispatcher : TheMovieDbDispatcherBase, IEntertainmentDispatcher
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TheMovieDbDispatcher(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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
                var restClient = new RestClient { BaseUrl = new Uri(TheMovieDbBaseUrl) };

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
                    return _mapper.Map<TelevisionSearchResponse, TelevisionSearchResponseDTO>(televisionSearchResponse);
                case HttpStatusCode.NotFound:
                    throw new Exception("Requested Resource is not found");
                case HttpStatusCode.BadRequest:
                default:
                    _logger.Error("Problem calling The Movie Db");
                    throw new Exception("Problem calling The Movie Db");
            }
        }

        public TelevisionSearchResponseDTO GetSimilarTelevisionShowsById(int id)
        {
            if (id <= 0) throw new Exception("id is not correct");

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"3/tv/{id}/similar?api_key={TheMovieDBApiKey}",
                RequestFormat = DataFormat.Json
            };

            try
            {
                var restClient = new RestClient { BaseUrl = new Uri(TheMovieDbBaseUrl) };

                var response = restClient.Execute(request);
                return MapGetTelevisionShowsResponse(response);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }
    }
}