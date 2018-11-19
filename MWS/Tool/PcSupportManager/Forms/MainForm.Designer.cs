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
			this.label1 = new System.Windows.Forms.Label();
			this.dateTimePickerSystemDate = new System.Windows.Forms.DateTimePicker();
			this.buttonSoftMainte = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonManagement
			// 
			this.buttonManagement.Location = new System.Drawing.Point(24, 37);
			this.buttonManagement.Name = "buttonManagement";
			this.buttonManagement.Size = new System.Drawing.Size(313, 45);
			this.buttonManagement.TabIndex = 2;
			this.buttonManagement.Text = "管理情報登録";
			this.buttonManagement.UseVisualStyleBackColor = true;
			this.buttonManagement.Click += new System.EventHandler(this.buttonManagement_Click);
			// 
			// buttonSendMail
			// 
			this.buttonSendMail.Location = new System.Drawing.Point(24, 88);
			this.buttonSendMail.Name = "buttonSendMail";
			this.buttonSendMail.Size = new System.Drawing.Size(313, 45);
			this.buttonSendMail.TabIndex = 3;
			this.buttonSendMail.Text = "メール送信";
			this.buttonSendMail.UseVisualStyleBackColor = true;
			this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■システム日付";
			// 
			// dateTimePickerSystemDate
			// 
			this.dateTimePickerSystemDate.Location = new System.Drawing.Point(107, 12);
			this.dateTimePickerSystemDate.Name = "dateTimePickerSystemDate";
			this.dateTimePickerSystemDate.Size = new System.Drawing.Size(133, 19);
			this.dateTimePickerSystemDate.TabIndex = 1;
			this.dateTimePickerSystemDate.ValueChanged += new System.EventHandler(this.dateTimePickerSystemDate_ValueChanged);
			// 
			// buttonSoftMainte
			// 
			this.buttonSoftMainte.Location = new System.Drawing.Point(24, 139);
			this.buttonSoftMainte.Name = "buttonSoftMainte";
			this.buttonSoftMainte.Size = new System.Drawing.Size(313, 45);
			this.buttonSoftMainte.TabIndex = 4;
			this.buttonSoftMainte.Text = "製品サポート情報ソフト保守更新";
			this.buttonSoftMainte.UseVisualStyleBackColor = true;
			this.buttonSoftMainte.Click += new System.EventHandler(this.buttonSoftMainte_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(250, 190);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(87, 45);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(361, 251);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonSoftMainte);
			this.Controls.Add(this.dateTimePickerSystemDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonSendMail);
			this.Controls.Add(this.buttonManagement);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PC安心サポート管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonManagement;
		private System.Windows.Forms.Button buttonSendMail;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePickerSystemDate;
		private System.Windows.Forms.Button buttonSoftMainte;
		private System.Windows.Forms.Button buttonClose;
	}
}

