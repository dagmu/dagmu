namespace DagMU.Forms.Helpers
{
	partial class CInfoHelperWindow
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonSave = new System.Windows.Forms.Button();
			this.labelCharName = new System.Windows.Forms.Label();
			this.buttonNewMiscField = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 46);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(755, 546);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(489, 12);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(168, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "> Save Changes <";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// labelCharName
			// 
			this.labelCharName.AutoSize = true;
			this.labelCharName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelCharName.Location = new System.Drawing.Point(31, 13);
			this.labelCharName.Name = "labelCharName";
			this.labelCharName.Size = new System.Drawing.Size(59, 20);
			this.labelCharName.TabIndex = 5;
			this.labelCharName.Text = "label1";
			// 
			// buttonNewMiscField
			// 
			this.buttonNewMiscField.Location = new System.Drawing.Point(315, 12);
			this.buttonNewMiscField.Name = "buttonNewMiscField";
			this.buttonNewMiscField.Size = new System.Drawing.Size(168, 23);
			this.buttonNewMiscField.TabIndex = 6;
			this.buttonNewMiscField.Text = "> New Misc Field <";
			this.buttonNewMiscField.UseVisualStyleBackColor = true;
			this.buttonNewMiscField.Click += new System.EventHandler(this.buttonNewMiscField_Click);
			// 
			// CInfoHelperWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(779, 604);
			this.Controls.Add(this.buttonNewMiscField);
			this.Controls.Add(this.labelCharName);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "CInfoHelperWindow";
			this.Text = "CInfoHelperWindow";
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.buttonSave, 0);
			this.Controls.SetChildIndex(this.labelCharName, 0);
			this.Controls.SetChildIndex(this.buttonNewMiscField, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelCharName;
		private System.Windows.Forms.Button buttonNewMiscField;


	}
}