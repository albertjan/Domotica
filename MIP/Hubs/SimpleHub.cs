using System.Collections.Generic;
using MIP.Interfaces;

namespace MIP.Hubs
{
    public class SimpleHub : IHub
    {
        public IList<IConnection> Connection { get; set; }
    }
}