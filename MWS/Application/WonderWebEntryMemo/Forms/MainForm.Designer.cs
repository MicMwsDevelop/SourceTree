﻿
namespace WonderWebEntryMemo.Forms
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
			this.labelVersion = new System.Windows.Forms.Label();
			this.buttonBank = new System.Windows.Forms.Button();
			this.buttonOnline = new System.Windows.Forms.Button();
			this.buttonWelfare = new System.Windows.Forms.Button();
			this.buttonImportFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelVersion
			// 
			this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(197, 209);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(138, 17);
			this.labelVersion.TabIndex = 4;
			this.labelVersion.Text = "Ver1.00 2022/03/10";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonBank
			// 
			this.buttonBank.Location = new System.Drawing.Point(24, 20);
			this.buttonBank.Name = "buttonBank";
			this.buttonBank.Size = new System.Drawing.Size(299, 37);
			this.buttonBank.TabIndex = 0;
			this.buttonBank.Text = "銀行振込請求書発行メモ追加";
			this.buttonBank.UseVisualStyleBackColor = true;
			this.buttonBank.Click += new System.EventHandler(this.buttonBank_Click);
			// 
			// buttonOnline
			// 
			this.buttonOnline.Location = new System.Drawing.Point(24, 63);
			this.buttonOnline.Name = "buttonOnline";
			this.buttonOnline.Size = new System.Drawing.Size(299, 37);
			this.buttonOnline.TabIndex = 1;
			this.buttonOnline.Text = "オン資格補助金申請書類メモ追加";
			this.buttonOnline.UseVisualStyleBackColor = true;
			this.buttonOnline.Click += new System.EventHandler(this.buttonOnline_Click);
			// 
			// buttonWelfare
			// 
			this.buttonWelfare.Location = new System.Drawing.Point(24, 106);
			this.buttonWelfare.Name = "buttonWelfare";
			this.buttonWelfare.Size = new System.Drawing.Size(299, 37);
			this.buttonWelfare.TabIndex = 2;
			this.buttonWelfare.Text = "厚生局データメモ追加";
			this.buttonWelfare.UseVisualStyleBackColor = true;
			this.buttonWelfare.Click += new System.EventHandler(this.buttonWelfare_Click);
			// 
			// buttonImportFile
			// 
			this.buttonImportFile.Location = new System.Drawing.Point(24, 149);
			this.buttonImportFile.Name = "buttonImportFile";
			this.buttonImportFile.Size = new System.Drawing.Size(299, 37);
			this.buttonImportFile.TabIndex = 3;
			this.buttonImportFile.Text = "CSVファイルによるメモ追加";
			this.buttonImportFile.UseVisualStyleBackColor = true;
			this.buttonImportFile.Click += new System.EventHandler(this.buttonImportFile_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 235);
			this.Controls.Add(this.buttonImportFile);
			this.Controls.Add(this.buttonWelfare);
			this.Controls.Add(this.buttonOnline);
			this.Controls.Add(this.buttonBank);
			this.Controls.Add(this.labelVersion);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WonderWebメモ追加";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Button buttonBank;
		private System.Windows.Forms.Button buttonOnline;
		private System.Windows.Forms.Button buttonWelfare;
		private System.Windows.Forms.Button buttonImportFile;
	}
}

