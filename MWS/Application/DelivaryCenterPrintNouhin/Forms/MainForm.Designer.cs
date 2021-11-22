namespace DeliveryCenterPrintNouhin.Forms
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
			this.buttonPrint = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(82, 84);
			this.buttonPrint.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(189, 46);
			this.buttonPrint.TabIndex = 2;
			this.buttonPrint.Text = "納品書印刷";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(281, 84);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(189, 46);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "終了";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(250, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "印刷状況";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 33);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(529, 34);
			this.progressBar.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(553, 150);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonPrint);
			this.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Mic 納品書印刷";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ProgressBar progressBar;
	}
}

