using System;
using System.Threading;
using MIP.Interfaces;

namespace MIPLIB.EndPoints
{
    public class DelayedTrigger
    {
        private readonly Action<object> _trigger;
        private long TicksToWait { get; set; }
        private object State { get; set; }
        private readonly Thread _thread;

        public DelayedTrigger(long ticksToWait, Action<object> trigger, object state)
        {
            _trigger = trigger;
            
            TicksToWait = ticksToWait;
            State = state;
            _thread = new Thread(Trigger);
            _thread.Start(this);
        }

        public void Abort()
        {
             _thread.Abort();
        }

        private static void Trigger(dynamic delayedTrigger)
        {
            try
            {
                Thread.Sleep (delayedTrigger.TicksToWait);
                delayedTrigger._trigger (delayedTrigger.State);
            }
            catch (ThreadAbortException)
            {
                //Die gracefully
            }
        }
    }
}