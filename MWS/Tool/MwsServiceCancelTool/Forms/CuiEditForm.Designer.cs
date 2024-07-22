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
			this.label1.Location = new System.Drawing.Point(39, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "利用開始日";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(327, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "利用終了日";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(39, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "解約申込日";
			// 
			// checkBoxPauseEndStatus
			// 
			this.checkBoxPauseEndStatus.AutoSize = true;
			this.checkBoxPauseEndStatus.Location = new System.Drawing.Point(118, 117);
			this.checkBoxPauseEndStatus.Name = "checkBoxPauseEndStatus";
			this.checkBoxPauseEndStatus.Size = new System.Drawing.Size(133, 21);
			this.checkBoxPauseEndStatus.TabIndex = 12;
			this.checkBoxPauseEndStatus.Text = "課金対象終了フラグ";
			this.checkBoxPauseEndStatus.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(328, 89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 17);
			this.label4.TabIndex = 10;
			this.label4.Text = "解約処理日";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(52, 147);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 17);
			this.label5.TabIndex = 13;
			this.label5.Text = "作成日時";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(354, 147);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 17);
			this.label6.TabIndex = 15;
			this.label6.Text = "作成者";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(52, 177);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 17);
			this.label7.TabIndex = 17;
			this.label7.Text = "更新日時";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(354, 177);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 17);
			this.label8.TabIndex = 19;
			this.label8.Text = "更新者";
			// 
			// checkBoxRenewalFlg
			// 
			this.checkBoxRenewalFlg.AutoSize = true;
			this.checkBoxRenewalFlg.Location = new System.Drawing.Point(407, 208);
			this.checkBoxRenewalFlg.Name = "checkBoxRenewalFlg";
			this.checkBoxRenewalFlg.Size = new System.Drawing.Size(107, 21);
			this.checkBoxRenewalFlg.TabIndex = 23;
			this.checkBoxRenewalFlg.Text = "顧客差分フラグ";
			this.checkBoxRenewalFlg.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(39, 207);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(73, 17);
			this.label9.TabIndex = 21;
			this.label9.Text = "利用期限日";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 11);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 17);
			this.label10.TabIndex = 0;
			this.label10.Text = "サービスID";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(249, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(75, 17);
			this.label11.TabIndex = 2;
			this.label11.Text = "サービス名称";
			// 
			// labelServiceID
			// 
			this.labelServiceID.BackColor = System.Drawing.Color.White;
			this.labelServiceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelServiceID.Location = new System.Drawing.Point(84, 9);
			this.labelServiceID.Name = "labelServiceID";
			this.labelServiceID.Size = new System.Drawing.Size(145, 24);
			this.labelServiceID.TabIndex = 1;
			// 
			// labelServiceName
			// 
			this.labelServiceName.BackColor = System.Drawing.Color.White;
			this.labelServiceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelServiceName.Location = new System.Drawing.Point(330, 9);
			this.labelServiceName.Name = "labelServiceName";
			this.labelServiceName.Size = new System.Drawing.Size(553, 24);
			this.labelServiceName.TabIndex = 3;
			// 
			// textBoxCreatePerson
			// 
			this.textBoxCreatePerson.Location = new System.Drawing.Point(407, 144);
			this.textBoxCreatePerson.MaxLength = 20;
			this.textBoxCreatePerson.Name = "textBoxCreatePerson";
			this.textBoxCreatePerson.Size = new System.Drawing.Size(261, 24);
			this.textBoxCreatePerson.TabIndex = 16;
			// 
			// textBoxUpdatePerson
			// 
			this.textBoxUpdatePerson.Location = new System.Drawing.Point(407, 174);
			this.textBoxUpdatePerson.MaxLength = 20;
			this.textBoxUpdatePerson.Name = "textBoxUpdatePerson";
			this.textBoxUpdatePerson.Size = new System.Drawing.Size(261, 24);
			this.textBoxUpdatePerson.TabIndex = 20;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(727, 195);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 32);
			this.buttonOK.TabIndex = 24;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(808, 195);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 32);
			this.buttonCancel.TabIndex = 25;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// dateTimePickerPeriodEndDate
			// 
			this.dateTimePickerPeriodEndDate.Location = new System.Drawing.Point(118, 204);
			this.dateTimePickerPeriodEndDate.Name = "dateTimePickerPeriodEndDate";
			this.dateTimePickerPeriodEndDate.ShowCheckBox = true;
			this.dateTimePickerPeriodEndDate.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerPeriodEndDate.TabIndex = 22;
			this.dateTimePickerPeriodEndDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 623);
			// 
			// dateTimePickerUpdateDate
			// 
			this.dateTimePickerUpdateDate.Location = new System.Drawing.Point(118, 174);
			this.dateTimePickerUpdateDate.Name = "dateTimePickerUpdateDate";
			this.dateTimePickerUpdateDate.ShowCheckBox = true;
			this.dateTimePickerUpdateDate.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerUpdateDate.TabIndex = 18;
			this.dateTimePickerUpdateDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 628);
			// 
			// dateTimePickerCreateDate
			// 
			this.dateTimePickerCreateDate.Location = new System.Drawing.Point(118, 144);
			this.dateTimePickerCreateDate.Name = "dateTimePickerCreateDate";
			this.dateTimePickerCreateDate.ShowCheckBox = true;
			this.dateTimePickerCreateDate.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerCreateDate.TabIndex = 14;
			this.dateTimePickerCreateDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 634);
			// 
			// dateTimePickerCancerationProcessingDate
			// 
			this.dateTimePickerCancerationProcessingDate.Location = new System.Drawing.Point(407, 87);
			this.dateTimePickerCancerationProcessingDate.Name = "dateTimePickerCancerationProcessingDate";
			this.dateTimePickerCancerationProcessingDate.ShowCheckBox = true;
			this.dateTimePickerCancerationProcessingDate.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerCancerationProcessingDate.TabIndex = 11;
			this.dateTimePickerCancerationProcessingDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 637);
			// 
			// dateTimePickerCancelationDay
			// 
			this.dateTimePickerCancelationDay.Location = new System.Drawing.Point(118, 87);
			this.dateTimePickerCancelationDay.Name = "dateTimePickerCancelationDay";
			this.dateTimePickerCancelationDay.ShowCheckBox = true;
			this.dateTimePickerCancelationDay.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerCancelationDay.TabIndex = 9;
			this.dateTimePickerCancelationDay.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 642);
			// 
			// dateTimePickerUseEndDate
			// 
			this.dateTimePickerUseEndDate.Location = new System.Drawing.Point(407, 57);
			this.dateTimePickerUseEndDate.Name = "dateTimePickerUseEndDate";
			this.dateTimePickerUseEndDate.ShowCheckBox = true;
			this.dateTimePickerUseEndDate.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerUseEndDate.TabIndex = 7;
			this.dateTimePickerUseEndDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 646);
			// 
			// dateTimePickerUseStartDate
			// 
			this.dateTimePickerUseStartDate.Location = new System.Drawing.Point(118, 57);
			this.dateTimePickerUseStartDate.Name = "dateTimePickerUseStartDate";
			this.dateTimePickerUseStartDate.ShowCheckBox = true;
			this.dateTimePickerUseStartDate.Size = new System.Drawing.Size(174, 24);
			this.dateTimePickerUseStartDate.TabIndex = 5;
			this.dateTimePickerUseStartDate.Value = new System.DateTime(2023, 8, 25, 9, 56, 7, 650);
			// 
			// CuiEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(905, 245);
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
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
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