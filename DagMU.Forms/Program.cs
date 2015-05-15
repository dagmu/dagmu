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
#if DEBUG
			Task.Run(() => new Server(new Server.Options() {
				echo = true,
			}).Start());
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
