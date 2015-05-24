using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagMU.Forms
{
	public static class Extensions
	{
		public static System.Windows.Media.Brush ToWPFBrush(this System.Drawing.Color value)
		{
			return new System.Windows.Media.SolidColorBrush(value.ToWPFColor());
		}

		public static System.Windows.Media.Color ToWPFColor(this System.Drawing.Color value)
		{
			return System.Windows.Media.Color.FromArgb(value.A, value.R, value.G, value.B);
		}

		public static System.Drawing.Color ToFormsColor(this System.Windows.Media.Color value)
		{
			return System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
		}

		public static System.Drawing.Color ToFormsColor(this System.Windows.Media.Brush value)
		{
			if (value is System.Windows.Media.SolidColorBrush) {
				return (value as System.Windows.Media.SolidColorBrush).Color.ToFormsColor();
			} else {
				throw new NotImplementedException();
			}
		}

		public static System.Windows.Forms.Keys ToForms(this System.Windows.Input.Key value)
		{
			return (System.Windows.Forms.Keys)System.Windows.Input.KeyInterop.VirtualKeyFromKey(value);
		}

		public static System.Windows.Forms.MouseEventArgs ToForms(this System.Windows.Input.MouseWheelEventArgs value)
		{
			return new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, 0, 0, value.Delta);
		}
	}
}
