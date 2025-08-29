namespace HardRentalManager.Forms
{
	partial class HeaderDetailForm
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
			this.dateTimePickerAcceptDate = new System.Windows.Forms.DateTimePicker();
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
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.labelUseEndDate = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxRentalNo = new System.Windows.Forms.TextBox();
			this.dateTimePickerDeliveryDate = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.dateTimePickerShippingDate = new System.Windows.Forms.DateTimePicker();
			this.label11 = new System.Windows.Forms.Label();
			this.labelUseStartDate = new System.Windows.Forms.Label();
			this.numericTextBoxMonthlyAmount = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxMonths = new MwsLib.Component.NumericTextBox();
			this.labelBillingStartDate = new System.Windows.Forms.Label();
			this.labelBillingEndDate = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Moccasin;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(16, 27);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "契約番号 ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Moccasin;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(16, 55);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "受付日 ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerAcceptDate
			// 
			this.dateTimePickerAcceptDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerAcceptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerAcceptDate.Location = new System.Drawing.Point(98, 60);
			this.dateTimePickerAcceptDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerAcceptDate.Name = "dateTimePickerAcceptDate";
			this.dateTimePickerAcceptDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerAcceptDate.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Moccasin;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(229, 55);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 25);
			this.label4.TabIndex = 5;
			this.label4.Text = "利用月数 ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Moccasin;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(443, 55);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81, 25);
			this.label6.TabIndex = 7;
			this.label6.Text = "月額利用料 ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(588, 610);
			this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(102, 31);
			this.buttonSave.TabIndex = 27;
			this.buttonSave.Text = "保存";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(698, 610);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 31);
			this.buttonCancel.TabIndex = 28;
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
			this.dateTimePickerCancelDate.Location = new System.Drawing.Point(441, 118);
			this.dateTimePickerCancelDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerCancelDate.Name = "dateTimePickerCancelDate";
			this.dateTimePickerCancelDate.ShowCheckBox = true;
			this.dateTimePickerCancelDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerCancelDate.TabIndex = 22;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Moccasin;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(359, 113);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 25);
			this.label3.TabIndex = 21;
			this.label3.Text = "解約日 ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listViewDetail
			// 
			this.listViewDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6});
			this.listViewDetail.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewDetail.FullRowSelect = true;
			this.listViewDetail.HideSelection = false;
			this.listViewDetail.Location = new System.Drawing.Point(13, 161);
			this.listViewDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.listViewDetail.Name = "listViewDetail";
			this.listViewDetail.Size = new System.Drawing.Size(787, 443);
			this.listViewDetail.TabIndex = 24;
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
			this.columnHeader4.Text = "カテゴリ";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "数量";
			this.columnHeader5.Width = 40;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "シリアルNo";
			this.columnHeader7.Width = 140;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "資産管理番号";
			this.columnHeader6.Width = 85;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 145);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 13);
			this.label7.TabIndex = 23;
			this.label7.Text = "■機器情報";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(13, 9);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "■契約情報";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(123, 610);
			this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(102, 31);
			this.buttonDelete.TabIndex = 26;
			this.buttonDelete.Text = "機器削除";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(13, 610);
			this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(102, 31);
			this.buttonAdd.TabIndex = 25;
			this.buttonAdd.Text = "機器追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.Moccasin;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(443, 83);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(81, 25);
			this.label9.TabIndex = 13;
			this.label9.Text = "利用開始日 ";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelUseEndDate
			// 
			this.labelUseEndDate.BackColor = System.Drawing.Color.White;
			this.labelUseEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelUseEndDate.Location = new System.Drawing.Point(696, 83);
			this.labelUseEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelUseEndDate.Name = "labelUseEndDate";
			this.labelUseEndDate.Size = new System.Drawing.Size(85, 25);
			this.labelUseEndDate.TabIndex = 16;
			this.labelUseEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.Moccasin;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(614, 83);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(81, 25);
			this.label12.TabIndex = 15;
			this.label12.Text = "利用終了日 ";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxRentalNo
			// 
			this.textBoxRentalNo.Location = new System.Drawing.Point(98, 32);
			this.textBoxRentalNo.MaxLength = 12;
			this.textBoxRentalNo.Name = "textBoxRentalNo";
			this.textBoxRentalNo.Size = new System.Drawing.Size(110, 20);
			this.textBoxRentalNo.TabIndex = 2;
			// 
			// dateTimePickerDeliveryDate
			// 
			this.dateTimePickerDeliveryDate.Checked = false;
			this.dateTimePickerDeliveryDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerDeliveryDate.Location = new System.Drawing.Point(312, 88);
			this.dateTimePickerDeliveryDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerDeliveryDate.Name = "dateTimePickerDeliveryDate";
			this.dateTimePickerDeliveryDate.ShowCheckBox = true;
			this.dateTimePickerDeliveryDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerDeliveryDate.TabIndex = 12;
			this.dateTimePickerDeliveryDate.ValueChanged += new System.EventHandler(this.dateTimePickerDeliveryDate_ValueChanged);
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.Moccasin;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(230, 83);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(81, 25);
			this.label10.TabIndex = 11;
			this.label10.Text = "納品日 ";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerShippingDate
			// 
			this.dateTimePickerShippingDate.Checked = false;
			this.dateTimePickerShippingDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerShippingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerShippingDate.Location = new System.Drawing.Point(98, 88);
			this.dateTimePickerShippingDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerShippingDate.Name = "dateTimePickerShippingDate";
			this.dateTimePickerShippingDate.ShowCheckBox = true;
			this.dateTimePickerShippingDate.Size = new System.Drawing.Size(127, 20);
			this.dateTimePickerShippingDate.TabIndex = 10;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.Moccasin;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(15, 83);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(81, 25);
			this.label11.TabIndex = 9;
			this.label11.Text = "出荷日 ";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelUseStartDate
			// 
			this.labelUseStartDate.BackColor = System.Drawing.Color.White;
			this.labelUseStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelUseStartDate.Location = new System.Drawing.Point(525, 83);
			this.labelUseStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelUseStartDate.Name = "labelUseStartDate";
			this.labelUseStartDate.Size = new System.Drawing.Size(85, 25);
			this.labelUseStartDate.TabIndex = 14;
			this.labelUseStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// numericTextBoxMonthlyAmount
			// 
			this.numericTextBoxMonthlyAmount.Location = new System.Drawing.Point(525, 60);
			this.numericTextBoxMonthlyAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonthlyAmount.MaxLength = 7;
			this.numericTextBoxMonthlyAmount.Name = "numericTextBoxMonthlyAmount";
			this.numericTextBoxMonthlyAmount.Size = new System.Drawing.Size(128, 20);
			this.numericTextBoxMonthlyAmount.TabIndex = 8;
			// 
			// numericTextBoxMonths
			// 
			this.numericTextBoxMonths.Location = new System.Drawing.Point(312, 60);
			this.numericTextBoxMonths.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonths.MaxLength = 2;
			this.numericTextBoxMonths.Name = "numericTextBoxMonths";
			this.numericTextBoxMonths.Size = new System.Drawing.Size(62, 20);
			this.numericTextBoxMonths.TabIndex = 6;
			// 
			// labelBillingStartDate
			// 
			this.labelBillingStartDate.BackColor = System.Drawing.Color.White;
			this.labelBillingStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelBillingStartDate.Location = new System.Drawing.Point(98, 111);
			this.labelBillingStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelBillingStartDate.Name = "labelBillingStartDate";
			this.labelBillingStartDate.Size = new System.Drawing.Size(85, 25);
			this.labelBillingStartDate.TabIndex = 18;
			this.labelBillingStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelBillingEndDate
			// 
			this.labelBillingEndDate.BackColor = System.Drawing.Color.White;
			this.labelBillingEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelBillingEndDate.Location = new System.Drawing.Point(269, 111);
			this.labelBillingEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelBillingEndDate.Name = "labelBillingEndDate";
			this.labelBillingEndDate.Size = new System.Drawing.Size(85, 25);
			this.labelBillingEndDate.TabIndex = 20;
			this.labelBillingEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.Moccasin;
			this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label16.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(187, 111);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(81, 25);
			this.label16.TabIndex = 19;
			this.label16.Text = "課金終了日 ";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.Moccasin;
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label17.Location = new System.Drawing.Point(16, 111);
			this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(81, 25);
			this.label17.TabIndex = 17;
			this.label17.Text = "課金開始日 ";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// HeaderDetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(816, 652);
			this.Controls.Add(this.labelBillingStartDate);
			this.Controls.Add(this.labelBillingEndDate);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.labelUseStartDate);
			this.Controls.Add(this.dateTimePickerShippingDate);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.dateTimePickerDeliveryDate);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textBoxRentalNo);
			this.Controls.Add(this.labelUseEndDate);
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
			this.Controls.Add(this.dateTimePickerAcceptDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HeaderDetailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "契約情報の入力";
			this.Load += new System.EventHandler(this.HeaderDetailForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerAcceptDate;
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
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label labelUseEndDate;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxRentalNo;
		private System.Windows.Forms.DateTimePicker dateTimePickerDeliveryDate;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DateTimePicker dateTimePickerShippingDate;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label labelUseStartDate;
		private System.Windows.Forms.Label labelBillingStartDate;
		private System.Windows.Forms.Label labelBillingEndDate;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
	}
}