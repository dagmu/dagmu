namespace DagMU.Forms.HelperWindows
{
	partial class HelperWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkBoxOnTop = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkBoxOnTop
			// 
			this.checkBoxOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxOnTop.AutoSize = true;
			this.checkBoxOnTop.Location = new System.Drawing.Point(501, 0);
			this.checkBoxOnTop.Name = "checkBoxOnTop";
			this.checkBoxOnTop.Size = new System.Drawing.Size(59, 17);
			this.checkBoxOnTop.TabIndex = 0;
			this.checkBoxOnTop.Text = "OnTop";
			this.checkBoxOnTop.UseVisualStyleBackColor = true;
			this.checkBoxOnTop.CheckedChanged += new System.EventHandler(this.checkBoxOnTop_CheckedChanged);
			// 
			// HelperWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 491);
			this.Controls.Add(this.checkBoxOnTop);
			this.Name = "HelperWindow";
			this.Text = "HelperWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelperWindow_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxOnTop;
	}
}