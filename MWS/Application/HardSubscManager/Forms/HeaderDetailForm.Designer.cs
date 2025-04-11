namespace HardSubscManager.Forms
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
			this.labelRentalNo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePickerAcceptDate = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.dateTimePickerCancelDate = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.numericTextBoxMonthlyAmount = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxTotalAmount = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxMonths = new MwsLib.Component.NumericTextBox();
			this.listViewDetail = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.labelContractStartDate = new System.Windows.Forms.Label();
			this.labelContractEndDate = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelRentalNo
			// 
			this.labelRentalNo.BackColor = System.Drawing.Color.White;
			this.labelRentalNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelRentalNo.Location = new System.Drawing.Point(92, 27);
			this.labelRentalNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelRentalNo.Name = "labelRentalNo";
			this.labelRentalNo.Size = new System.Drawing.Size(119, 25);
			this.labelRentalNo.TabIndex = 2;
			this.labelRentalNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Moccasin;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new System.Drawing.Point(16, 27);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "貸出番号";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Moccasin;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Location = new System.Drawing.Point(219, 27);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "受付日時";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dateTimePickerAcceptDate
			// 
			this.dateTimePickerAcceptDate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
			this.dateTimePickerAcceptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerAcceptDate.Location = new System.Drawing.Point(295, 32);
			this.dateTimePickerAcceptDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerAcceptDate.Name = "dateTimePickerAcceptDate";
			this.dateTimePickerAcceptDate.Size = new System.Drawing.Size(178, 20);
			this.dateTimePickerAcceptDate.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Moccasin;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Location = new System.Drawing.Point(481, 27);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 25);
			this.label4.TabIndex = 5;
			this.label4.Text = "契約月数";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Moccasin;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Location = new System.Drawing.Point(16, 53);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 25);
			this.label5.TabIndex = 7;
			this.label5.Text = "金額";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Moccasin;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Location = new System.Drawing.Point(219, 53);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 25);
			this.label6.TabIndex = 9;
			this.label6.Text = "月額";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(588, 577);
			this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(102, 31);
			this.buttonSave.TabIndex = 21;
			this.buttonSave.Text = "保存";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(698, 577);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 31);
			this.buttonCancel.TabIndex = 22;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// dateTimePickerCancelDate
			// 
			this.dateTimePickerCancelDate.Checked = false;
			this.dateTimePickerCancelDate.Enabled = false;
			this.dateTimePickerCancelDate.Location = new System.Drawing.Point(498, 84);
			this.dateTimePickerCancelDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerCancelDate.Name = "dateTimePickerCancelDate";
			this.dateTimePickerCancelDate.ShowCheckBox = true;
			this.dateTimePickerCancelDate.Size = new System.Drawing.Size(160, 20);
			this.dateTimePickerCancelDate.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Moccasin;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Location = new System.Drawing.Point(422, 79);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 25);
			this.label3.TabIndex = 15;
			this.label3.Text = "解約日";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericTextBoxMonthlyAmount
			// 
			this.numericTextBoxMonthlyAmount.Location = new System.Drawing.Point(295, 57);
			this.numericTextBoxMonthlyAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonthlyAmount.MaxLength = 7;
			this.numericTextBoxMonthlyAmount.Name = "numericTextBoxMonthlyAmount";
			this.numericTextBoxMonthlyAmount.Size = new System.Drawing.Size(118, 20);
			this.numericTextBoxMonthlyAmount.TabIndex = 10;
			// 
			// numericTextBoxTotalAmount
			// 
			this.numericTextBoxTotalAmount.Location = new System.Drawing.Point(92, 57);
			this.numericTextBoxTotalAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxTotalAmount.MaxLength = 7;
			this.numericTextBoxTotalAmount.Name = "numericTextBoxTotalAmount";
			this.numericTextBoxTotalAmount.Size = new System.Drawing.Size(118, 20);
			this.numericTextBoxTotalAmount.TabIndex = 8;
			// 
			// numericTextBoxMonths
			// 
			this.numericTextBoxMonths.Location = new System.Drawing.Point(557, 32);
			this.numericTextBoxMonths.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonths.MaxLength = 2;
			this.numericTextBoxMonths.Name = "numericTextBoxMonths";
			this.numericTextBoxMonths.Size = new System.Drawing.Size(62, 20);
			this.numericTextBoxMonths.TabIndex = 6;
			// 
			// listViewDetail
			// 
			this.listViewDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.listViewDetail.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewDetail.FullRowSelect = true;
			this.listViewDetail.HideSelection = false;
			this.listViewDetail.Location = new System.Drawing.Point(13, 128);
			this.listViewDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.listViewDetail.Name = "listViewDetail";
			this.listViewDetail.Size = new System.Drawing.Size(787, 443);
			this.listViewDetail.TabIndex = 18;
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
			this.columnHeader3.Width = 380;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "機器区分";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "数量";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "資産コード";
			this.columnHeader6.Width = 75;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "シリアルNo";
			this.columnHeader7.Width = 75;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(10, 112);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 13);
			this.label7.TabIndex = 17;
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
			this.buttonDelete.Location = new System.Drawing.Point(123, 577);
			this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(102, 31);
			this.buttonDelete.TabIndex = 20;
			this.buttonDelete.Text = "機器削除";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(13, 577);
			this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(102, 31);
			this.buttonAdd.TabIndex = 19;
			this.buttonAdd.Text = "機器追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.Moccasin;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Location = new System.Drawing.Point(16, 79);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 25);
			this.label9.TabIndex = 11;
			this.label9.Text = "利用開始日";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelContractStartDate
			// 
			this.labelContractStartDate.BackColor = System.Drawing.Color.White;
			this.labelContractStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractStartDate.Location = new System.Drawing.Point(92, 79);
			this.labelContractStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelContractStartDate.Name = "labelContractStartDate";
			this.labelContractStartDate.Size = new System.Drawing.Size(119, 25);
			this.labelContractStartDate.TabIndex = 12;
			this.labelContractStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelContractEndDate
			// 
			this.labelContractEndDate.BackColor = System.Drawing.Color.White;
			this.labelContractEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractEndDate.Location = new System.Drawing.Point(295, 79);
			this.labelContractEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelContractEndDate.Name = "labelContractEndDate";
			this.labelContractEndDate.Size = new System.Drawing.Size(119, 25);
			this.labelContractEndDate.TabIndex = 14;
			this.labelContractEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.Moccasin;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Location = new System.Drawing.Point(219, 79);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(75, 25);
			this.label12.TabIndex = 13;
			this.label12.Text = "利用終了日";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// HeaderDetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(816, 619);
			this.Controls.Add(this.labelContractEndDate);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.labelContractStartDate);
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
			this.Controls.Add(this.numericTextBoxTotalAmount);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericTextBoxMonths);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePickerAcceptDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelRentalNo);
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

		private System.Windows.Forms.Label labelRentalNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerAcceptDate;
		private System.Windows.Forms.Label label4;
		private MwsLib.Component.NumericTextBox numericTextBoxMonths;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private MwsLib.Component.NumericTextBox numericTextBoxTotalAmount;
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
		private System.Windows.Forms.Label labelContractStartDate;
		private System.Windows.Forms.Label labelContractEndDate;
		private System.Windows.Forms.Label label12;
	}
}