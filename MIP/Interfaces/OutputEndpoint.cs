using System;

namespace MIP.Interfaces
{
    public abstract class OutputEndpoint : IEndpoint
    {
        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs eventArgs);

        public event StateChangedEventHandler StateChanged;

        public void InvokeStateChanged(StateChangedEventArgs e)
        {
            StateChangedEventHandler handler = StateChanged;
            if (handler != null) handler(this, e);
        }

        #region Implementation of IEndpoint

        public abstract IEndpointState State { get; set; }
        public abstract IConnection ConnectedTo { get; set; }
        public abstract string Name { get; set; }

        #endregion
    }
}