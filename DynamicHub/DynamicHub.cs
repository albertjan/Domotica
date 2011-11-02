using System.Collections.Generic;
using MIP.Interfaces;

namespace DynamicHub
{
    public class DynamicHub : IHub
    {
        public IEnumerable<IEndpoint> RegisteredEndPoints { get; set; }
        public IList<IRule> Rules { get; set; }
        public void Trigger(IEndpoint endpoint, IEndpointState state)
        {
            throw new System.NotImplementedException();
        }
    }
}