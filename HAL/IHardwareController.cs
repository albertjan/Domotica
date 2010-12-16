using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAL
{
    public interface IHardwareController
    {
        event EventHandlers.EndpointEventHandler EndpointStateChanged;
    }
}
