using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using Kokugen.Core;
using Kokugen.Web.Behaviors;
using FubuMVC.UI;
using Kokugen.WebBackend.Conventions;
using Kokugen.WebBackend.Handlers;
using Kokugen.WebBackend.Handlers.Home;
using Kokugen.WebBackend.ViewModels;

namespace Kokugen.Web
{
    public class KokugenFubuRegistry : FubuRegistry
    {

        public KokugenFubuRegistry(bool enableDiagnostics, string controllerAssemblyName)
        {
            IncludeDiagnostics(enableDiagnostics);

            Applies.ToThisAssembly();
            Applies.ToAssemblyContainingType<HandlerUrlPolicy>();

            Actions
                .IncludeTypes(t => t.Namespace.StartsWith(typeof(HandlerUrlPolicy).Namespace) && t.Name.EndsWith("Handler"))
                .IncludeMethods(action => action.Method.Name == "Execute");

            
            this.HtmlConvention(new KokugenHtmlConventions());

            HomeIs<IndexHandler>(x => x.Execute());

            Routes.UrlPolicy<HandlerUrlPolicy>();

            
            
            //Policies.WrapBehaviorChainsWith<TransactionalContainerBehavior>();
            
            Output.ToJson.WhenCallMatches(action => action.Returns<AjaxResponse>());

            Views.TryToAttach(x =>
                                  {
                                      x.by(new KokugenViewAttachmentStrategy());
                                      x.by_ViewModel_and_Namespace_and_MethodName();
                                      x.by_ViewModel_and_Namespace();
                                      x.by_ViewModel();
                                  });
        }
    }

    public class KokugenViewAttachmentStrategy : IViewsForActionFilter
    {
        public IEnumerable<IViewToken> Apply(ActionCall call, ViewBag views)
        {
            return
                views
                    .ViewsFor(call.OutputType())
                    .Where(view => view.ViewType.Name == call.Method.Name
                                   && view.ViewType.Namespace == call.HandlerType.Namespace)
                    .Select(view => view);
        }
    }
}