using System;
using System.Globalization;
using System.Linq;
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
            var currentDateTime = DateTime.Now;
            var televisionResults = Builder<TelevisionResult>.CreateListOfSize(5).All()
                .With(x => x.FirstAirDate = currentDateTime.ToString(CultureInfo.InvariantCulture)).Build().ToList();
            var televisionSearchResponse = Builder<TelevisionSearchResponse>.CreateNew()
                                                                                  .With(x => x.Results = televisionResults).Build();

            var televisionResultsDto = televisionResults.Select(televisionResult => new TelevisionResultDto
            {
                FirstAirDate = currentDateTime,
                Id = televisionResult.Id,
                Name = televisionResult.Name,
                OriginalLanguage = televisionResult.OriginalLanguage,
                OriginalName = televisionResult.OriginalName,
                Overview = televisionResult.Overview,
                Popularity = televisionResult.Popularity,
                VoteAverage = televisionResult.VoteAverage,
                VoteCount = televisionResult.VoteCount
            }).ToList();

            var expectedResult = Builder<TelevisionSearchResponseDto>.CreateNew()
                                                                           .With(x => x.Page = televisionSearchResponse.Page)
                                                                           .With(x => x.TotalPages = televisionSearchResponse.TotalPages)
                                                                           .With(x => x.TotalResults = televisionSearchResponse.TotalResults)
                                                                           .With(x => x.Results = televisionResultsDto).Build();

            var actualResult = _mapper.Map<TelevisionSearchResponse, TelevisionSearchResponseDto>(televisionSearchResponse);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionResult_Returns_TelevisionResultDTO()
        {
            var televisionResult = Builder<TelevisionResult>.CreateNew().With(x => x.FirstAirDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)).Build();
            var expectedResult = Builder<TelevisionResultDto>.CreateNew()
                                                                   .With(x => x.OriginalLanguage = televisionResult.OriginalLanguage)
                                                                   .With(x => x.FirstAirDate = DateTime.Parse(televisionResult.FirstAirDate))
                                                                   .With(x => x.Id = televisionResult.Id)
                                                                   .With(x => x.Name = televisionResult.Name)
                                                                   .With(x => x.OriginalName = televisionResult.OriginalName)
                                                                   .With(x => x.Overview = televisionResult.Overview)
                                                                   .With(x => x.Popularity = televisionResult.Popularity)
                                                                   .With(x => x.VoteAverage = televisionResult.VoteAverage)
                                                                   .With(x => x.VoteCount = televisionResult.VoteCount).Build();

            var actualResult = _mapper.Map<TelevisionResult, TelevisionResultDto>(televisionResult);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_AlternativeTitlesResponse_Returns_AlternativeTitlesResponseDTO()
        {

            var titleResults = Builder<TitleResult>.CreateListOfSize(5).All().Build().ToList();

            var alternativeTitlesResponse = Builder<AlternativeTitlesResponse>.CreateNew().With(x => x.Titles = titleResults).Build();

            var titleResultsDto = titleResults.Select(titleResult => new TitleResultDto
            {
                Title = titleResult.Title,
                Iso_3166_1 = titleResult.Iso_3166_1,
                Type = titleResult.Type
            }).ToList();

            var expectedResult = Builder<AlternativeTitlesResponseDto>.CreateNew()
                                                                   .With(x => x.Id = alternativeTitlesResponse.Id)
                                                                   .With(x => x.Titles = titleResultsDto)
                                                                   .Build();

            var actualResult = _mapper.Map<AlternativeTitlesResponse, AlternativeTitlesResponseDto>(alternativeTitlesResponse);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_TitleResult_Returns_TitleResultDTO()
        {
            var titleResult = Builder<TitleResult>.CreateNew().Build();
            var expectedResult = Builder<TitleResultDto>.CreateNew()
                                                                   .With(x => x.Iso_3166_1 = titleResult.Iso_3166_1)
                                                                   .With(x => x.Title = titleResult.Title)
                                                                   .With(x => x.Type = titleResult.Type)
                                                                   .Build();

            var actualResult = _mapper.Map<TitleResult, TitleResultDto>(titleResult);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}
