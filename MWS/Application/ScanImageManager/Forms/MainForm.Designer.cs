namespace ScanImageManager.Forms
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
			this.buttonInputImagePath = new System.Windows.Forms.Button();
			this.textBoxImagePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonMakeIndexFile = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonRemakeScanData
			// 
			this.buttonRemakeScanData.Location = new System.Drawing.Point(12, 141);
			this.buttonRemakeScanData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonRemakeScanData.Name = "buttonRemakeScanData";
			this.buttonRemakeScanData.Size = new System.Drawing.Size(612, 52);
			this.buttonRemakeScanData.TabIndex = 4;
			this.buttonRemakeScanData.Text = "スキャﾅｰｲﾒｰｼﾞ登録情報再作成";
			this.buttonRemakeScanData.UseVisualStyleBackColor = true;
			this.buttonRemakeScanData.Click += new System.EventHandler(this.buttonRemakeScanData_Click);
			// 
			// buttonInputImagePath
			// 
			this.buttonInputImagePath.Location = new System.Drawing.Point(589, 110);
			this.buttonInputImagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonInputImagePath.Name = "buttonInputImagePath";
			this.buttonInputImagePath.Size = new System.Drawing.Size(35, 29);
			this.buttonInputImagePath.TabIndex = 3;
			this.buttonInputImagePath.Text = "...";
			this.buttonInputImagePath.UseVisualStyleBackColor = true;
			this.buttonInputImagePath.Click += new System.EventHandler(this.buttonInputImagePath_Click);
			// 
			// textBoxImagePath
			// 
			this.textBoxImagePath.BackColor = System.Drawing.Color.White;
			this.textBoxImagePath.Location = new System.Drawing.Point(12, 110);
			this.textBoxImagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxImagePath.Name = "textBoxImagePath";
			this.textBoxImagePath.ReadOnly = true;
			this.textBoxImagePath.Size = new System.Drawing.Size(571, 23);
			this.textBoxImagePath.TabIndex = 2;
			this.textBoxImagePath.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 91);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "■スキャﾅｰｲﾒｰｼﾞ登録パス";
			// 
			// buttonMakeIndexFile
			// 
			this.buttonMakeIndexFile.Location = new System.Drawing.Point(12, 28);
			this.buttonMakeIndexFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonMakeIndexFile.Name = "buttonMakeIndexFile";
			this.buttonMakeIndexFile.Size = new System.Drawing.Size(612, 52);
			this.buttonMakeIndexFile.TabIndex = 0;
			this.buttonMakeIndexFile.Text = "インデックスファイル作成";
			this.buttonMakeIndexFile.UseVisualStyleBackColor = true;
			this.buttonMakeIndexFile.Click += new System.EventHandler(this.buttonMakeIndexFile_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(472, 216);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Ver.1.00 2021/07/07版";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 251);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonMakeIndexFile);
			this.Controls.Add(this.buttonInputImagePath);
			this.Controls.Add(this.textBoxImagePath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonRemakeScanData);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "文書インデックス管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonRemakeScanData;
		private System.Windows.Forms.Button buttonInputImagePath;
		private System.Windows.Forms.TextBox textBoxImagePath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonMakeIndexFile;
		private System.Windows.Forms.Label label2;
	}
}

