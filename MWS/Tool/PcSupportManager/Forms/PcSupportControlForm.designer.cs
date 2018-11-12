namespace PcSupportManager.Forms
{
	partial class PcSupportControlForm
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
			this.textBoxCustomerNo = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxGoods = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerStartDate = new MwsLib.Component.YearMonthPicker();
			this.dateTimePickerPeriodEndDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxPrice = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxAgreeYear = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxRemark = new System.Windows.Forms.TextBox();
			this.textBoxCancelReason = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkBoxOrderReportAccept = new System.Windows.Forms.CheckBox();
			this.checkBoxCancelReportAccept = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxMailAdderss = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkBoxDisable = new System.Windows.Forms.CheckBox();
			this.label13 = new System.Windows.Forms.Label();
			this.dateTimePickerOrderDate = new System.Windows.Forms.DateTimePicker();
			this.textBoxOrderNo = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.textBoxClinicName = new System.Windows.Forms.TextBox();
			this.dateTimePickerOrderApprovalDate = new System.Windows.Forms.DateTimePicker();
			this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
			this.comboBoxBranch = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 78);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "■顧客No";
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.BackColor = System.Drawing.Color.White;
			this.textBoxCustomerNo.Location = new System.Drawing.Point(117, 75);
			this.textBoxCustomerNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxCustomerNo.MaxLength = 8;
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.Size = new System.Drawing.Size(116, 23);
			this.textBoxCustomerNo.TabIndex = 5;
			this.textBoxCustomerNo.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 109);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "■商品名";
			// 
			// comboBoxGoods
			// 
			this.comboBoxGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGoods.FormattingEnabled = true;
			this.comboBoxGoods.Location = new System.Drawing.Point(117, 106);
			this.comboBoxGoods.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comboBoxGoods.Name = "comboBoxGoods";
			this.comboBoxGoods.Size = new System.Drawing.Size(158, 23);
			this.comboBoxGoods.TabIndex = 7;
			this.comboBoxGoods.SelectedIndexChanged += new System.EventHandler(this.comboBoxGoods_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 205);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 15);
			this.label3.TabIndex = 12;
			this.label3.Text = "■契約開始日";
			// 
			// dateTimePickerStartDate
			// 
			this.dateTimePickerStartDate.Checked = false;
			this.dateTimePickerStartDate.CustomFormat = "yyyy年MM月";
			this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerStartDate.Location = new System.Drawing.Point(117, 199);
			this.dateTimePickerStartDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			this.dateTimePickerStartDate.ShowCheckBox = true;
			this.dateTimePickerStartDate.Size = new System.Drawing.Size(140, 23);
			this.dateTimePickerStartDate.TabIndex = 13;
			this.dateTimePickerStartDate.Value = new System.DateTime(2018, 11, 12, 17, 39, 27, 930);
			// 
			// dateTimePickerPeriodEndDate
			// 
			this.dateTimePickerPeriodEndDate.Checked = false;
			this.dateTimePickerPeriodEndDate.Location = new System.Drawing.Point(99, 22);
			this.dateTimePickerPeriodEndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerPeriodEndDate.Name = "dateTimePickerPeriodEndDate";
			this.dateTimePickerPeriodEndDate.ShowCheckBox = true;
			this.dateTimePickerPeriodEndDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerPeriodEndDate.TabIndex = 1;
			this.dateTimePickerPeriodEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerPeriodEndDate_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 140);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 15);
			this.label5.TabIndex = 8;
			this.label5.Text = "■契約年数";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 171);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 15);
			this.label6.TabIndex = 10;
			this.label6.Text = "■料金";
			// 
			// textBoxPrice
			// 
			this.textBoxPrice.BackColor = System.Drawing.Color.White;
			this.textBoxPrice.Location = new System.Drawing.Point(117, 168);
			this.textBoxPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxPrice.Name = "textBoxPrice";
			this.textBoxPrice.ReadOnly = true;
			this.textBoxPrice.Size = new System.Drawing.Size(116, 23);
			this.textBoxPrice.TabIndex = 11;
			this.textBoxPrice.TabStop = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 234);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(43, 15);
			this.label7.TabIndex = 14;
			this.label7.Text = "■拠店";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(14, 265);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 15);
			this.label8.TabIndex = 16;
			this.label8.Text = "■営業担当者";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(704, 361);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(87, 48);
			this.buttonOK.TabIndex = 28;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(798, 361);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(87, 48);
			this.buttonCancel.TabIndex = 29;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// textBoxAgreeYear
			// 
			this.textBoxAgreeYear.BackColor = System.Drawing.Color.White;
			this.textBoxAgreeYear.Location = new System.Drawing.Point(117, 137);
			this.textBoxAgreeYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxAgreeYear.Name = "textBoxAgreeYear";
			this.textBoxAgreeYear.ReadOnly = true;
			this.textBoxAgreeYear.Size = new System.Drawing.Size(116, 23);
			this.textBoxAgreeYear.TabIndex = 9;
			this.textBoxAgreeYear.TabStop = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(14, 327);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(43, 15);
			this.label9.TabIndex = 20;
			this.label9.Text = "■備考";
			// 
			// textBoxRemark
			// 
			this.textBoxRemark.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark.Location = new System.Drawing.Point(117, 324);
			this.textBoxRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxRemark.MaxLength = 256;
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(768, 23);
			this.textBoxRemark.TabIndex = 21;
			// 
			// textBoxCancelReason
			// 
			this.textBoxCancelReason.Enabled = false;
			this.textBoxCancelReason.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxCancelReason.Location = new System.Drawing.Point(17, 71);
			this.textBoxCancelReason.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxCancelReason.MaxLength = 500;
			this.textBoxCancelReason.Multiline = true;
			this.textBoxCancelReason.Name = "textBoxCancelReason";
			this.textBoxCancelReason.Size = new System.Drawing.Size(571, 126);
			this.textBoxCancelReason.TabIndex = 4;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(14, 52);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(67, 15);
			this.label11.TabIndex = 3;
			this.label11.Text = "■解約理由";
			// 
			// checkBoxOrderReportAccept
			// 
			this.checkBoxOrderReportAccept.AutoSize = true;
			this.checkBoxOrderReportAccept.Location = new System.Drawing.Point(281, 359);
			this.checkBoxOrderReportAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBoxOrderReportAccept.Name = "checkBoxOrderReportAccept";
			this.checkBoxOrderReportAccept.Size = new System.Drawing.Size(98, 19);
			this.checkBoxOrderReportAccept.TabIndex = 24;
			this.checkBoxOrderReportAccept.Text = "申込用紙有無";
			this.checkBoxOrderReportAccept.UseVisualStyleBackColor = true;
			// 
			// checkBoxCancelReportAccept
			// 
			this.checkBoxCancelReportAccept.AutoSize = true;
			this.checkBoxCancelReportAccept.Enabled = false;
			this.checkBoxCancelReportAccept.Location = new System.Drawing.Point(263, 24);
			this.checkBoxCancelReportAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBoxCancelReportAccept.Name = "checkBoxCancelReportAccept";
			this.checkBoxCancelReportAccept.Size = new System.Drawing.Size(86, 19);
			this.checkBoxCancelReportAccept.TabIndex = 2;
			this.checkBoxCancelReportAccept.Text = "解約届有無";
			this.checkBoxCancelReportAccept.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(14, 296);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(82, 15);
			this.label12.TabIndex = 18;
			this.label12.Text = "■メールアドレス";
			// 
			// textBoxMailAdderss
			// 
			this.textBoxMailAdderss.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.textBoxMailAdderss.Location = new System.Drawing.Point(117, 293);
			this.textBoxMailAdderss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxMailAdderss.MaxLength = 50;
			this.textBoxMailAdderss.Name = "textBoxMailAdderss";
			this.textBoxMailAdderss.Size = new System.Drawing.Size(468, 23);
			this.textBoxMailAdderss.TabIndex = 19;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.dateTimePickerPeriodEndDate);
			this.groupBox1.Controls.Add(this.checkBoxCancelReportAccept);
			this.groupBox1.Controls.Add(this.textBoxCancelReason);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Location = new System.Drawing.Point(283, 43);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(602, 211);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "解約/無効";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(14, 28);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(79, 15);
			this.label10.TabIndex = 0;
			this.label10.Text = "■利用期限日";
			// 
			// checkBoxDisable
			// 
			this.checkBoxDisable.AutoSize = true;
			this.checkBoxDisable.Enabled = false;
			this.checkBoxDisable.Location = new System.Drawing.Point(591, 17);
			this.checkBoxDisable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBoxDisable.Name = "checkBoxDisable";
			this.checkBoxDisable.Size = new System.Drawing.Size(84, 19);
			this.checkBoxDisable.TabIndex = 30;
			this.checkBoxDisable.Text = "申込の無効";
			this.checkBoxDisable.UseVisualStyleBackColor = true;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(14, 360);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(55, 15);
			this.label13.TabIndex = 22;
			this.label13.Text = "■受注日";
			// 
			// dateTimePickerOrderDate
			// 
			this.dateTimePickerOrderDate.Checked = false;
			this.dateTimePickerOrderDate.Location = new System.Drawing.Point(117, 355);
			this.dateTimePickerOrderDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate";
			this.dateTimePickerOrderDate.ShowCheckBox = true;
			this.dateTimePickerOrderDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerOrderDate.TabIndex = 23;
			// 
			// textBoxOrderNo
			// 
			this.textBoxOrderNo.BackColor = System.Drawing.Color.White;
			this.textBoxOrderNo.Location = new System.Drawing.Point(117, 44);
			this.textBoxOrderNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxOrderNo.MaxLength = 6;
			this.textBoxOrderNo.Name = "textBoxOrderNo";
			this.textBoxOrderNo.ReadOnly = true;
			this.textBoxOrderNo.Size = new System.Drawing.Size(116, 23);
			this.textBoxOrderNo.TabIndex = 3;
			this.textBoxOrderNo.TabStop = false;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(14, 47);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(59, 15);
			this.label14.TabIndex = 2;
			this.label14.Text = "■受注No";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(14, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(55, 15);
			this.label15.TabIndex = 0;
			this.label15.Text = "■医院名";
			// 
			// textBoxClinicName
			// 
			this.textBoxClinicName.BackColor = System.Drawing.Color.White;
			this.textBoxClinicName.Location = new System.Drawing.Point(117, 13);
			this.textBoxClinicName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxClinicName.MaxLength = 6;
			this.textBoxClinicName.Name = "textBoxClinicName";
			this.textBoxClinicName.ReadOnly = true;
			this.textBoxClinicName.Size = new System.Drawing.Size(468, 23);
			this.textBoxClinicName.TabIndex = 1;
			this.textBoxClinicName.TabStop = false;
			// 
			// dateTimePickerOrderApprovalDate
			// 
			this.dateTimePickerOrderApprovalDate.Checked = false;
			this.dateTimePickerOrderApprovalDate.Location = new System.Drawing.Point(117, 386);
			this.dateTimePickerOrderApprovalDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerOrderApprovalDate.Name = "dateTimePickerOrderApprovalDate";
			this.dateTimePickerOrderApprovalDate.ShowCheckBox = true;
			this.dateTimePickerOrderApprovalDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerOrderApprovalDate.TabIndex = 26;
			// 
			// comboBoxEmployee
			// 
			this.comboBoxEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEmployee.FormattingEnabled = true;
			this.comboBoxEmployee.Location = new System.Drawing.Point(117, 262);
			this.comboBoxEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comboBoxEmployee.Name = "comboBoxEmployee";
			this.comboBoxEmployee.Size = new System.Drawing.Size(140, 23);
			this.comboBoxEmployee.TabIndex = 17;
			// 
			// comboBoxBranch
			// 
			this.comboBoxBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBranch.FormattingEnabled = true;
			this.comboBoxBranch.Location = new System.Drawing.Point(117, 231);
			this.comboBoxBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comboBoxBranch.Name = "comboBoxBranch";
			this.comboBoxBranch.Size = new System.Drawing.Size(140, 23);
			this.comboBoxBranch.TabIndex = 15;
			this.comboBoxBranch.SelectedIndexChanged += new System.EventHandler(this.comboBoxBranch_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 392);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 15);
			this.label4.TabIndex = 25;
			this.label4.Text = "■受注承認日";
			// 
			// PcSupportControlForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(903, 425);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkBoxDisable);
			this.Controls.Add(this.dateTimePickerOrderApprovalDate);
			this.Controls.Add(this.textBoxClinicName);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.textBoxOrderNo);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.dateTimePickerOrderDate);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxMailAdderss);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.checkBoxOrderReportAccept);
			this.Controls.Add(this.textBoxRemark);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textBoxAgreeYear);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.comboBoxEmployee);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.comboBoxBranch);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxPrice);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dateTimePickerStartDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBoxGoods);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PcSupportControlForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "管理情報の登録";
			this.Load += new System.EventHandler(this.PcSupportControlForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCustomerNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxGoods;
		private System.Windows.Forms.Label label3;
		private MwsLib.Component.YearMonthPicker dateTimePickerStartDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerPeriodEndDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxPrice;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxAgreeYear;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxRemark;
		private System.Windows.Forms.TextBox textBoxCancelReason;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkBoxOrderReportAccept;
		private System.Windows.Forms.CheckBox checkBoxCancelReportAccept;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxMailAdderss;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DateTimePicker dateTimePickerOrderDate;
		private System.Windows.Forms.TextBox textBoxOrderNo;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.CheckBox checkBoxDisable;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBoxClinicName;
		private System.Windows.Forms.DateTimePicker dateTimePickerOrderApprovalDate;
		private System.Windows.Forms.ComboBox comboBoxEmployee;
		private System.Windows.Forms.ComboBox comboBoxBranch;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label4;
	}
}