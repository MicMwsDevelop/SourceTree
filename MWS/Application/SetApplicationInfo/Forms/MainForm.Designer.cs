namespace SetApplicationInfo.Forms
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
			this.textBoxInputFile = new System.Windows.Forms.TextBox();
			this.buttonInputFile = new System.Windows.Forms.Button();
			this.buttonExec = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "■アプリケーション情報設定ファイルの指定";
			// 
			// textBoxInputFile
			// 
			this.textBoxInputFile.BackColor = System.Drawing.Color.White;
			this.textBoxInputFile.Location = new System.Drawing.Point(16, 32);
			this.textBoxInputFile.Name = "textBoxInputFile";
			this.textBoxInputFile.ReadOnly = true;
			this.textBoxInputFile.Size = new System.Drawing.Size(509, 23);
			this.textBoxInputFile.TabIndex = 1;
			// 
			// buttonInputFile
			// 
			this.buttonInputFile.Location = new System.Drawing.Point(525, 31);
			this.buttonInputFile.Name = "buttonInputFile";
			this.buttonInputFile.Size = new System.Drawing.Size(28, 23);
			this.buttonInputFile.TabIndex = 2;
			this.buttonInputFile.Text = "▼";
			this.buttonInputFile.UseVisualStyleBackColor = true;
			this.buttonInputFile.Click += new System.EventHandler(this.buttonInputFile_Click);
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(443, 72);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(110, 43);
			this.buttonExec.TabIndex = 3;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 130);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.buttonInputFile);
			this.Controls.Add(this.textBoxInputFile);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "アプリケーション情報の設定";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxInputFile;
		private System.Windows.Forms.Button buttonInputFile;
		private System.Windows.Forms.Button buttonExec;
	}
}

