using System;
using System.Collections.Generic;
using MIP.Interfaces;
using Ninject;

namespace MIPLIB.EndPoints.Output
{
    public class Light : OutputEndpoint
    {
        #region Overrides of OutputEndpoint
        [Inject]
        public Light(IEnumerable<IEndpointState> states)
        {
            States = states;
        }

        public override IEnumerable<IEndpointState> States { get; set; }
        public override IEndpointState CurrentState { get; set; }
        public override IConnection ConnectedTo { get; set; }

        public override string Name { get; set; }

        #endregion
    }
}
