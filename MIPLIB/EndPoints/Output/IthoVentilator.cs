using System;
using System.Collections.Generic;
using MIP.Interfaces;
using Ninject;

namespace MIPLIB.EndPoints.Output
{
    public class IthoVentilator : OutputEndpoint
    {
        public IthoVentilator (IEnumerable<IEndpointState> states)
        {
            States = states;
        }
        
        #region Overrides of OutputEndpoint
        public override IEnumerable<IEndpointState> States { get; set; }
        //public IEndpointState CurrentState { get; set; }
        public override bool DetermineNextState()
        {
            throw new NotImplementedException();
        }

        public override IList<IHub> Hubs { get; set; }
        public override string Name { get; set; }
        public override void SetHub(IHub hub)
        {
            Hubs.Add(hub);
        }

        #endregion
    }
}
