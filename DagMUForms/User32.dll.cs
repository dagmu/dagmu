using System;
using System.Windows.Forms;

namespace DagMU
{
    sealed class User32
    {
		/// <summary>
		/// Flash a window on the taskbar to indicate activity
		/// </summary>
		/// <param name="hWnd">Handle to the window whose window procedure will receive the message.</param>
		/// <param name="Revert">Specifies whether the CWnd is to be flashed or returned to its original state. The CWnd is flashed from one state to the other if bInvert is TRUE. If bInvert is FALSE, the window is returned to its original state (either active or inactive).</param>
		/// <returns>Nonzero if the window was active before the call to the FlashWindow member function; otherwise 0.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int FlashWindow(IntPtr hWnd, bool Revert);

        /// <summary>
        /// The SendMessage function sends the specified message to a window or windows. It calls the window procedure for the specified window and does not return until the window procedure has processed the message. 
        /// To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
        /// </summary>
		/// <param name="hWnd">Handle to the window whose window procedure will receive the message.</param>
		/// <param name="wMsg">Specifies the message to be sent.</param>
		/// <param name="wParam">Specifies additional message-specific information.</param>
		/// <param name="lParam">Specifies additional message-specific information.</param>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        static IntPtr MAKELPARAM(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
        static int HIWORD(int Number)
        {
            return (Number >> 16) & 0xffff;
        }
        static int LOWORD(int Number)
        {
            return Number & 0xffff;
        }

        const int WM_SCROLL = 276; // Horizontal scroll
        const int WM_VSCROLL = 277; // Vertical scroll
        const int WM_MOUSEWHEEL = 0x20A; // mousewheel event
        const int SB_LINEUP = 0; // Scrolls one line up
        const int SB_LINELEFT = 0;// Scrolls one cell left
        const int SB_LINEDOWN = 1; // Scrolls one line down
        const int SB_LINERIGHT = 1;// Scrolls one cell right
        const int SB_PAGEUP = 2; // Scrolls one page up
        const int SB_PAGELEFT = 2;// Scrolls one page left
        const int SB_PAGEDOWN = 3; // Scrolls one page down
        const int SB_PAGERIGTH = 3; // Scrolls one page right
        const int SB_PAGETOP = 6; // Scrolls to the upper left
        const int SB_LEFT = 6; // Scrolls to the left
        const int SB_PAGEBOTTOM = 7; // Scrolls to the lower right
        const int SB_RIGHT = 7; // Scrolls to the right
        const int SB_ENDSCROLL = 8; // Ends scroll


        public static void SendScrollEvent(Control control, int scrollmessage)
        {
	        SendMessage(control.Handle, WM_VSCROLL, (IntPtr)scrollmessage, System.IntPtr.Zero);
        }

        public static void SendMouseWheelEvent(Control control, int delta)
        {
	        if (control != null)
	        {
		        SendMessage(control.Handle, WM_MOUSEWHEEL, MAKELPARAM(0,delta), System.IntPtr.Zero);
	        }
        }

        //WM_MOUSEWHEEL 
        //  fwKeys = LOWORD(wParam); 
        //  zDelta = HIWORD(wParam);
        //  xPos = LOWORD(lParam); 
        //  yPos = HIWORD(lParam);

        //fwKeys
        //Indicates the mouse buttons and keys that the user pressed. The following table shows the possible values, which can be combined. Value	Description
        //MK_CONTROL	The user pressed the CTRL key.
        //MK_LBUTTON	The user pressed the left mouse button.
        //MK_MBUTTON	The user pressed the middle mouse button.
        //MK_RBUTTON	The user pressed the right mouse button.
        //MK_SHIFT	The user pressed the SHIFT key.
        //
        //zDelta
        //Indicates that the mouse wheel was pressed, expressed in multiples or divisions of WHEEL_DELTA, which is 120.
        //xPos
        //Value of the low-order word of lParam. Specifies the x-coordinate of the pointer, relative to the upper-left corner of the screen.
        //yPos
        //Value of the high-order word of lParam. Specifies the y-coordinate of the pointer, relative to the upper-left corner of the screen.


    }
}