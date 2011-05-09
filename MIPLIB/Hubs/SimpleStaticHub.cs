using System;
using System.Collections.Generic;
using System.Linq;
using MIP.Interfaces;

namespace MIPLIB.Hubs
{
    public class SimpleStaticHub : IHub
    {
        public SimpleStaticHub()
        {
            Rules = new List<IRule>();
            
        }

        #region Implementation of IHub

        public IList<IEndpoint> RegisteredEndPoints { get; set; }

        public IList<IRule> Rules { get; set; }

        public void Trigger(IEndpoint endpoint)
        {
            var actions = Rules.Where(r => r.HasFriend(endpoint)).Select(rule => rule.FireWithInput(endpoint));
        }

        #endregion
    }
}