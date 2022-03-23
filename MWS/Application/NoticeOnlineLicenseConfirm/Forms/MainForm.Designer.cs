
namespace NoticeOnlineLicenseConfirm.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxNotice5 = new System.Windows.Forms.CheckBox();
			this.checkBoxNotice4 = new System.Windows.Forms.CheckBox();
			this.checkBoxNotice3 = new System.Windows.Forms.CheckBox();
			this.checkBoxNotice1 = new System.Windows.Forms.CheckBox();
			this.buttonConfirmFile = new System.Windows.Forms.Button();
			this.buttonSendMail = new System.Windows.Forms.Button();
			this.textBoxConfirmFile = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.labelNotice5 = new System.Windows.Forms.Label();
			this.labelNotice4 = new System.Windows.Forms.Label();
			this.labelNotice3 = new System.Windows.Forms.Label();
			this.labelNotice2 = new System.Windows.Forms.Label();
			this.labelNotice1 = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonProgressEast = new System.Windows.Forms.Button();
			this.textBoxProgressEast = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonCheck = new System.Windows.Forms.Button();
			this.buttonProgressWest = new System.Windows.Forms.Button();
			this.textBoxProgressWest = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerEast = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePickerWest = new System.Windows.Forms.DateTimePicker();
			this.label11 = new System.Windows.Forms.Label();
			this.buttonContactWest = new System.Windows.Forms.Button();
			this.textBoxContactWest = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxNotice5);
			this.groupBox2.Controls.Add(this.checkBoxNotice4);
			this.groupBox2.Controls.Add(this.checkBoxNotice3);
			this.groupBox2.Controls.Add(this.checkBoxNotice1);
			this.groupBox2.Controls.Add(this.buttonConfirmFile);
			this.groupBox2.Controls.Add(this.buttonSendMail);
			this.groupBox2.Controls.Add(this.textBoxConfirmFile);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Location = new System.Drawing.Point(20, 386);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(637, 200);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "担当者に連絡";
			// 
			// checkBoxNotice5
			// 
			this.checkBoxNotice5.AutoSize = true;
			this.checkBoxNotice5.Location = new System.Drawing.Point(26, 163);
			this.checkBoxNotice5.Name = "checkBoxNotice5";
			this.checkBoxNotice5.Size = new System.Drawing.Size(313, 21);
			this.checkBoxNotice5.TabIndex = 6;
			this.checkBoxNotice5.Text = "通知５：工事日14日前でヒアリングシート未完成通知";
			this.checkBoxNotice5.UseVisualStyleBackColor = true;
			// 
			// checkBoxNotice4
			// 
			this.checkBoxNotice4.AutoSize = true;
			this.checkBoxNotice4.Location = new System.Drawing.Point(26, 136);
			this.checkBoxNotice4.Name = "checkBoxNotice4";
			this.checkBoxNotice4.Size = new System.Drawing.Size(336, 21);
			this.checkBoxNotice4.TabIndex = 5;
			this.checkBoxNotice4.Text = "通知４：NTT西日本 ヒアリングシート修正アップデート通知";
			this.checkBoxNotice4.UseVisualStyleBackColor = true;
			// 
			// checkBoxNotice3
			// 
			this.checkBoxNotice3.AutoSize = true;
			this.checkBoxNotice3.Location = new System.Drawing.Point(26, 109);
			this.checkBoxNotice3.Name = "checkBoxNotice3";
			this.checkBoxNotice3.Size = new System.Drawing.Size(336, 21);
			this.checkBoxNotice3.TabIndex = 4;
			this.checkBoxNotice3.Text = "通知３：NTT東日本 ヒアリングシート修正アップデート通知";
			this.checkBoxNotice3.UseVisualStyleBackColor = true;
			// 
			// checkBoxNotice1
			// 
			this.checkBoxNotice1.AutoSize = true;
			this.checkBoxNotice1.BackColor = System.Drawing.SystemColors.Control;
			this.checkBoxNotice1.Location = new System.Drawing.Point(26, 82);
			this.checkBoxNotice1.Name = "checkBoxNotice1";
			this.checkBoxNotice1.Size = new System.Drawing.Size(230, 21);
			this.checkBoxNotice1.TabIndex = 3;
			this.checkBoxNotice1.Text = "通知１：作業日決定を担当者へ通知";
			this.checkBoxNotice1.UseVisualStyleBackColor = false;
			// 
			// buttonConfirmFile
			// 
			this.buttonConfirmFile.Location = new System.Drawing.Point(575, 51);
			this.buttonConfirmFile.Margin = new System.Windows.Forms.Padding(4);
			this.buttonConfirmFile.Name = "buttonConfirmFile";
			this.buttonConfirmFile.Size = new System.Drawing.Size(34, 25);
			this.buttonConfirmFile.TabIndex = 2;
			this.buttonConfirmFile.Text = "▼";
			this.buttonConfirmFile.UseVisualStyleBackColor = true;
			this.buttonConfirmFile.Click += new System.EventHandler(this.buttonConfirmFile_Click);
			// 
			// buttonSendMail
			// 
			this.buttonSendMail.Location = new System.Drawing.Point(442, 138);
			this.buttonSendMail.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSendMail.Name = "buttonSendMail";
			this.buttonSendMail.Size = new System.Drawing.Size(167, 46);
			this.buttonSendMail.TabIndex = 7;
			this.buttonSendMail.Text = "メール送信";
			this.buttonSendMail.UseVisualStyleBackColor = true;
			this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
			// 
			// textBoxConfirmFile
			// 
			this.textBoxConfirmFile.BackColor = System.Drawing.Color.White;
			this.textBoxConfirmFile.Location = new System.Drawing.Point(27, 51);
			this.textBoxConfirmFile.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxConfirmFile.Name = "textBoxConfirmFile";
			this.textBoxConfirmFile.ReadOnly = true;
			this.textBoxConfirmFile.Size = new System.Drawing.Size(548, 24);
			this.textBoxConfirmFile.TabIndex = 1;
			this.textBoxConfirmFile.TabStop = false;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(24, 30);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(173, 17);
			this.label10.TabIndex = 0;
			this.label10.Text = "■オンライン資格確認通知結果";
			// 
			// labelNotice5
			// 
			this.labelNotice5.AutoSize = true;
			this.labelNotice5.Location = new System.Drawing.Point(143, 300);
			this.labelNotice5.Name = "labelNotice5";
			this.labelNotice5.Size = new System.Drawing.Size(81, 17);
			this.labelNotice5.TabIndex = 17;
			this.labelNotice5.Text = "通知５：0件";
			// 
			// labelNotice4
			// 
			this.labelNotice4.AutoSize = true;
			this.labelNotice4.Location = new System.Drawing.Point(143, 274);
			this.labelNotice4.Name = "labelNotice4";
			this.labelNotice4.Size = new System.Drawing.Size(81, 17);
			this.labelNotice4.TabIndex = 16;
			this.labelNotice4.Text = "通知４：0件";
			// 
			// labelNotice3
			// 
			this.labelNotice3.AutoSize = true;
			this.labelNotice3.Location = new System.Drawing.Point(26, 327);
			this.labelNotice3.Name = "labelNotice3";
			this.labelNotice3.Size = new System.Drawing.Size(81, 17);
			this.labelNotice3.TabIndex = 15;
			this.labelNotice3.Text = "通知３：0件";
			// 
			// labelNotice2
			// 
			this.labelNotice2.AutoSize = true;
			this.labelNotice2.Location = new System.Drawing.Point(26, 300);
			this.labelNotice2.Name = "labelNotice2";
			this.labelNotice2.Size = new System.Drawing.Size(81, 17);
			this.labelNotice2.TabIndex = 14;
			this.labelNotice2.Text = "通知２：0件";
			// 
			// labelNotice1
			// 
			this.labelNotice1.AutoSize = true;
			this.labelNotice1.Location = new System.Drawing.Point(26, 274);
			this.labelNotice1.Name = "labelNotice1";
			this.labelNotice1.Size = new System.Drawing.Size(81, 17);
			this.labelNotice1.TabIndex = 13;
			this.labelNotice1.Text = "通知１：0件";
			// 
			// labelVersion
			// 
			this.labelVersion.Location = new System.Drawing.Point(507, 589);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(150, 29);
			this.labelVersion.TabIndex = 2;
			this.labelVersion.Text = "Ver1.00 2022/03/10";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 32);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "■NTT東日本 進捗管理表";
			// 
			// buttonProgressEast
			// 
			this.buttonProgressEast.Location = new System.Drawing.Point(575, 52);
			this.buttonProgressEast.Margin = new System.Windows.Forms.Padding(4);
			this.buttonProgressEast.Name = "buttonProgressEast";
			this.buttonProgressEast.Size = new System.Drawing.Size(34, 25);
			this.buttonProgressEast.TabIndex = 2;
			this.buttonProgressEast.Text = "▼";
			this.buttonProgressEast.UseVisualStyleBackColor = true;
			this.buttonProgressEast.Click += new System.EventHandler(this.buttonProgressEast_Click);
			// 
			// textBoxProgressEast
			// 
			this.textBoxProgressEast.BackColor = System.Drawing.Color.White;
			this.textBoxProgressEast.Location = new System.Drawing.Point(27, 53);
			this.textBoxProgressEast.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxProgressEast.Name = "textBoxProgressEast";
			this.textBoxProgressEast.ReadOnly = true;
			this.textBoxProgressEast.Size = new System.Drawing.Size(548, 24);
			this.textBoxProgressEast.TabIndex = 1;
			this.textBoxProgressEast.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 125);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(155, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "■NTT西日本 進捗管理表";
			// 
			// buttonCheck
			// 
			this.buttonCheck.Location = new System.Drawing.Point(442, 298);
			this.buttonCheck.Margin = new System.Windows.Forms.Padding(4);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(167, 46);
			this.buttonCheck.TabIndex = 18;
			this.buttonCheck.Text = "チェック";
			this.buttonCheck.UseVisualStyleBackColor = true;
			this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
			// 
			// buttonProgressWest
			// 
			this.buttonProgressWest.Location = new System.Drawing.Point(574, 145);
			this.buttonProgressWest.Margin = new System.Windows.Forms.Padding(4);
			this.buttonProgressWest.Name = "buttonProgressWest";
			this.buttonProgressWest.Size = new System.Drawing.Size(34, 25);
			this.buttonProgressWest.TabIndex = 7;
			this.buttonProgressWest.Text = "▼";
			this.buttonProgressWest.UseVisualStyleBackColor = true;
			this.buttonProgressWest.Click += new System.EventHandler(this.buttonProgressWest_Click);
			// 
			// textBoxProgressWest
			// 
			this.textBoxProgressWest.BackColor = System.Drawing.Color.White;
			this.textBoxProgressWest.Location = new System.Drawing.Point(26, 146);
			this.textBoxProgressWest.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxProgressWest.Name = "textBoxProgressWest";
			this.textBoxProgressWest.ReadOnly = true;
			this.textBoxProgressWest.Size = new System.Drawing.Size(548, 24);
			this.textBoxProgressWest.TabIndex = 6;
			this.textBoxProgressWest.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 88);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "ファイル作成日";
			// 
			// dateTimePickerEast
			// 
			this.dateTimePickerEast.Location = new System.Drawing.Point(114, 85);
			this.dateTimePickerEast.Name = "dateTimePickerEast";
			this.dateTimePickerEast.Size = new System.Drawing.Size(143, 24);
			this.dateTimePickerEast.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 183);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "ファイル作成日";
			// 
			// dateTimePickerWest
			// 
			this.dateTimePickerWest.Location = new System.Drawing.Point(114, 180);
			this.dateTimePickerWest.Name = "dateTimePickerWest";
			this.dateTimePickerWest.Size = new System.Drawing.Size(143, 24);
			this.dateTimePickerWest.TabIndex = 9;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(24, 221);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(129, 17);
			this.label11.TabIndex = 10;
			this.label11.Text = "■NTT西日本 連絡票";
			// 
			// buttonContactWest
			// 
			this.buttonContactWest.Location = new System.Drawing.Point(575, 241);
			this.buttonContactWest.Margin = new System.Windows.Forms.Padding(4);
			this.buttonContactWest.Name = "buttonContactWest";
			this.buttonContactWest.Size = new System.Drawing.Size(34, 25);
			this.buttonContactWest.TabIndex = 12;
			this.buttonContactWest.Text = "▼";
			this.buttonContactWest.UseVisualStyleBackColor = true;
			this.buttonContactWest.Click += new System.EventHandler(this.buttonContactWest_Click);
			// 
			// textBoxContactWest
			// 
			this.textBoxContactWest.BackColor = System.Drawing.Color.White;
			this.textBoxContactWest.Location = new System.Drawing.Point(27, 242);
			this.textBoxContactWest.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxContactWest.Name = "textBoxContactWest";
			this.textBoxContactWest.ReadOnly = true;
			this.textBoxContactWest.Size = new System.Drawing.Size(548, 24);
			this.textBoxContactWest.TabIndex = 11;
			this.textBoxContactWest.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelNotice5);
			this.groupBox1.Controls.Add(this.textBoxContactWest);
			this.groupBox1.Controls.Add(this.labelNotice4);
			this.groupBox1.Controls.Add(this.buttonContactWest);
			this.groupBox1.Controls.Add(this.labelNotice3);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.labelNotice2);
			this.groupBox1.Controls.Add(this.dateTimePickerWest);
			this.groupBox1.Controls.Add(this.labelNotice1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.dateTimePickerEast);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxProgressWest);
			this.groupBox1.Controls.Add(this.buttonProgressWest);
			this.groupBox1.Controls.Add(this.buttonCheck);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxProgressEast);
			this.groupBox1.Controls.Add(this.buttonProgressEast);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(20, 18);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(637, 361);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "進捗管理表のチェック";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(683, 630);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "オンライン資格確認通知";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonSendMail;
		private System.Windows.Forms.TextBox textBoxConfirmFile;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Button buttonConfirmFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonProgressEast;
		private System.Windows.Forms.TextBox textBoxProgressEast;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonCheck;
		private System.Windows.Forms.Button buttonProgressWest;
		private System.Windows.Forms.TextBox textBoxProgressWest;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dateTimePickerEast;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerWest;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button buttonContactWest;
		private System.Windows.Forms.TextBox textBoxContactWest;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelNotice5;
		private System.Windows.Forms.Label labelNotice4;
		private System.Windows.Forms.Label labelNotice3;
		private System.Windows.Forms.Label labelNotice2;
		private System.Windows.Forms.Label labelNotice1;
		private System.Windows.Forms.CheckBox checkBoxNotice5;
		private System.Windows.Forms.CheckBox checkBoxNotice4;
		private System.Windows.Forms.CheckBox checkBoxNotice3;
		private System.Windows.Forms.CheckBox checkBoxNotice1;
	}
}

