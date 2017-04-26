using AutoMapper;
using StrategyCorps.CodeSample.Dispatchers.MappingProfiles;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Dispatchers.Registries;
using StrategyCorps.CodeSample.Interfaces.Core;
using StrategyCorps.CodeSample.Models;
using StructureMap;

namespace StrategyCorps.CodeSample.Dispatchers
{
    public class StartUp:IStartUp
    {
        public void Execute(IContainer container)
        {
            container.Configure(c =>
            {
                c.AddRegistry<DefaultDispatchersRegistry>();
            });

            Mapper.Initialize(cfg => cfg.CreateMap<TelevisionSearchResponse, TelevisionSearchResponseDTO>());

            AutoMapperDispatcherConfiguration.Configure();
        }
    }
}
