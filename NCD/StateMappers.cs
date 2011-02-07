using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL;
using MIP.Interfaces;
using Ninject;

namespace NCD
{
    public class DimmedEndpointStateMapper : IEndpointStateMapper
    {
        public DimmedEndpointStateMapper(IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage();
        }
    }

    public class SwitchedEndpointStateMapper : IEndpointStateMapper
    {
        public SwitchedEndpointStateMapper (IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage ();
        }
    }

    public class IthoVentilatorStateMapper : IEndpointStateMapper
    {
        [Inject]
        public IthoVentilatorStateMapper (IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }
        
        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage();
        }
    }
}

