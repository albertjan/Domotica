using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL;

namespace NCD
{
    public class NCDHardwareIdentifier : IHardwareEndpointIndentifier
    {
        public string ID { get; set; }

        public HardwareEndpointType Type { get; set; }
    }
}
