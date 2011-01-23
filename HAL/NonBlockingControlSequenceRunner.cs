using System.Collections.Generic;
using System.Threading;

namespace HAL
{
    public class NonBlockingControlSequenceRunner
    {
        private Thread _runner;
        public IEnumerable<IControlMessage> Messages { get; set; }
        public void Run ()
        {
            _runner = new Thread (SequenceRunner);
            _runner.Start (this);
        }

        private static void SequenceRunner (object messages)
        {
            var sequenceRunner = messages as NonBlockingControlSequenceRunner;
            if (sequenceRunner != null)
                foreach (var controlMessage in sequenceRunner.Messages)
                {
                    Thread.Sleep (controlMessage.WaitTime);
                    controlMessage.Enter ();
                }
        }
    }
}
