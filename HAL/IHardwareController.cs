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

        void CoupleEndpoints(IEndPointCouplingInformation endPointCouplingInformation);

        IEnumerable<IHub> Hubs { get; set; }
        
        IEnumerable<IHardwareEndpointIndentifier> GetIdentifiers();
     }
}
