using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IEndpoint
    {
        IEnumerable<IEndpointState> States { get; set; }
        IEndpointState CurrentState { get; set; }
        IEndpointState DetermineNextState();
        IEnumerable<IHub> Hubs { get; set; }
        string Name { get; set; }
    }
}