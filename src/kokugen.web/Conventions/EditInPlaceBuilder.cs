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
                tag.UnEncoded().Text(new Markdown().Transform(request.RawValue== null ? "" : request.RawValue.ToString()));
                options.Markdown = true;
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

    public class EditOptions
    {
        public string EntityId { get; set; }
        public string SaveUrl { get; set; }
        public bool RequiresExplicitUserActionForSave { get; set; }
        public bool MultiLine { get; set; }

        public int MaximumLength { get; set; }
        public bool Required { get; set; }
        //public int MinimumValue { get; set; }
        //public int MaximumValue { get; set; }
        public bool IsNumber { get; set; }
        public bool IsDate { get; set; }
        public bool IsTime { get; set; }
        public string SaveButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string PlaceholderText { get; set; }
        public bool Markdown { get; set; }
        public EditOptions()
        {
            SaveButtonText = "Save";
            CancelButtonText = "Cancel";
        }
    }
}