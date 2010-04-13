using FubuMVC.Core.View;

namespace Kokugen.Web.Actions.Card
{
    public class Get : FubuPage<CardDetailModel>{}
    public class List : FubuPage<CardListModel>{}
    public class CardForm : FubuPage<CardInputFormModel>{}
    public class Add : FubuPage<CardInputFormModel> {}
    public class CompactCardForm : FubuPage<CompactCardFormModel> { }
}