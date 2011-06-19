 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HAL;
 using HAL.Endpoints;
 using MIP.Interfaces;
 using MIPLIB.EndPoints.Input;
 using MIPLIB.EndPoints.Output;
 using MIPLIB.Hubs;
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
        public override void Load ()
        {
            Bind<IHardwareController> ().To<NCDController> ().InSingletonScope();

            Bind<IHardwareEndpoint>().To<SwitchedEndpoint>();

            Bind<IEndPointCouplingInformation>().To<NCDEndPointCouplingInformation>();

            Bind<IHub>().To<SimpleStaticHub>().InSingletonScope();

            Bind<IEndpoint> ().To<Light> ().WhenInjectedInto(typeof (SimpleStaticHub));
            Bind<IEndpoint> ().To<PulseSwitch> ().WhenInjectedInto (typeof (SimpleStaticHub));
            //Bind<IEndpoint> ().To<Light> ().WhenInjectedInto (typeof (SimpleStaticHub));
            //Bind<IEndpoint> ().To<Light> ().WhenInjectedInto (typeof (SimpleStaticHub));


            Bind<IEndpointStateMapper> ().To<DimmedEndpointStateMapper> ().WhenTargetHas<DimmerAttribute> ();
            Bind<IEndpointStateMapper> ().To<SwitchedEndpointStateMapper> ().WhenTargetHas<SwitchAttribute> ();
            Bind<IEndpointStateMapper> ().To<GenericInputEndpointStateMapper> ().WhenTargetHas<InputAttribute> ();
            Bind<IEndpointStateMapper> ().To<IthoVentilatorStateMapper> ().WhenTargetHas<FourStateAttribute>();

            Bind<IControlMessage> ().To<NCDControllMessage> ();
            Bind<IHardwareEndpointIndentifier> ().To<NCDHardwareIdentifier>();

            Bind<IEndpointState> ().To<In> ().WhenInjectedInto (typeof (GenericInputEndpoint));
            Bind<IEndpointState> ().To<Out> ().WhenInjectedInto (typeof (GenericInputEndpoint));

            Bind<IEndpointState> ().To<On> ().WhenInjectedInto (typeof (Light));
            Bind<IEndpointState> ().To<Off> ().WhenInjectedInto (typeof (Light));
            
            Bind<IEndpointState> ().To<Low> ().WhenInjectedInto (typeof (IthoVentilator));
            Bind<IEndpointState> ().To<Medium> ().WhenInjectedInto (typeof (IthoVentilator));
            Bind<IEndpointState> ().To<High> ().WhenInjectedInto (typeof (IthoVentilator));
            Bind<IEndpointState> ().To<Off> ().WhenInjectedInto (typeof (IthoVentilator));

            Bind<IEndpointState> ().To<In> ().WhenInjectedInto (typeof (GenericInputEndpointStateMapper));
            Bind<IEndpointState> ().To<Out> ().WhenInjectedInto (typeof (GenericInputEndpointStateMapper));

            Bind<IEndpointState> ().To<On> ().WhenInjectedInto (typeof (SwitchedEndpointStateMapper));
            Bind<IEndpointState> ().To<Off> ().WhenInjectedInto (typeof (SwitchedEndpointStateMapper));
            
            Bind<IEndpointState> ().To<Low> ().WhenInjectedInto (typeof (IthoVentilatorStateMapper));
            Bind<IEndpointState> ().To<Medium> ().WhenInjectedInto (typeof (IthoVentilatorStateMapper));
            Bind<IEndpointState> ().To<High> ().WhenInjectedInto (typeof (IthoVentilatorStateMapper));
            Bind<IEndpointState> ().To<Off> ().WhenInjectedInto (typeof (IthoVentilatorStateMapper));
        }
    }
}
