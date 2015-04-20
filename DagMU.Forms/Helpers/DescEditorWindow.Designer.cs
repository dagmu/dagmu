namespace DagMU.Forms.Helpers
{
	partial class DescEditorWindow
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
			this.muckSpecies = new DagMU.Forms.MuckPropertyTextBox();
			this.muckScent = new DagMU.Forms.MuckPropertyTextBox();
			this.muckGender = new DagMU.Forms.MuckPropertyTextBox();
			this.muckDesc = new DagMU.Forms.MuckPropertyTextBox();
			this.muckSay = new DagMU.Forms.MuckPropertyTextBox();
			this.muckOSay = new DagMU.Forms.MuckPropertyTextBox();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.buttonTest = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonMorphs = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelName = new System.Windows.Forms.Label();
			this.muckLongName = new DagMU.Forms.MuckPropertyTextBox();
			this.muckActionText = new DagMU.Forms.MuckPropertyTextBox();
			this.buttonMorph = new System.Windows.Forms.Button();
			this.buttonQMorph = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// muckSpecies
			// 
			this.muckSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckSpecies.Checked = false;
			this.muckSpecies.Input = "";
			this.muckSpecies.ListName = null;
			this.muckSpecies.Location = new System.Drawing.Point(12, 33);
			this.muckSpecies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.muckSpecies.MuckCommandToGet = "exa me=/species";
			this.muckSpecies.MuckCommandToSet = "@set me=/species:";
			this.muckSpecies.Multiline = false;
			this.muckSpecies.MultiLineNotReally = false;
			this.muckSpecies.Name = "muckSpecies";
			this.muckSpecies.Size = new System.Drawing.Size(362, 23);
			this.muckSpecies.TabIndex = 0;
			this.muckSpecies.Title = "species:";
			// 
			// muckScent
			// 
			this.muckScent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckScent.Checked = false;
			this.muckScent.Input = "";
			this.muckScent.ListName = null;
			this.muckScent.Location = new System.Drawing.Point(12, 62);
			this.muckScent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.muckScent.MuckCommandToGet = "exa me=/_scent";
			this.muckScent.MuckCommandToSet = "@set me=/_scent:";
			this.muckScent.Multiline = true;
			this.muckScent.MultiLineNotReally = true;
			this.muckScent.Name = "muckScent";
			this.muckScent.Size = new System.Drawing.Size(362, 75);
			this.muckScent.TabIndex = 1;
			this.muckScent.Title = "scent:";
			// 
			// muckGender
			// 
			this.muckGender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckGender.Checked = false;
			this.muckGender.Input = "";
			this.muckGender.ListName = null;
			this.muckGender.Location = new System.Drawing.Point(190, 4);
			this.muckGender.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.muckGender.MuckCommandToGet = "exa me=/sex";
			this.muckGender.MuckCommandToSet = "@set me=/sex:";
			this.muckGender.Multiline = false;
			this.muckGender.MultiLineNotReally = false;
			this.muckGender.Name = "muckGender";
			this.muckGender.Size = new System.Drawing.Size(184, 23);
			this.muckGender.TabIndex = 2;
			this.muckGender.Title = "gender:";
			// 
			// muckDesc
			// 
			this.muckDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckDesc.AutoScroll = true;
			this.muckDesc.Checked = false;
			this.muckDesc.Input = "";
			this.muckDesc.ListName = null;
			this.muckDesc.Location = new System.Drawing.Point(12, 143);
			this.muckDesc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.muckDesc.MuckCommandToGet = null;
			this.muckDesc.MuckCommandToSet = null;
			this.muckDesc.Multiline = true;
			this.muckDesc.MultiLineNotReally = false;
			this.muckDesc.Name = "muckDesc";
			this.muckDesc.Size = new System.Drawing.Size(532, 223);
			this.muckDesc.TabIndex = 3;
			this.muckDesc.Title = "desc:";
			// 
			// muckSay
			// 
			this.muckSay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckSay.Checked = false;
			this.muckSay.Input = "";
			this.muckSay.ListName = null;
			this.muckSay.Location = new System.Drawing.Point(12, 372);
			this.muckSay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.muckSay.MuckCommandToGet = "exa me=/_say/";
			this.muckSay.MuckCommandToSet = "@set me=/_say/format:";
			this.muckSay.Multiline = false;
			this.muckSay.MultiLineNotReally = false;
			this.muckSay.Name = "muckSay";
			this.muckSay.Size = new System.Drawing.Size(428, 23);
			this.muckSay.TabIndex = 4;
			this.muckSay.Title = "you see:";
			// 
			// muckOSay
			// 
			this.muckOSay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckOSay.Checked = false;
			this.muckOSay.Input = "";
			this.muckOSay.ListName = null;
			this.muckOSay.Location = new System.Drawing.Point(12, 401);
			this.muckOSay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.muckOSay.MuckCommandToGet = null;
			this.muckOSay.MuckCommandToSet = "@set me=/_say/oformat:";
			this.muckOSay.Multiline = false;
			this.muckOSay.MultiLineNotReally = false;
			this.muckOSay.Name = "muckOSay";
			this.muckOSay.Size = new System.Drawing.Size(428, 23);
			this.muckOSay.TabIndex = 5;
			this.muckOSay.Title = "others see:";
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRefresh.Location = new System.Drawing.Point(433, 33);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 6;
			this.buttonRefresh.Text = "- Refresh -";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// buttonTest
			// 
			this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTest.ForeColor = System.Drawing.Color.Red;
			this.buttonTest.Location = new System.Drawing.Point(380, 4);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(53, 23);
			this.buttonTest.TabIndex = 7;
			this.buttonTest.Text = "Test";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(433, 91);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 8;
			this.buttonSave.Text = "- Save -";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonMorphs
			// 
			this.buttonMorphs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonMorphs.Enabled = false;
			this.buttonMorphs.Location = new System.Drawing.Point(405, 62);
			this.buttonMorphs.Name = "buttonMorphs";
			this.buttonMorphs.Size = new System.Drawing.Size(139, 23);
			this.buttonMorphs.TabIndex = 9;
			this.buttonMorphs.Text = "Open Morphs...";
			this.buttonMorphs.UseVisualStyleBackColor = true;
			this.buttonMorphs.Click += new System.EventHandler(this.buttonMorphs_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(455, 375);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "ex: You say \"%m\"";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(472, 402);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "ex: says \"%m\"";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 9);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "morph name:";
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelName.Location = new System.Drawing.Point(82, 9);
			this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(0, 13);
			this.labelName.TabIndex = 13;
			// 
			// muckLongName
			// 
			this.muckLongName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckLongName.Checked = false;
			this.muckLongName.Enabled = false;
			this.muckLongName.Input = "";
			this.muckLongName.ListName = null;
			this.muckLongName.Location = new System.Drawing.Point(12, 431);
			this.muckLongName.MuckCommandToGet = null;
			this.muckLongName.MuckCommandToSet = null;
			this.muckLongName.Multiline = false;
			this.muckLongName.MultiLineNotReally = false;
			this.muckLongName.Name = "muckLongName";
			this.muckLongName.Size = new System.Drawing.Size(362, 23);
			this.muckLongName.TabIndex = 14;
			this.muckLongName.Title = "long name:";
			// 
			// muckActionText
			// 
			this.muckActionText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.muckActionText.Checked = false;
			this.muckActionText.Enabled = false;
			this.muckActionText.Input = "";
			this.muckActionText.ListName = null;
			this.muckActionText.Location = new System.Drawing.Point(12, 460);
			this.muckActionText.MuckCommandToGet = null;
			this.muckActionText.MuckCommandToSet = null;
			this.muckActionText.Multiline = false;
			this.muckActionText.MultiLineNotReally = false;
			this.muckActionText.Name = "muckActionText";
			this.muckActionText.Size = new System.Drawing.Size(297, 23);
			this.muckActionText.TabIndex = 15;
			this.muckActionText.Title = "action text:";
			// 
			// buttonMorph
			// 
			this.buttonMorph.Enabled = false;
			this.buttonMorph.Location = new System.Drawing.Point(405, 118);
			this.buttonMorph.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonMorph.Name = "buttonMorph";
			this.buttonMorph.Size = new System.Drawing.Size(64, 19);
			this.buttonMorph.TabIndex = 16;
			this.buttonMorph.Text = "Morph";
			this.buttonMorph.UseVisualStyleBackColor = true;
			this.buttonMorph.Click += new System.EventHandler(this.buttonMorph_Click);
			// 
			// buttonQMorph
			// 
			this.buttonQMorph.Enabled = false;
			this.buttonQMorph.Location = new System.Drawing.Point(474, 118);
			this.buttonQMorph.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonQMorph.Name = "buttonQMorph";
			this.buttonQMorph.Size = new System.Drawing.Size(70, 19);
			this.buttonQMorph.TabIndex = 17;
			this.buttonQMorph.Text = "QMorph";
			this.buttonQMorph.UseVisualStyleBackColor = true;
			this.buttonQMorph.Click += new System.EventHandler(this.buttonQMorph_Click);
			// 
			// DescEditorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 494);
			this.Controls.Add(this.buttonQMorph);
			this.Controls.Add(this.buttonMorph);
			this.Controls.Add(this.muckActionText);
			this.Controls.Add(this.muckLongName);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonMorphs);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonTest);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.muckOSay);
			this.Controls.Add(this.muckSay);
			this.Controls.Add(this.muckDesc);
			this.Controls.Add(this.muckGender);
			this.Controls.Add(this.muckScent);
			this.Controls.Add(this.muckSpecies);
			this.HelpButton = true;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DescEditorWindow";
			this.Text = "DescEditor";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.DescEditorWindow_HelpButtonClicked);
			this.Controls.SetChildIndex(this.muckSpecies, 0);
			this.Controls.SetChildIndex(this.muckScent, 0);
			this.Controls.SetChildIndex(this.muckGender, 0);
			this.Controls.SetChildIndex(this.muckDesc, 0);
			this.Controls.SetChildIndex(this.muckSay, 0);
			this.Controls.SetChildIndex(this.muckOSay, 0);
			this.Controls.SetChildIndex(this.buttonRefresh, 0);
			this.Controls.SetChildIndex(this.buttonTest, 0);
			this.Controls.SetChildIndex(this.buttonSave, 0);
			this.Controls.SetChildIndex(this.buttonMorphs, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.labelName, 0);
			this.Controls.SetChildIndex(this.muckLongName, 0);
			this.Controls.SetChildIndex(this.muckActionText, 0);
			this.Controls.SetChildIndex(this.buttonMorph, 0);
			this.Controls.SetChildIndex(this.buttonQMorph, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MuckPropertyTextBox muckSpecies;
		private MuckPropertyTextBox muckScent;
		private MuckPropertyTextBox muckGender;
		private MuckPropertyTextBox muckDesc;
		private MuckPropertyTextBox muckSay;
		private MuckPropertyTextBox muckOSay;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Button buttonTest;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonMorphs;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelName;
		private MuckPropertyTextBox muckLongName;
		private MuckPropertyTextBox muckActionText;
		private System.Windows.Forms.Button buttonMorph;
		private System.Windows.Forms.Button buttonQMorph;
	}
}