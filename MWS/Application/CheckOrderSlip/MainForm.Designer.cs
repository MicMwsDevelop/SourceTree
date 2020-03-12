namespace CheckOrderSlip
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
			this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listBoxError = new System.Windows.Forms.ListBox();
			this.buttonExcel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonAll = new System.Windows.Forms.RadioButton();
			this.radioButtonJuchu = new System.Windows.Forms.RadioButton();
			this.radioButtonUriage = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
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
			this.label2.Location = new System.Drawing.Point(1291, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "Ver1.00（2020/02/21）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "受注伝票";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 786);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 12);
			this.label4.TabIndex = 8;
			this.label4.Text = "エラー内容";
			// 
			// listViewES
			// 
			this.listViewES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewES.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader18,
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17});
			this.listViewES.FullRowSelect = true;
			this.listViewES.Location = new System.Drawing.Point(15, 58);
			this.listViewES.Name = "listViewES";
			this.listViewES.Size = new System.Drawing.Size(1415, 714);
			this.listViewES.TabIndex = 7;
			this.listViewES.UseCompatibleStateImageBehavior = false;
			this.listViewES.View = System.Windows.Forms.View.Details;
			this.listViewES.SelectedIndexChanged += new System.EventHandler(this.listViewES_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "判定";
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "伝票種別";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "販売種別";
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
			// columnHeader8
			// 
			this.columnHeader8.Text = "顧客No";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "顧客名";
			this.columnHeader9.Width = 140;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "商品名";
			this.columnHeader10.Width = 180;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "開始日";
			this.columnHeader11.Width = 75;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "終了日";
			this.columnHeader12.Width = 75;
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
			// columnHeader17
			// 
			this.columnHeader17.Text = "件名";
			this.columnHeader17.Width = 300;
			// 
			// listBoxError
			// 
			this.listBoxError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxError.FormattingEnabled = true;
			this.listBoxError.ItemHeight = 12;
			this.listBoxError.Location = new System.Drawing.Point(15, 801);
			this.listBoxError.Name = "listBoxError";
			this.listBoxError.Size = new System.Drawing.Size(1415, 100);
			this.listBoxError.TabIndex = 9;
			// 
			// buttonExcel
			// 
			this.buttonExcel.Location = new System.Drawing.Point(299, 8);
			this.buttonExcel.Name = "buttonExcel";
			this.buttonExcel.Size = new System.Drawing.Size(101, 30);
			this.buttonExcel.TabIndex = 3;
			this.buttonExcel.Text = "EXCEL出力";
			this.buttonExcel.UseVisualStyleBackColor = true;
			this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonUriage);
			this.groupBox1.Controls.Add(this.radioButtonJuchu);
			this.groupBox1.Controls.Add(this.radioButtonAll);
			this.groupBox1.Location = new System.Drawing.Point(406, -3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(249, 44);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			// 
			// radioButtonAll
			// 
			this.radioButtonAll.AutoSize = true;
			this.radioButtonAll.Checked = true;
			this.radioButtonAll.Location = new System.Drawing.Point(16, 18);
			this.radioButtonAll.Name = "radioButtonAll";
			this.radioButtonAll.Size = new System.Drawing.Size(44, 16);
			this.radioButtonAll.TabIndex = 0;
			this.radioButtonAll.TabStop = true;
			this.radioButtonAll.Text = "全て";
			this.radioButtonAll.UseVisualStyleBackColor = true;
			this.radioButtonAll.CheckedChanged += new System.EventHandler(this.radioButtonAll_CheckedChanged);
			// 
			// radioButtonJuchu
			// 
			this.radioButtonJuchu.AutoSize = true;
			this.radioButtonJuchu.Location = new System.Drawing.Point(66, 18);
			this.radioButtonJuchu.Name = "radioButtonJuchu";
			this.radioButtonJuchu.Size = new System.Drawing.Size(83, 16);
			this.radioButtonJuchu.TabIndex = 1;
			this.radioButtonJuchu.Text = "受注承認済";
			this.radioButtonJuchu.UseVisualStyleBackColor = true;
			this.radioButtonJuchu.CheckedChanged += new System.EventHandler(this.radioButtonJuchu_CheckedChanged);
			// 
			// radioButtonUriage
			// 
			this.radioButtonUriage.AutoSize = true;
			this.radioButtonUriage.Location = new System.Drawing.Point(155, 18);
			this.radioButtonUriage.Name = "radioButtonUriage";
			this.radioButtonUriage.Size = new System.Drawing.Size(83, 16);
			this.radioButtonUriage.TabIndex = 2;
			this.radioButtonUriage.Text = "売上承認済";
			this.radioButtonUriage.UseVisualStyleBackColor = true;
			this.radioButtonUriage.CheckedChanged += new System.EventHandler(this.radioButtonUriage_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1442, 918);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonExcel);
			this.Controls.Add(this.listBoxError);
			this.Controls.Add(this.listViewES);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePickerSearchDate);
			this.Controls.Add(this.buttonExec);
			this.Name = "MainForm";
			this.Text = "受注伝票チェックツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ListBox listBoxError;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonExcel;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonUriage;
		private System.Windows.Forms.RadioButton radioButtonJuchu;
		private System.Windows.Forms.RadioButton radioButtonAll;
	}
}

