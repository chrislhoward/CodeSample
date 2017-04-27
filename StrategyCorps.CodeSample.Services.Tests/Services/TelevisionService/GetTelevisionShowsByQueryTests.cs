using System;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NLog;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services.Tests.Services.TelevisionService
{
    [TestFixture]
    public class GetTelevisionShowsByQueryTests
    {
        private Mock<ILogger> _loggerMock;
        private Mock<IEntertainmentDispatcher> _entertainmentDispatcherMock;
        
        [SetUp]
        public void SetUp()
        {
            _entertainmentDispatcherMock = new Mock<IEntertainmentDispatcher>();
        }

        [TearDown]
        public void TearDown()
        {
            _entertainmentDispatcherMock = null;
            _loggerMock = null;
        }

        [Test]
        public void GetTelevisionShowsByQuery_When_TheMovieDbDispatcherThrowsException_Throws_Exception()
        {
            _entertainmentDispatcherMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Throws<Exception>();
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object, _loggerMock.Object);

            Assert.Catch<Exception>(() => televisionService.GetTelevisionShowsByQuery(null));
        }

        [Test]
        public void GetTelevisionShowsByQuery_When_TheMovieDbDispatcherThrowsStrategyCorpsException_Throws_StrategyCorpsException()
        {
            var expectedException = Builder<StrategyCorpsException>.CreateNew()
                .With(x => x.StrategyCorpsErrorCode = ErrorCode.Default).Build();
            _entertainmentDispatcherMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Throws(expectedException);
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object, _loggerMock.Object);

            var actualException = Assert.Catch<Exception>(() => televisionService.GetTelevisionShowsByQuery(null));

            actualException.ToExpectedObject().ShouldEqual(expectedException);
        }

        [Test]
        public void GetTelevisionShowsByQuery_When_Successful_Returns_TelevisionSearchResponseDTO()
        {
            var expectedResult = Builder<TelevisionSearchResponseDTO>.CreateNew().Build();
            _entertainmentDispatcherMock.Setup(x => x.GetTelevisionShowsByQuery(It.IsAny<string>())).Returns(expectedResult);
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object, _loggerMock.Object);
            var actualResult = televisionService.GetTelevisionShowsByQuery(null);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}