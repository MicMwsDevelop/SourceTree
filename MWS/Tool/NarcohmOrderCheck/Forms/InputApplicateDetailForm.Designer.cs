namespace NarcohmOrderCheck.Forms
{
	partial class InputApplicateDetailForm
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
			this.comboBoxGoodsName = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.dateTimePickerOrderDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerUseEndDate = new MwsLib.Component.YearMonthPicker();
			this.dateTimePickerUseStartDate = new MwsLib.Component.YearMonthPicker();
			this.textBoxCount = new MwsLib.Component.NumericTextBox();
			this.textBoxPrice = new MwsLib.Component.NumericTextBox();
			this.textBoxOrderNo = new MwsLib.Component.NumericTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "■受注番号";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "■受注日";
			// 
			// comboBoxGoodsName
			// 
			this.comboBoxGoodsName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGoodsName.FormattingEnabled = true;
			this.comboBoxGoodsName.Location = new System.Drawing.Point(92, 73);
			this.comboBoxGoodsName.Name = "comboBoxGoodsName";
			this.comboBoxGoodsName.Size = new System.Drawing.Size(351, 25);
			this.comboBoxGoodsName.TabIndex = 5;
			this.comboBoxGoodsName.SelectedIndexChanged += new System.EventHandler(this.comboBoxGoodsName_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "■商品名";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 107);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "■金額";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 138);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 17);
			this.label5.TabIndex = 8;
			this.label5.Text = "■数量";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(216, 107);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 17);
			this.label6.TabIndex = 10;
			this.label6.Text = "■利用開始月";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(216, 137);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(86, 17);
			this.label7.TabIndex = 12;
			this.label7.Text = "■利用終了月";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(368, 169);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonCancel.TabIndex = 15;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(287, 169);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 33);
			this.buttonOK.TabIndex = 14;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// dateTimePickerOrderDate
			// 
			this.dateTimePickerOrderDate.Checked = false;
			this.dateTimePickerOrderDate.Location = new System.Drawing.Point(92, 43);
			this.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate";
			this.dateTimePickerOrderDate.ShowCheckBox = true;
			this.dateTimePickerOrderDate.Size = new System.Drawing.Size(164, 24);
			this.dateTimePickerOrderDate.TabIndex = 3;
			// 
			// dateTimePickerUseEndDate
			// 
			this.dateTimePickerUseEndDate.Checked = false;
			this.dateTimePickerUseEndDate.CustomFormat = "yyyy年MM月";
			this.dateTimePickerUseEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerUseEndDate.Location = new System.Drawing.Point(308, 134);
			this.dateTimePickerUseEndDate.Name = "dateTimePickerUseEndDate";
			this.dateTimePickerUseEndDate.ShowCheckBox = true;
			this.dateTimePickerUseEndDate.Size = new System.Drawing.Size(135, 24);
			this.dateTimePickerUseEndDate.TabIndex = 13;
			this.dateTimePickerUseEndDate.Value = new System.DateTime(2019, 6, 1, 12, 7, 41, 949);
			// 
			// dateTimePickerUseStartDate
			// 
			this.dateTimePickerUseStartDate.Checked = false;
			this.dateTimePickerUseStartDate.CustomFormat = "yyyy年MM月";
			this.dateTimePickerUseStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerUseStartDate.Location = new System.Drawing.Point(308, 104);
			this.dateTimePickerUseStartDate.Name = "dateTimePickerUseStartDate";
			this.dateTimePickerUseStartDate.ShowCheckBox = true;
			this.dateTimePickerUseStartDate.Size = new System.Drawing.Size(135, 24);
			this.dateTimePickerUseStartDate.TabIndex = 11;
			this.dateTimePickerUseStartDate.Value = new System.DateTime(2019, 6, 1, 12, 7, 41, 946);
			// 
			// textBoxCount
			// 
			this.textBoxCount.Location = new System.Drawing.Point(92, 134);
			this.textBoxCount.MaxLength = 3;
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.Size = new System.Drawing.Size(100, 24);
			this.textBoxCount.TabIndex = 9;
			this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxPrice
			// 
			this.textBoxPrice.Location = new System.Drawing.Point(92, 104);
			this.textBoxPrice.MaxLength = 7;
			this.textBoxPrice.Name = "textBoxPrice";
			this.textBoxPrice.Size = new System.Drawing.Size(100, 24);
			this.textBoxPrice.TabIndex = 7;
			this.textBoxPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxOrderNo
			// 
			this.textBoxOrderNo.Location = new System.Drawing.Point(92, 13);
			this.textBoxOrderNo.MaxLength = 10;
			this.textBoxOrderNo.Name = "textBoxOrderNo";
			this.textBoxOrderNo.Size = new System.Drawing.Size(100, 24);
			this.textBoxOrderNo.TabIndex = 1;
			// 
			// InputApplicateDetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(466, 213);
			this.Controls.Add(this.dateTimePickerOrderDate);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.dateTimePickerUseEndDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.dateTimePickerUseStartDate);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxPrice);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBoxGoodsName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxOrderNo);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputApplicateDetailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "申込詳細情報の入力";
			this.Load += new System.EventHandler(this.InputApplicateDetailForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox textBoxOrderNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxGoodsName;
		private System.Windows.Forms.Label label3;
		private MwsLib.Component.NumericTextBox textBoxPrice;
		private System.Windows.Forms.Label label4;
		private MwsLib.Component.NumericTextBox textBoxCount;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private MwsLib.Component.YearMonthPicker dateTimePickerUseStartDate;
		private MwsLib.Component.YearMonthPicker dateTimePickerUseEndDate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.DateTimePicker dateTimePickerOrderDate;
	}
}