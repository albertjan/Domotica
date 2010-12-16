using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IEndpointState
    {
        IEnumerable<IEndpointState> States { get; set; }
    }
}