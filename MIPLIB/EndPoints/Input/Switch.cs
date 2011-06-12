using System;
using System.Collections.Generic;
using MIP.Interfaces;

namespace MIPLIB.EndPoints.Input
{
    public class Switch : InputEndpoint
    {
        #region Overrides of InputEndpoint

        public Switch(IEnumerable<IEndpointState> states)
        {
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

        #endregion
    }
}
