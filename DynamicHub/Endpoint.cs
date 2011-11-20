using System.Collections.Generic;
using ProtoBuf;

namespace DynamicHub
{
    [ProtoContract]
    public class Endpoint
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string Type { get; set; }         

        [ProtoMember(3)]
        public string HardwareID { get; set; }
        
        [ProtoMember(4)]
        public List<EndpointState> States { get; set; }
    }

    [ProtoContract]
    public class EndpointState
    {
        [ProtoMember(1)]
        public string Type { get; set; }
    }
}