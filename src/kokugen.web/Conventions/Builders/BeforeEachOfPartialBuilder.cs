using FubuMVC.UI.Configuration;
using HtmlTags;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Conventions.Builders
{
    public class BeforeEachOfPartialBuilder : EachOfPartialBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.ModelType == typeof(ProjectListModel);
        }

        public override HtmlTag Build(ElementRequest request, int index, int total)
        {
            var tag = new HtmlTag("li").NoClosingTag().AddClass("project");
            return tag;
        }
    }
}