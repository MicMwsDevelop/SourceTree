namespace CloudDataBankStockDataOutput
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
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonExec = new System.Windows.Forms.Button();
			this.textBoxPcaVer = new MwsLib.Component.NumericTextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 48);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(165, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "仕入データSVファイルC名";
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.Location = new System.Drawing.Point(189, 45);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.Size = new System.Drawing.Size(473, 27);
			this.textBoxFilename.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 15);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "出力先フォルダ";
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Location = new System.Drawing.Point(189, 12);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(473, 27);
			this.textBoxFolder.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(17, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(645, 78);
			this.label3.TabIndex = 6;
			this.label3.Text = "本アプリはPCA売上データからクラウドデータバンクを抽出し、仕入データのCSVファイルを出力します。\r\n※実行後、データベースのレコードに対し変更は行いません。\r" +
    "\n   サイレイトモードでの実行は引数に \"Auto\" を指定してください。";
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(513, 171);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(149, 49);
			this.buttonExec.TabIndex = 7;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// textBoxPcaVer
			// 
			this.textBoxPcaVer.Location = new System.Drawing.Point(189, 78);
			this.textBoxPcaVer.MaxLength = 2;
			this.textBoxPcaVer.Name = "textBoxPcaVer";
			this.textBoxPcaVer.Size = new System.Drawing.Size(45, 27);
			this.textBoxPcaVer.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 81);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(126, 20);
			this.label4.TabIndex = 4;
			this.label4.Text = "PCAバージョン情報";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(690, 234);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxPcaVer);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxFilename);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "クラウドデータバンク仕入データ出力（デバッグ実行画面）";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonExec;
		private MwsLib.Component.NumericTextBox textBoxPcaVer;
		private System.Windows.Forms.Label label4;
	}
}

