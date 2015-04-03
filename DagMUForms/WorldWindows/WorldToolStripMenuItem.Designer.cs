namespace DagMU.WorldWindows
{
	partial class WorldToolStripMenuItem
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
			this.btnConnection = new System.Windows.Forms.Button();
			this.lblText = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnConnection
			// 
			this.btnConnection.Image = global::DagMU.Properties.Resources.lightning;
			this.btnConnection.Location = new System.Drawing.Point(4, 4);
			this.btnConnection.Name = "btnConnection";
			this.btnConnection.Size = new System.Drawing.Size(28, 23);
			this.btnConnection.TabIndex = 0;
			this.btnConnection.UseVisualStyleBackColor = true;
			// 
			// lblText
			// 
			this.lblText.AutoSize = true;
			this.lblText.Location = new System.Drawing.Point(38, 9);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(38, 13);
			this.lblText.TabIndex = 1;
			this.lblText.Text = "lblText";
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(196, 3);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(24, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// btnEdit
			// 
			this.btnEdit.Location = new System.Drawing.Point(166, 3);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(24, 23);
			this.btnEdit.TabIndex = 3;
			this.btnEdit.UseVisualStyleBackColor = true;
			// 
			// WorldToolStripMenuItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.btnConnection);
			this.Name = "WorldToolStripMenuItem";
			this.Size = new System.Drawing.Size(228, 29);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnConnection;
		private System.Windows.Forms.Label lblText;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnEdit;
	}
}
