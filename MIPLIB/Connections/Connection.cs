using System;
using MIP.Interfaces;

namespace MIPLIB.Connections
{
    public class Connection : IConnection
    {
        #region Implementation of IConnection

        public IHub Via { get; set; }
        public IEndpoint Endpoint { get; set; }
        public void Register()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
