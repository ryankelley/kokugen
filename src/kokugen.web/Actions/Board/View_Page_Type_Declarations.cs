using FubuMVC.Core.View;

namespace Kokugen.Web.Actions.Board
{
    public class ViewBoard : FubuPage<ViewBoardModel> { }
    

    public class EditBoardColumnForm : FubuPage<BoardColumnEditModel> {}

    public class BoardItem_Control : FubuControl<Core.Domain.BoardColumn> { }
    public class BoardPhase_Control : FubuControl<BoardColumnDTO> { }
}