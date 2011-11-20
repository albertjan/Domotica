using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DRC;
using ImpromptuInterface;
using ProtoBuf;

namespace DynamicHub.Storage
{
    public interface IStorageClient
    {
        #region Rules

        void AddRule(DynamicRule rule, string name);
        
        IEnumerable<DynamicRule> GetRules();

        void DeleteRule(string name);
        
        #endregion

        #region Endpoints

        void AddEndpoint(dynamic endpoint, string name);

        IEnumerable<EndPoint> GetEndpoints();

        void DeleteEndpoint(string name);

        #endregion

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

            client.GetRules.In = new Func<WebResponse, IEnumerable<DynamicRule>>(wr => Serializer.Deserialize<List<DynamicRule>>(wr.GetResponseStream()));

            //return null;
            return Impromptu.ActLike<IStorageClient> (client);
        }
    }
}
