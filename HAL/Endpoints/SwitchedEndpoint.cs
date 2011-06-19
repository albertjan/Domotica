using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace HAL
{
    public class SwitchedEndpoint : IHardwareEndpoint
    {
        [Switch, Inject]
        public IEndpointStateMapper Mapper { get; set; }
        public IEnumerable<IHardwareEndpointIndentifier> HardwareEndpointIndentifiers { get; set; }
    }
}