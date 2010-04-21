using FubuMVC.UI.Configuration;
using HtmlTags;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Board.Configure;
using Kokugen.Web.Actions.Card.Lists;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Conventions.Builders
{
    public class BeforeEachOfPartialBuilder : EachOfPartialBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.ModelType == typeof(ProjectListModel) ||
                def.ModelType == typeof(BoardConfigurationModel) ||
                def.ModelType == typeof(CardListModel);
        }

        public override HtmlTag Build(ElementRequest request, int index, int total)
        {
            var tag = new HtmlTag("li").NoClosingTag();
            return tag;
        }
    }
}