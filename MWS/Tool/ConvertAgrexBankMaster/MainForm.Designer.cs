namespace ConvertAgrexBankMaster
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
			this.textBoxPathname = new System.Windows.Forms.TextBox();
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.buttonExec = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■AGREX銀行マスターファイル";
			// 
			// textBoxPathname
			// 
			this.textBoxPathname.BackColor = System.Drawing.Color.White;
			this.textBoxPathname.Location = new System.Drawing.Point(15, 29);
			this.textBoxPathname.Name = "textBoxPathname";
			this.textBoxPathname.ReadOnly = true;
			this.textBoxPathname.Size = new System.Drawing.Size(452, 19);
			this.textBoxPathname.TabIndex = 1;
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(467, 28);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(26, 23);
			this.buttonSelectFile.TabIndex = 2;
			this.buttonSelectFile.Text = "▼";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(376, 58);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(115, 38);
			this.buttonExec.TabIndex = 3;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(15, 110);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(476, 57);
			this.label2.TabIndex = 4;
			this.label2.Text = "【処理内容】\r\n1. AGREX銀行マスタの読込\r\n2. 第２フィールドの銀行名から半角スペースを排除\r\n3. コンバートしたAGREX銀行マスタを \"NewAg" +
    "rexBank.txt\" で出力\r\n";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(505, 172);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.buttonSelectFile);
			this.Controls.Add(this.textBoxPathname);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("HGS創英角ｺﾞｼｯｸUB", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AGREX銀行マスタコンバーター";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxPathname;
		private System.Windows.Forms.Button buttonSelectFile;
		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.Label label2;
	}
}

