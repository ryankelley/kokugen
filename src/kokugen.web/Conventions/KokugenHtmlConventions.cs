using System;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using HtmlTags;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Validation;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Project;
using Kokugen.Web.Conventions.Builders;

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

            

            BeforePartial.Builder(new BeforePartialBuilder());
            AfterPartial.Builder(new AfterPartialBuilder());

            BeforeEachOfPartial.Builder<BeforeEachOfPartialBuilder>();
            BeforeEachOfPartial.Modifier<OddEvenLiModifier>();
            BeforeEachOfPartial.Modifier<FixedItemBoardModifier>();
            BeforeEachOfPartial.Modifier<BoardColumnIDAdder>();
            BeforeEachOfPartial.If(x => x.ModelType == typeof(ProjectListModel)).Modify(x => x.AddClass("project"));
            BeforeEachOfPartial.If(x => x.ModelType == typeof(BoardConfigurationModel)).Modify(x => x.AddClass("phase"));
            
            Profile("inplace", x=> x.Editors.Builder<EditInPlaceBuilder>());

            //BeforeEachOfPartial.If(x => x.Accessor.)

            //BeforeEachOfPartial.If(x => x.Is<ProjectListModel>()).Modify();
            AfterEachOfPartial.Builder<AfterEachOfPartialBuilder>();

        }

        private void editors()
        {
            Editors.Builder<ValueObjectDropdownBuilder>();
            Editors.IfPropertyIs<bool>().BuildBy(request => new CheckboxTag(request.Value<bool>()).Style("width", "auto !important").Attr("value", request.ElementId));
            Editors.IfPropertyIs<Guid>().BuildBy(request => new HiddenTag().Attr("value", request.StringValue()));
            Editors.If(x => x.Accessor.PropertyType.IsType<DateTime>() || x.Accessor.PropertyType.IsType<DateTime?>()).Modify(tag => tag.AddClass("datepicker"));

            Editors.If(x => x.Accessor.OwnerType.IsType<Card>() && x.Accessor.Name == "CardTitle").BuildBy(request => new HtmlTag("textarea").Attr("name", request.ElementId).Text(request.StringValue()));
            //Editors.Builder(new FormItemBuilder());

            Editors.If(x => x.Accessor.FieldName.ToLower().Contains("password"))
                .BuildBy(build => new HtmlTag("input").Attr("type", "password"));
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
            Editors.IfPropertyTypeIs(IsIntegerBased).AddClass("digits");
            Editors.IfPropertyTypeIs(IsFloatingPoint).AddClass("number");
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

        private static bool IsIntegerBased(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        private static bool IsFloatingPoint(Type type)
        {
            return type == typeof(decimal) || type == typeof(float) || type == typeof(double);
        }
    }

    public class EditInPlaceBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return true;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var tag = new HtmlTag("span").Text(request.StringValue()).AddClass("editable").Id(request.ElementId.Replace(request.Model.GetType().Name,""));

            var multiLine = request.Accessor.Name.Contains("Detail") ? "MultiLine:'true'" : "";
                

            tag.Attr("data", "{editoptions:{EntityId:'',SaveUrl:'',RequiresExplicitUserActionForSave:'true', "+ multiLine+"}}");
            return tag;
        }
    }
}