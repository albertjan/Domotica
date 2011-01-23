using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL;
using NCD;
using Ninject.Modules;
using Ninject.Planning.Bindings;

namespace NinjectionModules
{
// ReSharper disable UnusedMember.Global
    public class NCDHardwareModule : NinjectModule
// ReSharper restore UnusedMember.Global
    {
        public override void Load()
        {
            Bind<IHardwareController>().To<NCDController>();
            Bind<IEndpointStateMapper>().To<StateMappers>().WhenTargetHas<DimmerAttribute>();
            Bind<IEndpointStateMapper>().To<SwitchStateMapper>().WhenTargetHas<SwitchAttribute>();
        }
    }
}
