using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCD
{
    [Serializable]
    public class RelayBank
    {
        public int Number { get; set; }
        public int AvailableRelays { get; set; }
    }
}
