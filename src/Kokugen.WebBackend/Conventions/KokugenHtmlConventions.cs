using System;
using System.Linq;
using System.Text;
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

            var parts = BreakUpperCase(request.ElementId);

            string id = string.Empty;
            string label = string.Empty;
            bool isFirst = true;

            foreach (var s in parts)
            {
                if(!isFirst)
                {
                    label += " ";
                    id += "-";
                    
                }
                label += s;
                id += s.ToLower();

                isFirst = false;
            }
            


            var divTag = new HtmlTag("div").AddClass("form-item")
                .Child(new HtmlTag("label", x => x.Id(id + "-label").Text(label).Attr("for", id)));

            var inputTag = new TextboxTag(request.ElementId, "").Id(id);
            divTag.Child(new HtmlTag("br")).Child(inputTag);

            return divTag;
        }

        public string[] BreakUpperCase(string sInput)
        {
            StringBuilder[] sReturn = new StringBuilder[1];
            sReturn[0] = new StringBuilder(sInput.Length);
            const string CUPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int iArrayCount = 0;
            for (int iIndex = 0; iIndex < sInput.Length; iIndex++)
            {
                string sChar = sInput.Substring(iIndex, 1); // get a char
                if ((CUPPER.Contains(sChar)) && (iIndex > 0))
                {
                    iArrayCount++;
                    System.Text.StringBuilder[] sTemp = new System.Text.StringBuilder[iArrayCount + 1];
                    Array.Copy(sReturn, 0, sTemp, 0, iArrayCount);
                    sTemp[iArrayCount] = new StringBuilder(sInput.Length);
                    sReturn = sTemp;
                }
                sReturn[iArrayCount].Append(sChar);
            }
            string[] sReturnString = new string[iArrayCount + 1];
            for (int iIndex = 0; iIndex < sReturn.Length; iIndex++)
            {
                sReturnString[iIndex] = sReturn[iIndex].ToString();
            }
            return sReturnString;
        }
    }
}