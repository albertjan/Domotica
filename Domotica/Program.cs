using System;
using HAL;
using HAL.Endpoints;
using HAL.Factories;
using MIPLIB.EndPoints.Output;
using NCD;
using Ninject;

namespace Domotica
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load("NinjectionModules.dll");
            ControlFactory.Kernel = (StandardKernel)kernel;
            var hwc = kernel.Get<IHardwareController>();
            hwc.Initialize();
            hwc.Start();
            //hwc.Hubs.Add(kernel.Get<IHub>());
            hwc.InitializeEndpoints();

            //var p = kernel.Get<FourStateEndPoint>();
            Console.WriteLine("hit return to quit");
            Console.ReadLine();
            hwc.Stop();
        }
    }
}
