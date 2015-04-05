using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DagMU
{
	class Box : RichTextBox
	{
		public void Add(string s)
		{
			//if something is selected or if scrolled up, else autoscroll
			bool autoscroll = this.SelectionLength == 0 && IsAtMaxScroll();

			//if (!autoscroll) this.SuspendPainting();
			this.AppendText("\n" + s);
			//if (!autoscroll) this.ResumePainting();

			this.ScrollToCaret();
		}

		public event EventHandler ScrolledToBottom;

		//thanks to http://stackoverflow.com/a/6550415/3320154 for Suspend/ResumePainting for stealth mode appendtext
		//thanks to http://stackoverflow.com/a/10238729/3320154 for IsAtMaxScroll/OnScrolledToBottom
		//thanks to http://stackoverflow.com/a/10646424/3320154 for VerticalScroll get/set

		Point _ScrollPoint;
		bool _Painting = true;
		IntPtr _EventMask;
		int _SuspendIndex = 0;
		int _SuspendLength = 0;

		public int VerticalScroll
		{
			get { return GetScrollPos((IntPtr)this.Handle, SB_VERT); }
			set { SetScrollPos((IntPtr)this.Handle, SB_VERT, value, true); }
		}

		public void VerticalScrollRange(out int minScroll, out int maxScroll)
		{
			GetScrollRange(this.Handle, SB_VERT, out minScroll, out maxScroll);
		}

		public void SuspendPainting()
		{
			if (_Painting) {
				_SuspendIndex = this.SelectionStart;
				_SuspendLength = this.SelectionLength;
				SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref _ScrollPoint);
				SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
				_EventMask = SendMessage(this.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
				_Painting = false;
			}
		}

		public void ResumePainting()
		{
			if (!_Painting) {
				this.Select(_SuspendIndex, _SuspendLength);
				SendMessage(this.Handle, EM_SETSCROLLPOS, 0, ref _ScrollPoint);
				SendMessage(this.Handle, EM_SETEVENTMASK, 0, _EventMask);
				SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
				_Painting = true;
				this.Invalidate();
			}
		}

		public bool IsAtMaxScroll()
		{
			int minScroll, maxScroll;
			VerticalScrollRange(out minScroll, out maxScroll);

			Point rtfPoint = Point.Empty;
			SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);
			return (rtfPoint.Y + this.ClientSize.Height >= maxScroll);
		}

		protected virtual void OnScrolledToBottom(EventArgs e)
		{
			if (ScrolledToBottom != null)
				ScrolledToBottom(this, e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			NotSureIfScrolled();

			base.OnKeyUp(e);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
				NotSureIfScrolled();

			base.WndProc(ref m);
		}

		private void NotSureIfScrolled()
		{
			bool isAtMaxScroll = IsAtMaxScroll();
			int minScroll, curScroll, maxScroll;
			curScroll = this.VerticalScroll;
			VerticalScrollRange(out minScroll, out maxScroll);

			if (isAtMaxScroll) OnScrolledToBottom(EventArgs.Empty);
			Console.WriteLine("scroll " + String.Join(" ", new int[] { minScroll, curScroll, maxScroll }));
		}

		[DllImport("user32.dll")]
		static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern int GetScrollPos(IntPtr hWnd, int nBar);

		[DllImport("user32.dll")]
		static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, IntPtr lParam);

		const int WM_USER = 0x400;
		const int WM_VSCROLL = 0x115;
		const int WM_MOUSEWHEEL = 0x20A;
		const int WM_SETREDRAW = 0x000B;
		const int SB_VERT = 1;
		const int EM_GETSCROLLPOS = WM_USER + 221;
		const int EM_SETSCROLLPOS = WM_USER + 222;
		const int EM_GETEVENTMASK = WM_USER + 59;
		const int EM_SETEVENTMASK = WM_USER + 69;
	}
}
