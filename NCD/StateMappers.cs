using System;
using System.Collections.Generic;
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

        public IEndpointState DetermineState(IDictionary<int, IEnumerable<bool>> currentState)
        {
            throw new NotImplementedException();
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

        public IEndpointState DetermineState(IDictionary<int, IEnumerable<bool>> currentState)
        {
            throw new NotImplementedException();
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

        public IEndpointState DetermineState(IDictionary<int, IEnumerable<bool>> currentState)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }
        
        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage();
        }
    }
}

