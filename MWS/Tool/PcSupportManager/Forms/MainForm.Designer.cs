namespace PcSupportManager.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonManagement = new System.Windows.Forms.Button();
			this.buttonSendMail = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonManagement
			// 
			this.buttonManagement.Location = new System.Drawing.Point(23, 27);
			this.buttonManagement.Name = "buttonManagement";
			this.buttonManagement.Size = new System.Drawing.Size(313, 45);
			this.buttonManagement.TabIndex = 0;
			this.buttonManagement.Text = "管理情報登録";
			this.buttonManagement.UseVisualStyleBackColor = true;
			this.buttonManagement.Click += new System.EventHandler(this.buttonManagement_Click);
			// 
			// buttonSendMail
			// 
			this.buttonSendMail.Location = new System.Drawing.Point(23, 78);
			this.buttonSendMail.Name = "buttonSendMail";
			this.buttonSendMail.Size = new System.Drawing.Size(313, 45);
			this.buttonSendMail.TabIndex = 1;
			this.buttonSendMail.Text = "メール送信";
			this.buttonSendMail.UseVisualStyleBackColor = true;
			this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 146);
			this.Controls.Add(this.buttonSendMail);
			this.Controls.Add(this.buttonManagement);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PC安心サポート管理ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonManagement;
		private System.Windows.Forms.Button buttonSendMail;
	}
}

