using System;
using System.Collections.Generic;
using MIP.Interfaces;

namespace MIPLIB
{
    public class OnOff : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "On"; }
        }

        #endregion
    }

    public class Off : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "Off"; }
        }

        #endregion
    }
}