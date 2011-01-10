using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using HAL;
using MIP.Interfaces;

namespace NCD
{
    public class NCDController : IHardwareController, IDisposable
    {
        #region Controllerthread

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
                        //| bank        |       value |
                        //| 0000 | 0000 | 0000 | 0000 |
                        
                        for (var contactClosureBank = 0; contactClosureBank < BasicConfiguration.Configuration.NumberOfContactClosureBanks; contactClosureBank++)
                        {
                            controller.InputStack.Push((ushort)(((byte)contactClosureBank << 8) + controller.NCDComponent.ProXR.Scan.ScanValue((byte)contactClosureBank)));
                        }
                    }
                }      
            }
            catch(ThreadAbortException)
            {
                
            }
        }

        public Thread Run { get; set; }

        #endregion

        #region Inputthread

        private static void InputRunner(object ncdController)
        {
            var controller = (NCDController) ncdController;

            try
            {
                while (true)
                {
                    if (controller.InputStack.Count > 0)
                    {
                        var val = controller.InputStack.Pop();
                        var bank = (val & 0xFF00) >> 8;
                        var value = (val & 0x00FF);
                        controller.ReportStates(bank, ParseValue(value));
                    }   
                    Thread.Sleep(5);
                }
            }
            catch(ThreadAbortException)
            {
                
            }
        }

        public IDictionary<int, IEnumerable<bool>> CurrentState { get; set; }

        private void ReportStates(int bank, IEnumerable<bool> states)
        {
            
        }

        private static IEnumerable<bool> ParseValue(int value)
        {
            yield return (value & 1) > 0 ? true : false;
            yield return (value & 2) > 0 ? true : false;
            yield return (value & 4) > 0 ? true : false;
            yield return (value & 8) > 0 ? true : false;
            yield return (value & 16) > 0 ? true : false;
            yield return (value & 32) > 0 ? true : false;
            yield return (value & 64) > 0 ? true : false;
            yield return (value & 128) > 0 ? true : false;
        }

        public Thread Input { get; set; }

        #endregion

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

        private void OnEndPointStateChanged()
        {
            if (EndpointStateChanged != null)
                EndpointStateChanged(this, new EndpointEventArgs());
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
                                         ID = "B" + relayBank.Number + ":" + i,
                                         Type = HardwareEndpointType.Output
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
