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
            }, "test");
            client.AddRule (new DynamicRule ()
            {
                Script = "hoi"
            }, "test2");

            foreach (var dynamicRule in client.GetRules())
            {
                Console.WriteLine(dynamicRule.Script);
            }
            Console.ReadLine();
        }
    }
}
