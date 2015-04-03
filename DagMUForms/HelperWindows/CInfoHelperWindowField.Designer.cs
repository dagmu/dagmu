namespace DagMU.HelperWindows
{
	partial class CInfoHelperWindowField
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textbox = new System.Windows.Forms.RichTextBox();
			this.labelFontNormal = new System.Windows.Forms.Label();
			this.labelFontBold = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textbox
			// 
			this.textbox.BackColor = System.Drawing.SystemColors.Control;
			this.textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textbox.Location = new System.Drawing.Point(0, 0);
			this.textbox.Name = "textbox";
			this.textbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textbox.Size = new System.Drawing.Size(615, 132);
			this.textbox.TabIndex = 5;
			this.textbox.Text = "";
			this.textbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.textbox_LinkClicked);
			this.textbox.Click += new System.EventHandler(this.textbox_Click);
			this.textbox.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			this.textbox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.textbox_MouseWheel);
			// 
			// labelFontNormal
			// 
			this.labelFontNormal.AutoSize = true;
			this.labelFontNormal.Location = new System.Drawing.Point(4, 61);
			this.labelFontNormal.Name = "labelFontNormal";
			this.labelFontNormal.Size = new System.Drawing.Size(46, 17);
			this.labelFontNormal.TabIndex = 6;
			this.labelFontNormal.Text = "label1";
			this.labelFontNormal.Visible = false;
			// 
			// labelFontBold
			// 
			this.labelFontBold.AutoSize = true;
			this.labelFontBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelFontBold.Location = new System.Drawing.Point(4, 78);
			this.labelFontBold.Name = "labelFontBold";
			this.labelFontBold.Size = new System.Drawing.Size(52, 17);
			this.labelFontBold.TabIndex = 7;
			this.labelFontBold.Text = "label2";
			this.labelFontBold.Visible = false;
			// 
			// CInfoHelperWindowField
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelFontBold);
			this.Controls.Add(this.labelFontNormal);
			this.Controls.Add(this.textbox);
			this.Name = "CInfoHelperWindowField";
			this.Size = new System.Drawing.Size(615, 132);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox textbox;
		private System.Windows.Forms.Label labelFontNormal;
		private System.Windows.Forms.Label labelFontBold;
	}
}
