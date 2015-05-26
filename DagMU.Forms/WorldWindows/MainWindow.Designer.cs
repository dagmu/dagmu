using System.Windows.Forms;

namespace DagMU.Forms
{
    partial class MainWindow
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
			this.cbFlash = new System.Windows.Forms.CheckBox();
			this.menu = new System.Windows.Forms.ToolStrip();
			this.tbnConnect = new System.Windows.Forms.ToolStripDropDownButton();
			this.tbnSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnForce = new System.Windows.Forms.ToolStripMenuItem();
			this.tbnTaps = new System.Windows.Forms.ToolStripMenuItem();
			this.tbnSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnSites = new System.Windows.Forms.ToolStripMenuItem();
			this.tbnAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbFlash
			// 
			this.cbFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbFlash.AutoSize = true;
			this.cbFlash.ForeColor = System.Drawing.Color.Red;
			this.cbFlash.Location = new System.Drawing.Point(605, 0);
			this.cbFlash.Name = "cbFlash";
			this.cbFlash.Size = new System.Drawing.Size(48, 17);
			this.cbFlash.TabIndex = 2;
			this.cbFlash.Text = "flash";
			this.cbFlash.UseVisualStyleBackColor = true;
			this.cbFlash.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// menu
			// 
			this.menu.CanOverflow = false;
			this.menu.Dock = System.Windows.Forms.DockStyle.None;
			this.menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbnConnect});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(63, 25);
			this.menu.TabIndex = 4;
			this.menu.Text = "glassToolStrip1";
			// 
			// tbnConnect
			// 
			this.tbnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbnConnect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbnSeparator1,
            this.tbnForce,
            this.tbnTaps,
            this.tbnSeparator2,
            this.tbnSites,
            this.tbnAdd});
			this.tbnConnect.Image = global::DagMU.Forms.Properties.Resources.lightning;
			this.tbnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnConnect.Name = "tbnConnect";
			this.tbnConnect.Size = new System.Drawing.Size(29, 22);
			this.tbnConnect.Text = "Connect";
			this.tbnConnect.Click += new System.EventHandler(this.tbnConnect_Click);
			// 
			// tbnSeparator1
			// 
			this.tbnSeparator1.Name = "tbnSeparator1";
			this.tbnSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// tbnForce
			// 
			this.tbnForce.Image = global::DagMU.Forms.Properties.Resources.lightning;
			this.tbnForce.Name = "tbnForce";
			this.tbnForce.Size = new System.Drawing.Size(152, 22);
			this.tbnForce.Text = "Local";
			this.tbnForce.Click += new System.EventHandler(this.tbnForceLocal_Click);
			// 
			// tbnTaps
			// 
			this.tbnTaps.Image = global::DagMU.Forms.Properties.Resources.lightning;
			this.tbnTaps.Name = "tbnTaps";
			this.tbnTaps.Size = new System.Drawing.Size(152, 22);
			this.tbnTaps.Text = "Taps";
			this.tbnTaps.Click += new System.EventHandler(this.tbnTaps_Click);
			// 
			// tbnSeparator2
			// 
			this.tbnSeparator2.Name = "tbnSeparator2";
			this.tbnSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// tbnSites
			// 
			this.tbnSites.Enabled = false;
			this.tbnSites.Image = global::DagMU.Forms.Properties.Resources.cog_edit;
			this.tbnSites.Name = "tbnSites";
			this.tbnSites.Size = new System.Drawing.Size(152, 22);
			this.tbnSites.Text = "Sites";
			// 
			// tbnAdd
			// 
			this.tbnAdd.Enabled = false;
			this.tbnAdd.Name = "tbnAdd";
			this.tbnAdd.Size = new System.Drawing.Size(152, 22);
			this.tbnAdd.Text = "Add New";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(746, 292);
			this.Controls.Add(this.menu);
			this.Controls.Add(this.cbFlash);
			this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::DagMU.Forms.Properties.Settings.Default, "MainLastLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.Location = global::DagMU.Forms.Properties.Settings.Default.MainLastLocation;
			this.Name = "MainWindow";
			this.Text = "DagMU";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.ResizeBegin += OnResizeBegin;
			this.ResizeEnd += OnResizeEnd;
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

		private System.Windows.Forms.CheckBox cbFlash;
		private ToolStrip menu;
		private System.Windows.Forms.ToolStripDropDownButton tbnConnect;
		private System.Windows.Forms.ToolStripSeparator tbnSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tbnForce;
		private System.Windows.Forms.ToolStripMenuItem tbnTaps;
		private System.Windows.Forms.ToolStripSeparator tbnSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tbnSites;
		private System.Windows.Forms.ToolStripMenuItem tbnAdd;
	}
}