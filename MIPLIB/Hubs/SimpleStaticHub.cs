using System.Collections.Generic;
using MIP.Interfaces;

namespace MIPLIB.Hubs
{
    public class SimpleStaticHub : IHub
    {
        public SimpleStaticHub()
        {
            RegisteredConnections = new List<IConnection>();
        }

        public IList<IConnection> RegisteredConnections { get; set; }
    }
}