using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAL
{
    public interface IHardwareEndpoint
    {
        IEndpointStateMapper Mapper { get; set; }
        IHardwareEndpointIndentifier ID { get; set; }
    }

    public class DimmerAttribute : Attribute {} 

    public class SwitchAttribute : Attribute {}

}
