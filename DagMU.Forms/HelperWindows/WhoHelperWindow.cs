using System;
using System.Windows.Forms;

namespace DagMU.Forms.HelperWindows
{
    public partial class WhoHelperWindow : Form
    {
        public WhoHelperWindow()
        {
            InitializeComponent();
        }

        public void starting()
        {
			textBox1.Text = "starting\r\n";
			SuspendLayout();
        }

		public void done()
		{
			textBox1.AppendText("\r\nDone");
			ResumeLayout(false);
		}

		public void newline(string s)
		{
			textBox1.AppendText("\r\n" + s);
		}

		void textBox1_TextChanged(object sender, EventArgs e)
		{
			textBox1.ScrollToCaret();
		}
    }
}
