// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using NLog;
using StrategyCorps.CodeSample.Interfaces.Core;
using StructureMap;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace StrategyCorps.CodeSample.WebApi.DependencyResolution
{
#pragma warning disable 1591
    public class DefaultWebApiRegistry : Registry {
#pragma warning restore 1591
        #region Constructors and Destructors

#pragma warning disable 1591
        public DefaultWebApiRegistry() {
#pragma warning restore 1591
            Scan(
                scan => {
                    scan.WithDefaultConventions();
                    scan.AssembliesFromApplicationBaseDirectory(x => x.FullName.Contains("StrategyCorps"));

                    //this provides bootstrap into the other assemblies that want or need a StartUp.cs
                    scan.AddAllTypesOf<IStartUp>();
                });
            For<ILogger>().Use(logger => LogManager.GetCurrentClassLogger());
            ConfigureAutoMapper();
        }

        #endregion

        private void ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new List<string>{"StrategyCorps.CodeSample.WebApi", "StrategyCorps.CodeSample.Dispatchers", "StrategyCorps.CodeSample.Repositories"});
            });

            //Create a mapper that will be used by the DI container
            var mapper = config.CreateMapper();

            //Register the DI interfaces with their implementation
            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(mapper);
        }

#pragma warning disable 1591
        public void ScanTypes(TypeSet types, Registry registry)
#pragma warning restore 1591
        {
            foreach (var type in types.AllTypes())
            {
                if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
                {
                    registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
                }
            }
        }
    }
}