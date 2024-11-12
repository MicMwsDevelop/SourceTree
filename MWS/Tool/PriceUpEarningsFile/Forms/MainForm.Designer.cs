namespace PriceUpEarningsFile.Forms
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
			this.label1 = new System.Windows.Forms.Label();
			this.buttonExec = new System.Windows.Forms.Button();
			this.textBoxExportFilename = new System.Windows.Forms.TextBox();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxImportPathname = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxGoodsPathname = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.buttonImportFile = new System.Windows.Forms.Button();
			this.buttonGoodsFile = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.dateTimePickerMonth = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.numericTextBoxPcaVerGoods = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxPcaVerEarnings = new MwsLib.Component.NumericTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(37, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(605, 59);
			this.label1.TabIndex = 0;
			this.label1.Text = "MWSサービスの価格改定に伴い、月額課金サービスの新規利用申込の初月分の新価格を旧価格で相殺する売上データを作成。※本アプリはデータベースを参照しません。";
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(496, 305);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(146, 45);
			this.buttonExec.TabIndex = 17;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// textBoxExportFilename
			// 
			this.textBoxExportFilename.Location = new System.Drawing.Point(158, 267);
			this.textBoxExportFilename.Name = "textBoxExportFilename";
			this.textBoxExportFilename.Size = new System.Drawing.Size(484, 23);
			this.textBoxExportFilename.TabIndex = 16;
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Location = new System.Drawing.Point(158, 238);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(484, 23);
			this.textBoxFolder.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(75, 270);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 15);
			this.label3.TabIndex = 15;
			this.label3.Text = "出力ファイル名";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(74, 241);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(78, 15);
			this.label5.TabIndex = 13;
			this.label5.Text = "出力先フォルダ";
			// 
			// textBoxImportPathname
			// 
			this.textBoxImportPathname.BackColor = System.Drawing.Color.White;
			this.textBoxImportPathname.Location = new System.Drawing.Point(158, 92);
			this.textBoxImportPathname.Name = "textBoxImportPathname";
			this.textBoxImportPathname.ReadOnly = true;
			this.textBoxImportPathname.Size = new System.Drawing.Size(484, 23);
			this.textBoxImportPathname.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(47, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "売上データファイル名";
			// 
			// textBoxGoodsPathname
			// 
			this.textBoxGoodsPathname.BackColor = System.Drawing.Color.White;
			this.textBoxGoodsPathname.Location = new System.Drawing.Point(158, 149);
			this.textBoxGoodsPathname.Name = "textBoxGoodsPathname";
			this.textBoxGoodsPathname.ReadOnly = true;
			this.textBoxGoodsPathname.Size = new System.Drawing.Size(484, 23);
			this.textBoxGoodsPathname.TabIndex = 7;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 153);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(140, 15);
			this.label7.TabIndex = 6;
			this.label7.Text = "PCA商品マスタ（旧価格）";
			// 
			// buttonImportFile
			// 
			this.buttonImportFile.Location = new System.Drawing.Point(641, 92);
			this.buttonImportFile.Name = "buttonImportFile";
			this.buttonImportFile.Size = new System.Drawing.Size(27, 23);
			this.buttonImportFile.TabIndex = 3;
			this.buttonImportFile.Text = "▼";
			this.buttonImportFile.UseVisualStyleBackColor = true;
			this.buttonImportFile.Click += new System.EventHandler(this.buttonImportFile_Click);
			// 
			// buttonGoodsFile
			// 
			this.buttonGoodsFile.Location = new System.Drawing.Point(641, 149);
			this.buttonGoodsFile.Name = "buttonGoodsFile";
			this.buttonGoodsFile.Size = new System.Drawing.Size(27, 23);
			this.buttonGoodsFile.TabIndex = 8;
			this.buttonGoodsFile.Text = "▼";
			this.buttonGoodsFile.UseVisualStyleBackColor = true;
			this.buttonGoodsFile.Click += new System.EventHandler(this.buttonGoodsFile_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(406, 124);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(179, 15);
			this.label8.TabIndex = 4;
			this.label8.Text = "売上明細データ PCAバージョン番号";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(432, 182);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(153, 15);
			this.label9.TabIndex = 9;
			this.label9.Text = "商品マスタ PCAバージョン番号";
			// 
			// dateTimePickerMonth
			// 
			this.dateTimePickerMonth.CustomFormat = "yyyy年MM月";
			this.dateTimePickerMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerMonth.Location = new System.Drawing.Point(158, 209);
			this.dateTimePickerMonth.Name = "dateTimePickerMonth";
			this.dateTimePickerMonth.Size = new System.Drawing.Size(121, 23);
			this.dateTimePickerMonth.TabIndex = 12;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(109, 215);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 15);
			this.label2.TabIndex = 11;
			this.label2.Text = "実行月";
			// 
			// numericTextBoxPcaVerGoods
			// 
			this.numericTextBoxPcaVerGoods.BackColor = System.Drawing.Color.White;
			this.numericTextBoxPcaVerGoods.Location = new System.Drawing.Point(591, 179);
			this.numericTextBoxPcaVerGoods.Name = "numericTextBoxPcaVerGoods";
			this.numericTextBoxPcaVerGoods.ReadOnly = true;
			this.numericTextBoxPcaVerGoods.Size = new System.Drawing.Size(51, 23);
			this.numericTextBoxPcaVerGoods.TabIndex = 10;
			// 
			// numericTextBoxPcaVerEarnings
			// 
			this.numericTextBoxPcaVerEarnings.BackColor = System.Drawing.Color.White;
			this.numericTextBoxPcaVerEarnings.Location = new System.Drawing.Point(591, 121);
			this.numericTextBoxPcaVerEarnings.Name = "numericTextBoxPcaVerEarnings";
			this.numericTextBoxPcaVerEarnings.ReadOnly = true;
			this.numericTextBoxPcaVerEarnings.Size = new System.Drawing.Size(51, 23);
			this.numericTextBoxPcaVerEarnings.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(687, 362);
			this.Controls.Add(this.dateTimePickerMonth);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericTextBoxPcaVerGoods);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.numericTextBoxPcaVerEarnings);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.buttonGoodsFile);
			this.Controls.Add(this.buttonImportFile);
			this.Controls.Add(this.textBoxGoodsPathname);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxImportPathname);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.textBoxExportFilename);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MWSサービス価格変更売上データ作成ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.TextBox textBoxExportFilename;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxImportPathname;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxGoodsPathname;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonImportFile;
		private System.Windows.Forms.Button buttonGoodsFile;
		private MwsLib.Component.NumericTextBox numericTextBoxPcaVerEarnings;
		private System.Windows.Forms.Label label8;
		private MwsLib.Component.NumericTextBox numericTextBoxPcaVerGoods;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DateTimePicker dateTimePickerMonth;
		private System.Windows.Forms.Label label2;
	}
}

