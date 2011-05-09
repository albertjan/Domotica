using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IHub
    {
        IList<IEndpoint> RegisteredEndPoints { get; set; }

        IList<IRule> Rules { get; set; }

        void Trigger(IEndpoint endpoint);
    }
}