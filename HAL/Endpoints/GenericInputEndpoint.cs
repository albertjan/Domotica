using System.Collections.Generic;

namespace HAL.Endpoints
{
    public class GenericInputEndpoint : IHardwareEndpoint
    {
        #region Implementation of IHardwareEndpoint

        public IEndpointStateMapper Mapper { get; set; }
        public IEnumerable<IHardwareEndpointIndentifier> HardwareEndpointIndentifiers { get; set; }

        #endregion
    }
}