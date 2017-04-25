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

namespace StrategyCorps.SampleCode.WebApi.DependencyResolution
{
    using AutoMapper;
    using NLog;
    using StrategyCorps.CodeSample.Interfaces.Core;
    using StructureMap;
    using StructureMap.Graph.Scanning;
    using StructureMap.Pipeline;
    using StructureMap.TypeRules;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
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
            //Get all Profiles
            var profiles = from t in typeof(DefaultRegistry).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            //For each Profile, include that profile in the MapperConfiguration
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            //Create a mapper that will be used by the DI container
            var mapper = config.CreateMapper();

            //Register the DI interfaces with their implementation
            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(mapper);
        }

        public void ScanTypes(TypeSet types, Registry registry)
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