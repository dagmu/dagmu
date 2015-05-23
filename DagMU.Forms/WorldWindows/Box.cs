﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DagMU.Forms
{
	class Box : RichTextBox
	{
		public Box() {
			stuffToMatch = new List<TextMatch>() {
				new TextMatch(new Regex("\bpage(s|(-pose))?\b"), Color.Red),
				new TextMatch(new Regex("\bwhisper(s)?\b"), Color.Blue),
				new TextMatch(new Regex(@"^([A-Za-z0-9_\-]+) (?:has ((?:dis|re|)connected|left|arrived)|(goes home)|(?:concentrates on a distant place, and )(fades from sight)|(?:(?:is )(taken home)(?: to sleep by the local police))).$"), Color.Gray),
				new TextMatch(new Regex(@"^Somewhere on the muck, ([A-Za-z0-9_\-]+) has ((?:|re|dis)connected).$"), Color.Gray),
			};

			namesToMatch = new List<TextMatch>() {
				new TextMatch("Shean", Color.Teal),
				new TextMatch("Dagon", Color.MediumPurple),
				new TextMatch("Yko", Color.Yellow),
				new TextMatch("Szai", Color.Yellow),
				new TextMatch("Mkosi", Color.Orange),
			};

			this.ReadOnly = true;
		}

		public class TextMatch
		{
			public TextMatch(string value, Color color) : this(color) { Match = value; }
			public TextMatch(Regex regex, Color color) : this(color) { Regex = regex; }
			public TextMatch(Color color) { this.Color = color; }

			public String Match {
				get { return match; }
				set { regex = null; match = value; }
			}

			public Regex Regex {
				get { if (regex == null) regex = new Regex(@"\b" + match + @"\b"); return regex; }
				set { regex = value; }
			}

			public Color Color;

			Regex regex = null;
			string match;
		}

		public struct TextMatchPlace
		{
			public int Index, Length;
			public Color Color;
		}

		public void Add(string s, Color? color = null)
		{
			bool autoScroll = this.SelectionLength == 0 && IsAtMaxScroll && !mouseDown;
			List<TextMatchPlace> placesToColor = GetColorPlaces(s);

			this.SuspendPainting();
			this.SelectionStart = this.TextLength;
			this.AppendText("\n");
			int startIndex = this.TextLength;
			this.AppendText(s);
			foreach (TextMatchPlace place in placesToColor) {
				this.Select(startIndex + place.Index, place.Length);
				this.SelectionColor = place.Color;
			}
			this.SelectionLength = 0;
			this.SelectionColor = this.ForeColor;
			this.ResumePainting();

			if (autoScroll) ScrollToBottom();
		}

		public List<TextMatch> stuffToMatch { get; set; }
		public List<TextMatch> namesToMatch { get; set; }

		public event EventHandler ScrolledToBottom;

		//thanks to http://stackoverflow.com/a/6550415/3320154 for Suspend/ResumePainting for stealth mode appendtext
		//thanks to http://stackoverflow.com/a/10238729/3320154 for IsAtMaxScroll/OnScrolledToBottom
		//thanks to http://stackoverflow.com/a/10646424/3320154 for VerticalScroll get/set

		Point _ScrollPoint;
		bool _Painting = true;
		IntPtr _EventMask;
		int _SuspendIndex = 0;
		int _SuspendLength = 0;
		bool mouseDown = false;

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

		protected virtual void OnScrolledToBottom(EventArgs e)
		{
			if (ScrolledToBottom != null)
				ScrolledToBottom(this, e);
		}

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
				Refocus();
			}

			this.mouseDown = false;
			base.OnMouseUp(mevent);
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

		private void Refocus()
		{
			//send event to refocus inputbox
		}

		private List<TextMatchPlace> GetColorPlaces(string s)
		{
			List<TextMatchPlace> placesToColor = new List<TextMatchPlace>();
			foreach (TextMatch textMatch in stuffToMatch) {
				foreach (Match match in textMatch.Regex.Matches(s)) {
					TextMatchPlace blah = new TextMatchPlace() { Color = textMatch.Color, Index = match.Index, Length = match.Length };
					placesToColor.Add(blah);
				}
			}
			foreach (TextMatch textMatch in namesToMatch) {
				foreach (Match match in textMatch.Regex.Matches(s)) {
					TextMatchPlace blah = new TextMatchPlace() { Color = textMatch.Color, Index = match.Index, Length = match.Length };
					placesToColor.Add(blah);
				}
			}
			return placesToColor;
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
	}
}
