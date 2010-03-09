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
            numbers();
            validationAttributes();
            editors();
            Editors.Builder<FormItemBuilder>();
        }

        private void editors()
        {
            Editors.IfPropertyIs<bool>().BuildBy(request => new CheckboxTag(request.Value<bool>()).Style("width", "auto !important").Attr("value", request.ElementId));
            Editors.Builder(new FormItemBuilder());
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

    public class FormItemBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(string);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var elementId = buildId(request.Accessor.Name);

            var label = new HtmlTag("label").Attr("for", elementId).Id(elementId+"-label").Text(request.Accessor.Name.SplitCamelCase());

            var tag = new TextboxTag(request.Accessor.FieldName, request.RawValue == null ? "" : request.RawValue.ToString()).Id(elementId);
            var divWrapper = new DivTag(request.Accessor.Name).AddClass("form-item");
            divWrapper.Child(label);
            divWrapper.Child(tag);
            return divWrapper;
        }

        private static string buildId(string name)
        {
            var splitName = name.SplitCamelCase();
            var parts = splitName.Split(' ');
            var output = string.Empty;
            var isFirst = true;
            foreach (var s in parts)
            {
                if (!isFirst)
                    output += "-";
                output += s.ToLower();
                isFirst = false;
            }
            return output;
        }
    }
}