using System.Collections.Generic;
using MIP.Interfaces;
using ProtoBuf;

namespace DynamicHub
{
    [ProtoContract]
    public class DynamicRule : IRule
    {
        [ProtoMember(1)]
        public string Script { get; set; }

        public IHub Hub { get; set; }
        public IList<IEndpoint> Friends { get; set; }
        public bool HasFriend(IEndpoint endpoint)
        {
            throw new System.NotImplementedException();
        }

        public bool FireWithInput(IEndpoint endpoint)
        {
            throw new System.NotImplementedException();
        }
    }
}