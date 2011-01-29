using System;
using System.Collections.Generic;
using MIP.Interfaces;
using Ninject;

namespace MIPLIB.EndPoints.Output
{
    public class IthoVentilator : OutputEndpoint
    {
        [Inject]
        public IthoVentilator (IEnumerable<IEndpointState> states)
        {
            States = states;
        }
        
        #region Overrides of OutputEndpoint
        public override IEnumerable<IEndpointState> States { get; set; }
        public override IEndpointState CurrentState { get; set; }
        public override IConnection ConnectedTo { get; set; }
        public override string Name { get; set; }
        
        #endregion
    }
}
