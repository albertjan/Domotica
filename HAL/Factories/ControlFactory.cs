using Ninject;

namespace HAL.Factories
{
    public static class ControlFactory
    {
        private static StandardKernel Kernel { get; set; }

        public static IControlMessage GetControlMessage()
        {
            return Kernel.Get<IControlMessage>();
        }
    }
}
