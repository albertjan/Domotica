using System;
using System.Collections.Generic;

namespace MIP.Interfaces
{
    public abstract class OutputEndpoint : IEndpoint
    {
        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs eventArgs);

        public event StateChangedEventHandler StateChanged;

        public void InvokeStateChanged(StateChangedEventArgs e)
        {
            var handler = StateChanged;
            if (handler != null) handler(this, e);
        }

        #region Implementation of IEndpoint

        public abstract IEnumerable<IEndpointState> States { get; set; }
        public abstract IEndpointState CurrentState { get; set; }
        public IEndpointState DetermineNextState()
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable<IHub> Hubs { get; set; }
        public abstract string Name { get; set; }

        #endregion
    }
}