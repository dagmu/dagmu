namespace DagMU.Forms.Helpers
{
	partial class DebugWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindow));
			this.textbox = new System.Windows.Forms.TextBox();
			this.labelStatus = new System.Windows.Forms.Label();
			this.buttonResetNormal = new System.Windows.Forms.Button();
			this.textboxCharName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textbox
			// 
			this.textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox.Location = new System.Drawing.Point(9, 43);
			this.textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textbox.Multiline = true;
			this.textbox.Name = "textbox";
			this.textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textbox.Size = new System.Drawing.Size(372, 232);
			this.textbox.TabIndex = 0;
			this.textbox.Text = resources.GetString("textbox.Text");
			this.textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_KeyDown);
			// 
			// labelStatus
			// 
			this.labelStatus.AutoSize = true;
			this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelStatus.Location = new System.Drawing.Point(9, 11);
			this.labelStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(41, 13);
			this.labelStatus.TabIndex = 1;
			this.labelStatus.Text = "label1";
			// 
			// buttonResetNormal
			// 
			this.buttonResetNormal.Location = new System.Drawing.Point(284, 11);
			this.buttonResetNormal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonResetNormal.Name = "buttonResetNormal";
			this.buttonResetNormal.Size = new System.Drawing.Size(96, 19);
			this.buttonResetNormal.TabIndex = 2;
			this.buttonResetNormal.Text = "Reset to Normal";
			this.buttonResetNormal.UseVisualStyleBackColor = true;
			this.buttonResetNormal.Click += new System.EventHandler(this.buttonResetNormal_Click);
			// 
			// textboxCharName
			// 
			this.textboxCharName.Location = new System.Drawing.Point(179, 12);
			this.textboxCharName.Name = "textboxCharName";
			this.textboxCharName.Size = new System.Drawing.Size(100, 20);
			this.textboxCharName.TabIndex = 3;
			this.textboxCharName.TextChanged += new System.EventHandler(this.textboxCharName_TextChanged);
			// 
			// DebugWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 284);
			this.Controls.Add(this.textboxCharName);
			this.Controls.Add(this.buttonResetNormal);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.textbox);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "DebugWindow";
			this.Controls.SetChildIndex(this.textbox, 0);
			this.Controls.SetChildIndex(this.labelStatus, 0);
			this.Controls.SetChildIndex(this.buttonResetNormal, 0);
			this.Controls.SetChildIndex(this.textboxCharName, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textbox;
		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.Button buttonResetNormal;
		public System.Windows.Forms.TextBox textboxCharName;
	}
}
