namespace PrintNouhin
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
			this.labelFileFolder = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(72, 95);
			this.buttonPrint.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(189, 46);
			this.buttonPrint.TabIndex = 0;
			this.buttonPrint.Text = "納品書印刷";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(271, 95);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(189, 46);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "終了";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// labelFileFolder
			// 
			this.labelFileFolder.BackColor = System.Drawing.Color.White;
			this.labelFileFolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelFileFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelFileFolder.Location = new System.Drawing.Point(30, 32);
			this.labelFileFolder.Name = "labelFileFolder";
			this.labelFileFolder.Size = new System.Drawing.Size(430, 36);
			this.labelFileFolder.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 164);
			this.Controls.Add(this.labelFileFolder);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonPrint);
			this.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "MainForm";
			this.Text = "Mic 納品書印刷";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label labelFileFolder;
	}
}

