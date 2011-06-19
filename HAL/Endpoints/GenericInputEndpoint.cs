using System.Collections.Generic;
using Ninject;

namespace HAL.Endpoints
{
    public class GenericInputEndpoint : IHardwareEndpoint
    {
        #region Implementation of IHardwareEndpoint

        [Input,Inject]
        public IEndpointStateMapper Mapper { get; set; }
        public IEnumerable<IHardwareEndpointIndentifier> HardwareEndpointIndentifiers { get; set; }

        #endregion
    }
}