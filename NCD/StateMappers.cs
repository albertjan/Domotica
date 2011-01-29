using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL;
using MIP.Interfaces;

namespace NCD
{
    public class SwitchDimmerStateMapper : IEndpointStateMapper
    {
        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage();
        }
    }

    public class SwitchStateMapper : IEndpointStateMapper
    {
        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage ();
        }
    }

    public class IthoVentilatorStateMapper : IEndpointStateMapper
    {
        #region Implementation of IEndpointStateMapper

        public IEnumerable<IControlMessage> GetControllMessagesForEndpointState(IEndpointState state, IHardwareEndpointIndentifier hwid)
        {
            yield return new NCDControllMessage();
        }

        #endregion
    }
}

