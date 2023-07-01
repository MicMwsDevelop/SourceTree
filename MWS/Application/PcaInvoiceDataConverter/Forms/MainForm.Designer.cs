namespace PcaInvoiceDataConverter.Forms
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
			this.buttonAccountTransfer = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonBankTransfer = new System.Windows.Forms.Button();
			this.buttonSaveResultSheet = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.labelVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonAccountTransfer
			// 
			this.buttonAccountTransfer.Location = new System.Drawing.Point(12, 77);
			this.buttonAccountTransfer.Name = "buttonAccountTransfer";
			this.buttonAccountTransfer.Size = new System.Drawing.Size(368, 57);
			this.buttonAccountTransfer.TabIndex = 0;
			this.buttonAccountTransfer.Text = "口座振替データ作成";
			this.buttonAccountTransfer.UseVisualStyleBackColor = true;
			this.buttonAccountTransfer.Click += new System.EventHandler(this.buttonAccountTransfer_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(60, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 30);
			this.label1.TabIndex = 1;
			this.label1.Text = "PCA請求データコンバータ";
			// 
			// buttonBankTransfer
			// 
			this.buttonBankTransfer.Location = new System.Drawing.Point(12, 140);
			this.buttonBankTransfer.Name = "buttonBankTransfer";
			this.buttonBankTransfer.Size = new System.Drawing.Size(368, 57);
			this.buttonBankTransfer.TabIndex = 2;
			this.buttonBankTransfer.Text = "銀行振込請求書データ作成";
			this.buttonBankTransfer.UseVisualStyleBackColor = true;
			this.buttonBankTransfer.Click += new System.EventHandler(this.buttonBankTransfer_Click);
			// 
			// buttonSaveResultSheet
			// 
			this.buttonSaveResultSheet.Location = new System.Drawing.Point(12, 203);
			this.buttonSaveResultSheet.Name = "buttonSaveResultSheet";
			this.buttonSaveResultSheet.Size = new System.Drawing.Size(368, 57);
			this.buttonSaveResultSheet.TabIndex = 3;
			this.buttonSaveResultSheet.Text = "結果データシート保存";
			this.buttonSaveResultSheet.UseVisualStyleBackColor = true;
			this.buttonSaveResultSheet.Click += new System.EventHandler(this.buttonSaveResultSheet_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(12, 266);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(368, 57);
			this.buttonExit.TabIndex = 4;
			this.buttonExit.Text = "終了";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// labelVersion
			// 
			this.labelVersion.Location = new System.Drawing.Point(216, 59);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(164, 15);
			this.labelVersion.TabIndex = 5;
			this.labelVersion.Text = "label2";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 337);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonSaveResultSheet);
			this.Controls.Add(this.buttonBankTransfer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonAccountTransfer);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PCA請求データコンバータ";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonAccountTransfer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonBankTransfer;
		private System.Windows.Forms.Button buttonSaveResultSheet;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label labelVersion;
	}
}

