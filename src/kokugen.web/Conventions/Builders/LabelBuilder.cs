using FubuMVC.UI.Configuration;
using HtmlTags;

namespace Kokugen.Web.Conventions.Builders
{
    public class LabelBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return true;
        }

        public override HtmlTag Build(ElementRequest request)
        {

            var elementId = request.Accessor.Name.BuildHtmlID();

            var label = new HtmlTag("label").Attr("for", elementId).Id(elementId + "-label").Text(request.Accessor.Name.SplitCamelCase());

            return label;
        }


    }
}