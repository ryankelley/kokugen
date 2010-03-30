using FubuMVC.Core.View;

namespace Kokugen.Web.Actions.Board
{
    public class List : FubuPage<BoardListModel> { }
    public class Configure : FubuPage<BoardConfigurationModel> { }

    public class EditBoardColumnForm : FubuPage<BoardColumnEditModel> {}

    public class BoardItem_Control : FubuControl<Core.Domain.BoardColumn> { }
}