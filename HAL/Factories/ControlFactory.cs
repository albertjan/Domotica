using System.Collections.Generic;
using Ninject;

namespace HAL.Factories
{
    public static class ControlFactory
    {
        public static StandardKernel Kernel { get; set; }

        public static IControlMessage GetControlMessage()
        {
            return Kernel.Get<IControlMessage>();
        }

        public static IHardwareEndpoint GetEndpoint<T>(IEnumerable<IHardwareEndpointIndentifier> endpointIndentifiers)
        {
            var ret = Kernel.Get<T>() as IHardwareEndpoint;
            if (ret != null)
            {
                ret.HardwareEndpointIndentifiers = endpointIndentifiers;
                return ret;
            }
            throw new ActivationException("Not a hardware endpoint " + typeof(T));
        }
    }
}
