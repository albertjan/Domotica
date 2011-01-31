using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAL
{
    public class DimmedEndpoint : IHardwareEndpoint
    {
        [Dimmer]
        public IEndpointStateMapper Mapper { get; set; }
        public IEnumerable<IHardwareEndpointIndentifier> HardwareEndpointIndentifiers { get; set; }
    }
}
