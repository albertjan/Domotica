using System;
using HAL;
using MIPLIB.EndPoints.Output;
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
            var hwc = kernel.Get<IHardwareController>();
            //hwc.Initialize();
            //hwc.Start();

            var l = kernel.Get<Light>();
            var i = kernel.Get<IthoVentilator>();

            Console.WriteLine("hit return to quit");
            Console.ReadLine();
            hwc.Stop();
        }
    }
}
