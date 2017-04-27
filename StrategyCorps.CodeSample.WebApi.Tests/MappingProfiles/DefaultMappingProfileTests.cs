using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using NUnit.Framework;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.MappingProfiles;
using StrategyCorps.CodeSample.WebApi.ViewModels;

namespace StrategyCorps.CodeSample.WebApi.Tests.MappingProfiles
{
    [TestFixture]
    public class DefaultMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<DefaultMappingProfile>(); });
            _mapper = config.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            _mapper = null;
        }

        [Test]
        public void DefaultMappingProfile_When_TelevsionSearchResponseDTO_Returns_TelevisionSearchResponseViewModel()
        {
            var televisionResultsDTO = Builder<TelevisionResultDTO>.CreateListOfSize(5).Build().ToList();
            var televisionSearchResponseDTO = Builder<TelevisionSearchResponseDTO>.CreateNew()
                .With(x => x.Results = televisionResultsDTO).Build();

            var televisionResultsViewModel = televisionResultsDTO.Select(televisionResultDTO => new TelevisionResultViewModel
            {
                FirstAirDate = televisionResultDTO.FirstAirDate,
                Id = televisionResultDTO.Id,
                Name = televisionResultDTO.Name,
                OriginalLanguage = televisionResultDTO.OriginalLanguage,
                OriginalName = televisionResultDTO.OriginalName,
                Overview = televisionResultDTO.Overview,
                Popularity = televisionResultDTO.Popularity,
                VoteAverage = televisionResultDTO.VoteAverage,
                VoteCount = televisionResultDTO.VoteCount
            }).ToList();

            var expectedResult = Builder<TelevisionSearchResponseViewModel>.CreateNew()
                .With(x => x.Page = televisionSearchResponseDTO.Page)
                .With(x => x.TotalPages = televisionSearchResponseDTO.TotalPages)
                .With(x => x.TotalResults = televisionSearchResponseDTO.TotalResults)
                .With(x => x.Results = televisionResultsViewModel);

            var actualResult = _mapper.Map<TelevisionSearchResponseDTO, TelevisionSearchResponseViewModel>(televisionSearchResponseDTO);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }

        [Test]
        public void DefaultMappingProfile_When_TelevisionResultDTO_Returns_TelevisionResultViewModel()
        {
            var televisionResultDTO = Builder<TelevisionResultDTO>.CreateNew().Build();
            var expectedResult = Builder<TelevisionResultViewModel>.CreateNew()
                .With(x => x.OriginalLanguage = televisionResultDTO.OriginalLanguage)
                .With(x => x.FirstAirDate = televisionResultDTO.FirstAirDate)
                .With(x => x.Id = televisionResultDTO.Id)
                .With(x => x.Name = televisionResultDTO.Name)
                .With(x => x.OriginalName = televisionResultDTO.OriginalName)
                .With(x => x.Overview = televisionResultDTO.Overview)
                .With(x => x.Popularity = televisionResultDTO.Popularity)
                .With(x => x.VoteAverage = televisionResultDTO.VoteAverage)
                .With(x => x.VoteCount = televisionResultDTO.VoteCount).Build();

            var actualResult = _mapper.Map<TelevisionResultDTO, TelevisionResultViewModel>(televisionResultDTO);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}
