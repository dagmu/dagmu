namespace DagMU.Forms
{
	partial class LogSettingsWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.buttonLogFolder = new System.Windows.Forms.Button();
			this.numLogOffset = new System.Windows.Forms.NumericUpDown();
			this.txtLogDir = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numLogOffset)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Log Directory";
			// 
			// buttonLogFolder
			// 
			this.buttonLogFolder.Location = new System.Drawing.Point(365, 28);
			this.buttonLogFolder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonLogFolder.Name = "buttonLogFolder";
			this.buttonLogFolder.Size = new System.Drawing.Size(56, 19);
			this.buttonLogFolder.TabIndex = 2;
			this.buttonLogFolder.Text = "Choose Folder";
			this.buttonLogFolder.UseVisualStyleBackColor = true;
			this.buttonLogFolder.Click += new System.EventHandler(this.buttonLogFolder_Click);
			// 
			// numLogOffset
			// 
			this.numLogOffset.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::DagMU.Forms.Properties.Settings.Default, "LogOffset", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.numLogOffset.Location = new System.Drawing.Point(12, 83);
			this.numLogOffset.Name = "numLogOffset";
			this.numLogOffset.Size = new System.Drawing.Size(120, 20);
			this.numLogOffset.TabIndex = 3;
			this.numLogOffset.Value = global::DagMU.Forms.Properties.Settings.Default.LogOffset;
			// 
			// txtLogDir
			// 
			this.txtLogDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DagMU.Forms.Properties.Settings.Default, "LogDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtLogDir.Location = new System.Drawing.Point(12, 28);
			this.txtLogDir.Margin = new System.Windows.Forms.Padding(2);
			this.txtLogDir.Name = "txtLogDir";
			this.txtLogDir.ReadOnly = true;
			this.txtLogDir.Size = new System.Drawing.Size(349, 20);
			this.txtLogDir.TabIndex = 1;
			this.txtLogDir.Text = global::DagMU.Forms.Properties.Settings.Default.LogDir;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(402, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Log Offset - Hours after midnight a log in still considered \"today\" and not \"tomo" +
    "rrow\"";
			// 
			// LogSettingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(432, 162);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numLogOffset);
			this.Controls.Add(this.buttonLogFolder);
			this.Controls.Add(this.txtLogDir);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "LogSettingsWindow";
			this.Text = "Log Settings";
			((System.ComponentModel.ISupportInitialize)(this.numLogOffset)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLogDir;
		private System.Windows.Forms.Button buttonLogFolder;
		private System.Windows.Forms.NumericUpDown numLogOffset;
		private System.Windows.Forms.Label label2;
	}
}