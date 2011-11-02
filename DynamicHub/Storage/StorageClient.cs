using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DRC;
using ImpromptuInterface;
using ProtoBuf;

namespace DynamicHub.Storage
{
    public interface IStorageClient
    {
        void AddRule(DynamicRule rule, string name);
        
        IEnumerable<DynamicRule> GetRules();
    }

    public static class ClientFactory
    {
        public static IStorageClient GetStorageClient()
        {
            dynamic client = new RESTClient {Url = "http://localhost:8081"};

            client.AddRule.Out = new Func<DynamicRule, byte[]>(rule =>
            {
                using (var ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, rule);
                    return ms.ToArray();
                }
            });

            //return null;
            return Impromptu.ActLike<IStorageClient> (client);
        }
    }
}
