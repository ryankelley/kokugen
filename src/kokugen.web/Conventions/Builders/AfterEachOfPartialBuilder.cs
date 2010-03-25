using FubuMVC.UI.Configuration;
using HtmlTags;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Conventions.Builders
{
    public class AfterEachOfPartialBuilder : EachOfPartialBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.ModelType == typeof(ProjectListModel);
        }

        public override HtmlTag Build(ElementRequest request, int index, int total)
        {
            return new HtmlTag("/li").NoClosingTag();
        }
    }
}