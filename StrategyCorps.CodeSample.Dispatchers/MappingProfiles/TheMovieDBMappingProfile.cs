using System;
using System.Globalization;
using AutoMapper;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.MappingProfiles
{
    public class TheMovieDbMappingProfile:Profile
    {
        public TheMovieDbMappingProfile()
        {
            CreateMap<TelevisionResult, TelevisionResultDto>()
                .ForMember(destination => destination.FirstAirDate, options => options.MapFrom(source => TryParseDateTime(source.FirstAirDate)));
            CreateMap<TelevisionSearchResponse, TelevisionSearchResponseDto>();
            CreateMap<AlternativeTitlesResponse, AlternativeTitlesResponseDto>();
            CreateMap<TitleResult, TitleResultDto>();
        }

        /// <summary>
        /// Attempts to parse the string to a DateTime, if not parsable (e.g. empty string) returns DateTime.MinValue.
        /// Added because empty dates were causing application to fail.
        /// </summary>
        /// <param name="s">String to be parsed</param>
        /// <returns>DateTime for string or MinValue if not parsable</returns>
        private DateTime TryParseDateTime(string s)
        {
            if (DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime dt))
                return dt;
            else
                return DateTime.MinValue;
        }
    }
}
