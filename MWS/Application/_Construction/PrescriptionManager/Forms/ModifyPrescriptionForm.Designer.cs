namespace PrescriptionManager.Forms
{
	partial class ModifyPrescriptionForm
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
			this.textBoxCustomerName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxApplyDate = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePickerOperationDate = new System.Windows.Forms.DateTimePicker();
			this.textBoxContractStartDate = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxContractEndDate = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "顧客No";
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.BackColor = System.Drawing.Color.White;
			this.textBoxCustomerNo.Location = new System.Drawing.Point(97, 16);
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.ReadOnly = true;
			this.textBoxCustomerNo.Size = new System.Drawing.Size(127, 24);
			this.textBoxCustomerNo.TabIndex = 1;
			// 
			// textBoxCustomerName
			// 
			this.textBoxCustomerName.BackColor = System.Drawing.Color.White;
			this.textBoxCustomerName.Location = new System.Drawing.Point(97, 46);
			this.textBoxCustomerName.Name = "textBoxCustomerName";
			this.textBoxCustomerName.ReadOnly = true;
			this.textBoxCustomerName.Size = new System.Drawing.Size(553, 24);
			this.textBoxCustomerName.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "顧客名";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "申込日時";
			// 
			// textBoxApplyDate
			// 
			this.textBoxApplyDate.BackColor = System.Drawing.Color.White;
			this.textBoxApplyDate.Location = new System.Drawing.Point(97, 76);
			this.textBoxApplyDate.Name = "textBoxApplyDate";
			this.textBoxApplyDate.ReadOnly = true;
			this.textBoxApplyDate.Size = new System.Drawing.Size(242, 24);
			this.textBoxApplyDate.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "運用開始日";
			// 
			// dateTimePickerOperationDate
			// 
			this.dateTimePickerOperationDate.Checked = false;
			this.dateTimePickerOperationDate.Location = new System.Drawing.Point(97, 108);
			this.dateTimePickerOperationDate.Name = "dateTimePickerOperationDate";
			this.dateTimePickerOperationDate.ShowCheckBox = true;
			this.dateTimePickerOperationDate.Size = new System.Drawing.Size(153, 24);
			this.dateTimePickerOperationDate.TabIndex = 7;
			this.dateTimePickerOperationDate.ValueChanged += new System.EventHandler(this.dateTimePickerOperationDate_ValueChanged);
			// 
			// textBoxContractStartDate
			// 
			this.textBoxContractStartDate.BackColor = System.Drawing.Color.White;
			this.textBoxContractStartDate.Location = new System.Drawing.Point(97, 138);
			this.textBoxContractStartDate.Name = "textBoxContractStartDate";
			this.textBoxContractStartDate.ReadOnly = true;
			this.textBoxContractStartDate.Size = new System.Drawing.Size(101, 24);
			this.textBoxContractStartDate.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(18, 141);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 17);
			this.label5.TabIndex = 8;
			this.label5.Text = "契約開始日";
			// 
			// textBoxContractEndDate
			// 
			this.textBoxContractEndDate.BackColor = System.Drawing.Color.White;
			this.textBoxContractEndDate.Location = new System.Drawing.Point(293, 138);
			this.textBoxContractEndDate.Name = "textBoxContractEndDate";
			this.textBoxContractEndDate.ReadOnly = true;
			this.textBoxContractEndDate.Size = new System.Drawing.Size(101, 24);
			this.textBoxContractEndDate.TabIndex = 11;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(214, 141);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 17);
			this.label6.TabIndex = 10;
			this.label6.Text = "契約終了日";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(459, 176);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(89, 39);
			this.buttonOK.TabIndex = 12;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(554, 176);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(89, 39);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// ModifyPrescription
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 227);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxContractEndDate);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxContractStartDate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dateTimePickerOperationDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxApplyDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxCustomerName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModifyPrescription";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "運用開始日の設定";
			this.Load += new System.EventHandler(this.ModifyPrescription_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCustomerNo;
		private System.Windows.Forms.TextBox textBoxCustomerName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxApplyDate;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerOperationDate;
		private System.Windows.Forms.TextBox textBoxContractStartDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxContractEndDate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
	}
}