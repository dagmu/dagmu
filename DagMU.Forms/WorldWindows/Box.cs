using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using DagMU.Model;
using System.Collections.ObjectModel;

namespace DagMU.Forms
{
	class Box : RichTextBox
	{
		#region constructor
		public Box() {
			this.ReadOnly = true;
		}

		public void IoC(Tuple<ObservableCollection<Data.TextMatch>, ObservableCollection<Data.TextMatch>> ioc)
		{
			stuffToMatch = ioc.Item1;
			namesToMatch = ioc.Item2;
		}
		#endregion

		#region public interface
		public void Add(string s, Color? color = null)
		{
			bool autoScroll = this.SelectionLength == 0 && IsAtMaxScroll && !mouseDown;
			List<Data.TextMatchPlace> placesToColor = GetColorPlaces(s);

			this.SuspendPainting();
			this.SelectionStart = this.TextLength;
			this.AppendText("\n");
			int startIndex = this.TextLength;
			this.AppendText(s);
			foreach (Data.TextMatchPlace place in placesToColor) {
				this.Select(startIndex + place.Index, place.Length);
				this.SelectionColor = Color.FromArgb(place.Color.R, place.Color.G, place.Color.B);
			}
			this.SelectionLength = 0;
			this.SelectionColor = this.ForeColor;
			this.ResumePainting();

			if (autoScroll) ScrollToBottom();
		}
		#endregion

		#region textMatch
		private ObservableCollection<Data.TextMatch> stuffToMatch;
		private ObservableCollection<Data.TextMatch> namesToMatch;
		
		private List<Data.TextMatchPlace> GetColorPlaces(string s)
		{
			List<Data.TextMatchPlace> placesToColor = new List<Data.TextMatchPlace>();
			foreach (Data.TextMatch textMatch in stuffToMatch) {
				foreach (Match match in textMatch.Regex.Matches(s)) {
					for (int j = 1; j < match.Groups.Count; j++) {
						var group = match.Groups[j];
						Data.TextMatchPlace blah = new Data.TextMatchPlace() { Color = textMatch.Color, Index = group.Index, Length = group.Length };
						placesToColor.Add(blah);
					}
				}
			}
			foreach (Data.TextMatch textMatch in namesToMatch) {
				foreach (Match match in textMatch.Regex.Matches(s)) {
					Data.TextMatchPlace blah = new Data.TextMatchPlace() { Color = textMatch.Color, Index = match.Index, Length = match.Length };
					placesToColor.Add(blah);
				}
			}
			return placesToColor;
		}
		#endregion

		#region suspend painting
		/// <summary>
		/// suspend painting, for stealth mode appendtext - thanks to http://stackoverflow.com/a/6550415/3320154
		/// </summary>
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

		bool _Painting = true;
		int _SuspendIndex = 0;
		int _SuspendLength = 0;
		Point _ScrollPoint;
		IntPtr _EventMask;
		#endregion

		#region text selection
		public event EventHandler Refocus;

		bool mouseDown = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.mouseDown = true;

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (this.SelectionLength > 0) {
				Clipboard.SetText(this.SelectedText);
				this.SelectionLength = 0;
				Refocus(this, null);
			}

			this.mouseDown = false;

			base.OnMouseUp(mevent);
		}
		#endregion

		#region scroll
		/// <summary>
		/// thanks to http://stackoverflow.com/a/10238729/3320154 for IsAtMaxScroll/OnScrolledToBottom
		/// </summary>
		public event EventHandler ScrolledToBottom;

		public int VerticalScroll
		{
			get { return GetScrollPos((IntPtr)this.Handle, SB_VERT); }
			set { SetScrollPos((IntPtr)this.Handle, SB_VERT, value, true); }
		}

		public void VerticalScrollRange(out int minScroll, out int maxScroll)
		{
			GetScrollRange(this.Handle, SB_VERT, out minScroll, out maxScroll);
		}

		public bool IsAtMaxScroll
		{
			get
			{
				int minScroll, maxScroll;
				VerticalScrollRange(out minScroll, out maxScroll);

				Point rtfPoint = Point.Empty;
				SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);
				return (rtfPoint.Y + this.ClientSize.Height >= maxScroll);
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			NotSureIfScrolled();

			base.OnKeyUp(e);
		}

		protected virtual void OnScrolledToBottom(EventArgs e)
		{
			if (ScrolledToBottom != null)
				ScrolledToBottom(this, e);
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			//ScrollToBottom();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
				NotSureIfScrolled();

			base.WndProc(ref m);
		}

		private void ScrollToBottom()
		{
			this.SelectionStart = this.TextLength;
			this.ScrollToCaret();
		}

		private void NotSureIfScrolled()
		{
			bool isAtMaxScroll = IsAtMaxScroll;
			int minScroll, curScroll, maxScroll;
			curScroll = this.VerticalScroll;
			VerticalScrollRange(out minScroll, out maxScroll);

			if (isAtMaxScroll) OnScrolledToBottom(EventArgs.Empty);
			//Console.WriteLine("scroll " + String.Join(" ", new int[] { minScroll, curScroll, maxScroll, isAtMaxScroll ? 1 : 0 }));
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
		#endregion
	}
}
