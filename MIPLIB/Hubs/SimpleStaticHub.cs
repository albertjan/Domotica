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
            RegisteredEndPoints = endpoints;

            var e1 = RegisteredEndPoints.First (e => e is PulseSwitch);

            e1.Name = "KnopToilet";

            //RegisteredEndPoints.First (e => e is Light).Name = "LampToilet";

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

        public IEnumerable<IEndpoint> RegisteredEndPoints { get; set; }

        public IList<IRule> Rules { get; set; }

        public void Trigger(IEndpoint endpoint)
        {
            var actions = Rules.Where(r => r.HasFriend(endpoint)).Select(rule => rule.FireWithInput(endpoint));
        }

        #endregion
    }
}