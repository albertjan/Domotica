using System;
using System.Collections.Generic;
using System.Linq;
using MIP.Interfaces;
using MIPLIB.EndPoints.Input;
using MIPLIB.EndPoints.Output;
using MIPLIB.Hubs.Rules;
using Ninject;

namespace MIPLIB.Hubs
{
    public class SimpleStaticHub : IHub
    {
        public SimpleStaticHub (IEnumerable<IEndpoint> endpoints)
        {
            RegisteredEndPoints = endpoints.ToList();
            RegisteredEndPoints.Where (e => e is Light).ToList()[0].Name = "LampToilet";
            RegisteredEndPoints.Where (e => e is PulseSwitch).ToList ()[0].Name = "KnopToilet";
            RegisteredEndPoints.Where (e => e is Light).ToList ()[1].Name = "LedSpotToilet";
            RegisteredEndPoints.Where (e => e is PulseSwitch).ToList ()[1].Name = "KnopToiletBuiten";
            RegisteredEndPoints.Where (e => e is Light).ToList ()[2].Name = "ZuuklampjesWoonkamer";
            RegisteredEndPoints.Where (e => e is PulseSwitch).ToList ()[2].Name = "VoorsteKnopGang";
            RegisteredEndPoints.Where (e => e is Light).ToList ()[3].Name = "MerlinTEetkamer";
            RegisteredEndPoints.Where (e => e is PulseSwitch).ToList ()[3].Name = "BovensteKnopLinksVoorzetwand";
            Rules = new List<IRule>
                        {
                            new BasicRule
                                {
                                    Hub = this,
                                    Friends = new List<IEndpoint> { RegisteredEndPoints.First(e => e.Name == "KnopToilet"), RegisteredEndPoints.First(e => e.Name == "LampToilet") }
                                },
                            new BasicRule
                                {
                                    Hub = this,
                                    Friends = new List<IEndpoint> { RegisteredEndPoints.First(e => e.Name == "KnopToiletBuiten"), RegisteredEndPoints.First(e => e.Name == "LedSpotToilet") }
                                },
                            new BasicRule
                                {
                                    Hub = this,
                                    Friends = new List<IEndpoint> { RegisteredEndPoints.First(e => e.Name == "VoorsteKnopGang"), RegisteredEndPoints.First(e => e.Name == "ZuuklampjesWoonkamer") }
                                },
                            new BasicRule
                                {
                                    Hub = this,
                                    Friends = new List<IEndpoint> { RegisteredEndPoints.First(e => e.Name == "BovensteKnopLinksVoorzetwand"), RegisteredEndPoints.First(e => e.Name == "MerlinTEetkamer") }
                                }
                        };
        }

        #region Implementation of IHub

        private IEnumerable<IEndpoint> _registeredEndPoints;
        public IEnumerable<IEndpoint> RegisteredEndPoints
        {
            get { return _registeredEndPoints; }
            set
            {
                foreach (var endpoint in value)
                {
                    endpoint.SetHub(this);
                }
                _registeredEndPoints = value;
            }
        }

        public IList<IRule> Rules { get; set; }

        public void Trigger(IEndpoint endpoint, IEndpointState state)
        {
            endpoint.CurrentState = state;
            var actions = Rules.Where(r => r.HasFriend(endpoint)).Select(rule => rule.FireWithInput(endpoint)).ToList();
        }

        #endregion
    }
}