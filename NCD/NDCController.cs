using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using HAL;
using MIP.Interfaces;

namespace NCD
{
    public class NDCController : IHardwareController, IDisposable
    {
        public void Initialize()
        {
            var configfile = ConfigurationManager.AppSettings["ConfigurationFilePath"];
            if (File.Exists(configfile))
                BasicConfiguration.Load(configfile);
            else
            {
                new BasicConfiguration().FillExample();
                BasicConfiguration.Save();
                throw new Exception("EmptyConfigurationException, Please fill the configuraion with the right information and start again.");
            }
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
}
