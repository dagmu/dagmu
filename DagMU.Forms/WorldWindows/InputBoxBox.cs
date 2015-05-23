using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
{
	public partial class InputBoxBox : UserControl, IRefocus
	{
		public InputBoxBox()
		{
			InitializeComponent();
			inputBoxes = new List<InputBox>();
			MakeNewInputBox(defaultheight);
		}

		List<InputBox> inputBoxes;

		public static int defaultheight = 3;

		/// <summary>
		/// Update a specified input boxofmucktext with the status of its send attempt
		/// </summary>
		public void UpdateStatus(InputBox whichbox, InputBox.Status newstatus)
		{
			if ((whichbox.IsDisposed) || (whichbox == null))
				return;

			whichbox.newstatus(newstatus);
		}

		/// <summary>
		/// Update ALL inputbox status, Good for when we disconnect, connect, etc
		/// </summary>
		public void UpdateStatus(InputBox.Status newstatus)
		{
			foreach (InputBox box in inputBoxes)
			{
				box.newstatus(newstatus);
			}
		}

		void MakeNewInputBox(int lineshigh)
		{
			SuspendLayout();

			InputBox box = new InputBox(lineshigh);

			box.Resize += OnInputBoxResize;
			box.Load += OnInputBoxResize;
			box.EClose += OnInputBoxWantsToClose;
			box.ENew += OnInputBoxWantsNew;
			box.ESend += OnInputBoxWantsToSend;
			box.EScroll += OnInputBoxMouseWheel;

			box.Left = 0;
			if (inputBoxes.Count > 0)
			{
				box.Top = inputBoxes.Last().Bottom;
				Height = box.Top + box.Height;
			}
			else
			{
				box.Top = 0;
				Height = box.Height;
			}
			box.Width = ClientRectangle.Width;
			box.Anchor = AnchorStyles.Left | AnchorStyles.Right;

			inputBoxes.Add(box); // add it to our list

			Controls.Add(box); // inputbox should trigger resize event soon

			ResumeLayout();

			OnInputBoxResize(null, null);
		}

		void OnInputBoxWantsNew(object sender, EventArgs e)
		{
			MakeNewInputBox(defaultheight);
		}

		void OnInputBoxWantsToClose(object sender, EventArgs e)
		{
			InputBox inputBox = sender as InputBox;
			inputBoxes.Remove(inputBox);
			Controls.Remove(inputBox);
			inputBox.Dispose();

			if (inputBoxes.Count == 0) MakeNewInputBox(defaultheight);

			OnInputBoxResize(null, null);//recompute size
		}

		/// <summary>
		/// An inputbox has changed its size
		/// Recompute our size and inputbox positions completely
		/// This triggers resize event monitored in World: OnInputBoxesResize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnInputBoxResize(object sender, EventArgs e)
		{
			SuspendLayout();

			int y = 0;
			foreach (InputBox box in inputBoxes)
			{
				box.Anchor = AnchorStyles.None;
				box.Top = y+1;
				y = box.Bottom;
			}

			Height = y;

			foreach (InputBox box in inputBoxes)
				box.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

			ResumeLayout();
		}

		/// <summary>
		/// An inputbox has text to send to the muck
		/// </summary>
		public event EventHandler<string> EWantsToSend;
		void OnInputBoxWantsToSend(object sender, string msg)
		{
			EWantsToSend(sender, msg);
		}

		/// <summary>
		/// Pass mousewheel messages from inputbox to main text window
		/// </summary>
		public event EventHandler<MouseEventArgs> EScroll;
		void OnInputBoxMouseWheel(object sender, MouseEventArgs e)
		{
			EScroll(sender, e);
		}

		/// <summary>
		/// Snap window focus to an inputbox
		/// </summary>
		public void Refocus()
		{
			inputBoxes[0].Refocus();
		}
	}
}
