using System;
using System.Linq.Expressions;
using FubuMVC.Core.Util;
using FubuMVC.Core.View;
using FubuMVC.UI;
using HtmlTags;

namespace Kokugen.Web.Conventions
{
    public static class HtmlExtensions
    {
        public static HtmlTag Edit<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression) where T : class
        {
            var divWrapper = new HtmlTag("div").AddClass("form-item");

            var label = page.LabelFor(expression);
            var textbox = page.InputFor(expression).Id(label.Attr("for"));

            divWrapper.Child(label);
            divWrapper.Child(textbox);

            return divWrapper;




        }

        public static string BuildHtmlID(this string name)
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