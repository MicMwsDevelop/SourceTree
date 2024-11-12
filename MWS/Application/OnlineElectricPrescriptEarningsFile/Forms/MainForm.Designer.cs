namespace OnlineElectricPrescriptEarningsFile.Forms
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
			this.buttonExec = new System.Windows.Forms.Button();
			this.textBoxPcaVer = new MwsLib.Component.NumericTextBox();
			this.textBoxExportFilename = new System.Windows.Forms.TextBox();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.dateTimePickerMonth = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(482, 159);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(146, 45);
			this.buttonExec.TabIndex = 8;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// textBoxPcaVer
			// 
			this.textBoxPcaVer.Location = new System.Drawing.Point(144, 107);
			this.textBoxPcaVer.Name = "textBoxPcaVer";
			this.textBoxPcaVer.Size = new System.Drawing.Size(51, 23);
			this.textBoxPcaVer.TabIndex = 7;
			// 
			// textBoxExportFilename
			// 
			this.textBoxExportFilename.Location = new System.Drawing.Point(144, 78);
			this.textBoxExportFilename.Name = "textBoxExportFilename";
			this.textBoxExportFilename.Size = new System.Drawing.Size(484, 23);
			this.textBoxExportFilename.TabIndex = 5;
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Location = new System.Drawing.Point(144, 48);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(484, 23);
			this.textBoxFolder.TabIndex = 3;
			// 
			// dateTimePickerMonth
			// 
			this.dateTimePickerMonth.CustomFormat = "yyyy年MM月";
			this.dateTimePickerMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerMonth.Location = new System.Drawing.Point(144, 18);
			this.dateTimePickerMonth.Name = "dateTimePickerMonth";
			this.dateTimePickerMonth.Size = new System.Drawing.Size(121, 23);
			this.dateTimePickerMonth.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 15);
			this.label4.TabIndex = 6;
			this.label4.Text = "PCAバージョン番号";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "売上データファイル名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "出力先フォルダ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "実行月";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(641, 216);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.textBoxPcaVer);
			this.Controls.Add(this.textBoxExportFilename);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.dateTimePickerMonth);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オン資電子処方箋売上データ作成";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonExec;
		private MwsLib.Component.NumericTextBox textBoxPcaVer;
		private System.Windows.Forms.TextBox textBoxExportFilename;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.DateTimePicker dateTimePickerMonth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}

