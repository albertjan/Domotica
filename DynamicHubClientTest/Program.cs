using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicHub;
using DynamicHub.Storage;

namespace DynamicHubClientTest
{
    class Program
    {
        static void Main (string[] args)
        {
            var client = ClientFactory.GetStorageClient();
            client.AddRule(new DynamicRule()
                               {
                                   Script = "hoi"
                               },"test");
        }
    }
}
