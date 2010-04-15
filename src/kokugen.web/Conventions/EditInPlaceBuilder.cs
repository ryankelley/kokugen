using FubuCore.Reflection;
using FubuMVC.UI.Configuration;
using HtmlTags;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Validation;
using MarkdownSharp;

namespace Kokugen.Web.Conventions
{
    public class EditInPlaceBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return true;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var tag = new HtmlTag("div").Text(request.StringValue()).AddClass("editable").Id(request.Accessor.Name);

            var options = new EditOptions();

            if (request.Accessor.HasAttribute<MarkdownAttribute>())
            {
                tag.Text(new Markdown().Transform(request.RawValue.ToString()));
            }

            options.MultiLine = request.Accessor.Name == "Details";
            options.RequiresExplicitUserActionForSave = true;
            
            options.MaximumLength = request.Accessor.PropertyType.Equals(typeof(string)) ? Entity.UnboundedStringLength : 0;
            options.IsDate = request.Accessor.PropertyType.IsDateTime();
            options.IsTime = request.Accessor.Name.ToLower().Contains("time");
            options.IsNumber = request.Accessor.PropertyType.IsIntegerBased() || request.Accessor.PropertyType.IsFloatingPoint();
            options.Required = request.Accessor.HasAttribute<RequiredAttribute>();
            options.PlaceholderText = "Double-Click to edit " + request.Accessor.Name.ToLower() + ".";

            var data = options.ToJson();    

            tag.Attr("data", "{editoptions:"+data+"}");
            return tag;
        }
    }
}