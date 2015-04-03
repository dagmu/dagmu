namespace DagMU
{
	partial class WF
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
			this.thelist = new System.Windows.Forms.ListView();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.buttonHideFrom = new System.Windows.Forms.Button();
			this.labelTitle = new System.Windows.Forms.Label();
			this.labelVisibleIn = new System.Windows.Forms.Label();
			this.HiddenForTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// thelist
			// 
			this.thelist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.thelist.Location = new System.Drawing.Point(3, 3);
			this.thelist.Name = "thelist";
			this.thelist.Size = new System.Drawing.Size(311, 165);
			this.thelist.TabIndex = 0;
			this.thelist.UseCompatibleStateImageBehavior = false;
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(158, 3);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 19);
			this.buttonRefresh.TabIndex = 1;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			// 
			// buttonHideFrom
			// 
			this.buttonHideFrom.Location = new System.Drawing.Point(239, 3);
			this.buttonHideFrom.Name = "buttonHideFrom";
			this.buttonHideFrom.Size = new System.Drawing.Size(75, 19);
			this.buttonHideFrom.TabIndex = 2;
			this.buttonHideFrom.UseVisualStyleBackColor = true;
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.labelTitle.Location = new System.Drawing.Point(13, 9);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(24, 13);
			this.labelTitle.TabIndex = 3;
			this.labelTitle.Text = "WF";
			// 
			// labelVisibleIn
			// 
			this.labelVisibleIn.AutoSize = true;
			this.labelVisibleIn.Location = new System.Drawing.Point(43, 9);
			this.labelVisibleIn.Name = "labelVisibleIn";
			this.labelVisibleIn.Size = new System.Drawing.Size(48, 13);
			this.labelVisibleIn.TabIndex = 4;
			this.labelVisibleIn.Text = "Visible in";
			// 
			// HiddenForTimer
			// 
			this.HiddenForTimer.Interval = 1000;
			this.HiddenForTimer.Tick += new System.EventHandler(this.HiddenForTimer_Tick);
			// 
			// WF
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelVisibleIn);
			this.Controls.Add(this.labelTitle);
			this.Controls.Add(this.buttonHideFrom);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.thelist);
			this.Name = "WF";
			this.Size = new System.Drawing.Size(317, 171);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView thelist;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Button buttonHideFrom;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelVisibleIn;
		private System.Windows.Forms.Timer HiddenForTimer;
	}
}
