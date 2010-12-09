using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IEndpointState
    {
        IEnumerator<IEndpointState> EnumerateStates();
    }
}