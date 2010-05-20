using FubuMVC.Core.View;
using Kokugen.Core.Services;

namespace Kokugen.Web.Actions.Card
{
    public class Get : FubuPage<CardDetailModel>{}
    public class CardForm : FubuPage<CardInputFormModel>{}
    public class Add : FubuPage<CardInputFormModel> {}
    public class CompactCardForm : FubuPage<CompactCardFormModel> { }
    public class CardProgress : FubuPage<CardProgressModel>{}
    public class CardTask_Item : FubuControl<TaskDTO> {}
    
}