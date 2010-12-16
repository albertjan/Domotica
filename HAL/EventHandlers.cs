using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Interfaces;

namespace HAL
{
    public class EventHandlers
    {
        public delegate void EndpointEventHandler(IHardwareController sender, EndpointEventArgs eventArgs);
    }

    public class EndpointEventArgs
    {
        public IEndpointState State { get; set; }
        public IEndpoint Endpoint { get; set; } 
    }
}
