﻿using System;
using System.Collections.Generic;
using MIP.Interfaces;

namespace HAL
{
    public class EndPointCouplingInformation
    {
        public IEnumerable<Tuple<IEndpoint, IHardwareEndpointIndentifier>> EndpointCouples { get; set; }
    }
}