using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DagMU.HelperWindows
{
	public partial class InputBoxBox : UserControl
	{
		public InputBoxBox()
		{
			InitializeComponent();
			inputboxes = new List<InputBox>();
			MakeNewInputBox(defaultheight);
		}

		List<InputBox> inputboxes;

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
			foreach (InputBox box in inputboxes)
			{
				box.newstatus(newstatus);
			}
		}

		void MakeNewInputBox(int lineshigh)
		{
			SuspendLayout();

			InputBox box = new InputBox(lineshigh);

			box.Resize += new EventHandler(OnInputBoxResize);
			box.Load += new EventHandler(OnInputBoxResize);
			box.EClose += new InputBox.RefMessage(OnInputBoxWantsToClose);
			box.ENew += new InputBox.RefMessage(OnInputBoxWantsNew);
			box.ESend += new InputBox.TextMessage(OnInputBoxWantsToSend);
			box.EScroll += new InputBox.ScrollMessage(OnInputBoxMouseWheel);

			box.Left = 0;
			if (inputboxes.Count > 0)
			{
				box.Top = inputboxes.Last().Bottom;
				Height = box.Top + box.Height;
			}
			else
			{
				box.Top = 0;
				Height = box.Height;
			}
			box.Width = ClientRectangle.Width;
			box.Anchor = AnchorStyles.Left | AnchorStyles.Right;

			inputboxes.Add(box); // add it to our list

			Controls.Add(box); // inputbox should trigger resize event soon

			ResumeLayout();

			OnInputBoxResize(null, null);
		}

		void OnInputBoxWantsNew(InputBox sender)
		{
			MakeNewInputBox(defaultheight);
		}

		void OnInputBoxWantsToClose(InputBox sender)
		{
			inputboxes.Remove(sender);// remove boxofmucktext from list
			Controls.Remove(sender);// remove boxofmucktext from controls
			sender.Dispose();// dispose of inputbox

			if (inputboxes.Count == 0) MakeNewInputBox(defaultheight);

			// recompute size
			OnInputBoxResize(null, null);
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
			foreach (InputBox box in inputboxes)
			{
				box.Anchor = AnchorStyles.None;
				box.Top = y+1;
				y = box.Bottom;
			}

			Height = y;

			foreach (InputBox box in inputboxes)
				box.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

			ResumeLayout();
		}

		/// <summary>
		/// An inputbox has text to send to the muck
		/// </summary>
		public event InputBox.TextMessage EWantsToSend;
		void OnInputBoxWantsToSend(InputBox sender, string msg)
		{
			EWantsToSend(sender, msg);
		}

		/// <summary>
		/// Pass mousewheel messages from inputbox to main text window
		/// </summary>
		public event InputBox.ScrollMessage EScroll;
		void OnInputBoxMouseWheel(MouseEventArgs e)
		{
			EScroll(e);
		}

		/// <summary>
		/// Snap window focus to an inputbox
		/// </summary>
		public void refocus()
		{
			inputboxes[0].refocus();
		}
	}
}
