using System;
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

        public static HtmlTag Build(string listName, string @for, Func<IEnumerable<ValueObject>> giveMeTheList)
        {
            return new SelectTag(tag =>
                                     {
                                         tag.Attr("name", @for);
                                         tag.TopOption(string.Format("-- Select {0} --", listName), null);
                                         giveMeTheList().Each(vo => tag.Option(vo.Value, vo.Key));
                                     });
        }
    }
}