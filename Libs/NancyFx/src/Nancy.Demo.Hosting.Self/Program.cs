﻿namespace Nancy.Demo.Hosting.Self
{
    using System;
    using System.Diagnostics;

    using Nancy.Hosting.Self;

    class Program
    {
        static void Main()
        {
            var nancyHost = new NancyHost(new Uri("http://localhost:8888/nancy/"), new Uri("http://127.0.0.1:8888/nancy/"), new Uri("http://localhost:8889/nancytoo/"));
            nancyHost.Start();

            Console.WriteLine("Nancy now listening - navigating to http://localhost:8888/nancy/. Press enter to stop");
            Process.Start("http://localhost:8888/nancy/");
            Console.ReadKey();

            nancyHost.Stop();

            Console.WriteLine("Stopped. Good bye!");
        }
    }
}