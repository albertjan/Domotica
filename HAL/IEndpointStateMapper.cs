﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Interfaces;

namespace HAL
{
    public interface IEndpointStateMapper
    {
        IEnumerable<IEndpointState> HandledStates { get; set; }
        IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid);
    }
}
