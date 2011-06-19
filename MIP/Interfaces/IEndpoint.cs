using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IEndpoint
    {
        IEnumerable<IEndpointState> States { get; set; }
        IEndpointState CurrentState { get; set; }
        bool DetermineNextState();
        IList<IHub> Hubs { get; set; }
        string Name { get; set; }
        void SetHub(IHub hub);
        bool ShouldTriggerRule();
    }
}