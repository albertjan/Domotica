using System;
using System.Collections.Generic;
using HAL;
using HAL.Endpoints;
using HAL.Factories;
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


        /// <summary>
        /// Load the Hardware Software bindings
        /// 
        /// A binding basicly consists of the following items:
        ///     -A name (this is used throuhg out the system for referencing this endpoint)
        ///     -A type (what kind of thing is it in the hardware, might be all kinds of things to all kinds of people)
        ///     -A list of hardware identifiers (One hardware endpoint may consist of several endpoint sockets)
        /// </summary>
        /// <returns></returns>
        public IEndPointCouplingInformation Load()
        {
            return new NCDEndPointCouplingInformation
            {
                EndpointCouples = new List<Tuple<string,IHardwareEndpoint>>
                {
                    new Tuple<string, IHardwareEndpoint>("KnopToilet", ControlFactory.GetEndpoint<GenericInputEndpoint> 
                    (
                        new List<IHardwareEndpointIndentifier> 
                        {
                            new NCDHardwareIdentifier
                                {
                                    ID = "B0:0",
                                    Type = HardwareEndpointType.Input
                                }
                        }
                    )),
                    new Tuple<string, IHardwareEndpoint>("LampToilet", ControlFactory.GetEndpoint<SwitchedEndpoint>
                    (
                        new List<IHardwareEndpointIndentifier> {
                            new NCDHardwareIdentifier
                                {
                                    ID = "B1:0",
                                    Type = HardwareEndpointType.Output
                                }
                            }
                    )),
                    new Tuple<string, IHardwareEndpoint>("LedSpotToilet", ControlFactory.GetEndpoint<SwitchedEndpoint>
                    (
                        new List<IHardwareEndpointIndentifier> {
                            new NCDHardwareIdentifier
                                {
                                    ID = "B1:1",
                                    Type = HardwareEndpointType.Output
                                }
                            }
                    )),
                    new Tuple<string, IHardwareEndpoint>("KnopToiletBuiten", ControlFactory.GetEndpoint<GenericInputEndpoint> 
                    (
                        new List<IHardwareEndpointIndentifier> 
                        {
                            new NCDHardwareIdentifier
                                {
                                    ID = "B0:1",
                                    Type = HardwareEndpointType.Input
                                }
                        }
                    )),
                }
            };
        }
    }
}