using System;
using System.Net;
using System.Web.Http;
using AutoMapper;
using NLog;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.ViewModels;
using Swashbuckle.Swagger.Annotations;

namespace StrategyCorps.CodeSample.WebApi.Controllers
{
    /// <summary>
    /// The television controller
    /// </summary>
    public class MovieController : ApiController
    {
        private const string InternalServerErrorDefaultMessage = "There was a problem processing the request, please try again later.";

        private readonly ITelevisionService _televisionService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// The television controller constructor
        /// </summary>
        /// <param name="televisionService" cref="ITelevisionService">The television service</param>
        /// <param name="logger" cref="ILogger">The NLog logger</param>
        /// <param name="mapper" cref="IMapper">The AutoMapper mapper</param>
        public MovieController(ITelevisionService televisionService, ILogger logger, IMapper mapper)
        {
            _televisionService = televisionService;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        ///     Get alternative movie titles
        /// </summary>
        /// <remarks>
        ///     Get alternative movie titles
        /// </remarks>
        /// <param name="id">Movie id</param>
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(TelevisionResultViewModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Movie id is required.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "The movie id {id} was not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage)]
        [HttpGet]
        [Route("api/movie/{id}/titles")]
        public IHttpActionResult AlternativeMovieTitles(int id)
        {
            if (id <= 0) return Content(HttpStatusCode.BadRequest, "Movie id is not correct");

            try
            {
                AlternativeTitlesResponseDto alternativeTitlesResponseDto = _televisionService.GetAlternativeMovieTitlesById(id);

                if (alternativeTitlesResponseDto == null) return Content(HttpStatusCode.NotFound, $"The movie id {id} was not found.");

                AlternativeTitlesResponseViewModel alternativeTitlesResponseseViewModel = _mapper.Map<AlternativeTitlesResponseDto, AlternativeTitlesResponseViewModel>(alternativeTitlesResponseDto);

                return Ok(alternativeTitlesResponseseViewModel);

            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return Content(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage);
            }
        }
    }
}
