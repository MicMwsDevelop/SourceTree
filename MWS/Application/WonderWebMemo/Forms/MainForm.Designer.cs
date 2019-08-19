namespace WonderWebMemo.Forms
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
			this.buttonBankTransfer = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonBankTransfer
			// 
			this.buttonBankTransfer.Location = new System.Drawing.Point(12, 12);
			this.buttonBankTransfer.Name = "buttonBankTransfer";
			this.buttonBankTransfer.Size = new System.Drawing.Size(283, 68);
			this.buttonBankTransfer.TabIndex = 0;
			this.buttonBankTransfer.Text = "銀行振込請求書発行先";
			this.buttonBankTransfer.UseVisualStyleBackColor = true;
			this.buttonBankTransfer.Click += new System.EventHandler(this.buttonBankTransfer_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(310, 199);
			this.Controls.Add(this.buttonBankTransfer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WonderWebメモ追加ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonBankTransfer;
	}
}

