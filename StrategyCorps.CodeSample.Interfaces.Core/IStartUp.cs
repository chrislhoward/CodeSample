using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyCorps.CodeSample.Interfaces.Core
{
    public interface IStartUp
    {
        void Execute(IContainer container);
    }
}
