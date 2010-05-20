using System;
using System.Xml;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration.DSL;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Urls;
using Kokugen.Core.Membership;
using Kokugen.Web.Actions;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Errors;
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
                x.IfIsType<DateTime>().ConvertBy(d => d.ToString("g"));
                x.IfIsType<decimal>().ConvertBy(d => d.ToString("N2"));
                x.IfIsType<float>().ConvertBy(f => f.ToString("N2"));
                x.IfIsType<double>().ConvertBy(d => d.ToString("N2"));
            });

            // Configure Permissions
            SecurityProvider.Configure(new KokugenSecurityRegistry());


            Policies.WrapBehaviorChainsWith<load_the_current_principal>();
            Policies.Add<AuthenticationBehaviorPolicy>();

            Services(x =>
                         {
                             x.SetServiceIfNone(typeof(IXMLWriter), typeof(XMLWriter));
                         });

            //Policies.WrapBehaviorChainsWith<MustBeAuthorizedBehavior>();
            //Policies.ConditionallyWrapBehaviorChainsWith<MustBeAuthorizedBehavior>(c => c.OutputType() == typeof (BoardConfigurationModel));
            
            //Policies.WrapBehaviorChainsWith<MustBeAuthorizedBehavior>();
            
            Output.ToJson.WhenCallMatches(action => action.Returns<AjaxResponse>());
            Output.ToJson.WhenCallMatches(action => action.Returns<InPlaceAjaxResponse>());
            //Output.ToXml().WhenCallMatches(action => action.Returns<XmlResponse>());
            Output.To(call => new RenderXMLNode(call.OutputType())).WhenCallMatches(action => action.Returns<XmlResponse>());

            Views.TryToAttach(x =>
                                  {
                                      x.by_ViewModel_and_Namespace_and_MethodName();
                                      x.by_ViewModel_and_Namespace();
                                      x.by_ViewModel();
                                  });


            
            
        }
    }

    public static class FubuRegistryExtensions
    {
        public static ActionCallFilterExpression ToXml(this OutputDeterminationExpression expr)
        {

            return expr.To(call => new RenderXMLNode(call.OutputType()));
            
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