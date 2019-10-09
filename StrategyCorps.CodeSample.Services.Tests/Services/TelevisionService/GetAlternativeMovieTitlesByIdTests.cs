using System;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services.Tests.Services.TelevisionService
{
    [TestFixture]
    public class GetAlternativeMovieTitleByIdTests
    {
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
        }

        [Test]
        public void GetAlternativeMovieTitlesById_When_TheMovieDbDispatcherThrowsException_Throws_Exception()
        {
            _entertainmentDispatcherMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Throws<Exception>();
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object);

            Assert.Catch<Exception>(() => televisionService.GetAlternativeMovieTitlesById(0));
        }

        [Test]
        public void GetAlternativeMovieTitlesById_When_TheMovieDbDispatcherThrowsStrategyCorpsException_Throws_Exception()
        {
            var expectedException = Builder<StrategyCorpsException>.CreateNew()
                .With(x => x.StrategyCorpsErrorCode = ErrorCode.Default).Build();
            _entertainmentDispatcherMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Throws(expectedException);
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object);

            var actualException = Assert.Catch<Exception>(() => televisionService.GetAlternativeMovieTitlesById(0));

            actualException.ToExpectedObject().ShouldEqual(expectedException);
        }

        [Test]
        public void GetAlternativeMovieTitlesById_When_Successful_Returns_TelevisionSearchResponseDTO()
        {
            var expectedResult = Builder<AlternativeTitlesResponseDto>.CreateNew().Build();
            _entertainmentDispatcherMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Returns(expectedResult);
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object);
            var actualResult = televisionService.GetAlternativeMovieTitlesById(0);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}