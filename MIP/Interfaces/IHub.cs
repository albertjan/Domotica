using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IHub
    {
        IList<IEndpoint> RegisteredEndPoints { get; set; }

        IEnumerable<IEndpoint> DetermineRoute(IEndpoint endpoint);
    }
}