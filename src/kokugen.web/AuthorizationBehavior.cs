using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using Kokugen.Core;
using Kokugen.Web.Behaviors;

namespace Kokugen.Web
{
    public class AuthorizationBehavior : IConfigurationAction
    {
        private List<BehaviorChain> _chains;
        private List<ActionCall> _calls = new List<ActionCall>();

        public void Configure(BehaviorGraph graph)
        {
            var actions = graph.Actions();
           actions.Each(call => call.AddBefore(Wrapper.For<MustBeAuthorizedBehavior>()));


        }

        private void modifyChain(BehaviorChain chain, IConfigurationObserver observer)
        {
            foreach (var c in chain)
            {
                c.AddBefore(Wrapper.For<MustBeAuthorizedBehavior>());
            }
        }
    }
}