using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAL
{
    public class InputObserver : IObserver<IInputTask>
    {
        public void OnNext(IInputTask value)
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
