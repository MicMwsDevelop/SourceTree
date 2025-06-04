namespace HardSubscriptManager.Forms
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
			this.dateTimePickerContractDate = new System.Windows.Forms.DateTimePicker();
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
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.labelUseEndDate = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxRentalNo = new System.Windows.Forms.TextBox();
			this.dateTimePickerUseStartDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerCancelApplyDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.numericTextBoxMonthlyAmount = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxMonths = new MwsLib.Component.NumericTextBox();
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
			this.label1.Size = new System.Drawing.Size(99, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "契約番号 ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Moccasin;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(243, 27);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "契約日 ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePickerContractDate
			// 
			this.dateTimePickerContractDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerContractDate.Enabled = false;
			this.dateTimePickerContractDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerContractDate.Location = new System.Drawing.Point(343, 32);
			this.dateTimePickerContractDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerContractDate.Name = "dateTimePickerContractDate";
			this.dateTimePickerContractDate.Size = new System.Drawing.Size(110, 20);
			this.dateTimePickerContractDate.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Moccasin;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(243, 53);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 25);
			this.label4.TabIndex = 7;
			this.label4.Text = "利用月数 ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Moccasin;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(16, 53);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(99, 25);
			this.label6.TabIndex = 5;
			this.label6.Text = "月額利用料 ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(588, 603);
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
			this.buttonCancel.Location = new System.Drawing.Point(698, 603);
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
			this.dateTimePickerCancelDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerCancelDate.Enabled = false;
			this.dateTimePickerCancelDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerCancelDate.Location = new System.Drawing.Point(116, 110);
			this.dateTimePickerCancelDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerCancelDate.Name = "dateTimePickerCancelDate";
			this.dateTimePickerCancelDate.ShowCheckBox = true;
			this.dateTimePickerCancelDate.Size = new System.Drawing.Size(119, 20);
			this.dateTimePickerCancelDate.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Moccasin;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(16, 105);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 25);
			this.label3.TabIndex = 13;
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
            this.columnHeader6,
            this.columnHeader7});
			this.listViewDetail.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewDetail.FullRowSelect = true;
			this.listViewDetail.HideSelection = false;
			this.listViewDetail.Location = new System.Drawing.Point(13, 154);
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
			this.label7.Location = new System.Drawing.Point(10, 138);
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
			this.buttonDelete.Location = new System.Drawing.Point(123, 603);
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
			this.buttonAdd.Location = new System.Drawing.Point(13, 603);
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
			this.label9.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(16, 79);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(99, 25);
			this.label9.TabIndex = 9;
			this.label9.Text = "利用開始日 ";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelUseEndDate
			// 
			this.labelUseEndDate.BackColor = System.Drawing.Color.White;
			this.labelUseEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelUseEndDate.Location = new System.Drawing.Point(343, 79);
			this.labelUseEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelUseEndDate.Name = "labelUseEndDate";
			this.labelUseEndDate.Size = new System.Drawing.Size(85, 25);
			this.labelUseEndDate.TabIndex = 12;
			this.labelUseEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.Moccasin;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(243, 79);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(99, 25);
			this.label12.TabIndex = 11;
			this.label12.Text = "利用終了日 ";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxRentalNo
			// 
			this.textBoxRentalNo.Enabled = false;
			this.textBoxRentalNo.Location = new System.Drawing.Point(116, 32);
			this.textBoxRentalNo.MaxLength = 12;
			this.textBoxRentalNo.Name = "textBoxRentalNo";
			this.textBoxRentalNo.Size = new System.Drawing.Size(119, 20);
			this.textBoxRentalNo.TabIndex = 2;
			// 
			// dateTimePickerUseStartDate
			// 
			this.dateTimePickerUseStartDate.Checked = false;
			this.dateTimePickerUseStartDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerUseStartDate.Enabled = false;
			this.dateTimePickerUseStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerUseStartDate.Location = new System.Drawing.Point(116, 84);
			this.dateTimePickerUseStartDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerUseStartDate.Name = "dateTimePickerUseStartDate";
			this.dateTimePickerUseStartDate.ShowCheckBox = true;
			this.dateTimePickerUseStartDate.Size = new System.Drawing.Size(119, 20);
			this.dateTimePickerUseStartDate.TabIndex = 10;
			this.dateTimePickerUseStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerContractStartDate_ValueChanged);
			// 
			// dateTimePickerCancelApplyDate
			// 
			this.dateTimePickerCancelApplyDate.CustomFormat = "yyyy/MM/dd";
			this.dateTimePickerCancelApplyDate.Enabled = false;
			this.dateTimePickerCancelApplyDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerCancelApplyDate.Location = new System.Drawing.Point(343, 110);
			this.dateTimePickerCancelApplyDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dateTimePickerCancelApplyDate.Name = "dateTimePickerCancelApplyDate";
			this.dateTimePickerCancelApplyDate.Size = new System.Drawing.Size(110, 20);
			this.dateTimePickerCancelApplyDate.TabIndex = 16;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Moccasin;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(243, 105);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 25);
			this.label5.TabIndex = 15;
			this.label5.Text = "解約受付日 ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numericTextBoxMonthlyAmount
			// 
			this.numericTextBoxMonthlyAmount.Enabled = false;
			this.numericTextBoxMonthlyAmount.Location = new System.Drawing.Point(116, 58);
			this.numericTextBoxMonthlyAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonthlyAmount.MaxLength = 7;
			this.numericTextBoxMonthlyAmount.Name = "numericTextBoxMonthlyAmount";
			this.numericTextBoxMonthlyAmount.Size = new System.Drawing.Size(119, 20);
			this.numericTextBoxMonthlyAmount.TabIndex = 6;
			// 
			// numericTextBoxMonths
			// 
			this.numericTextBoxMonths.Enabled = false;
			this.numericTextBoxMonths.Location = new System.Drawing.Point(343, 58);
			this.numericTextBoxMonths.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxMonths.MaxLength = 2;
			this.numericTextBoxMonths.Name = "numericTextBoxMonths";
			this.numericTextBoxMonths.Size = new System.Drawing.Size(62, 20);
			this.numericTextBoxMonths.TabIndex = 8;
			// 
			// HeaderDetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(816, 644);
			this.Controls.Add(this.dateTimePickerCancelApplyDate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dateTimePickerUseStartDate);
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
			this.Controls.Add(this.dateTimePickerContractDate);
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
		private System.Windows.Forms.DateTimePicker dateTimePickerContractDate;
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
		private System.Windows.Forms.DateTimePicker dateTimePickerUseStartDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerCancelApplyDate;
		private System.Windows.Forms.Label label5;
	}
}