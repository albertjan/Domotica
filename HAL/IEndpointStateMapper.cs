using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Interfaces;
using Ninject;

namespace HAL
{
    public interface IEndpointStateMapper
    {
        IEndpointState DetermineState(IDictionary<int, bool> currentState);
        [Inject]
        IEnumerable<IEndpointState> HandledStates { get; set; }
        IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid);
    }
}
