namespace DagMU.Forms
{
	partial class aLogViewer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System.ComponentModel.IContainer components = null;

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
		void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("1");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("3");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("4");
			this.listView1 = new System.Windows.Forms.ListView();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
			this.listView1.Location = new System.Drawing.Point(155, 530);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(284, 89);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(13, 259);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(121, 360);
			this.treeView1.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(13, 233);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(426, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "C:\\shared130";
			// 
			// aLogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 698);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.listView1);
			this.Name = "aLogViewer";
			this.Text = "aLogViewer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		System.Windows.Forms.ListView listView1;
		System.Windows.Forms.TreeView treeView1;
		System.Windows.Forms.TextBox textBox1;
	}
}