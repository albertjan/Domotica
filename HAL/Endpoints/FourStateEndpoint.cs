using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace HAL.Endpoints
{
    public class FourStateEndpoint : IHardwareEndpoint
    {
        #region Implementation of IHardwareEndpoint
        [FourState]
        public IEndpointStateMapper Mapper { get; set; }
        public IEnumerable<IHardwareEndpointIndentifier> HardwareEndpointIndentifiers { get; set; }

        #endregion
    }
}
