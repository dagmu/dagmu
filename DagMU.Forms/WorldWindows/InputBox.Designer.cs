namespace DagMU.Forms.Helpers
{
	partial class InputBox
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.input = new DagMU.Forms.InputTextBox();
			this.buttonMore = new System.Windows.Forms.Button();
			this.buttonBigger = new System.Windows.Forms.Button();
			this.buttonSmaller = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.input);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonMore);
			this.splitContainer1.Panel2.Controls.Add(this.buttonBigger);
			this.splitContainer1.Panel2.Controls.Add(this.buttonSmaller);
			this.splitContainer1.Panel2.Controls.Add(this.buttonClose);
			this.splitContainer1.Size = new System.Drawing.Size(590, 133);
			this.splitContainer1.SplitterDistance = 533;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 0;
			// 
			// input
			// 
			this.input.AcceptsTab = true;
			this.input.BackColor = System.Drawing.Color.Black;
			this.input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.input.Dock = System.Windows.Forms.DockStyle.Fill;
			this.input.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.input.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.input.Location = new System.Drawing.Point(0, 0);
			this.input.Multiline = true;
			this.input.Name = "input";
			this.input.Size = new System.Drawing.Size(533, 133);
			this.input.TabIndex = 0;
			this.input.TextChanged += new System.EventHandler(this.input_TextChanged);
			this.input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_KeyDown);
			this.input.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.input_MouseWheel);
			// 
			// buttonMore
			// 
			this.buttonMore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonMore.BackColor = System.Drawing.SystemColors.Control;
			this.buttonMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMore.Location = new System.Drawing.Point(0, 110);
			this.buttonMore.Margin = new System.Windows.Forms.Padding(0);
			this.buttonMore.Name = "buttonMore";
			this.buttonMore.Size = new System.Drawing.Size(56, 23);
			this.buttonMore.TabIndex = 3;
			this.buttonMore.TabStop = false;
			this.buttonMore.Text = "n";
			this.toolTip1.SetToolTip(this.buttonMore, "Create an additional input boxofmucktext");
			this.buttonMore.UseVisualStyleBackColor = false;
			this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
			// 
			// buttonBigger
			// 
			this.buttonBigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBigger.BackColor = System.Drawing.SystemColors.Control;
			this.buttonBigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonBigger.Location = new System.Drawing.Point(37, 23);
			this.buttonBigger.Margin = new System.Windows.Forms.Padding(0);
			this.buttonBigger.Name = "buttonBigger";
			this.buttonBigger.Size = new System.Drawing.Size(19, 23);
			this.buttonBigger.TabIndex = 2;
			this.buttonBigger.TabStop = false;
			this.buttonBigger.Text = ">";
			this.toolTip1.SetToolTip(this.buttonBigger, "Expand this input boxofmucktext");
			this.buttonBigger.UseVisualStyleBackColor = false;
			this.buttonBigger.Click += new System.EventHandler(this.buttonBigger_Click);
			// 
			// buttonSmaller
			// 
			this.buttonSmaller.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSmaller.BackColor = System.Drawing.SystemColors.Control;
			this.buttonSmaller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSmaller.Location = new System.Drawing.Point(0, 23);
			this.buttonSmaller.Margin = new System.Windows.Forms.Padding(0);
			this.buttonSmaller.Name = "buttonSmaller";
			this.buttonSmaller.Size = new System.Drawing.Size(16, 23);
			this.buttonSmaller.TabIndex = 1;
			this.buttonSmaller.TabStop = false;
			this.buttonSmaller.Text = "<";
			this.toolTip1.SetToolTip(this.buttonSmaller, "Shrink this input boxofmucktext");
			this.buttonSmaller.UseVisualStyleBackColor = false;
			this.buttonSmaller.Click += new System.EventHandler(this.buttonSmaller_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.BackColor = System.Drawing.SystemColors.Control;
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClose.Location = new System.Drawing.Point(0, 0);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(56, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.TabStop = false;
			this.buttonClose.Text = "X";
			this.toolTip1.SetToolTip(this.buttonClose, "Close this input boxofmucktext");
			this.buttonClose.UseVisualStyleBackColor = false;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// InputBox
			// 
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.splitContainer1);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "InputBox";
			this.Size = new System.Drawing.Size(590, 133);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private DagMU.Forms.InputTextBox input;
		private System.Windows.Forms.Button buttonMore;
		private System.Windows.Forms.Button buttonBigger;
		private System.Windows.Forms.Button buttonSmaller;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ToolTip toolTip1;

	}
}
