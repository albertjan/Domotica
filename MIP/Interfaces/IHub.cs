using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IHub
    {
        IList<IConnection> Connection { get; set; }
    }
}