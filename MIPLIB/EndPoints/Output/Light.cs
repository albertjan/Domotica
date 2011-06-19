using System;
using System.Collections.Generic;
using System.Linq;
using MIP.Interfaces;
using Ninject;

namespace MIPLIB.EndPoints.Output
{
    public class Light : OutputEndpoint
    {
        #region Overrides of OutputEndpoint
       
        public Light(IEnumerable<IEndpointState> states)
        {
            Hubs = new List<IHub>();
            States = states;
        }

        public override IEnumerable<IEndpointState> States { get; set; }
        //public override IEndpointState CurrentState { get; set; }
        public override bool DetermineNextState()
        {
            CurrentState = States.First(s => s.GetType() != CurrentState.GetType());
            return true;
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
