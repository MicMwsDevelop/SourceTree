namespace HardSubscManager.Forms
{
	partial class ContractForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePickerOrderDate = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.dateTimePickerCancelDate = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.listViewDetail = new System.Windows.Forms.ListView();
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
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.labelContractEndDate = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.dateTimePickerShippingDate = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.labelContractStartDate = new System.Windows.Forms.Label();
			this.labelBillingStartDate = new System.Windows.Forms.Label();
			this.labelBillingEndDate = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.dateTimePickerCollectDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.dateTimePickerDisposalDate = new System.Windows.Forms.DateTimePicker();
			this.label11 = new System.Windows.Forms.Label();
			this.labelEndFlag = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.labelTel = new System.Windows.Forms.Label();
			this.labelAddress = new System.Windows.Forms.Label();
			this.labelClinickKana = new System.Windows.Forms.Label();
			this.labelClinicName = new System.Windows.Forms.Label();
			this.labelOffice = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.labelCustomerNo = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.labelTokuisakiNo = new System.Windows.Forms.Label();
			this.labelContractNo = new System.Windows.Forms.Label();
			this.buttonLoadSheet = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.numericTextBoxMonthlyAmount = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxMonths = new MwsLib.Component.NumericTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Moccasin;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(16, 237);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 25);
			this.label1.TabIndex = 18;
			this.label1.Text = "契約番号 ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Moccasin;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(16, 265);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 25);
			this.label2.TabIndex = 20;
			this.label2.Text = "受注日 ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerOrderDate
			// 
			this.dateTimePickerOrderDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerOrderDate.Location = new System.Drawing.Point(98, 270);
			this.dateTimePickerOrderDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate";
			this.dateTimePickerOrderDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerOrderDate.TabIndex = 21;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Moccasin;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(229, 265);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 25);
			this.label4.TabIndex = 22;
			this.label4.Text = "利用月数 ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Moccasin;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(443, 265);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81, 25);
			this.label6.TabIndex = 24;
			this.label6.Text = "月額利用料 ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(954, 676);
			this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(102, 31);
			this.buttonSave.TabIndex = 48;
			this.buttonSave.Text = "保存";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(1064, 676);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 31);
			this.buttonCancel.TabIndex = 49;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// dateTimePickerCancelDate
			// 
			this.dateTimePickerCancelDate.Checked = false;
			this.dateTimePickerCancelDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerCancelDate.Enabled = false;
			this.dateTimePickerCancelDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerCancelDate.Location = new System.Drawing.Point(98, 354);
			this.dateTimePickerCancelDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerCancelDate.Name = "dateTimePickerCancelDate";
			this.dateTimePickerCancelDate.ShowCheckBox = true;
			this.dateTimePickerCancelDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerCancelDate.TabIndex = 37;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Moccasin;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(16, 349);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 25);
			this.label3.TabIndex = 36;
			this.label3.Text = "解約日 ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listViewDetail
			// 
			this.listViewDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
			this.listViewDetail.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewDetail.FullRowSelect = true;
			this.listViewDetail.HideSelection = false;
			this.listViewDetail.Location = new System.Drawing.Point(13, 404);
			this.listViewDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.listViewDetail.Name = "listViewDetail";
			this.listViewDetail.Size = new System.Drawing.Size(1153, 266);
			this.listViewDetail.TabIndex = 43;
			this.listViewDetail.UseCompatibleStateImageBehavior = false;
			this.listViewDetail.View = System.Windows.Forms.View.Details;
			this.listViewDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDetail_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "No";
			this.columnHeader1.Width = 30;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "商品コード";
			this.columnHeader2.Width = 75;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "機器名";
			this.columnHeader3.Width = 300;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "区分";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "数量";
			this.columnHeader5.Width = 40;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "シリアルNo";
			this.columnHeader6.Width = 140;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "スキャナイメージ";
			this.columnHeader7.Width = 150;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "資産コード";
			this.columnHeader8.Width = 85;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "交換日";
			this.columnHeader9.Width = 75;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "交換元シリアルNo";
			this.columnHeader10.Width = 140;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 388);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 13);
			this.label7.TabIndex = 42;
			this.label7.Text = "■貸出機器情報";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(13, 219);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 13);
			this.label8.TabIndex = 17;
			this.label8.Text = "■契約情報";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDelete.Location = new System.Drawing.Point(229, 676);
			this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(102, 31);
			this.buttonDelete.TabIndex = 47;
			this.buttonDelete.Text = "貸出機器削除";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Visible = false;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Location = new System.Drawing.Point(123, 676);
			this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(102, 31);
			this.buttonAdd.TabIndex = 46;
			this.buttonAdd.Text = "貸出機器追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Visible = false;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.Moccasin;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(229, 293);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(81, 25);
			this.label9.TabIndex = 28;
			this.label9.Text = "契約開始日 ";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelContractEndDate
			// 
			this.labelContractEndDate.BackColor = System.Drawing.Color.White;
			this.labelContractEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractEndDate.Location = new System.Drawing.Point(525, 293);
			this.labelContractEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelContractEndDate.Name = "labelContractEndDate";
			this.labelContractEndDate.Size = new System.Drawing.Size(127, 25);
			this.labelContractEndDate.TabIndex = 31;
			this.labelContractEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.Moccasin;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(443, 293);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(81, 25);
			this.label12.TabIndex = 30;
			this.label12.Text = "契約終了日 ";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerShippingDate
			// 
			this.dateTimePickerShippingDate.Checked = false;
			this.dateTimePickerShippingDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerShippingDate.Enabled = false;
			this.dateTimePickerShippingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerShippingDate.Location = new System.Drawing.Point(98, 298);
			this.dateTimePickerShippingDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerShippingDate.Name = "dateTimePickerShippingDate";
			this.dateTimePickerShippingDate.ShowCheckBox = true;
			this.dateTimePickerShippingDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerShippingDate.TabIndex = 27;
			this.dateTimePickerShippingDate.ValueChanged += new System.EventHandler(this.dateTimePickerShippingDate_ValueChanged);
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.Moccasin;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(16, 293);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(81, 25);
			this.label10.TabIndex = 26;
			this.label10.Text = "出荷日 ";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelContractStartDate
			// 
			this.labelContractStartDate.BackColor = System.Drawing.Color.White;
			this.labelContractStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractStartDate.Location = new System.Drawing.Point(312, 293);
			this.labelContractStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelContractStartDate.Name = "labelContractStartDate";
			this.labelContractStartDate.Size = new System.Drawing.Size(127, 25);
			this.labelContractStartDate.TabIndex = 29;
			this.labelContractStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelBillingStartDate
			// 
			this.labelBillingStartDate.BackColor = System.Drawing.Color.White;
			this.labelBillingStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelBillingStartDate.Location = new System.Drawing.Point(98, 321);
			this.labelBillingStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelBillingStartDate.Name = "labelBillingStartDate";
			this.labelBillingStartDate.Size = new System.Drawing.Size(127, 25);
			this.labelBillingStartDate.TabIndex = 33;
			this.labelBillingStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelBillingEndDate
			// 
			this.labelBillingEndDate.BackColor = System.Drawing.Color.White;
			this.labelBillingEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelBillingEndDate.Location = new System.Drawing.Point(312, 321);
			this.labelBillingEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelBillingEndDate.Name = "labelBillingEndDate";
			this.labelBillingEndDate.Size = new System.Drawing.Size(127, 25);
			this.labelBillingEndDate.TabIndex = 35;
			this.labelBillingEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.Moccasin;
			this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label16.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(229, 321);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(81, 25);
			this.label16.TabIndex = 34;
			this.label16.Text = "課金終了日 ";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.Moccasin;
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label17.Location = new System.Drawing.Point(16, 321);
			this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(81, 25);
			this.label17.TabIndex = 32;
			this.label17.Text = "課金開始日 ";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerCollectDate
			// 
			this.dateTimePickerCollectDate.Checked = false;
			this.dateTimePickerCollectDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerCollectDate.Enabled = false;
			this.dateTimePickerCollectDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerCollectDate.Location = new System.Drawing.Point(312, 354);
			this.dateTimePickerCollectDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerCollectDate.Name = "dateTimePickerCollectDate";
			this.dateTimePickerCollectDate.ShowCheckBox = true;
			this.dateTimePickerCollectDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerCollectDate.TabIndex = 39;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Moccasin;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(229, 349);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 25);
			this.label5.TabIndex = 38;
			this.label5.Text = "機器回収日 ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerDisposalDate
			// 
			this.dateTimePickerDisposalDate.Checked = false;
			this.dateTimePickerDisposalDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerDisposalDate.Enabled = false;
			this.dateTimePickerDisposalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerDisposalDate.Location = new System.Drawing.Point(525, 354);
			this.dateTimePickerDisposalDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerDisposalDate.Name = "dateTimePickerDisposalDate";
			this.dateTimePickerDisposalDate.ShowCheckBox = true;
			this.dateTimePickerDisposalDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerDisposalDate.TabIndex = 41;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.Moccasin;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(443, 349);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(81, 25);
			this.label11.TabIndex = 40;
			this.label11.Text = "機器廃棄日 ";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelEndFlag
			// 
			this.labelEndFlag.BackColor = System.Drawing.Color.White;
			this.labelEndFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelEndFlag.ForeColor = System.Drawing.Color.Red;
			this.labelEndFlag.Location = new System.Drawing.Point(113, 183);
			this.labelEndFlag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelEndFlag.Name = "labelEndFlag";
			this.labelEndFlag.Size = new System.Drawing.Size(73, 25);
			this.labelEndFlag.TabIndex = 16;
			this.labelEndFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.LightCyan;
			this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label13.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label13.Location = new System.Drawing.Point(13, 183);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(99, 25);
			this.label13.TabIndex = 15;
			this.label13.Text = "終了届 ";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelTel
			// 
			this.labelTel.BackColor = System.Drawing.Color.White;
			this.labelTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTel.Location = new System.Drawing.Point(113, 157);
			this.labelTel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTel.Name = "labelTel";
			this.labelTel.Size = new System.Drawing.Size(317, 25);
			this.labelTel.TabIndex = 14;
			this.labelTel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelAddress
			// 
			this.labelAddress.BackColor = System.Drawing.Color.White;
			this.labelAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAddress.Location = new System.Drawing.Point(113, 131);
			this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(634, 25);
			this.labelAddress.TabIndex = 12;
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClinickKana
			// 
			this.labelClinickKana.BackColor = System.Drawing.Color.White;
			this.labelClinickKana.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelClinickKana.Location = new System.Drawing.Point(113, 105);
			this.labelClinickKana.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelClinickKana.Name = "labelClinickKana";
			this.labelClinickKana.Size = new System.Drawing.Size(634, 25);
			this.labelClinickKana.TabIndex = 10;
			this.labelClinickKana.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClinicName
			// 
			this.labelClinicName.BackColor = System.Drawing.Color.White;
			this.labelClinicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelClinicName.Location = new System.Drawing.Point(113, 79);
			this.labelClinicName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelClinicName.Name = "labelClinicName";
			this.labelClinicName.Size = new System.Drawing.Size(634, 25);
			this.labelClinicName.TabIndex = 8;
			this.labelClinicName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelOffice
			// 
			this.labelOffice.BackColor = System.Drawing.Color.White;
			this.labelOffice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelOffice.Location = new System.Drawing.Point(113, 53);
			this.labelOffice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelOffice.Name = "labelOffice";
			this.labelOffice.Size = new System.Drawing.Size(228, 25);
			this.labelOffice.TabIndex = 6;
			this.labelOffice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.BackColor = System.Drawing.Color.LightCyan;
			this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label14.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label14.Location = new System.Drawing.Point(13, 157);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(99, 25);
			this.label14.TabIndex = 13;
			this.label14.Text = "電話番号 ";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.BackColor = System.Drawing.Color.LightCyan;
			this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label15.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label15.Location = new System.Drawing.Point(13, 131);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(99, 25);
			this.label15.TabIndex = 11;
			this.label15.Text = "住所 ";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.BackColor = System.Drawing.Color.LightCyan;
			this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label18.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label18.Location = new System.Drawing.Point(13, 105);
			this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(99, 25);
			this.label18.TabIndex = 9;
			this.label18.Text = "顧客名カナ ";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.BackColor = System.Drawing.Color.LightCyan;
			this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label19.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label19.Location = new System.Drawing.Point(13, 79);
			this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(99, 25);
			this.label19.TabIndex = 7;
			this.label19.Text = "顧客名 ";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.BackColor = System.Drawing.Color.LightCyan;
			this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label20.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label20.Location = new System.Drawing.Point(13, 53);
			this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(99, 25);
			this.label20.TabIndex = 5;
			this.label20.Text = "担当オフィス ";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.BackColor = System.Drawing.Color.LightCyan;
			this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label21.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label21.Location = new System.Drawing.Point(13, 27);
			this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(99, 25);
			this.label21.TabIndex = 1;
			this.label21.Text = "顧客No ";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(13, 9);
			this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(72, 13);
			this.label22.TabIndex = 0;
			this.label22.Text = "■顧客情報";
			// 
			// labelCustomerNo
			// 
			this.labelCustomerNo.BackColor = System.Drawing.Color.White;
			this.labelCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerNo.Location = new System.Drawing.Point(113, 27);
			this.labelCustomerNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelCustomerNo.Name = "labelCustomerNo";
			this.labelCustomerNo.Size = new System.Drawing.Size(127, 25);
			this.labelCustomerNo.TabIndex = 2;
			this.labelCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label23
			// 
			this.label23.BackColor = System.Drawing.Color.LightCyan;
			this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label23.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label23.Location = new System.Drawing.Point(242, 27);
			this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(99, 25);
			this.label23.TabIndex = 3;
			this.label23.Text = "得意先番号 ";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelTokuisakiNo
			// 
			this.labelTokuisakiNo.BackColor = System.Drawing.Color.White;
			this.labelTokuisakiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTokuisakiNo.Location = new System.Drawing.Point(342, 27);
			this.labelTokuisakiNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTokuisakiNo.Name = "labelTokuisakiNo";
			this.labelTokuisakiNo.Size = new System.Drawing.Size(127, 25);
			this.labelTokuisakiNo.TabIndex = 4;
			this.labelTokuisakiNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelContractNo
			// 
			this.labelContractNo.BackColor = System.Drawing.Color.White;
			this.labelContractNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractNo.Location = new System.Drawing.Point(98, 237);
			this.labelContractNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelContractNo.Name = "labelContractNo";
			this.labelContractNo.Size = new System.Drawing.Size(127, 25);
			this.labelContractNo.TabIndex = 19;
			this.labelContractNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonLoadSheet
			// 
			this.buttonLoadSheet.Enabled = false;
			this.buttonLoadSheet.Location = new System.Drawing.Point(922, 371);
			this.buttonLoadSheet.Name = "buttonLoadSheet";
			this.buttonLoadSheet.Size = new System.Drawing.Size(244, 27);
			this.buttonLoadSheet.TabIndex = 44;
			this.buttonLoadSheet.Text = "顧客情報連絡シートの読込";
			this.buttonLoadSheet.UseVisualStyleBackColor = true;
			this.buttonLoadSheet.Click += new System.EventHandler(this.buttonLoadSheet_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonModify.Location = new System.Drawing.Point(13, 676);
			this.buttonModify.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(102, 31);
			this.buttonModify.TabIndex = 45;
			this.buttonModify.Text = "貸出機器変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// numericTextBoxMonthlyAmount
			// 
			this.numericTextBoxMonthlyAmount.Location = new System.Drawing.Point(525, 270);
			this.numericTextBoxMonthlyAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonthlyAmount.MaxLength = 7;
			this.numericTextBoxMonthlyAmount.Name = "numericTextBoxMonthlyAmount";
			this.numericTextBoxMonthlyAmount.Size = new System.Drawing.Size(127, 20);
			this.numericTextBoxMonthlyAmount.TabIndex = 25;
			// 
			// numericTextBoxMonths
			// 
			this.numericTextBoxMonths.Location = new System.Drawing.Point(312, 270);
			this.numericTextBoxMonths.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonths.MaxLength = 2;
			this.numericTextBoxMonths.Name = "numericTextBoxMonths";
			this.numericTextBoxMonths.Size = new System.Drawing.Size(127, 20);
			this.numericTextBoxMonths.TabIndex = 23;
			// 
			// ContractForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1179, 716);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonLoadSheet);
			this.Controls.Add(this.labelContractNo);
			this.Controls.Add(this.labelTokuisakiNo);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.labelCustomerNo);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.labelEndFlag);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.labelTel);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.labelClinickKana);
			this.Controls.Add(this.labelClinicName);
			this.Controls.Add(this.labelOffice);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.dateTimePickerDisposalDate);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.dateTimePickerCollectDate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelBillingStartDate);
			this.Controls.Add(this.labelBillingEndDate);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.labelContractStartDate);
			this.Controls.Add(this.dateTimePickerShippingDate);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.labelContractEndDate);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.listViewDetail);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.dateTimePickerCancelDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.numericTextBoxMonthlyAmount);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.numericTextBoxMonths);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePickerOrderDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ContractForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "契約情報の入力";
			this.Load += new System.EventHandler(this.ContractForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerOrderDate;
		private System.Windows.Forms.Label label4;
		private MwsLib.Component.NumericTextBox numericTextBoxMonths;
		private System.Windows.Forms.Label label6;
		private MwsLib.Component.NumericTextBox numericTextBoxMonthlyAmount;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.DateTimePicker dateTimePickerCancelDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView listViewDetail;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label labelContractEndDate;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.DateTimePicker dateTimePickerShippingDate;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label labelContractStartDate;
		private System.Windows.Forms.Label labelBillingStartDate;
		private System.Windows.Forms.Label labelBillingEndDate;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.DateTimePicker dateTimePickerCollectDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dateTimePickerDisposalDate;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label labelEndFlag;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label labelTel;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.Label labelClinickKana;
		private System.Windows.Forms.Label labelClinicName;
		private System.Windows.Forms.Label labelOffice;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label labelCustomerNo;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label labelTokuisakiNo;
		private System.Windows.Forms.Label labelContractNo;
		private System.Windows.Forms.Button buttonLoadSheet;
		private System.Windows.Forms.Button buttonModify;
	}
}