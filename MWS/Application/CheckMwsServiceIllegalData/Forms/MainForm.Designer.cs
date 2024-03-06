namespace CheckMwsServiceIllegalData.Forms
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
			this.buttonExec = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonExec
			// 
			this.buttonExec.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonExec.Location = new System.Drawing.Point(14, 15);
			this.buttonExec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(216, 115);
			this.buttonExec.TabIndex = 0;
			this.buttonExec.Text = "検出開始";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(14, 146);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(508, 49);
			this.label1.TabIndex = 1;
			this.label1.Text = "顧客利用情報と申込情報から異常データを検出して、結果をエクセルファイルで出力します。※本処理の実行ではデータベースへの書き込み処理はありません。";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(543, 204);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonExec);
			this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MWSサービス異常データ検出";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.Label label1;
	}
}

