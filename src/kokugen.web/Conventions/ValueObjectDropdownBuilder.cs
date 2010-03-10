using System;
using FubuMVC.Core;
using FubuMVC.Core.Util;
using FubuMVC.UI.Configuration;
using HtmlTags;
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

            var defaultValue = request.Value<string>();


            if (defaultValue.IsEmpty())
            {

                request.ForListName(name =>
                {

                    ValueObject @default = ValueObjectRegistry.FindDefault(name);

                    if (@default != null) defaultValue = @default.Key;

                });

            }



            return new SelectTag(tag =>
            {

                request.EachValueObject(
                    vo => tag.Option(vo.LocalizedText(), vo.Key)
                    );

                tag.SelectByValue(defaultValue);

            });

        }

        public static HtmlTag Build(string listName)
        {

            return new SelectTag(tag =>
            {

                ValueObjectRegistry.GetAllActive(listName).Each(x => tag.Option(x.LocalizedText(), x.Key));



                var defaultVO = ValueObjectRegistry.FindDefault(listName);

                if (defaultVO != null)
                {

                    tag.SelectByValue(defaultVO.Key);

                }

            });

        }
    }

    public static class ValueObjectDropdownExtensions 
    {
        

        public static void ForListName(this ElementRequest request, Action<string> action)
        {
            throw new NotImplementedException();
        }

        public static void EachValueObject(this ElementRequest request, Action<ValueObject> tag )
        {
            throw new NotImplementedException();
        }
    }
}