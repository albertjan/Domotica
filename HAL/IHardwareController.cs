using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Interfaces;

namespace HAL
{
    public interface IHardwareController
    {
        void Initialize();

        event EventHandlers.EndpointEventHandler EndpointStateChanged;

        void CoupleEndpoints(EndPointCouplingInformation endPointCouplingInformation);

        IEnumerable<IHardwareEndpointIndentifier> GetIdentifiers();
    }
}
