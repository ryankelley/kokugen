namespace Kokugen.Web.Actions.Home
{
    public class IndexAction
    {
        public HomeViewModel Query()
        {
            return new HomeViewModel
                       {
                           Text = "Hello, world."
                       };
        }
    }
}