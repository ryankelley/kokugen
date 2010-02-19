using System.Web.UI;
using FubuMVC.Core.View;
using HtmlTags;

namespace Kokugen.Web
{
    public static class HtmlExtensions
    {
        public static HtmlTag TagFor(this Page page, string tag)
        {
            return new HtmlTag(tag);
        }
    }
}