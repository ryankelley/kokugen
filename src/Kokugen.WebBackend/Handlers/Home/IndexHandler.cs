using Kokugen.WebBackend.ViewModels;

namespace Kokugen.WebBackend.Handlers.Home
{
    public class IndexHandler
    {
        public HomeViewModel Execute()
        {
            return new HomeViewModel
                       {
                           Text = "Hello, world."
                       };
        }
    }
}