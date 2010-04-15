using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuCore.Reflection;
using FubuMVC.Core;

using FubuMVC.UI.Configuration;
using HtmlTags;
using Kokugen.Core;
using Kokugen.Core.Attributes;

namespace Kokugen.Web.Conventions
{
    public class ValueObjectDropdownBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.HasAttribute<ValueOfAttribute>();
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var attr = request.Accessor.GetAttribute<ValueOfAttribute>();

            var value = ValueObjectRegistry.GetValueObjectHolder(attr.Name);

            if (value == null) return new TextboxTag(request.ElementId, request.Value<string>());

            var defaultValue = "";
            if (request.RawValue != null)
            {
                defaultValue = request.RawValue.ToString();
            }
            if (defaultValue.IsEmpty())
            {
                ValueObject @default = value.Values.FirstOrDefault(x => x.IsDefault);
                if (@default != null) defaultValue = @default.Key;
            }

            return new SelectTag(tag =>
                                     {
                                         tag.TopOption(string.Format("-- Select {0} --", value.GetKey()), null);
                                         value.Values.Each(vo => tag.Option(vo.Value, vo.Key));
                                         tag.SelectByValue(defaultValue);
                                     });
        }

        public static HtmlTag Build(string listName)
        {
            //return new SelectTag(tag =>
            //{
            //    ValueObjectRegistry.GetAllActive(listName).Each(x => tag.Option(x.LocalizedText(), x.Key));

            //    var defaultVO = ValueObjectRegistry.FindDefault(listName);
            //    if (defaultVO != null)
            //    {
            //        tag.SelectByValue(defaultVO.Key);
            //    }
            //});
            return new HtmlTag("DIV");
        }
    }
}