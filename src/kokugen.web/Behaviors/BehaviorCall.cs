using System;
using System.Collections.Generic;
using System.Reflection;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace Kokugen.Web.Behaviors
{
    public class BehaviorCall : BehaviorNode
    {
        public ActionCall ActionCall { get; set; }

        public BehaviorCall(ActionCall actionCall)
        {
            ActionCall = actionCall;

        }

        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef
                       {
                           Dependencies = new List<IDependency>
                                              {
                                                  new ValueDependency()
                                                      {
                                                          DependencyType = typeof(ActionCall),
                                                          Value = this.ActionCall
                                                      }
                                              },
                                              Type = typeof(MustBeAuthorizedBehavior)
                       };
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }
    }

   
}