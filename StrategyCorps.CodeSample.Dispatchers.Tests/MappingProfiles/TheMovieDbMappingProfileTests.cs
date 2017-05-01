using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using NUnit.Framework;
using StrategyCorps.CodeSample.Dispatchers.MappingProfiles;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.Tests.MappingProfiles
{
    [TestFixture]
    public class TheMovieDbMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<TheMovieDbMappingProfile>(); });
            _mapper = config.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            _mapper = null;
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionSearchResponse_Returns_TelevisionSearchResponseDTO()
        {
            var televisionResults = Builder<TelevisionResult>.CreateListOfSize(5).Build().ToList();
            var televisionSearchResponse = Builder<TelevisionSearchResponse>.CreateNew()
                                                                                  .With(x => x.Results = televisionResults).Build();

            var televisionResultsDTO = televisionResults.Select(televisionResult => new TelevisionResultDTO
            {
                FirstAirDate = televisionResult.FirstAirDate,
                Id = televisionResult.Id,
                Name = televisionResult.Name,
                OriginalLanguage = televisionResult.OriginalLanguage,
                OriginalName = televisionResult.OriginalName,
                Overview = televisionResult.Overview,
                Popularity = televisionResult.Popularity,
                VoteAverage = televisionResult.VoteAverage,
                VoteCount = televisionResult.VoteCount
            }).ToList();

            var expectedResult = Builder<TelevisionSearchResponseDTO>.CreateNew()
                                                                           .With(x => x.Page = televisionSearchResponse.Page)
                                                                           .With(x => x.TotalPages = televisionSearchResponse.TotalPages)
                                                                           .With(x => x.TotalResults = televisionSearchResponse.TotalResults)
                                                                           .With(x => x.Results = televisionResultsDTO).Build();

            var actualResult = _mapper.Map<TelevisionSearchResponse, TelevisionSearchResponseDTO>(televisionSearchResponse);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionResult_Returns_TelevisionResultDTO()
        {
            var televisionResult = Builder<TelevisionResult>.CreateNew().Build();
            var expectedResult = Builder<TelevisionResultDTO>.CreateNew()
                                                                   .With(x => x.OriginalLanguage = televisionResult.OriginalLanguage)
                                                                   .With(x => x.FirstAirDate = televisionResult.FirstAirDate)
                                                                   .With(x => x.Id = televisionResult.Id)
                                                                   .With(x => x.Name = televisionResult.Name)
                                                                   .With(x => x.OriginalName = televisionResult.OriginalName)
                                                                   .With(x => x.Overview = televisionResult.Overview)
                                                                   .With(x => x.Popularity = televisionResult.Popularity)
                                                                   .With(x => x.VoteAverage = televisionResult.VoteAverage)
                                                                   .With(x => x.VoteCount = televisionResult.VoteCount).Build();

            var actualResult = _mapper.Map<TelevisionResult, TelevisionResultDTO>(televisionResult);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

    }
}
