using HAL;

namespace NCD
{
    public class NCDControllMessage : IControlMessage
    {
        public NCDController NCDController { get; set; }

        public int WaitTime { get; set; }

        public byte Bank { get; set; }

        public byte Relay { get; set; }

        public byte Status { get; set; }

        public void Enter()
        {
            //  on/off banknumber    relay
            //  0-1    0-32          0-7
            // | 0000 | 0000 | 0000 | 0000 |

            //turn relay 4 on bank 3 on
            // | 0001 | 0000 | 0011 | 0100 |

            //var input = NCDController.OutputStack.Pop ();
            NCDController.OutputStack.Push((ushort)(Relay + Bank << 4 + Status));
        }
    }
}
