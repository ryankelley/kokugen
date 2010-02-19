using System;
using FubuMVC.Core;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using HtmlTags;
using Kokugen.Core;
using Kokugen.Core.Validation;

namespace Kokugen.WebBackend.Conventions
{
    // HtmlConventionRegistry is the base class that provides the DSL
    // This is an example of "Object Scoping" from Fowler speak like
    // StructureMap's Registry and Fluent NHibernate's ClassMap
    public class KokugenHtmlConventions : HtmlConventionRegistry
    {
        public KokugenHtmlConventions()
        {
            validationAttributes();
            numbers();
 
            //Profile("edit", x => x.Editors.Builder<EditInPlaceBuilder>());
 
            editors();
 
            //Labels.Builder<LabelBuilder>();
 
            //Displays.Builder<LogDateDisplay>();
            //Displays.Builder<DisplayBuilder>();
 
        }

        private void editors()
        {
            Editors.Builder<RyansTestBuilder>();
            //Editors.Builder<ValueObjectDropdownBuilder>();
            Editors.IfPropertyIs<bool>().BuildBy(request => new CheckboxTag(request.Value<bool>()).Style("width", "auto !important").Attr("value", request.ElementId));
            
            Editors.Always.Modify((request, tag) =>
            {
                //tag.Attr("label", request.Header());
                tag.Attr("label", request.ElementId);
                tag.Attr("name", request.ElementId);
            });
 
            // Ugly hack because of the hacky Edit in Place jQuery plugin we use, but
            // I'm gonna kill it some day
            Editors.IfPropertyTypeIs(t => t.IsDateTime()).Modify(x =>
            {
                //if (!x.HasMetaData(EditInPlaceBuilder.EDITABLE_ATTRIBUTE_NAME))
                //{
                //    x.AddClass("DatePicker");
                //}
            });
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
            Editors.IfPropertyTypeIs(t => t.IsIntegerBased()).AddClass("integer");
            Editors.IfPropertyTypeIs(t => t.IsFloatingPoint()).AddClass("number");
        }
 
        // Declare policies for using validation attributes
        private void validationAttributes()
        {
            Editors.AddClassForAttribute<RequiredAttribute>("required");
            Editors.ModifyForAttribute<MaximumStringLengthAttribute>((tag, att) =>
            {
                if (att.Length < Entity.UnboundedStringLength)
                {
                    tag.Attr("maxlength", att.Length);
                }
            });
 
 
            Editors.ModifyForAttribute<GreaterOrEqualToZeroAttribute>(tag => tag.Attr("min", 0));
            Editors.ModifyForAttribute<GreaterThanZeroAttribute>(tag => tag.Attr("min", 1));
        }
    }

    public class RyansTestBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (string);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            string title = request.Accessor.InnerProperty.Name;

            var divTag = new HtmlTag("div").AddClass("form-item")
                .Child(new HtmlTag("label").Text(request.Accessor.InnerProperty.Name).Attr("for", request.ElementId));

            var inputTag = new TextboxTag(request.Accessor.InnerProperty.Name, "").Id(request.ElementId);
            divTag.Child(inputTag);

            return divTag;
        }
    }
}