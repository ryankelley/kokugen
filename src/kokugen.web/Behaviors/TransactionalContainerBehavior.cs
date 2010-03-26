using System;
using FubuMVC.Core.Behaviors;
using FubuCore.Binding;
using Kokugen.Core;
using StructureMap;

namespace Kokugen.Web.Behaviors
{
    public class TransactionalContainerBehavior : IActionBehavior
    {
        private readonly IContainer _container;
        private readonly ServiceArguments _arguments;
        private readonly Guid _behaviorId;

        public TransactionalContainerBehavior(IContainer container, ServiceArguments arguments, Guid behaviorId)
        {
            _container = container;
            _arguments = arguments;
            _behaviorId = behaviorId;

        }

        public void Invoke()
        {
            _container.ExecuteInTransaction(c =>
                                                {
                                                    //c.GetInstance<IAuthenticationService>().SetupAuthenticationContext();
                                                    invokeRequestedBehavior(c);
                                                });
        }

        public void InvokePartial()
        {
            
        }


        private void invokeRequestedBehavior(IContainer c)
        {

            var behavior = c.GetInstance<IActionBehavior>(_arguments.ToExplicitArgs(), _behaviorId.ToString());

            behavior.Invoke();

        }

    }
}