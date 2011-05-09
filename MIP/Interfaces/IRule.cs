using System.Collections.Generic;

namespace MIP.Interfaces
{
    public interface IRule
    {
        IHub Hub { get; set; }

        IList<IEndpoint> Friends { get; set; }

        bool HasFriend(IEndpoint endpoint);

        bool FireWithInput(IEndpoint endpoint);
    }
}