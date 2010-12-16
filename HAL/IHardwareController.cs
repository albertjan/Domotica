using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Interfaces;

namespace HAL
{
    public interface IHardwareController
    {
        event EventHandlers.EndpointEventHandler EndpointStateChanged;

        void CoupleEndpoints(IEnumerable<IEndpoint> endpoints, EndPointCouplingInformation endPointCouplingInformation);

        IEnumerable<IHardwareEndpointIndentifiers> GetIdentifiers();
    }
}
