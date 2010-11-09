using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IHub
    {
        IList<IConnection> RegisteredConnections { get; set; }
    }
}