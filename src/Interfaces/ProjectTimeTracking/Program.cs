using System;
using System.Windows.Forms;
using StructureMap;
namespace ProjectTimeTracking
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Bootstrapper.Bootstrap();
            Application.ThreadException += Application_ThreadException;
            var timeTracker = ObjectFactory.GetInstance<ProjectTimeTracker>();
            
            Application.Run(timeTracker);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }
    }
}