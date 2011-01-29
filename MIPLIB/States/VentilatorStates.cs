using System;
using MIP.Interfaces;

namespace MIPLIB
{
    public class Low : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "Low"; }
        }

        #endregion
    }

    public class Medium : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "Medium"; }
        }

        #endregion
    }

    public class High : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "High"; }
        }

        #endregion
    }
}