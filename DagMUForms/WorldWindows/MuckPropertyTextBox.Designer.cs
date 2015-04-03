namespace DagMU
{
	partial class MuckPropertyTextBox
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
			this.textbox = new System.Windows.Forms.TextBox();
			this.checkbox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textbox
			// 
			this.textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox.Location = new System.Drawing.Point(107, 3);
			this.textbox.Name = "textbox";
			this.textbox.Size = new System.Drawing.Size(286, 22);
			this.textbox.TabIndex = 0;
			// 
			// checkbox
			// 
			this.checkbox.AutoSize = true;
			this.checkbox.Location = new System.Drawing.Point(3, 5);
			this.checkbox.Name = "checkbox";
			this.checkbox.Size = new System.Drawing.Size(98, 21);
			this.checkbox.TabIndex = 1;
			this.checkbox.Text = "checkBox1";
			this.checkbox.UseVisualStyleBackColor = true;
			// 
			// MuckPropertyTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkbox);
			this.Controls.Add(this.textbox);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MuckPropertyTextBox";
			this.Size = new System.Drawing.Size(396, 28);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textbox;
		private System.Windows.Forms.CheckBox checkbox;

	}
}
