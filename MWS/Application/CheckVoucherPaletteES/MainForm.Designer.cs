namespace CheckVoucherPaletteES
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
			this.buttonExec = new System.Windows.Forms.Button();
			this.dateTimePickerSearchDate = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listViewES = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listBoxError = new System.Windows.Forms.ListBox();
			this.buttonOutputLog = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(192, 8);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(101, 30);
			this.buttonExec.TabIndex = 2;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// dateTimePickerSearchDate
			// 
			this.dateTimePickerSearchDate.Location = new System.Drawing.Point(60, 12);
			this.dateTimePickerSearchDate.Name = "dateTimePickerSearchDate";
			this.dateTimePickerSearchDate.Size = new System.Drawing.Size(126, 19);
			this.dateTimePickerSearchDate.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "検索日";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(1147, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(139, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "Ver 1.00（2019/11/15）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "伝票情報";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 445);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "エラー内容";
			// 
			// listViewES
			// 
			this.listViewES.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewES.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader13,
            this.columnHeader12,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
			this.listViewES.FullRowSelect = true;
			this.listViewES.Location = new System.Drawing.Point(14, 58);
			this.listViewES.Name = "listViewES";
			this.listViewES.Size = new System.Drawing.Size(1272, 374);
			this.listViewES.TabIndex = 6;
			this.listViewES.UseCompatibleStateImageBehavior = false;
			this.listViewES.View = System.Windows.Forms.View.Details;
			this.listViewES.SelectedIndexChanged += new System.EventHandler(this.listViewES_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "判定";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "受注番号";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "受注日";
			this.columnHeader3.Width = 75;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "受注承認日";
			this.columnHeader4.Width = 75;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "売上承認日";
			this.columnHeader5.Width = 75;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "納期";
			this.columnHeader6.Width = 75;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "販売種別";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "販売先コード ";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "販売先";
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "顧客No";
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "顧客名";
			this.columnHeader11.Width = 140;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "商品名";
			this.columnHeader13.Width = 180;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "開始日";
			this.columnHeader12.Width = 75;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "終了日";
			this.columnHeader14.Width = 75;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "拠点名";
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "担当者";
			this.columnHeader16.Width = 80;
			// 
			// listBoxError
			// 
			this.listBoxError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxError.FormattingEnabled = true;
			this.listBoxError.ItemHeight = 12;
			this.listBoxError.Location = new System.Drawing.Point(13, 461);
			this.listBoxError.Name = "listBoxError";
			this.listBoxError.Size = new System.Drawing.Size(1273, 100);
			this.listBoxError.TabIndex = 8;
			// 
			// buttonOutputLog
			// 
			this.buttonOutputLog.Location = new System.Drawing.Point(299, 8);
			this.buttonOutputLog.Name = "buttonOutputLog";
			this.buttonOutputLog.Size = new System.Drawing.Size(101, 30);
			this.buttonOutputLog.TabIndex = 3;
			this.buttonOutputLog.Text = "ログ出力";
			this.buttonOutputLog.UseVisualStyleBackColor = true;
			this.buttonOutputLog.Click += new System.EventHandler(this.buttonOutputLog_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1298, 578);
			this.Controls.Add(this.buttonOutputLog);
			this.Controls.Add(this.listBoxError);
			this.Controls.Add(this.listViewES);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePickerSearchDate);
			this.Controls.Add(this.buttonExec);
			this.Name = "MainForm";
			this.Text = "paletteES 起票確認ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.DateTimePicker dateTimePickerSearchDate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView listViewES;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ListBox listBoxError;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonOutputLog;
	}
}

