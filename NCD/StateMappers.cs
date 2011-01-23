using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL;
using MIP.Interfaces;

namespace NCD
{
    public class StateMappers : IEndpointStateMapper
    {
        public ushort GetControllMessageForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            throw new NotImplementedException();
        }
    }

    public class SwitchStateMapper : IEndpointStateMapper
    {
        public ushort GetControllMessageForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            throw new NotImplementedException();
        }
    }
}
}
