using System;
using System.Collections.Generic;
using MIP.Interfaces;

namespace MIPLIB.EndPoints.Input
{
    public class Switch : InputEndpoint
    {
        #region Overrides of InputEndpoint

        public override IEnumerable<IEndpointState> States { get; set; }
        public override IEndpointState CurrentState { get; set; }
        public override bool DetermineNextState()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IHub> Hubs { get; set; }
        public override void Trigger(object state)
        {
            throw new NotImplementedException();
        }

        public override string Name { get; set; }

        #endregion
    }
}
