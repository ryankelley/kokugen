using System;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using HtmlTags;
using Kokugen.Core.Domain;
using Kokugen.Core.Validation;

namespace Kokugen.Web.Conventions
{
    public class KokugenHtmlConventions : HtmlConventionRegistry
    {
        public KokugenHtmlConventions()
        {
            Labels.Builder(new LabelBuilder());
            numbers();
            validationAttributes();
            editors();
            
        }

        private void editors()
        {
            Editors.Builder<ValueObjectDropdownBuilder>();
            Editors.IfPropertyIs<bool>().BuildBy(request => new CheckboxTag(request.Value<bool>()).Style("width", "auto !important").Attr("value", request.ElementId));
            //Editors.Builder(new FormItemBuilder());
        }
 
        // Setting up rules for tagging elements with jQuery validation
        // metadata
        // I think that a lot of this gets added into the core Fubu as a
        // "jQueryValidationPack"
        private void numbers()
        {
            Editors.IfPropertyIs<Int32>().Attr("max", Int32.MaxValue);
            Editors.IfPropertyIs<Int16>().Attr("max", Int16.MaxValue);
            Editors.IfPropertyIs<Int64>().Attr("max", Int64.MaxValue);
            Editors.IfPropertyTypeIs(IsIntegerBased).AddClass("integer");
            Editors.IfPropertyTypeIs(IsFloatingPoint).AddClass("number");
        }
 
        // Declare policies for using validation attributes
        private void validationAttributes()
        {
            Editors.AddClassForAttribute<RequiredAttribute>("required");
            Editors.ModifyForAttribute<MaximumStringLengthAttribute>((tag, att) =>
                                                                         {
                                                                             if (att.Length <Entity.UnboundedStringLength)
                                                                             {
                                                                                 tag.Attr("maxlength", att.Length);
                                                                             }
                                                                         });
 
 
            Editors.ModifyForAttribute<GreaterOrEqualToZeroAttribute>(tag => tag.Attr("min", 0));
            Editors.ModifyForAttribute<GreaterThanZeroAttribute>(tag => tag.Attr("min", 1));
        }

        private static bool IsIntegerBased(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        private static bool IsFloatingPoint(Type type)
        {
            return type == typeof(decimal) || type == typeof(float) || type == typeof(double);
        }
    }

    public class LabelBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return true;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            
            var elementId = request.Accessor.Name.BuildHtmlID();

            var label = new HtmlTag("label").Attr("for", elementId).Id(elementId+"-label").Text(request.Accessor.Name.SplitCamelCase());

            return label;
        }

        
    }
}