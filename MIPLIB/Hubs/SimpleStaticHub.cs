using System;
using System.Collections.Generic;
using MIP.Interfaces;

namespace MIPLIB.Hubs
{
    public class SimpleStaticHub : IHub
    {
        public SimpleStaticHub()
        {
                
        }

        #region Implementation of IHub

        public IList<IEndpoint> RegisteredEndPoints { get; set; }
        public IEnumerable<IEndpoint> DetermineRoute(IEndpoint endpoint)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}