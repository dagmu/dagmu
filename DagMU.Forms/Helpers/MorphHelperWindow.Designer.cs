namespace DagMU.Forms.Helpers
{
	partial class MorphHelperWindow
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
			this.components = new System.ComponentModel.Container();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.buttonNewMorph = new System.Windows.Forms.Button();
			this.buttonDescEditor = new System.Windows.Forms.Button();
			this.listView = new System.Windows.Forms.ListView();
			this.columnHeaderCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.morphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.qMorphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.overwriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxCommand = new System.Windows.Forms.TextBox();
			this.textBoxLongName = new System.Windows.Forms.TextBox();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.buttonNewCancel = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.contextmenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(13, 12);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 0;
			this.buttonRefresh.Text = "refesh list";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// buttonNewMorph
			// 
			this.buttonNewMorph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNewMorph.Location = new System.Drawing.Point(94, 12);
			this.buttonNewMorph.Name = "buttonNewMorph";
			this.buttonNewMorph.Size = new System.Drawing.Size(387, 23);
			this.buttonNewMorph.TabIndex = 1;
			this.buttonNewMorph.Text = "> click to save new morph <";
			this.buttonNewMorph.UseVisualStyleBackColor = true;
			this.buttonNewMorph.Click += new System.EventHandler(this.buttonNewMorph_Click);
			// 
			// buttonDescEditor
			// 
			this.buttonDescEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDescEditor.Location = new System.Drawing.Point(487, 12);
			this.buttonDescEditor.Name = "buttonDescEditor";
			this.buttonDescEditor.Size = new System.Drawing.Size(75, 23);
			this.buttonDescEditor.TabIndex = 2;
			this.buttonDescEditor.Text = "desc...";
			this.buttonDescEditor.UseVisualStyleBackColor = true;
			this.buttonDescEditor.Click += new System.EventHandler(this.buttonDescEditor_Click);
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCommand,
            this.columnHeaderName,
            this.columnHeaderMessage});
			this.listView.ContextMenuStrip = this.contextmenu;
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(12, 189);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(550, 252);
			this.listView.TabIndex = 3;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderCommand
			// 
			this.columnHeaderCommand.Text = "Command";
			this.columnHeaderCommand.Width = 114;
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Long Name";
			this.columnHeaderName.Width = 121;
			// 
			// columnHeaderMessage
			// 
			this.columnHeaderMessage.Text = "Morph Message";
			this.columnHeaderMessage.Width = 199;
			// 
			// contextmenu
			// 
			this.contextmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.morphToolStripMenuItem,
            this.qMorphToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.overwriteToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.contextmenu.Name = "contextmenu";
			this.contextmenu.Size = new System.Drawing.Size(139, 120);
			// 
			// morphToolStripMenuItem
			// 
			this.morphToolStripMenuItem.Name = "morphToolStripMenuItem";
			this.morphToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.morphToolStripMenuItem.Text = "Morph";
			this.morphToolStripMenuItem.Click += new System.EventHandler(this.morphToolStripMenuItem_Click);
			// 
			// qMorphToolStripMenuItem
			// 
			this.qMorphToolStripMenuItem.Name = "qMorphToolStripMenuItem";
			this.qMorphToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.qMorphToolStripMenuItem.Text = "QMorph";
			this.qMorphToolStripMenuItem.Click += new System.EventHandler(this.qMorphToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.editToolStripMenuItem.Text = "Edit";
			this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
			// 
			// overwriteToolStripMenuItem
			// 
			this.overwriteToolStripMenuItem.Name = "overwriteToolStripMenuItem";
			this.overwriteToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.overwriteToolStripMenuItem.Text = "Overwrite";
			this.overwriteToolStripMenuItem.Click += new System.EventHandler(this.overwriteToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "command:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "long name:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(31, 123);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "message:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(212, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(238, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "<- the command name for the morph";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(286, 94);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(367, 17);
			this.label5.TabIndex = 8;
			this.label5.Text = "<- a longer name only you see, shows up in the list below";
			// 
			// textBoxCommand
			// 
			this.textBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCommand.Location = new System.Drawing.Point(106, 63);
			this.textBoxCommand.Name = "textBoxCommand";
			this.textBoxCommand.Size = new System.Drawing.Size(100, 22);
			this.textBoxCommand.TabIndex = 9;
			// 
			// textBoxLongName
			// 
			this.textBoxLongName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLongName.Location = new System.Drawing.Point(106, 91);
			this.textBoxLongName.Name = "textBoxLongName";
			this.textBoxLongName.Size = new System.Drawing.Size(174, 22);
			this.textBoxLongName.TabIndex = 10;
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessage.Location = new System.Drawing.Point(106, 120);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(456, 22);
			this.textBoxMessage.TabIndex = 11;
			// 
			// buttonNewCancel
			// 
			this.buttonNewCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNewCancel.Location = new System.Drawing.Point(185, 34);
			this.buttonNewCancel.Name = "buttonNewCancel";
			this.buttonNewCancel.Size = new System.Drawing.Size(193, 23);
			this.buttonNewCancel.TabIndex = 12;
			this.buttonNewCancel.Text = "> cancel new morph <";
			this.buttonNewCancel.UseVisualStyleBackColor = true;
			this.buttonNewCancel.Click += new System.EventHandler(this.buttonNewCancel_Click);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(49, 145);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(497, 41);
			this.label6.TabIndex = 13;
			this.label6.Text = "^ An emote that displays when you use this morh (beginning : is not needed) Leave" +
    " blank for default ^";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(30, 168);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(114, 17);
			this.label7.TabIndex = 14;
			this.label7.Text = "morph inventory:";
			// 
			// MorphHelperWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 453);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.buttonNewCancel);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.textBoxLongName);
			this.Controls.Add(this.textBoxCommand);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.buttonDescEditor);
			this.Controls.Add(this.buttonNewMorph);
			this.Controls.Add(this.buttonRefresh);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MorphHelperWindow";
			this.Text = "MorphHelper";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MorphHelperWindow_HelpButtonClicked);
			this.Controls.SetChildIndex(this.buttonRefresh, 0);
			this.Controls.SetChildIndex(this.buttonNewMorph, 0);
			this.Controls.SetChildIndex(this.buttonDescEditor, 0);
			this.Controls.SetChildIndex(this.listView, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.textBoxCommand, 0);
			this.Controls.SetChildIndex(this.textBoxLongName, 0);
			this.Controls.SetChildIndex(this.textBoxMessage, 0);
			this.Controls.SetChildIndex(this.buttonNewCancel, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.contextmenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Button buttonNewMorph;
		private System.Windows.Forms.Button buttonDescEditor;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeaderCommand;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderMessage;
		private System.Windows.Forms.ContextMenuStrip contextmenu;
		private System.Windows.Forms.ToolStripMenuItem morphToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem qMorphToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem overwriteToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxCommand;
		private System.Windows.Forms.TextBox textBoxLongName;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Button buttonNewCancel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
	}
}