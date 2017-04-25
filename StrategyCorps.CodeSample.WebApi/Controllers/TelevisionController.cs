using System;
using System.Net;
using System.Web.Http;
using NLog;
using AutoMapper;
using StrategyCorps.CodeSample.Interfaces.Services;
using Swashbuckle.Swagger.Annotations;

namespace StrategyCorps.SampleCode.WebApi.Controllers
{
    public class TelevisionController : ApiController
    {
        private const string InternalServerErrorDefaultMessage = "There has been a problem processing the request, please try again later.";

        private readonly ITelevisionService _televisionService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TelevisionController(ITelevisionService televisionService, ILogger logger, IMapper mapper)
        {
            _televisionService = televisionService;
            _logger = logger;
            _mapper = mapper;
        }

        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage)]
        [HttpGet]
        [Route("api/television/{query}")]
        public IHttpActionResult TelevisionSearchByQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Content(HttpStatusCode.BadRequest, "television search query is required");

            try
            {
                var televisionShows = _televisionService.GetTelevisionShowsByQuery(query);
                //TODO : check has shows and return correct error if not

                //TODO : Map
                return Ok(televisionShows);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return Content(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage);
            }
        }
    }
}
