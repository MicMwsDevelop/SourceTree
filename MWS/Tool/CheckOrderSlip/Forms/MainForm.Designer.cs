namespace CheckOrderSlip.Forms
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
			this.label4 = new System.Windows.Forms.Label();
			this.listViewSlip = new System.Windows.Forms.ListView();
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
			this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listBoxError = new System.Windows.Forms.ListBox();
			this.buttonExcel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonSale = new System.Windows.Forms.RadioButton();
			this.radioButtonOrderAccept = new System.Windows.Forms.RadioButton();
			this.radioButtonOrder = new System.Windows.Forms.RadioButton();
			this.checkBoxOnlyError = new System.Windows.Forms.CheckBox();
			this.comboBoxMode = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(444, 5);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(101, 30);
			this.buttonExec.TabIndex = 4;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// dateTimePickerSearchDate
			// 
			this.dateTimePickerSearchDate.Location = new System.Drawing.Point(305, 9);
			this.dateTimePickerSearchDate.Name = "dateTimePickerSearchDate";
			this.dateTimePickerSearchDate.Size = new System.Drawing.Size(126, 19);
			this.dateTimePickerSearchDate.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(258, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "検索日";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(1474, 591);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 12);
			this.label2.TabIndex = 11;
			this.label2.Text = "Ver1.11（2020/10/29）";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 591);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 12);
			this.label4.TabIndex = 9;
			this.label4.Text = "エラー内容";
			// 
			// listViewSlip
			// 
			this.listViewSlip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewSlip.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
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
            this.columnHeader17,
            this.columnHeader13});
			this.listViewSlip.FullRowSelect = true;
			this.listViewSlip.HideSelection = false;
			this.listViewSlip.Location = new System.Drawing.Point(15, 39);
			this.listViewSlip.Name = "listViewSlip";
			this.listViewSlip.Size = new System.Drawing.Size(1593, 542);
			this.listViewSlip.TabIndex = 8;
			this.listViewSlip.UseCompatibleStateImageBehavior = false;
			this.listViewSlip.View = System.Windows.Forms.View.Details;
			this.listViewSlip.SelectedIndexChanged += new System.EventHandler(this.listViewSlip_SelectedIndexChanged);
			this.listViewSlip.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSlip_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "判定";
			this.columnHeader1.Width = 40;
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
			// columnHeader13
			// 
			this.columnHeader13.Text = "リプレース";
			this.columnHeader13.Width = 90;
			// 
			// listBoxError
			// 
			this.listBoxError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxError.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listBoxError.FormattingEnabled = true;
			this.listBoxError.ItemHeight = 16;
			this.listBoxError.Location = new System.Drawing.Point(15, 606);
			this.listBoxError.Name = "listBoxError";
			this.listBoxError.Size = new System.Drawing.Size(1593, 100);
			this.listBoxError.TabIndex = 10;
			// 
			// buttonExcel
			// 
			this.buttonExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExcel.Location = new System.Drawing.Point(1507, 5);
			this.buttonExcel.Name = "buttonExcel";
			this.buttonExcel.Size = new System.Drawing.Size(101, 30);
			this.buttonExcel.TabIndex = 7;
			this.buttonExcel.Text = "EXCEL出力";
			this.buttonExcel.UseVisualStyleBackColor = true;
			this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonSale);
			this.groupBox1.Controls.Add(this.radioButtonOrderAccept);
			this.groupBox1.Controls.Add(this.radioButtonOrder);
			this.groupBox1.Location = new System.Drawing.Point(551, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 32);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "検索日";
			// 
			// radioButtonSale
			// 
			this.radioButtonSale.AutoSize = true;
			this.radioButtonSale.Location = new System.Drawing.Point(201, 10);
			this.radioButtonSale.Name = "radioButtonSale";
			this.radioButtonSale.Size = new System.Drawing.Size(83, 16);
			this.radioButtonSale.TabIndex = 2;
			this.radioButtonSale.Text = "売上承認日";
			this.radioButtonSale.UseVisualStyleBackColor = true;
			this.radioButtonSale.CheckedChanged += new System.EventHandler(this.radioButtonSale_CheckedChanged);
			// 
			// radioButtonOrderAccept
			// 
			this.radioButtonOrderAccept.AutoSize = true;
			this.radioButtonOrderAccept.Location = new System.Drawing.Point(112, 10);
			this.radioButtonOrderAccept.Name = "radioButtonOrderAccept";
			this.radioButtonOrderAccept.Size = new System.Drawing.Size(83, 16);
			this.radioButtonOrderAccept.TabIndex = 1;
			this.radioButtonOrderAccept.Text = "受注承認日";
			this.radioButtonOrderAccept.UseVisualStyleBackColor = true;
			this.radioButtonOrderAccept.CheckedChanged += new System.EventHandler(this.radioButtonOrderAccept_CheckedChanged);
			// 
			// radioButtonOrder
			// 
			this.radioButtonOrder.AutoSize = true;
			this.radioButtonOrder.Checked = true;
			this.radioButtonOrder.Location = new System.Drawing.Point(47, 10);
			this.radioButtonOrder.Name = "radioButtonOrder";
			this.radioButtonOrder.Size = new System.Drawing.Size(59, 16);
			this.radioButtonOrder.TabIndex = 0;
			this.radioButtonOrder.TabStop = true;
			this.radioButtonOrder.Text = "受注日";
			this.radioButtonOrder.UseVisualStyleBackColor = true;
			this.radioButtonOrder.CheckedChanged += new System.EventHandler(this.radioButtonOrder_CheckedChanged);
			// 
			// checkBoxOnlyError
			// 
			this.checkBoxOnlyError.AutoSize = true;
			this.checkBoxOnlyError.Location = new System.Drawing.Point(853, 13);
			this.checkBoxOnlyError.Name = "checkBoxOnlyError";
			this.checkBoxOnlyError.Size = new System.Drawing.Size(108, 16);
			this.checkBoxOnlyError.TabIndex = 6;
			this.checkBoxOnlyError.Text = "エラー行のみ表示";
			this.checkBoxOnlyError.UseVisualStyleBackColor = true;
			this.checkBoxOnlyError.CheckedChanged += new System.EventHandler(this.checkBoxOnlyError_CheckedChanged);
			// 
			// comboBoxMode
			// 
			this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMode.FormattingEnabled = true;
			this.comboBoxMode.Location = new System.Drawing.Point(72, 8);
			this.comboBoxMode.Name = "comboBoxMode";
			this.comboBoxMode.Size = new System.Drawing.Size(166, 20);
			this.comboBoxMode.TabIndex = 1;
			this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 12);
			this.label5.TabIndex = 0;
			this.label5.Text = "確認対象";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1620, 719);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboBoxMode);
			this.Controls.Add(this.checkBoxOnlyError);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonExcel);
			this.Controls.Add(this.listBoxError);
			this.Controls.Add(this.listViewSlip);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePickerSearchDate);
			this.Controls.Add(this.buttonExec);
			this.Name = "MainForm";
			this.Text = "伝票確認ツール";
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
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView listViewSlip;
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
		private System.Windows.Forms.RadioButton radioButtonSale;
		private System.Windows.Forms.RadioButton radioButtonOrderAccept;
		private System.Windows.Forms.RadioButton radioButtonOrder;
		private System.Windows.Forms.CheckBox checkBoxOnlyError;
		private System.Windows.Forms.ComboBox comboBoxMode;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ColumnHeader columnHeader13;
	}
}

