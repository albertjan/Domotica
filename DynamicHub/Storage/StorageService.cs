using System.Collections.Generic;
using System.IO;
using Nancy;
using ProtoBuf;
using Raven.Client.Document;

namespace DynamicHub.Storage
{
    public class StorageService : NancyModule
    {
        private static readonly DocumentStore Store;
        static StorageService()
        {
            Store = new DocumentStore { Url = "http://localhost:8080" };
            Store.Initialize ();
        }

        public StorageService()
        {
            Get["/"] = x =>
            {
                return "Hello world!";
            };

            Get["/rules"] = x =>
            {
                IEnumerable<DynamicRule> rules; 
                using (var session = Store.OpenSession())
                {
                    rules = session.Query<DynamicRule>();
                }
                
                return new Response
                {
                    ContentType = "x-google-protocol-buggers",
                    Contents = s => Serializer.Serialize(s, rules),
                    StatusCode = HttpStatusCode.Accepted
                };
            };

            Put["/rule/{name}"] = x =>
            {
                var rule = Serializer.Deserialize<DynamicRule>(Request.Body);
                using (var session = Store.OpenSession())
                {
                    session.Store(rule, (string)x.name);
                }
                return HttpStatusCode.Accepted;
            };
        } 
    }
}