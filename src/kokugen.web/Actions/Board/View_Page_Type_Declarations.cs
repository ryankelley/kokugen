using FubuMVC.Core.View;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Actions.Board
{
    public class List : FubuPage<BoardListModel> { }

    public class BoardItem_Control : FubuControl<Kokugen.Core.Domain.CustomBoardColumn> { }
}