using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.Controllers;
using StrategyCorps.CodeSample.WebApi.Tests.Extensions;
using StrategyCorps.CodeSample.WebApi.ViewModels;
using ILogger = NLog.ILogger;

namespace StrategyCorps.CodeSample.WebApi.Tests.Controllers
{
    [TestFixture]
    public class TelevisionControllerTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void TelevisionSearchByQuery_When_QueryIsNullOrWhitespace_Returns_BadRequest(string query)
        {
            var televisionController = new TelevisionController(null, null, null);
            var actionResult = televisionController.TelevisionSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<HttpError>>();
            response.CheckForCorrectErrorResponseBody(HttpStatusCode.BadRequest,ErrorCode.Default);
        }

        [Test]
        [TestCase("Gotham")]
        public void TelevisionSearchByQuery_When_TelevisionServiceReturnsNull_Returns_InternalServerError(string query)
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();
            var televisionServiceMock = new Mock<ITelevisionService>(null, null);
            televisionServiceMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Returns((TelevisionSearchResponseDTO) null);
            var televisionController = new TelevisionController(televisionServiceMock.Object, logger.Object, null);
            var actionResult = televisionController.TelevisionSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<HttpError>>();
            response.CheckForCorrectErrorResponseBody(HttpStatusCode.InternalServerError,ErrorCode.Default);

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
        }

        [Test]
        [TestCase("Gotham")]
        public void TelevisionSearchByQuery_When_TelevisionServiceThrowsException_Returns_InternalServerError(string query)
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();
            var televisionServiceMock = new Mock<ITelevisionService>(null, null);
            televisionServiceMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Throws<Exception>();
            var televisionController = new TelevisionController(televisionServiceMock.Object, logger.Object, null);
            var actionResult = televisionController.TelevisionSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<HttpError>>();
            response.CheckForCorrectErrorResponseBody(HttpStatusCode.InternalServerError, ErrorCode.Default);

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        [TestCase("Gotham")]
        public void TelevisionSearchByQuery_When_TelevisionServiceThrowsStrategyCorpsException_Returns_InternalServerError(string query)
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();
            var televisionServiceMock = new Mock<ITelevisionService>(null, null);
            televisionServiceMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Throws<StrategyCorpsException>();
            var televisionController = new TelevisionController(televisionServiceMock.Object, logger.Object, null);
            var actionResult = televisionController.TelevisionSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<HttpError>>();
            response.CheckForCorrectErrorResponseBody(HttpStatusCode.InternalServerError, ErrorCode.Default);

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        [TestCase("Gotham")]
        public void TelevisionSearchByQuery_When_TelevisionServiceReturnsTelevisionSearchResponseDTO_Returns_Ok(string query)
        {
            var televisionResultViewModels = Builder<TelevisionResultViewModel>.CreateListOfSize(5).Build();
            var expectedResult = Builder<TelevisionSearchResponseViewModel>.CreateNew()
                .With(x => x.Results = televisionResultViewModels.ToList()).Build();
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<TelevisionSearchResponseDTO, TelevisionSearchResponseViewModel>(It.IsAny<TelevisionSearchResponseDTO>()))
                .Returns(expectedResult);

            var televisionServiceMock = new Mock<ITelevisionService>(null, null);
            televisionServiceMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Throws<StrategyCorpsException>();
            var televisionController = new TelevisionController(televisionServiceMock.Object, logger.Object, null);
            var actionResult = televisionController.TelevisionSearchByQuery(query);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<HttpError>>();
            response.CheckForCorrectErrorResponseBody(HttpStatusCode.InternalServerError, ErrorCode.Default);

            Assert.That(response.Content, Is.TypeOf<TelevisionSearchResponseViewModel>());

            response.Content.ToExpectedObject().ShouldEqual(expectedResult);

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
        }
    }
}
