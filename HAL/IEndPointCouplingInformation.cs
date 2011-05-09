using System;
using System.Collections.Generic;
using HAL;

namespace HAL
{
    public interface IEndPointCouplingInformation
    {
        IEnumerable<Tuple<string, IHardwareEndpoint>> EndpointCouples { get; set; }
        bool Save();
        IEndPointCouplingInformation Load();
    }
}