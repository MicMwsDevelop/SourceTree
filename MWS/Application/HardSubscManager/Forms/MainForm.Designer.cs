namespace HardSubscManager.Forms
{
	partial class MainForm
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
			this.buttonManager = new System.Windows.Forms.Button();
			this.buttonNotify = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonManager
			// 
			this.buttonManager.Location = new System.Drawing.Point(12, 12);
			this.buttonManager.Name = "buttonManager";
			this.buttonManager.Size = new System.Drawing.Size(455, 75);
			this.buttonManager.TabIndex = 0;
			this.buttonManager.Text = "ハードサブスク契約管理";
			this.buttonManager.UseVisualStyleBackColor = true;
			this.buttonManager.Click += new System.EventHandler(this.buttonManager_Click);
			// 
			// buttonNotify
			// 
			this.buttonNotify.Location = new System.Drawing.Point(12, 93);
			this.buttonNotify.Name = "buttonNotify";
			this.buttonNotify.Size = new System.Drawing.Size(455, 75);
			this.buttonNotify.TabIndex = 1;
			this.buttonNotify.Text = "利用期限通知";
			this.buttonNotify.UseVisualStyleBackColor = true;
			this.buttonNotify.Click += new System.EventHandler(this.buttonNotify_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(482, 176);
			this.Controls.Add(this.buttonNotify);
			this.Controls.Add(this.buttonManager);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "MainForm";
			this.Text = "ハードサブスク管理";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonManager;
		private System.Windows.Forms.Button buttonNotify;
	}
}