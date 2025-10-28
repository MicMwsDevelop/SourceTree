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
			this.buttonAddNewContract = new System.Windows.Forms.Button();
			this.buttonModifyContract = new System.Windows.Forms.Button();
			this.labelVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonAddNewContract
			// 
			this.buttonAddNewContract.Font = new System.Drawing.Font("BIZ UDゴシック", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonAddNewContract.Location = new System.Drawing.Point(12, 37);
			this.buttonAddNewContract.Name = "buttonAddNewContract";
			this.buttonAddNewContract.Size = new System.Drawing.Size(455, 75);
			this.buttonAddNewContract.TabIndex = 1;
			this.buttonAddNewContract.Text = "契約情報の新規登録";
			this.buttonAddNewContract.UseVisualStyleBackColor = true;
			this.buttonAddNewContract.Click += new System.EventHandler(this.buttonAddNewContract_Click);
			// 
			// buttonModifyContract
			// 
			this.buttonModifyContract.Font = new System.Drawing.Font("BIZ UDゴシック", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonModifyContract.Location = new System.Drawing.Point(12, 118);
			this.buttonModifyContract.Name = "buttonModifyContract";
			this.buttonModifyContract.Size = new System.Drawing.Size(455, 75);
			this.buttonModifyContract.TabIndex = 2;
			this.buttonModifyContract.Text = "契約情報の更新";
			this.buttonModifyContract.UseVisualStyleBackColor = true;
			this.buttonModifyContract.Click += new System.EventHandler(this.buttonModifyContract_Click);
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(337, 9);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(133, 13);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Ver1.00 2025/05/20";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(482, 207);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.buttonModifyContract);
			this.Controls.Add(this.buttonAddNewContract);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "ハードサブスク管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonAddNewContract;
		private System.Windows.Forms.Button buttonModifyContract;
		private System.Windows.Forms.Label labelVersion;
	}
}