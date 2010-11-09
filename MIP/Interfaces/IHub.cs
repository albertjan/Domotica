using System.Collections.Generic;

namespace MIP
{
    public interface IHub
    {
        IList<IConnection> Connection { get; set; }
    }
}