using System;
using HAL;
using HAL.Factories;
using Ninject;

namespace Domotica
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine ("hit return to quit");
            IKernel kernel = new StandardKernel();
            kernel.Load("NinjectionModules.dll");
            ControlFactory.Kernel = (StandardKernel)kernel;
            var hwc = kernel.Get<IHardwareController>();
            hwc.Initialize();
            hwc.Start();
            //hwc.Hubs.Add(kernel.Get<IHub>());
            hwc.InitializeEndpoints();
            //var p = kernel.Get<FourStateEndPoint>();
            Console.ReadLine();
            hwc.Stop();
        }
    }
}
