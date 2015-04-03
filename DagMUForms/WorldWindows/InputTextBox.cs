using System.Drawing;
using System.Windows.Forms;

namespace DagMU
{
	public partial class InputTextBox : TextBox
	{
		public InputTextBox()
		{
			//SetStyle(ControlStyles.UserPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle borderRectangle = this.ClientRectangle;
			borderRectangle.Inflate(-1, -1);
			ControlPaint.DrawBorder(e.Graphics, borderRectangle, Color.Blue, ButtonBorderStyle.Solid);
		}
	}
}
