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
			this.buttonCheckAbnormalData = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonCheckIllegalCuiServiceTerm = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonCheckAbnormalData
			// 
			this.buttonCheckAbnormalData.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonCheckAbnormalData.Location = new System.Drawing.Point(14, 15);
			this.buttonCheckAbnormalData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCheckAbnormalData.Name = "buttonCheckAbnormalData";
			this.buttonCheckAbnormalData.Size = new System.Drawing.Size(216, 115);
			this.buttonCheckAbnormalData.TabIndex = 0;
			this.buttonCheckAbnormalData.Text = "MWSサービス異常データ出力";
			this.buttonCheckAbnormalData.UseVisualStyleBackColor = true;
			this.buttonCheckAbnormalData.Click += new System.EventHandler(this.buttonCheckAbnormalData_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(14, 146);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(508, 49);
			this.label1.TabIndex = 2;
			this.label1.Text = "顧客利用情報と申込情報から異常データを検出して、結果をエクセルファイルで出力します。※本処理の実行ではデータベースへの書き込み処理はありません。";
			// 
			// buttonCheckIllegalCuiServiceTerm
			// 
			this.buttonCheckIllegalCuiServiceTerm.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonCheckIllegalCuiServiceTerm.Location = new System.Drawing.Point(306, 15);
			this.buttonCheckIllegalCuiServiceTerm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCheckIllegalCuiServiceTerm.Name = "buttonCheckIllegalCuiServiceTerm";
			this.buttonCheckIllegalCuiServiceTerm.Size = new System.Drawing.Size(216, 115);
			this.buttonCheckIllegalCuiServiceTerm.TabIndex = 1;
			this.buttonCheckIllegalCuiServiceTerm.Text = "受注伝票サービス利用期間不具合検出";
			this.buttonCheckIllegalCuiServiceTerm.UseVisualStyleBackColor = true;
			this.buttonCheckIllegalCuiServiceTerm.Click += new System.EventHandler(this.buttonCheckIllegalCuiServiceTerm_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(543, 204);
			this.Controls.Add(this.buttonCheckIllegalCuiServiceTerm);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCheckAbnormalData);
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

		private System.Windows.Forms.Button buttonCheckAbnormalData;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCheckIllegalCuiServiceTerm;
	}
}

