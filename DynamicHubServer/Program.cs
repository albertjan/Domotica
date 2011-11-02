using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Hosting.Self;

namespace DynamicHubServer
{
    public static class Program
    {
        public static void Main (string[] args)
        {
            var host = new NancyHost(new Uri("http://localhost:8081"));
            DynamicHub.Storage.StorageService service;
            host.Start();
            Console.ReadLine();
            host.Stop();
        }
    }
}
