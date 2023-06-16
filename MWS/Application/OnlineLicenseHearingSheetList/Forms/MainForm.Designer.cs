namespace OnlineLicenseHearingSheetList.Forms
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
			this.buttonHearingSheetList = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonHearingSheetList
			// 
			this.buttonHearingSheetList.Location = new System.Drawing.Point(13, 13);
			this.buttonHearingSheetList.Name = "buttonHearingSheetList";
			this.buttonHearingSheetList.Size = new System.Drawing.Size(243, 88);
			this.buttonHearingSheetList.TabIndex = 0;
			this.buttonHearingSheetList.Text = "ヒアリングシートリスト作成";
			this.buttonHearingSheetList.UseVisualStyleBackColor = true;
			this.buttonHearingSheetList.Click += new System.EventHandler(this.buttonHearingSheetList_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(446, 235);
			this.Controls.Add(this.buttonHearingSheetList);
			this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オンライン資格確認ヒアリングシートリスト作成";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonHearingSheetList;
	}
}

