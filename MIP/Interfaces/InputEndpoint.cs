using System;

namespace MIP.Interfaces
{
    public abstract class InputEndpoint : IEndpoint
    {
        public abstract IEndpointState State { get; set; }
        
        public abstract IConnection ConnectedTo { get; set; }
        public abstract void Trigger(object state);
        public abstract string Name { get; set; }
    }
}