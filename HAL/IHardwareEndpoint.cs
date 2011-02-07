using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace HAL
{
    public interface IHardwareEndpoint
    {
        [Inject]
        IEndpointStateMapper Mapper { get; set; }
        IEnumerable<IHardwareEndpointIndentifier> HardwareEndpointIndentifiers { get; set; }
    }

    public class DimmerAttribute : Attribute { }

    public class SwitchAttribute : Attribute { }

    public class FourStateAttribute : Attribute { }
}