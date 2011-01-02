using System;

namespace MIP.Interfaces
{
    public abstract class InputEndpoint : IEndpoint
    {
        public delegate void InputReveiced(object sender, string[] e);

        public event InputReveiced InputReceived;

        public IEndpointState State { get; set; }
        
        public abstract IConnection ConnectedTo { get; set; }
        public abstract string Name { get; set; }
    }
}