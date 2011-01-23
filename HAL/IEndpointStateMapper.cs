using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Interfaces;

namespace HAL
{
    public interface IEndpointStateMapper
    {
        ushort GetControllMessageForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid);
    }
}
