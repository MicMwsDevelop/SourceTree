namespace MwsServiceCancelTool.Forms
{
	partial class CuiEditForm
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
			this.label3 = new System.Windows.Forms.Label();
			this.checkBoxPauseEndStatus = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.checkBoxRenewalFlg = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.labelServiceID = new System.Windows.Forms.Label();
			this.labelServiceName = new System.Windows.Forms.Label();
			this.textBoxCreatePerson = new System.Windows.Forms.TextBox();
			this.textBoxUpdatePerson = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.dateTimePickerPeriodEndDate = new MwsLib.Component.DateTimePickerNull();
			this.dateTimePickerUpdateDate = new MwsLib.Component.DateTimePickerNull();
			this.dateTimePickerCreateDate = new MwsLib.Component.DateTimePickerNull();
			this.dateTimePickerCancerationProcessingDate = new MwsLib.Component.DateTimePickerNull();
			this.dateTimePickerCancelationDay = new MwsLib.Component.DateTimePickerNull();
			this.dateTimePickerUseEndDate = new MwsLib.Component.DateTimePickerNull();
			this.dateTimePickerUseStartDate = new MwsLib.Component.DateTimePickerNull();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(44, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "利用開始日";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(368, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "利用終了日";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(44, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "解約申込日";
			// 
			// checkBoxPauseEndStatus
			// 
			this.checkBoxPauseEndStatus.AutoSize = true;
			this.checkBoxPauseEndStatus.Location = new System.Drawing.Point(133, 89);
			this.checkBoxPauseEndStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxPauseEndStatus.Name = "checkBoxPauseEndStatus";
			this.checkBoxPauseEndStatus.Size = new System.Drawing.Size(141, 17);
			this.checkBoxPauseEndStatus.TabIndex = 12;
			this.checkBoxPauseEndStatus.Text = "課金対象終了フラグ";
			this.checkBoxPauseEndStatus.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(369, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 12);
			this.label4.TabIndex = 10;
			this.label4.Text = "解約処理日";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(58, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 12);
			this.label5.TabIndex = 13;
			this.label5.Text = "作成日時";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(398, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 12);
			this.label6.TabIndex = 15;
			this.label6.Text = "作成者";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(58, 135);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 12);
			this.label7.TabIndex = 17;
			this.label7.Text = "更新日時";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label8.Location = new System.Drawing.Point(398, 135);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 12);
			this.label8.TabIndex = 19;
			this.label8.Text = "更新者";
			// 
			// checkBoxRenewalFlg
			// 
			this.checkBoxRenewalFlg.AutoSize = true;
			this.checkBoxRenewalFlg.Location = new System.Drawing.Point(458, 159);
			this.checkBoxRenewalFlg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBoxRenewalFlg.Name = "checkBoxRenewalFlg";
			this.checkBoxRenewalFlg.Size = new System.Drawing.Size(115, 17);
			this.checkBoxRenewalFlg.TabIndex = 23;
			this.checkBoxRenewalFlg.Text = "顧客差分フラグ";
			this.checkBoxRenewalFlg.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(44, 158);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(65, 12);
			this.label9.TabIndex = 21;
			this.label9.Text = "利用期限日";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(14, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(67, 12);
			this.label10.TabIndex = 0;
			this.label10.Text = "サービスID";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(280, 8);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(76, 12);
			this.label11.TabIndex = 2;
			this.label11.Text = "サービス名称";
			// 
			// labelServiceID
			// 
			this.labelServiceID.BackColor = System.Drawing.Color.White;
			this.labelServiceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelServiceID.Location = new System.Drawing.Point(94, 7);
			this.labelServiceID.Name = "labelServiceID";
			this.labelServiceID.Size = new System.Drawing.Size(163, 19);
			this.labelServiceID.TabIndex = 1;
			// 
			// labelServiceName
			// 
			this.labelServiceName.BackColor = System.Drawing.Color.White;
			this.labelServiceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelServiceName.Location = new System.Drawing.Point(371, 7);
			this.labelServiceName.Name = "labelServiceName";
			this.labelServiceName.Size = new System.Drawing.Size(622, 19);
			this.labelServiceName.TabIndex = 3;
			// 
			// textBoxCreatePerson
			// 
			this.textBoxCreatePerson.Location = new System.Drawing.Point(458, 110);
			this.textBoxCreatePerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.textBoxCreatePerson.MaxLength = 20;
			this.textBoxCreatePerson.Name = "textBoxCreatePerson";
			this.textBoxCreatePerson.Size = new System.Drawing.Size(293, 20);
			this.textBoxCreatePerson.TabIndex = 16;
			// 
			// textBoxUpdatePerson
			// 
			this.textBoxUpdatePerson.Location = new System.Drawing.Point(458, 133);
			this.textBoxUpdatePerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.textBoxUpdatePerson.MaxLength = 20;
			this.textBoxUpdatePerson.Name = "textBoxUpdatePerson";
			this.textBoxUpdatePerson.Size = new System.Drawing.Size(293, 20);
			this.textBoxUpdatePerson.TabIndex = 20;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(818, 149);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(84, 24);
			this.buttonOK.TabIndex = 24;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(909, 149);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(84, 24);
			this.buttonCancel.TabIndex = 25;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// dateTimePickerPeriodEndDate
			// 
			this.dateTimePickerPeriodEndDate.Location = new System.Drawing.Point(133, 156);
			this.dateTimePickerPeriodEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerPeriodEndDate.Name = "dateTimePickerPeriodEndDate";
			this.dateTimePickerPeriodEndDate.ShowCheckBox = true;
			this.dateTimePickerPeriodEndDate.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerPeriodEndDate.TabIndex = 22;
			this.dateTimePickerPeriodEndDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 623);
			// 
			// dateTimePickerUpdateDate
			// 
			this.dateTimePickerUpdateDate.Location = new System.Drawing.Point(133, 133);
			this.dateTimePickerUpdateDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerUpdateDate.Name = "dateTimePickerUpdateDate";
			this.dateTimePickerUpdateDate.ShowCheckBox = true;
			this.dateTimePickerUpdateDate.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerUpdateDate.TabIndex = 18;
			this.dateTimePickerUpdateDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 628);
			// 
			// dateTimePickerCreateDate
			// 
			this.dateTimePickerCreateDate.Location = new System.Drawing.Point(133, 110);
			this.dateTimePickerCreateDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerCreateDate.Name = "dateTimePickerCreateDate";
			this.dateTimePickerCreateDate.ShowCheckBox = true;
			this.dateTimePickerCreateDate.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerCreateDate.TabIndex = 14;
			this.dateTimePickerCreateDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 634);
			// 
			// dateTimePickerCancerationProcessingDate
			// 
			this.dateTimePickerCancerationProcessingDate.Location = new System.Drawing.Point(458, 67);
			this.dateTimePickerCancerationProcessingDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerCancerationProcessingDate.Name = "dateTimePickerCancerationProcessingDate";
			this.dateTimePickerCancerationProcessingDate.ShowCheckBox = true;
			this.dateTimePickerCancerationProcessingDate.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerCancerationProcessingDate.TabIndex = 11;
			this.dateTimePickerCancerationProcessingDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 637);
			// 
			// dateTimePickerCancelationDay
			// 
			this.dateTimePickerCancelationDay.Location = new System.Drawing.Point(133, 67);
			this.dateTimePickerCancelationDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerCancelationDay.Name = "dateTimePickerCancelationDay";
			this.dateTimePickerCancelationDay.ShowCheckBox = true;
			this.dateTimePickerCancelationDay.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerCancelationDay.TabIndex = 9;
			this.dateTimePickerCancelationDay.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 642);
			// 
			// dateTimePickerUseEndDate
			// 
			this.dateTimePickerUseEndDate.Location = new System.Drawing.Point(458, 44);
			this.dateTimePickerUseEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerUseEndDate.Name = "dateTimePickerUseEndDate";
			this.dateTimePickerUseEndDate.ShowCheckBox = true;
			this.dateTimePickerUseEndDate.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerUseEndDate.TabIndex = 7;
			this.dateTimePickerUseEndDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 646);
			// 
			// dateTimePickerUseStartDate
			// 
			this.dateTimePickerUseStartDate.Location = new System.Drawing.Point(133, 44);
			this.dateTimePickerUseStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dateTimePickerUseStartDate.Name = "dateTimePickerUseStartDate";
			this.dateTimePickerUseStartDate.ShowCheckBox = true;
			this.dateTimePickerUseStartDate.Size = new System.Drawing.Size(195, 20);
			this.dateTimePickerUseStartDate.TabIndex = 5;
			this.dateTimePickerUseStartDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 650);
			// 
			// CuiEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1018, 187);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.dateTimePickerPeriodEndDate);
			this.Controls.Add(this.textBoxUpdatePerson);
			this.Controls.Add(this.dateTimePickerUpdateDate);
			this.Controls.Add(this.textBoxCreatePerson);
			this.Controls.Add(this.dateTimePickerCreateDate);
			this.Controls.Add(this.dateTimePickerCancerationProcessingDate);
			this.Controls.Add(this.dateTimePickerCancelationDay);
			this.Controls.Add(this.dateTimePickerUseEndDate);
			this.Controls.Add(this.dateTimePickerUseStartDate);
			this.Controls.Add(this.labelServiceName);
			this.Controls.Add(this.labelServiceID);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.checkBoxRenewalFlg);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkBoxPauseEndStatus);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MinimizeBox = false;
			this.Name = "CuiEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.EditForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxPauseEndStatus;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox checkBoxRenewalFlg;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label labelServiceID;
		private System.Windows.Forms.Label labelServiceName;
		private MwsLib.Component.DateTimePickerNull dateTimePickerUseStartDate;
		private MwsLib.Component.DateTimePickerNull dateTimePickerUseEndDate;
		private MwsLib.Component.DateTimePickerNull dateTimePickerCancelationDay;
		private MwsLib.Component.DateTimePickerNull dateTimePickerCancerationProcessingDate;
		private MwsLib.Component.DateTimePickerNull dateTimePickerCreateDate;
		private System.Windows.Forms.TextBox textBoxCreatePerson;
		private MwsLib.Component.DateTimePickerNull dateTimePickerUpdateDate;
		private System.Windows.Forms.TextBox textBoxUpdatePerson;
		private MwsLib.Component.DateTimePickerNull dateTimePickerPeriodEndDate;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
	}
}