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
            RegisteredEndPoints.First (e => e is Light).Name = "LampToilet";
            RegisteredEndPoints.First (e => e is PulseSwitch).Name = "KnopToilet";

                //= new List<IEndpoint>
            //                          {
            //                              new PulseSwitch(this)
            //                                  {
            //                                      Name = "KnopToilet"
            //                                  },
            //                              new Light(this)
            //                                  {
            //                                      Name = "LampToilet"
            //                                  }
            //                          };

            Rules = new List<IRule>
                        {
                            new BasicRule
                                {
                                    Hub = this,
                                    Friends = new List<IEndpoint> { RegisteredEndPoints.First(e => e is PulseSwitch), RegisteredEndPoints.First(e => e is Light) }
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