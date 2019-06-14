namespace ScanImageData.Forms
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
			this.buttonRemakeScanData = new System.Windows.Forms.Button();
			this.buttonInputPath = new System.Windows.Forms.Button();
			this.textBoxScanImageDataPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonMakeIndexFile = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonRemakeScanData
			// 
			this.buttonRemakeScanData.Location = new System.Drawing.Point(15, 362);
			this.buttonRemakeScanData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonRemakeScanData.Name = "buttonRemakeScanData";
			this.buttonRemakeScanData.Size = new System.Drawing.Size(612, 52);
			this.buttonRemakeScanData.TabIndex = 2;
			this.buttonRemakeScanData.Text = "スキャﾅｰｲﾒｰｼﾞ登録情報再作成";
			this.buttonRemakeScanData.UseVisualStyleBackColor = true;
			this.buttonRemakeScanData.Click += new System.EventHandler(this.buttonRemakeScanData_Click);
			// 
			// buttonInputPath
			// 
			this.buttonInputPath.Location = new System.Drawing.Point(592, 331);
			this.buttonInputPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonInputPath.Name = "buttonInputPath";
			this.buttonInputPath.Size = new System.Drawing.Size(35, 29);
			this.buttonInputPath.TabIndex = 7;
			this.buttonInputPath.Text = "...";
			this.buttonInputPath.UseVisualStyleBackColor = true;
			this.buttonInputPath.Click += new System.EventHandler(this.buttonInputPath_Click);
			// 
			// textBoxScanImageDataPath
			// 
			this.textBoxScanImageDataPath.BackColor = System.Drawing.Color.White;
			this.textBoxScanImageDataPath.Location = new System.Drawing.Point(15, 331);
			this.textBoxScanImageDataPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxScanImageDataPath.Name = "textBoxScanImageDataPath";
			this.textBoxScanImageDataPath.ReadOnly = true;
			this.textBoxScanImageDataPath.Size = new System.Drawing.Size(571, 23);
			this.textBoxScanImageDataPath.TabIndex = 6;
			this.textBoxScanImageDataPath.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 312);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 15);
			this.label1.TabIndex = 5;
			this.label1.Text = "■スキャﾅｰｲﾒｰｼﾞ登録パス";
			// 
			// buttonMakeIndexFile
			// 
			this.buttonMakeIndexFile.Location = new System.Drawing.Point(12, 28);
			this.buttonMakeIndexFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonMakeIndexFile.Name = "buttonMakeIndexFile";
			this.buttonMakeIndexFile.Size = new System.Drawing.Size(612, 52);
			this.buttonMakeIndexFile.TabIndex = 8;
			this.buttonMakeIndexFile.Text = "インデックスファイル作成";
			this.buttonMakeIndexFile.UseVisualStyleBackColor = true;
			this.buttonMakeIndexFile.Click += new System.EventHandler(this.buttonMakeIndexFile_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(480, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 15);
			this.label2.TabIndex = 9;
			this.label2.Text = "Ver.1.00 2010/09/14版";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 88);
			this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(612, 52);
			this.button1.TabIndex = 10;
			this.button1.Text = "スキャナーイメージ参照";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 430);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonMakeIndexFile);
			this.Controls.Add(this.buttonInputPath);
			this.Controls.Add(this.textBoxScanImageDataPath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonRemakeScanData);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "文書インデックス管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonRemakeScanData;
		private System.Windows.Forms.Button buttonInputPath;
		private System.Windows.Forms.TextBox textBoxScanImageDataPath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonMakeIndexFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
	}
}

