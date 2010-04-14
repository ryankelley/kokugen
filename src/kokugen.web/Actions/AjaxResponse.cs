namespace Kokugen.Web.Actions
{
    public class AjaxResponse
    {
        public bool Success { get; set; }
        public object Item { get; set; }
    }

    public class InPlaceAjaxResponse
    {
        public bool success { get; set; }
        public object NewValueToDisplay { get; set; }
    }
}