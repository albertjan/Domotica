using System;
using System.Collections.Generic;
using MIP.Interfaces;
using MIPLIB.States;

namespace MIPLIB.EndPoints.Input
{
    public class PulseSwitch : InputEndpoint
    {
        #region Overrides of InputEndpoint

        public PulseSwitch(IEnumerable<IEndpointState> states)
        {
            Hubs = new List<IHub>();
            States = states;
        }

        public override IEnumerable<IEndpointState> States { get; set; }
        public override IEndpointState CurrentState { get; set; }
        public override bool DetermineNextState()
        {
            throw new NotImplementedException();
        }

        public override IList<IHub> Hubs { get; set; }
        public override void Trigger(object state)
        {
            throw new NotImplementedException();
        }

        public override string Name { get; set; }
        public override void SetHub(IHub hub)
        {
            Hubs.Add(hub);
        }

        public override bool ShouldTriggerRule()
        {
            return CurrentState is Out;
        }

        #endregion
    }
}
