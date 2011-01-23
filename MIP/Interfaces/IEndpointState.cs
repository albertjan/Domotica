using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IEndpointState
    {
        IEndpointState State { get; set; }
        IEnumerable<IEndpointState> States { get; set; }
    }
}