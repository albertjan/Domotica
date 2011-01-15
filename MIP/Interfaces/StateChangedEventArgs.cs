using System;

namespace MIP.Interfaces
{
    public class StateChangedEventArgs : EventArgs
    {
        public IEndpoint Endpoint { get; set; }
        public DateTime Time { get; set; }
    }
}