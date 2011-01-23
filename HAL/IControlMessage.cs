using System;

namespace HAL
{
    public interface IControlMessage
    {
        void Enter();
        int WaitTime { get; set; }
    }
}