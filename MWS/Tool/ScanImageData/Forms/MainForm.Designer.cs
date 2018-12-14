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
			this.SuspendLayout();
			// 
			// buttonRemakeScanData
			// 
			this.buttonRemakeScanData.Location = new System.Drawing.Point(12, 37);
			this.buttonRemakeScanData.Name = "buttonRemakeScanData";
			this.buttonRemakeScanData.Size = new System.Drawing.Size(379, 42);
			this.buttonRemakeScanData.TabIndex = 2;
			this.buttonRemakeScanData.Text = "スキャンデータ登録情報再作成";
			this.buttonRemakeScanData.UseVisualStyleBackColor = true;
			this.buttonRemakeScanData.Click += new System.EventHandler(this.buttonRemakeScanData_Click);
			// 
			// buttonInputPath
			// 
			this.buttonInputPath.Location = new System.Drawing.Point(672, 8);
			this.buttonInputPath.Name = "buttonInputPath";
			this.buttonInputPath.Size = new System.Drawing.Size(30, 23);
			this.buttonInputPath.TabIndex = 7;
			this.buttonInputPath.Text = "...";
			this.buttonInputPath.UseVisualStyleBackColor = true;
			this.buttonInputPath.Click += new System.EventHandler(this.buttonInputPath_Click);
			// 
			// textBoxScanImageDataPath
			// 
			this.textBoxScanImageDataPath.BackColor = System.Drawing.Color.White;
			this.textBoxScanImageDataPath.Location = new System.Drawing.Point(141, 12);
			this.textBoxScanImageDataPath.Name = "textBoxScanImageDataPath";
			this.textBoxScanImageDataPath.ReadOnly = true;
			this.textBoxScanImageDataPath.Size = new System.Drawing.Size(525, 19);
			this.textBoxScanImageDataPath.TabIndex = 6;
			this.textBoxScanImageDataPath.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "■スキャンデータ登録パス";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(811, 416);
			this.Controls.Add(this.buttonInputPath);
			this.Controls.Add(this.textBoxScanImageDataPath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonRemakeScanData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
	}
}

