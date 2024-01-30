namespace AdjustServiceApply.Forms
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
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this.buttonUpdateApplyInfo = new System.Windows.Forms.Button();
			this.buttonUpdateCustomerInfo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePickerCustomer = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerApply = new System.Windows.Forms.DateTimePicker();
			this.buttonExit = new System.Windows.Forms.Button();
			this.listBoxCustomerLog = new System.Windows.Forms.ListBox();
			this.listBoxApplyLog = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonUpdateApplyInfo
			// 
			this.buttonUpdateApplyInfo.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonUpdateApplyInfo.Location = new System.Drawing.Point(318, 12);
			this.buttonUpdateApplyInfo.Margin = new System.Windows.Forms.Padding(4);
			this.buttonUpdateApplyInfo.Name = "buttonUpdateApplyInfo";
			this.buttonUpdateApplyInfo.Size = new System.Drawing.Size(209, 91);
			this.buttonUpdateApplyInfo.TabIndex = 5;
			this.buttonUpdateApplyInfo.Text = "2. 申込情報更新";
			this.buttonUpdateApplyInfo.UseVisualStyleBackColor = true;
			this.buttonUpdateApplyInfo.Click += new System.EventHandler(this.buttonUpdateApplyInfo_Click);
			// 
			// buttonUpdateCustomerInfo
			// 
			this.buttonUpdateCustomerInfo.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonUpdateCustomerInfo.Location = new System.Drawing.Point(14, 12);
			this.buttonUpdateCustomerInfo.Margin = new System.Windows.Forms.Padding(4);
			this.buttonUpdateCustomerInfo.Name = "buttonUpdateCustomerInfo";
			this.buttonUpdateCustomerInfo.Size = new System.Drawing.Size(209, 91);
			this.buttonUpdateCustomerInfo.TabIndex = 0;
			this.buttonUpdateCustomerInfo.Text = "1. 顧客情報更新";
			this.buttonUpdateCustomerInfo.UseVisualStyleBackColor = true;
			this.buttonUpdateCustomerInfo.Click += new System.EventHandler(this.buttonUpdateCustomerInfo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 113);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(143, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "■顧客情報 前回同期日時";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(318, 113);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(143, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "■申込情報 前回同期日時";
			// 
			// dateTimePickerCustomer
			// 
			this.dateTimePickerCustomer.CustomFormat = "yyyy/MM/dd HH:mm:ss";
			this.dateTimePickerCustomer.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerCustomer.Location = new System.Drawing.Point(14, 132);
			this.dateTimePickerCustomer.Margin = new System.Windows.Forms.Padding(4);
			this.dateTimePickerCustomer.Name = "dateTimePickerCustomer";
			this.dateTimePickerCustomer.Size = new System.Drawing.Size(162, 23);
			this.dateTimePickerCustomer.TabIndex = 2;
			// 
			// dateTimePickerApply
			// 
			this.dateTimePickerApply.CustomFormat = "yyyy/MM/dd HH:mm:ss";
			this.dateTimePickerApply.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerApply.Location = new System.Drawing.Point(318, 132);
			this.dateTimePickerApply.Margin = new System.Windows.Forms.Padding(4);
			this.dateTimePickerApply.Name = "dateTimePickerApply";
			this.dateTimePickerApply.Size = new System.Drawing.Size(162, 23);
			this.dateTimePickerApply.TabIndex = 7;
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(535, 12);
			this.buttonExit.Margin = new System.Windows.Forms.Padding(4);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(69, 36);
			this.buttonExit.TabIndex = 10;
			this.buttonExit.Text = "終了";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// listBoxCustomerLog
			// 
			this.listBoxCustomerLog.FormattingEnabled = true;
			this.listBoxCustomerLog.ItemHeight = 15;
			this.listBoxCustomerLog.Location = new System.Drawing.Point(14, 184);
			this.listBoxCustomerLog.Margin = new System.Windows.Forms.Padding(4);
			this.listBoxCustomerLog.Name = "listBoxCustomerLog";
			this.listBoxCustomerLog.Size = new System.Drawing.Size(286, 499);
			this.listBoxCustomerLog.TabIndex = 4;
			this.listBoxCustomerLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxCustomerLog_MouseDoubleClick);
			// 
			// listBoxApplyLog
			// 
			this.listBoxApplyLog.FormattingEnabled = true;
			this.listBoxApplyLog.ItemHeight = 15;
			this.listBoxApplyLog.Location = new System.Drawing.Point(318, 184);
			this.listBoxApplyLog.Margin = new System.Windows.Forms.Padding(4);
			this.listBoxApplyLog.Name = "listBoxApplyLog";
			this.listBoxApplyLog.Size = new System.Drawing.Size(286, 499);
			this.listBoxApplyLog.TabIndex = 9;
			this.listBoxApplyLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxApplyLog_MouseDoubleClick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 165);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(109, 15);
			this.label3.TabIndex = 3;
			this.label3.Text = "■顧客情報更新ログ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(318, 165);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(109, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "■申込情報更新ログ";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(616, 696);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listBoxApplyLog);
			this.Controls.Add(this.listBoxCustomerLog);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.dateTimePickerApply);
			this.Controls.Add(this.dateTimePickerCustomer);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonUpdateCustomerInfo);
			this.Controls.Add(this.buttonUpdateApplyInfo);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "申込情報調整処理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonUpdateApplyInfo;
		private System.Windows.Forms.Button buttonUpdateCustomerInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerCustomer;
		private System.Windows.Forms.DateTimePicker dateTimePickerApply;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.ListBox listBoxCustomerLog;
		private System.Windows.Forms.ListBox listBoxApplyLog;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}

