using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HAL;
using MIP.Interfaces;
using MIPLIB.EndPoints.Output;
using MIPLIB.States;
using NCD;
using MIPLIB;
using MIP;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using SwitchAttribute = HAL.SwitchAttribute;

namespace NinjectionModules
{
// ReSharper disable UnusedMember.Global
    public class NCDHardwareModule : NinjectModule
// ReSharper restore UnusedMember.Global
    {
        public override void Load()
        {
            Bind<IHardwareController>().To<NCDController>();
            Bind<IEndpointStateMapper>().To<SwitchDimmerStateMapper>().WhenTargetHas<DimmerAttribute>();
            Bind<IEndpointStateMapper>().To<SwitchStateMapper>().WhenTargetHas<SwitchAttribute>();
            Bind<IControlMessage>().To<NCDControllMessage>();
            Bind<IEndpointState>().To<On>().WhenInjectedInto(typeof(Light));
            Bind<IEndpointState>().To<Off>().WhenInjectedInto (typeof (Light));
            Bind<IEndpointState>().To<Low>().WhenInjectedInto (typeof (IthoVentilator));
            Bind<IEndpointState>().To<Medium>().WhenInjectedInto (typeof (IthoVentilator));
            Bind<IEndpointState>().To<High>().WhenInjectedInto (typeof (IthoVentilator));
            Bind<IEndpointState>().To<Off>().WhenInjectedInto (typeof (IthoVentilator));

        }
    }
}
