using StrategyCorps.CodeSample.Interfaces.Core;
using StructureMap;

namespace StrategyCorps.SampleCode.Dispatchers
{
    public class StartUp:IStartUp
    {
        public void Execute(IContainer container)
        {
            container.Configure(c =>
            {
                //add registry here
                //c.AddRegistry<DefaultDispatchersRegistry>();
            });

            //add mapping profile here
            //Mapper.AddProfile<MemberMappingProfile>();
        }
    }
}
