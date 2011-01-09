using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using MIP.Interfaces;

namespace HAL
{
    public interface IHardwareController
    {
        void Initialize();

        void Start();

        void Stop();

        event EventHandlers.EndpointEventHandler EndpointStateChanged;

        void CoupleEndpoints(EndPointCouplingInformation endPointCouplingInformation);

        IEnumerable<IHardwareEndpointIndentifier> GetIdentifiers();
    }
}
