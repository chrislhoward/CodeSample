using System;
using System.Globalization;
using AutoMapper;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.ViewModels;

namespace StrategyCorps.CodeSample.WebApi.MappingProfiles
{
    /// <summary>
    /// The WebApi default mapping profiles
    /// </summary>
    public class DefaultMappingProfile : Profile
    {
        /// <summary>
        /// The  WebApi default mapping profile constructor
        /// </summary>
        public DefaultMappingProfile()
        {
            CreateMap<TelevisionResultDto, TelevisionResultViewModel>()
                .ForMember(destination => destination.FirstAirDate, options => options.MapFrom(source => FormatDate(source.FirstAirDate)));
            CreateMap<TelevisionSearchResponseDto, TelevisionSearchResponseViewModel>();
            CreateMap<AlternativeTitlesResponseDto, AlternativeTitlesResponseViewModel>();
            CreateMap<TitleResultDto, TitleResultViewModel>();
        }

        /// <summary>
        /// Formats DateTime to string value. Converts DateTime.MinValue to empty string. 
        /// Added because empty dates were causing application to fail.
        /// </summary>
        /// <param name="dt">Date to be formated</param>
        /// <returns>Formated value of DateTime or empty string if MinValue</returns>
        private string FormatDate(DateTime dt)
        {
            if (dt != DateTime.MinValue)
                return dt.ToString(CultureInfo.InvariantCulture);
            else
                return $"";
        }
    }
}
