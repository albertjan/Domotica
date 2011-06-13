using System;
using System.Collections.Generic;
using System.Linq;
using HAL;
using HAL.Factories;
using MIP.Interfaces;
using MIPLIB.States;
using Ninject;

namespace NCD
{
    public class DimmedEndpointStateMapper : IEndpointStateMapper
    {
        public DimmedEndpointStateMapper(IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEndpointState DetermineState(IDictionary<int, bool> currentState)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return ControlFactory.GetControlMessage();
        } 
    }

    public class SwitchedEndpointStateMapper : IEndpointStateMapper
    {
        public SwitchedEndpointStateMapper (IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEndpointState DetermineState(IDictionary<int, bool> currentState)
        {
            return currentState.First ().Value ? HandledStates.First (s => s is On) : HandledStates.First (s => s is Off);
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return ControlFactory.GetControlMessage ();
        }
    }

    public class IthoVentilatorStateMapper : IEndpointStateMapper
    {
        public IthoVentilatorStateMapper (IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEndpointState DetermineState(IDictionary<int, bool> currentState)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }
        
        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return ControlFactory.GetControlMessage();
        }
    }
}

