using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MIP.Interfaces;

namespace MIPLIB.Hubs.Rules
{
    public class BasicRule : IRule
    {
        #region Implementation of IRule

        public IHub Hub { get; set; }
        public IList<IEndpoint> Friends { get; set; }
        
        public bool HasFriend(IEndpoint endpoint)
        {
            return Friends.Contains(endpoint);
        }

        public bool FireWithInput(IEndpoint endpoint)
        {
            if (Friends.Count == 2)
            {
                if (endpoint.ShouldTriggerRule ())
                {
                    var friendToCall = Friends.First(f => f != endpoint);

                    if (!friendToCall.DetermineNextState())
                        throw new EndpointException("Next state not determined.", friendToCall);

                    return true;
                }
                return false;
            }
            throw new RuleException("Basic Rules can only handle 2 endpoints");
        }

        #endregion
    }

    public class EndpointException : Exception
    {
        public EndpointException(string message, IEndpoint endpoint) : base(message + " : "  + endpoint.Name)
        {
        }
    }

    public class RuleException : Exception
    {
        public RuleException(string message) : base(message)
        {
           
        }
    }
}