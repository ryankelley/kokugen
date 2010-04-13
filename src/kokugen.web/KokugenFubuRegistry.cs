using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.DSL;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using Kokugen.Web.Actions;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Home;
using FubuMVC.UI;
using Kokugen.Web.Behaviors;
using Kokugen.Web.Conventions;

namespace Kokugen.Web
{
    public class KokugenFubuRegistry : FubuRegistry
    {
        public KokugenFubuRegistry(bool enableDiagnostics, string controllerAssemblyName)
        {
            IncludeDiagnostics(enableDiagnostics);

            Applies.ToThisAssembly();


            Actions
                 .IncludeTypesNamed(x => x.EndsWith("Action"));

            Routes
                .IgnoreNamespaceText(typeof(AjaxResponse).Namespace)
                .IgnoreControllerNamesEntirely()
                .IgnoreClassSuffix("Action")
                .IgnoreMethodsNamed("Execute")
                .IgnoreMethodSuffix("Command")
                .IgnoreMethodSuffix("Query")
                .IgnoreMethodSuffix("Remove")
                .ConstrainToHttpMethod(action => action.Method.Name.EndsWith("Command"), "POST")
                .ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Query"), "GET")
                .ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Remove"), "DELETE")
                .ForInputTypesOf<IRequestById>(x => x.RouteInputFor(request => request.Id).DefaultValue = "");

            this.UseDefaultHtmlConventions();
            this.HtmlConvention(new KokugenHtmlConventions());

#if RELEASE
            HomeIs<IndexAction>(x => x.Query());
#endif

            this.StringConversions(x =>
            {
                x.IfIsType<DateTime>(d => d.ToString("g"));
                x.IfIsType<decimal>(d => d.ToString("N2"));
                x.IfIsType<float>(f => f.ToString("N2"));
                x.IfIsType<double>(d => d.ToString("N2"));
            });

            Policies.Add<TestBehaviorPolicy>();
            //Policies.WrapBehaviorChainsWith<MustBeAuthorizedBehavior>();
            //Policies.ConditionallyWrapBehaviorChainsWith<MustBeAuthorizedBehavior>(c => c.OutputType() == typeof (BoardConfigurationModel));
            
            //Policies.WrapBehaviorChainsWith<MustBeAuthorizedBehavior>();
            
            Output.ToJson.WhenCallMatches(action => action.Returns<AjaxResponse>());

            Views.TryToAttach(x =>
                                  {
                                      x.by_ViewModel_and_Namespace_and_MethodName();
                                      x.by_ViewModel_and_Namespace();
                                      x.by_ViewModel();
                                  });


            
            
        }
    }

    public class TestBehaviorPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var myBehavs = graph.Behaviors.Where(c => c.FirstCall().Category == BehaviorCategory.Call).ToList();
            var myActions = graph.Actions().Where(c => c.Category == BehaviorCategory.Call).ToList();
            myActions.Each(act => act.AddBefore(new AuthorizationWrapper(act)));// Wrapper.For<MustBeAuthorizedBehavior>()));
            //graph.Behaviors.Where(c => c.FirstCall().Category == BehaviorCategory.Call).Each(c => c.Prepend(new Wrapper(typeof(MustBeAuthorizedBehavior)))); 
        }
    }

    public class AuthorizationWrapper : BehaviorNode
    {
        public AuthorizationWrapper(object act)
        {
            
        }

        protected override ObjectDef buildObjectDef()
        {
            return new ObjectDef(typeof(MustBeAuthorizedBehavior));
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Wrapper; }
        }


    }

    //public class KokugenViewAttachmentStrategy : IViewsForActionFilter
    //{
    //    public IEnumerable<IViewToken> Apply(ActionCall call, ViewBag views)
    //    {
    //        return
    //            views
    //                .ViewsFor(call.OutputType())
    //                .Where(view => view.ViewType.Name == call.Method.Name
    //                               && view.ViewType.Namespace == call.HandlerType.Namespace)
    //                .Select(view => view);
    //    }
    //}
    
}