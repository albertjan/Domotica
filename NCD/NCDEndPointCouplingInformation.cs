using System;
using System.Collections.Generic;
using HAL;
using HAL.Endpoints;
using MIP.Interfaces;

namespace NCD
{
    public class NCDEndPointCouplingInformation: IEndPointCouplingInformation
    {
        public IEnumerable<Tuple<string, IHardwareEndpoint>> EndpointCouples { get; set; }

        public bool Save()
        {
            return true;
        }

        public IEndPointCouplingInformation Load()
        {
            return new NCDEndPointCouplingInformation
            {
                EndpointCouples = new List<Tuple<string,IHardwareEndpoint>>
                {
                    new Tuple<string, IHardwareEndpoint>("KnopToilet", new GenericInputEndpoint
                    {
                        HardwareEndpointIndentifiers = new List<IHardwareEndpointIndentifier> 
                        {
                            new NCD.NCDHardwareIdentifier()
                                {
                                    ID = "01",
                                    Type = HardwareEndpointType.Input
                                } 
                        }
                    }),
                }
            };
        }
    }
}