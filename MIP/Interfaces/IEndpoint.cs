using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IEndpoint
    {
        IEnumerable<IEndpoint> States {get;set;}
        IEndpointState CurrentState { get; set; }
        IConnection ConnectedTo { get; set; }
        string Name { get; set; }
    }
}