using System;
using System.Collections.Generic;
using System.Threading;
using MIP.Interfaces;

namespace MIPLIB.EndPoints.Input
{
    public class SunRiseSunSetTimer : InputEndpoint
    {
        private static void Chrono(object input)
        {
            var srsst = input as SunRiseSunSetTimer;
            if (srsst == null) throw new InvalidCastException("input must be a SunRiseSunSetTimer!");
            while (true)
            {
                try
                {

                    Thread.Sleep(60 * 1000);
                }
                catch(ThreadAbortException)
                {
                    break;
                }
            }
        }

        private Thread ChronoThread { get; set; }

        public SunRiseSunSetTimer(IEnumerable<IEndpointState> states)
        {
            Hubs = new List<IHub>();
            States = states;
            ChronoThread = new Thread(Chrono);
            ChronoThread.Start(this);
        }

        #region Overrides of InputEndpoint

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
            return false;
        }

        public ~SunRiseSunSetTimer()
        {
            ChronoThread.Abort();
        }
        
        #endregion
    }
}