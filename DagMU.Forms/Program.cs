using System;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;

using DagMUServer;

namespace DagMU.Forms
{
    static partial class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			Task.Run(() => new Server().Start());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
