using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
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

        public static HtmlTag Show<T>(this IFubuPage<T> page, Expression<Func<T, object>> expression) where T : class
        {
            var divWrapper = new HtmlTag("div").AddClass("show-item");
            var accesor = expression.ToAccessor();
            var label = new HtmlTag("label").Text(accesor.FieldName.SplitCamelCase());
            var span = page.DisplayFor(expression);

            divWrapper.Child(label);
            divWrapper.Child(span);

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

        /// <summary>
        /// Adds attributes to the HTML for use with jQuery and the "Hint" function which puts the text specified
        /// into the textbox until you click in it.
        /// Sets the title attribute
        /// Sets the _hint attribute
        /// </summary>
        /// <param name="hintText">Text that will display inside hint</param>
        /// <returns></returns>
        public static HtmlTag Hint(this HtmlTag tag, string hintText)
        {
            tag.AddClass("hint");
            tag.Attr("title", hintText);
            tag.Attr("_hint", hintText);

            return tag;
        }
    }
}