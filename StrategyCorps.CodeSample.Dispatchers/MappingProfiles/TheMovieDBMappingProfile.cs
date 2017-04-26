using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.MappingProfiles
{
    public class TheMovieDbMappingProfile:Profile
    {
        public TheMovieDbMappingProfile()
        {
            CreateMap<TelevisionResult, TelevisionResultDTO>();
            CreateMap<TelevisionSearchResponse, TelevisionSearchResponseDTO>();
        }
    }
}
