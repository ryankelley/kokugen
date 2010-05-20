using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using Kokugen.Core.Membership;

namespace Kokugen.Web.Behaviors
{
    public class AuthenticationBehaviorPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            //var myBehavs = graph.Behaviors.Where(c => c.FirstCall().Category == BehaviorCategory.Call).ToList();
            var myActions = graph.Actions().Where(c => c.Category == BehaviorCategory.Call).ToList();
            myActions.Each(act =>
                               {
                                   if (SecurityProvider.HasPermissionForMethod(act.HandlerType, act.Method))
                                       act.AddBefore(new BehaviorCall(act));
                               });
        }
    }
}