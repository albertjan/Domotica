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
        private IEndpointState _currentState;
        public IEndpointState CurrentState
        {
            get { return _currentState; }
            set
            {
                InvokeStateChanged(new StateChangedEventArgs()
                                       {
                                           Endpoint = this,
                                           Time = DateTime.Now
                                       });
                _currentState = value;
            }
        }

        public abstract bool DetermineNextState();
        public abstract IList<IHub> Hubs { get; set; }
        public abstract string Name { get; set; }
        public abstract void SetHub(IHub hub);

        #endregion
    }
}