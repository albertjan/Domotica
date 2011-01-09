using System;
using HAL;
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
            hwc.Initialize();

            
        }
    }
}
