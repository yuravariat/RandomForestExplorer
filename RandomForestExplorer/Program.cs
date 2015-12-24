using System;
using System.Windows.Forms;
using RandomForestExplorer.Data;

namespace RandomForestExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
        }

        private static void Bootstrap()
        {
            var model = new DataModel();
            var view = new Explorer(model);
            var controller = new ExplorerController(view, model);
            Application.Run(view);
            controller.Dispose();
        }
    }
}
