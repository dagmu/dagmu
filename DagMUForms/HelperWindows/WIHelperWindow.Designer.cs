namespace DagMU.Forms.HelperWindows
{
    partial class WIHelperWindow
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
			this.box = new System.Windows.Forms.FlowLayoutPanel();
			this.text = new System.Windows.Forms.RichTextBox();
			this.textBoxCustom = new System.Windows.Forms.TextBox();
			this.checkBoxCustom = new System.Windows.Forms.CheckBox();
			this.buttonClear = new System.Windows.Forms.Button();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.labelPreview = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// box
			// 
			this.box.Location = new System.Drawing.Point(12, 83);
			this.box.Name = "box";
			this.box.Size = new System.Drawing.Size(551, 291);
			this.box.TabIndex = 0;
			// 
			// text
			// 
			this.text.Location = new System.Drawing.Point(316, 43);
			this.text.Name = "text";
			this.text.Size = new System.Drawing.Size(100, 96);
			this.text.TabIndex = 2;
			this.text.Text = "";
			// 
			// textBoxCustom
			// 
			this.textBoxCustom.Location = new System.Drawing.Point(179, 3);
			this.textBoxCustom.Name = "textBoxCustom";
			this.textBoxCustom.Size = new System.Drawing.Size(100, 22);
			this.textBoxCustom.TabIndex = 3;
			// 
			// checkBoxCustom
			// 
			this.checkBoxCustom.AutoSize = true;
			this.checkBoxCustom.Location = new System.Drawing.Point(64, 7);
			this.checkBoxCustom.Name = "checkBoxCustom";
			this.checkBoxCustom.Size = new System.Drawing.Size(115, 21);
			this.checkBoxCustom.TabIndex = 4;
			this.checkBoxCustom.Text = "Custom Field:";
			this.checkBoxCustom.UseVisualStyleBackColor = true;
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(285, 3);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(75, 23);
			this.buttonClear.TabIndex = 5;
			this.buttonClear.Text = "Clear";
			this.buttonClear.UseVisualStyleBackColor = true;
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(367, 3);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 6;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(497, 3);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 7;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// labelPreview
			// 
			this.labelPreview.AutoSize = true;
			this.labelPreview.Location = new System.Drawing.Point(13, 381);
			this.labelPreview.Name = "labelPreview";
			this.labelPreview.Size = new System.Drawing.Size(128, 17);
			this.labelPreview.TabIndex = 8;
			this.labelPreview.Text = "label1preview flags";
			// 
			// WIHelperWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(575, 446);
			this.Controls.Add(this.labelPreview);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.buttonClear);
			this.Controls.Add(this.checkBoxCustom);
			this.Controls.Add(this.textBoxCustom);
			this.Controls.Add(this.text);
			this.Controls.Add(this.box);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WIHelperWindow";
			this.Text = "WIHelperWindow";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.WIHelperWindow_HelpButtonClicked);
			this.Load += new System.EventHandler(this.WIHelperWindow_Load);
			this.Controls.SetChildIndex(this.box, 0);
			this.Controls.SetChildIndex(this.text, 0);
			this.Controls.SetChildIndex(this.textBoxCustom, 0);
			this.Controls.SetChildIndex(this.checkBoxCustom, 0);
			this.Controls.SetChildIndex(this.buttonClear, 0);
			this.Controls.SetChildIndex(this.buttonRefresh, 0);
			this.Controls.SetChildIndex(this.buttonSave, 0);
			this.Controls.SetChildIndex(this.labelPreview, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.FlowLayoutPanel box;
		private System.Windows.Forms.RichTextBox text;
		private System.Windows.Forms.TextBox textBoxCustom;
		private System.Windows.Forms.CheckBox checkBoxCustom;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelPreview;
    }
}