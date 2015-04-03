using System;

namespace DagMU.HelperWindows
{
	public partial class ConsoleWindow : HelperWindow
	{
		public ConsoleWindow()
		{
			InitializeComponent();
		}

        delegate void AddLineCallback(string text);

		/// <summary>
		/// Add text to the console window
		/// </summary>
		public void Print(String s)
		{
            if (this.textBox1.InvokeRequired)
            {
                World.StringDelegate callback = new World.StringDelegate(Print);
                this.Invoke(callback, s);
                return;
            }

            textBox1.AppendText(s);
            textBox1.ScrollToCaret();
		}

		void textBox1_TextChanged(object sender, EventArgs e)
		{
		}
	}
}
