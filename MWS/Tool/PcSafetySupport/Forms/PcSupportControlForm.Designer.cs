namespace PcSafetySupport.Forms
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
			this.textBoxCustomerID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxGoods = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBoxPeriodEndDate = new System.Windows.Forms.CheckBox();
			this.dateTimePickerPeriodEndDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxPrice = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBoxBranch = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxAgreeYear = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxRemark1 = new System.Windows.Forms.TextBox();
			this.textBoxRemark2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxCancelReason = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkBoxApplyReportAccept = new System.Windows.Forms.CheckBox();
			this.checkBoxCancelReportAccept = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxMaleAdderss = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.dateTimePickerAcceptDate = new System.Windows.Forms.DateTimePicker();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "■顧客ID";
			// 
			// textBoxCustomerID
			// 
			this.textBoxCustomerID.BackColor = System.Drawing.Color.White;
			this.textBoxCustomerID.Location = new System.Drawing.Point(117, 12);
			this.textBoxCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxCustomerID.Name = "textBoxCustomerID";
			this.textBoxCustomerID.ReadOnly = true;
			this.textBoxCustomerID.Size = new System.Drawing.Size(116, 23);
			this.textBoxCustomerID.TabIndex = 1;
			this.textBoxCustomerID.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "■商品名";
			// 
			// comboBoxGoods
			// 
			this.comboBoxGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGoods.FormattingEnabled = true;
			this.comboBoxGoods.Location = new System.Drawing.Point(117, 44);
			this.comboBoxGoods.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comboBoxGoods.Name = "comboBoxGoods";
			this.comboBoxGoods.Size = new System.Drawing.Size(278, 23);
			this.comboBoxGoods.TabIndex = 3;
			this.comboBoxGoods.SelectedIndexChanged += new System.EventHandler(this.comboBoxGoods_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 146);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "■契約開始日";
			// 
			// dateTimePickerStartDate
			// 
			this.dateTimePickerStartDate.Location = new System.Drawing.Point(117, 140);
			this.dateTimePickerStartDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			this.dateTimePickerStartDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerStartDate.TabIndex = 9;
			this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
			// 
			// dateTimePickerEndDate
			// 
			this.dateTimePickerEndDate.Location = new System.Drawing.Point(117, 171);
			this.dateTimePickerEndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
			this.dateTimePickerEndDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerEndDate.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 178);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "■契約終了日";
			// 
			// checkBoxPeriodEndDate
			// 
			this.checkBoxPeriodEndDate.AutoSize = true;
			this.checkBoxPeriodEndDate.Enabled = false;
			this.checkBoxPeriodEndDate.Location = new System.Drawing.Point(17, 25);
			this.checkBoxPeriodEndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBoxPeriodEndDate.Name = "checkBoxPeriodEndDate";
			this.checkBoxPeriodEndDate.Size = new System.Drawing.Size(86, 19);
			this.checkBoxPeriodEndDate.TabIndex = 0;
			this.checkBoxPeriodEndDate.Text = "利用期限日";
			this.checkBoxPeriodEndDate.UseVisualStyleBackColor = true;
			this.checkBoxPeriodEndDate.CheckedChanged += new System.EventHandler(this.checkBoxPeriodEndDate_CheckedChanged);
			// 
			// dateTimePickerPeriodEndDate
			// 
			this.dateTimePickerPeriodEndDate.Enabled = false;
			this.dateTimePickerPeriodEndDate.Location = new System.Drawing.Point(120, 22);
			this.dateTimePickerPeriodEndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerPeriodEndDate.Name = "dateTimePickerPeriodEndDate";
			this.dateTimePickerPeriodEndDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerPeriodEndDate.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 81);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 15);
			this.label5.TabIndex = 4;
			this.label5.Text = "■契約年数";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 15);
			this.label6.TabIndex = 6;
			this.label6.Text = "■料金";
			// 
			// textBoxPrice
			// 
			this.textBoxPrice.BackColor = System.Drawing.Color.White;
			this.textBoxPrice.Location = new System.Drawing.Point(115, 109);
			this.textBoxPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxPrice.Name = "textBoxPrice";
			this.textBoxPrice.ReadOnly = true;
			this.textBoxPrice.Size = new System.Drawing.Size(116, 23);
			this.textBoxPrice.TabIndex = 7;
			this.textBoxPrice.TabStop = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 206);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(43, 15);
			this.label7.TabIndex = 12;
			this.label7.Text = "■拠店";
			// 
			// comboBoxBranch
			// 
			this.comboBoxBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBranch.FormattingEnabled = true;
			this.comboBoxBranch.Location = new System.Drawing.Point(117, 202);
			this.comboBoxBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comboBoxBranch.Name = "comboBoxBranch";
			this.comboBoxBranch.Size = new System.Drawing.Size(140, 23);
			this.comboBoxBranch.TabIndex = 13;
			this.comboBoxBranch.SelectedIndexChanged += new System.EventHandler(this.comboBoxBranch_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(14, 239);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 15);
			this.label8.TabIndex = 14;
			this.label8.Text = "■営業担当者";
			// 
			// comboBoxEmployee
			// 
			this.comboBoxEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEmployee.FormattingEnabled = true;
			this.comboBoxEmployee.Location = new System.Drawing.Point(117, 235);
			this.comboBoxEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.comboBoxEmployee.Name = "comboBoxEmployee";
			this.comboBoxEmployee.Size = new System.Drawing.Size(140, 23);
			this.comboBoxEmployee.TabIndex = 15;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(859, 368);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(87, 48);
			this.buttonOK.TabIndex = 26;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(953, 368);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(87, 48);
			this.buttonCancel.TabIndex = 27;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// textBoxAgreeYear
			// 
			this.textBoxAgreeYear.BackColor = System.Drawing.Color.White;
			this.textBoxAgreeYear.Location = new System.Drawing.Point(117, 76);
			this.textBoxAgreeYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxAgreeYear.Name = "textBoxAgreeYear";
			this.textBoxAgreeYear.ReadOnly = true;
			this.textBoxAgreeYear.Size = new System.Drawing.Size(116, 23);
			this.textBoxAgreeYear.TabIndex = 5;
			this.textBoxAgreeYear.TabStop = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(14, 301);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(55, 15);
			this.label9.TabIndex = 18;
			this.label9.Text = "■備考１";
			// 
			// textBoxRemark1
			// 
			this.textBoxRemark1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark1.Location = new System.Drawing.Point(114, 296);
			this.textBoxRemark1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxRemark1.MaxLength = 256;
			this.textBoxRemark1.Name = "textBoxRemark1";
			this.textBoxRemark1.Size = new System.Drawing.Size(657, 23);
			this.textBoxRemark1.TabIndex = 19;
			// 
			// textBoxRemark2
			// 
			this.textBoxRemark2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark2.Location = new System.Drawing.Point(114, 330);
			this.textBoxRemark2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxRemark2.MaxLength = 256;
			this.textBoxRemark2.Name = "textBoxRemark2";
			this.textBoxRemark2.Size = new System.Drawing.Size(657, 23);
			this.textBoxRemark2.TabIndex = 21;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(14, 334);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(55, 15);
			this.label10.TabIndex = 20;
			this.label10.Text = "■備考２";
			// 
			// textBoxCancelReason
			// 
			this.textBoxCancelReason.Enabled = false;
			this.textBoxCancelReason.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxCancelReason.Location = new System.Drawing.Point(13, 105);
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
			this.label11.Enabled = false;
			this.label11.Location = new System.Drawing.Point(15, 86);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(67, 15);
			this.label11.TabIndex = 3;
			this.label11.Text = "■解約理由";
			// 
			// checkBoxApplyReportAccept
			// 
			this.checkBoxApplyReportAccept.AutoSize = true;
			this.checkBoxApplyReportAccept.Location = new System.Drawing.Point(15, 395);
			this.checkBoxApplyReportAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.checkBoxApplyReportAccept.Name = "checkBoxApplyReportAccept";
			this.checkBoxApplyReportAccept.Size = new System.Drawing.Size(98, 19);
			this.checkBoxApplyReportAccept.TabIndex = 24;
			this.checkBoxApplyReportAccept.Text = "申込用紙有無";
			this.checkBoxApplyReportAccept.UseVisualStyleBackColor = true;
			// 
			// checkBoxCancelReportAccept
			// 
			this.checkBoxCancelReportAccept.AutoSize = true;
			this.checkBoxCancelReportAccept.Enabled = false;
			this.checkBoxCancelReportAccept.Location = new System.Drawing.Point(17, 54);
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
			this.label12.Location = new System.Drawing.Point(14, 270);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(82, 15);
			this.label12.TabIndex = 16;
			this.label12.Text = "■メールアドレス";
			// 
			// textBoxMaleAdderss
			// 
			this.textBoxMaleAdderss.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.textBoxMaleAdderss.Location = new System.Drawing.Point(114, 266);
			this.textBoxMaleAdderss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxMaleAdderss.MaxLength = 50;
			this.textBoxMaleAdderss.Name = "textBoxMaleAdderss";
			this.textBoxMaleAdderss.Size = new System.Drawing.Size(468, 23);
			this.textBoxMaleAdderss.TabIndex = 17;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dateTimePickerPeriodEndDate);
			this.groupBox1.Controls.Add(this.checkBoxPeriodEndDate);
			this.groupBox1.Controls.Add(this.checkBoxCancelReportAccept);
			this.groupBox1.Controls.Add(this.textBoxCancelReason);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Location = new System.Drawing.Point(434, 12);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(602, 248);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "解約";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(14, 368);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(67, 15);
			this.label13.TabIndex = 22;
			this.label13.Text = "■申込日付";
			// 
			// dateTimePickerAcceptDate
			// 
			this.dateTimePickerAcceptDate.Location = new System.Drawing.Point(114, 361);
			this.dateTimePickerAcceptDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerAcceptDate.Name = "dateTimePickerAcceptDate";
			this.dateTimePickerAcceptDate.Size = new System.Drawing.Size(158, 23);
			this.dateTimePickerAcceptDate.TabIndex = 23;
			// 
			// PcSupportControlForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1055, 429);
			this.Controls.Add(this.dateTimePickerAcceptDate);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxMaleAdderss);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.checkBoxApplyReportAccept);
			this.Controls.Add(this.textBoxRemark2);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textBoxRemark1);
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
			this.Controls.Add(this.dateTimePickerEndDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePickerStartDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBoxGoods);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxCustomerID);
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
		private System.Windows.Forms.TextBox textBoxCustomerID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxGoods;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBoxPeriodEndDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerPeriodEndDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxPrice;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboBoxBranch;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboBoxEmployee;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxAgreeYear;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxRemark1;
		private System.Windows.Forms.TextBox textBoxRemark2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxCancelReason;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkBoxApplyReportAccept;
		private System.Windows.Forms.CheckBox checkBoxCancelReportAccept;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxMaleAdderss;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DateTimePicker dateTimePickerAcceptDate;
	}
}