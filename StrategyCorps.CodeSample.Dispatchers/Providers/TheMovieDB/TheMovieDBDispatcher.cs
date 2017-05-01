using System;
using System.Net;
using AutoMapper;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.Core.Exceptions;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB
{
    public class TheMovieDbDispatcher : TheMovieDbDispatcherBase, IEntertainmentDispatcher
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRestClient _restClient;

        public TheMovieDbDispatcher(IRestClient restClient, ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _restClient = restClient;
        }

        public TelevisionSearchResponseDTO GetTelevisionShowsByQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException(nameof(query), "The search query is required.");

            var queryString = $"api_key={TheMovieDBApiKey}&query={query}";

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"3/search/tv?{queryString}",
                RequestFormat = DataFormat.Json
            };

            try
            {
                _restClient.BaseUrl = new Uri(TheMovieDbBaseUrl);

                var response = _restClient.Execute(request);
                return MapGetTelevisionShowsResponse(response);
            }
            catch (StrategyCorpsException strategyCorpsException)
            {
                _logger.Error(strategyCorpsException);
                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }

        public TelevisionSearchResponseDTO GetSimilarTelevisionShowsById(int id)
        {
            if (id <= 0) throw new ArgumentException("The id  must be greater than 0.", nameof(id));

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"3/tv/{id}/similar?api_key={TheMovieDBApiKey}",
                RequestFormat = DataFormat.Json
            };

            try
            {
                _restClient.BaseUrl = new Uri(TheMovieDbBaseUrl);

                var response = _restClient.Execute(request);
                return MapGetTelevisionShowsResponse(response);
            }
            catch (StrategyCorpsException strategyCorpsException)
            {
                _logger.Error(strategyCorpsException);
                throw;
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
                    var televisionSearchResponse = JsonConvert.DeserializeObject<TelevisionSearchResponse>(response.Content);
                    return _mapper.Map<TelevisionSearchResponse, TelevisionSearchResponseDTO>(televisionSearchResponse);
                case HttpStatusCode.NotFound:
                    throw new StrategyCorpsException("Requested Resource is not found");
                default:
                    throw new StrategyCorpsException("Problem calling The Movie Db");
            }
        }
    }
}