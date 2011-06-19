using System;
using MIP.Interfaces;

namespace MIPLIB.States
{
    public class In : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "In"; }
        }

        #endregion
    }

    public class Out : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "Out"; }
        }

        #endregion
    }

    public class Value : IEndpointState
    {
        #region Implementation of IEndpointState

        public string Name
        {
            get { return "Value" + CurrentValue; }
        }

        public int CurrentValue { get; set; }

        #endregion
    }
}