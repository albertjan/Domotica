using System;
using System.Collections.Generic;

namespace MIP.Interfaces
{
    public abstract class InputEndpoint : IEndpoint
    {
        public abstract IEnumerable<IEndpointState> States { get; set; }
        public abstract IEndpointState CurrentState { get; set; }
        public abstract bool DetermineNextState();
        public abstract IEnumerable<IHub> Hubs { get; set; }
        public abstract void Trigger(object state);
        public abstract string Name { get; set; }
    }
}