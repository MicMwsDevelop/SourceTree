namespace HardSubscNotifyMail.Forms
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
			this.buttonNotifyLimitMail = new System.Windows.Forms.Button();
			this.buttonNotifyFinishedMail = new System.Windows.Forms.Button();
			this.labelVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonNotifyLimitMail
			// 
			this.buttonNotifyLimitMail.Font = new System.Drawing.Font("BIZ UDゴシック", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonNotifyLimitMail.Location = new System.Drawing.Point(12, 25);
			this.buttonNotifyLimitMail.Name = "buttonNotifyLimitMail";
			this.buttonNotifyLimitMail.Size = new System.Drawing.Size(455, 75);
			this.buttonNotifyLimitMail.TabIndex = 1;
			this.buttonNotifyLimitMail.Text = "利用期限通知";
			this.buttonNotifyLimitMail.UseVisualStyleBackColor = true;
			this.buttonNotifyLimitMail.Click += new System.EventHandler(this.buttonNotifyLimitMail_Click);
			// 
			// buttonNotifyFinishedMail
			// 
			this.buttonNotifyFinishedMail.Font = new System.Drawing.Font("BIZ UDゴシック", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonNotifyFinishedMail.Location = new System.Drawing.Point(12, 106);
			this.buttonNotifyFinishedMail.Name = "buttonNotifyFinishedMail";
			this.buttonNotifyFinishedMail.Size = new System.Drawing.Size(455, 75);
			this.buttonNotifyFinishedMail.TabIndex = 2;
			this.buttonNotifyFinishedMail.Text = "利用終了通知";
			this.buttonNotifyFinishedMail.UseVisualStyleBackColor = true;
			this.buttonNotifyFinishedMail.Click += new System.EventHandler(this.buttonNotifyFinishedMail_Click);
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(334, 9);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(133, 13);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Ver1.00 2025/10/28";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(483, 195);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.buttonNotifyFinishedMail);
			this.Controls.Add(this.buttonNotifyLimitMail);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ハードサブスク通知";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonNotifyLimitMail;
		private System.Windows.Forms.Button buttonNotifyFinishedMail;
		private System.Windows.Forms.Label labelVersion;
	}
}

