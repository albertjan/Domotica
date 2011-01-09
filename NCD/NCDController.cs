using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using HAL;
using MIP.Interfaces;

namespace NCD
{
    public class NCDController : IHardwareController, IDisposable
    {
        private static void Runner (object Controller)
        {
            var controller = (NCDController) Controller;
            try
            {
                while (true)
                {
                    if (controller.OutputStack.Count > 0)
                    {
                        //  on/off banknumber    relay
                        //  0-1    0-32          0-7
                        // | 0000 | 0000 | 0000 | 0000 |

                        //turn relay 4 on bank 3 on
                        // | 0001 | 0000 | 0011 | 0100 |

                        var input = controller.OutputStack.Pop();
                        var relay = (byte) (input & 15);
                        var bank = (byte) (input & 4080 >> 4);
                        var status = (byte) (input >> 12);
                        if (status == 0)
                            controller.NCDComponent.ProXR.RelayBanks.TurnOffRelayInBank(relay, bank);
                        else
                            controller.NCDComponent.ProXR.RelayBanks.TurnOnRelayInBank(relay, bank);
                    }
                    else
                    {
                        for (var contactClosureBank = 0; contactClosureBank < BasicConfiguration.Configuration.NumberOfContactClosureBanks; contactClosureBank++)
                        {
                            controller.InputStack.Push((byte)((byte)contactClosureBank + controller.NCDComponent.ProXR.Scan.ScanValue((byte)contactClosureBank)));
                        }
                    }
                }      
            }
            catch(ThreadAbortException)
            {
                
            }
        }

        public Thread Run { get; set; }

        public Thread Input { get; set; }

        public Thread Output { get; set; }

        public Stack<ushort> OutputStack { get; set; }

        public Stack<ushort> InputStack { get; set; }

        public NCDComponent NCDComponent { get; set; }

        public void Initialize()
        {
            var configfile = ConfigurationManager.AppSettings["ConfigurationFilePath"];
            if (File.Exists(configfile))
                BasicConfiguration.Load(configfile);
            else
            {
                new BasicConfiguration()
                    {
                        Path = configfile
                    }.FillExample();
                
                BasicConfiguration.Save();
                throw new Exception("EmptyConfigurationException, Please fill the configuration with the right information and start again.");
            }

            NCDComponent = new NCDComponent {BaudRate = 38400, PortName = BasicConfiguration.Configuration.Comport};
            //ncdComponent.Port = 1;
            NCDComponent.OpenPort();
            if (!NCDComponent.IsOpen) throw new HardwareInitializationException();
        }
        
        public void Start()
        {
            Run = new Thread(Runner);
            Run.Start(this);
        }

        public void Stop()
        {
            Run.Abort();
        }

        public EndPointCouplingInformation CouplingInformation { get; set; }

        public event EventHandlers.EndpointEventHandler EndpointStateChanged;

        public IEnumerable<IHardwareEndpointIndentifier> GetIdentifiers()
        {
            for (int i = 0; i < BasicConfiguration.Configuration.NumberOfContactClosureBanks; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    yield return new NCDHardwareIdentifier
                                     {
                                         ID = "B" + i + ":" + j,
                                         Type = HardwareEndpointType.Input
                                     };
                }
            }
            foreach (var relayBank in BasicConfiguration.Configuration.AvailableRelayBanks)
            {
                for (int i = 0; i < relayBank.AvailableRelays; i++)
                {
                    yield return new NCDHardwareIdentifier()
                                     {
                                         ID = "B" + relayBank.Number + ":" + i
                                     };
                }
            }
        }

        public void CoupleEndpoints(EndPointCouplingInformation endPointCouplingInformation)
        {
            CouplingInformation = endPointCouplingInformation;
        }

        public void Dispose()
        {
            BasicConfiguration.Save();
        }
    }

    public class HardwareInitializationException : Exception
    {
    }
}
