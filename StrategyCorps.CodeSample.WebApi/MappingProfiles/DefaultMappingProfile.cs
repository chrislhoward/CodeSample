using AutoMapper;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.ViewModels;

namespace StrategyCorps.CodeSample.WebApi.MappingProfiles
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<TelevisionResultDto, TelevisionResultViewModel>();
            CreateMap<TelevisionSearchResponseDto, TelevisionSearchResponseViewModel>();
        }
    }
}
