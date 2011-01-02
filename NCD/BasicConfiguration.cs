using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace NCD
{
    [Serializable]
    public class BasicConfiguration
    {
        private static BasicConfiguration _instance;

        public static BasicConfiguration Configuration { 
            get { return _instance; }
        }

        [NonSerialized]
        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public string Comport { get; set; }
        public int NumberOfContactClosureBanks { get; set; }
        public IEnumerable<RelayBank> AvailableRelayBanks { get; set; }

        public void FillExample()
        {
            Comport = "COM1";
            NumberOfContactClosureBanks = 1;
            AvailableRelayBanks = new List<RelayBank>
                                      {
                                          new RelayBank
                                              {
                                                  AvailableRelays = 8,
                                                  Number = 0
                                              }
                                      };
            _instance = this;
        }

        public static void Load (string path)
        {
            if (_instance == null)
            {
                using (var s = File.Open (path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    _instance = (BasicConfiguration)new XmlSerializer (typeof (BasicConfiguration)).Deserialize (s);
                    _instance.Path = path;
                }
            }
        }

        public static void Save ()
        {
            if (_instance != null)
            {
                using (var s = File.Open(_instance.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    new XmlSerializer(typeof(BasicConfiguration)).Serialize(s, _instance);
                }
            }
            else
            {
                throw new ArgumentException("Must be loaded before saving.");
            }
        }
    }
}
