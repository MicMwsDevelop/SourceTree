namespace EntryFinishedUser.Forms
{
	partial class EntryFinishedUserForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTokuisakiID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePickerAcceptDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxReplace = new System.Windows.Forms.ComboBox();
			this.labelVersion = new System.Windows.Forms.Label();
			this.comboBoxFinishedReason = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxReason = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.checkBoxNonPaletteUser = new System.Windows.Forms.CheckBox();
			this.dateTimePickerFinishedYearMonth = new System.Windows.Forms.DateTimePicker();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 50);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "得意先No";
			// 
			// textBoxTokuisakiID
			// 
			this.textBoxTokuisakiID.BackColor = System.Drawing.Color.White;
			this.textBoxTokuisakiID.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBoxTokuisakiID.Location = new System.Drawing.Point(86, 47);
			this.textBoxTokuisakiID.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxTokuisakiID.MaxLength = 6;
			this.textBoxTokuisakiID.Name = "textBoxTokuisakiID";
			this.textBoxTokuisakiID.ReadOnly = true;
			this.textBoxTokuisakiID.Size = new System.Drawing.Size(132, 24);
			this.textBoxTokuisakiID.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 82);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "顧客名";
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxUserName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxUserName.Location = new System.Drawing.Point(86, 79);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(295, 24);
			this.textBoxUserName.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 112);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "終了月";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(242, 112);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "終了届受領日";
			// 
			// dateTimePickerAcceptDate
			// 
			this.dateTimePickerAcceptDate.Checked = false;
			this.dateTimePickerAcceptDate.Location = new System.Drawing.Point(335, 110);
			this.dateTimePickerAcceptDate.Name = "dateTimePickerAcceptDate";
			this.dateTimePickerAcceptDate.ShowCheckBox = true;
			this.dateTimePickerAcceptDate.Size = new System.Drawing.Size(149, 24);
			this.dateTimePickerAcceptDate.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 170);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 17);
			this.label5.TabIndex = 12;
			this.label5.Text = "リプレース";
			// 
			// comboBoxReplace
			// 
			this.comboBoxReplace.FormattingEnabled = true;
			this.comboBoxReplace.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.comboBoxReplace.Location = new System.Drawing.Point(86, 167);
			this.comboBoxReplace.MaxLength = 50;
			this.comboBoxReplace.Name = "comboBoxReplace";
			this.comboBoxReplace.Size = new System.Drawing.Size(182, 25);
			this.comboBoxReplace.TabIndex = 13;
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(322, 9);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(146, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Ver 1.00  2018/08/24";
			// 
			// comboBoxFinishedReason
			// 
			this.comboBoxFinishedReason.FormattingEnabled = true;
			this.comboBoxFinishedReason.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.comboBoxFinishedReason.Items.AddRange(new object[] {
            "１．他メーカーリプレース",
            "２．閉院",
            "３．手書きなどに変更",
            "４．その他"});
			this.comboBoxFinishedReason.Location = new System.Drawing.Point(86, 198);
			this.comboBoxFinishedReason.MaxLength = 50;
			this.comboBoxFinishedReason.Name = "comboBoxFinishedReason";
			this.comboBoxFinishedReason.Size = new System.Drawing.Size(398, 25);
			this.comboBoxFinishedReason.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 201);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "終了事由";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 232);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(34, 17);
			this.label7.TabIndex = 16;
			this.label7.Text = "理由";
			// 
			// textBoxReason
			// 
			this.textBoxReason.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxReason.Location = new System.Drawing.Point(86, 229);
			this.textBoxReason.Multiline = true;
			this.textBoxReason.Name = "textBoxReason";
			this.textBoxReason.Size = new System.Drawing.Size(398, 121);
			this.textBoxReason.TabIndex = 17;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(328, 356);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 42);
			this.buttonOK.TabIndex = 18;
			this.buttonOK.Text = "登録";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(409, 356);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 42);
			this.buttonCancel.TabIndex = 20;
			this.buttonCancel.Text = "閉じる";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// checkBoxNonPaletteUser
			// 
			this.checkBoxNonPaletteUser.AutoSize = true;
			this.checkBoxNonPaletteUser.Location = new System.Drawing.Point(86, 140);
			this.checkBoxNonPaletteUser.Name = "checkBoxNonPaletteUser";
			this.checkBoxNonPaletteUser.Size = new System.Drawing.Size(182, 21);
			this.checkBoxNonPaletteUser.TabIndex = 11;
			this.checkBoxNonPaletteUser.Text = "非paletteユーザーとして使用";
			this.checkBoxNonPaletteUser.UseVisualStyleBackColor = true;
			// 
			// dateTimePickerFinishedYearMonth
			// 
			this.dateTimePickerFinishedYearMonth.Checked = false;
			this.dateTimePickerFinishedYearMonth.CustomFormat = "yyyy年MM月";
			this.dateTimePickerFinishedYearMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerFinishedYearMonth.Location = new System.Drawing.Point(86, 110);
			this.dateTimePickerFinishedYearMonth.Name = "dateTimePickerFinishedYearMonth";
			this.dateTimePickerFinishedYearMonth.ShowCheckBox = true;
			this.dateTimePickerFinishedYearMonth.Size = new System.Drawing.Size(149, 24);
			this.dateTimePickerFinishedYearMonth.TabIndex = 21;
			// 
			// EntryFinishedUserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(498, 411);
			this.Controls.Add(this.dateTimePickerFinishedYearMonth);
			this.Controls.Add(this.checkBoxNonPaletteUser);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxReason);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboBoxFinishedReason);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.comboBoxReplace);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dateTimePickerAcceptDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxUserName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxTokuisakiID);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "EntryFinishedUserForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "終了ユーザー登録";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxTokuisakiID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerAcceptDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxReplace;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.ComboBox comboBoxFinishedReason;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxReason;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.CheckBox checkBoxNonPaletteUser;
		private System.Windows.Forms.DateTimePicker dateTimePickerFinishedYearMonth;
	}
}

