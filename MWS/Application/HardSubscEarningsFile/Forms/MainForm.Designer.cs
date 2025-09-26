namespace HardSubscEarningsFile.Forms
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
			this.dateTimePickerMonth = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonExec = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxPcaVer = new MwsLib.Component.NumericTextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// dateTimePickerMonth
			// 
			this.dateTimePickerMonth.CustomFormat = "yyyy年MM月";
			this.dateTimePickerMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerMonth.Location = new System.Drawing.Point(158, 12);
			this.dateTimePickerMonth.Name = "dateTimePickerMonth";
			this.dateTimePickerMonth.Size = new System.Drawing.Size(117, 20);
			this.dateTimePickerMonth.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 17);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "実行月";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 114);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "PCAバージョン番号";
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(482, 211);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(149, 49);
			this.buttonExec.TabIndex = 10;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(12, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(619, 56);
			this.label3.TabIndex = 8;
			this.label3.Text = "本アプリはハードサブスク契約情報から売上データのCSVファイルを出力します。\r\n※実行後、ハードサブスク契約情報の課金終了日が１ヵ月更新されます。\r\n   サイレ" +
    "イトモードでの実行は引数に \"Auto\" を指定してください。";
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Location = new System.Drawing.Point(158, 45);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(473, 20);
			this.textBoxFolder.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "出力先フォルダ";
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.Location = new System.Drawing.Point(158, 78);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.Size = new System.Drawing.Size(473, 20);
			this.textBoxFilename.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 81);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "売上データファイル名";
			// 
			// textBoxPcaVer
			// 
			this.textBoxPcaVer.Location = new System.Drawing.Point(158, 111);
			this.textBoxPcaVer.Name = "textBoxPcaVer";
			this.textBoxPcaVer.Size = new System.Drawing.Size(55, 20);
			this.textBoxPcaVer.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(15, 211);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(438, 49);
			this.label6.TabIndex = 9;
			this.label6.Text = "注意事項\r\n本アプリは終了ユーザーの売上を作成しないため、課金データ作成バッチの後に実行する必要がある";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(651, 273);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxPcaVer);
			this.Controls.Add(this.dateTimePickerMonth);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxFilename);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ハードサブスク売上データ作成";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dateTimePickerMonth;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox textBoxPcaVer;
		private System.Windows.Forms.Label label6;
	}
}

