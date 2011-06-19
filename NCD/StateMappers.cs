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

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpoint hardwareEndpoint)
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

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpoint hardwareEndpoint)
        {
            var hwid = hardwareEndpoint.HardwareEndpointIndentifiers.First ().ID;
            var message = ControlFactory.GetControlMessage () as NCDControllMessage;
            if (message != null)
            {
                message.Bank = byte.Parse (hwid.Substring (1, hwid.IndexOf (":") - 1));
                message.Relay = byte.Parse (hwid.Substring (hwid.IndexOf (":") + 1));
                if (state != null)
                {
                    switch (state.Name)
                    {
                        case "On":
                            message.Status = 1;
                            yield return message;
                            break;
                        case "Off":
                            message.Status = 0;
                            yield return message;
                            break;
                        default:
                            message.Status = 0;
                            yield return message;
                            break;
                    }
                }
            }
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
        
        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpoint hardwareEndpoint)
        {
            yield return ControlFactory.GetControlMessage();
        }
    }

    public class GenericInputEndpointStateMapper : IEndpointStateMapper
    {
        #region Implementation of IEndpointStateMapper

        public GenericInputEndpointStateMapper(IEnumerable<IEndpointState> handledStates)
        {
            HandledStates = handledStates;
        }

        public IEndpointState DetermineState(IDictionary<int, bool> currentState)
        {
            return currentState.First().Value
                       ? HandledStates.First(s => s.Name == "In")
                       : HandledStates.First(s => s.Name == "Out");
        }

        public IEnumerable<IEndpointState> HandledStates { get; set; }

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpoint hardwareEndpoint)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

