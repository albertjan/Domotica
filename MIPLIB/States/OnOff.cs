using MIP.Interfaces;

namespace MIPLIB.States
{
    public class On : IEndpointState
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