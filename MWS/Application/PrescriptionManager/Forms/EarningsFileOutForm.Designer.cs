namespace PrescriptionManager.Forms
{
	partial class EarningsFileOutForm
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
			this.dateTimePickerMonth = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxPcaVer = new MwsLib.Component.NumericTextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxExportFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonExec = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// dateTimePickerMonth
			// 
			this.dateTimePickerMonth.CustomFormat = "yyyy年MM月";
			this.dateTimePickerMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerMonth.Location = new System.Drawing.Point(117, 30);
			this.dateTimePickerMonth.Name = "dateTimePickerMonth";
			this.dateTimePickerMonth.Size = new System.Drawing.Size(101, 24);
			this.dateTimePickerMonth.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(25, 35);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 17);
			this.label5.TabIndex = 0;
			this.label5.Text = "実行月";
			// 
			// textBoxPcaVer
			// 
			this.textBoxPcaVer.Location = new System.Drawing.Point(198, 90);
			this.textBoxPcaVer.MaxLength = 2;
			this.textBoxPcaVer.Name = "textBoxPcaVer";
			this.textBoxPcaVer.Size = new System.Drawing.Size(52, 24);
			this.textBoxPcaVer.TabIndex = 5;
			this.textBoxPcaVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(25, 93);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(166, 17);
			this.label4.TabIndex = 4;
			this.label4.Text = "売上明細 PCAバージョン番号";
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(25, 132);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(565, 47);
			this.label3.TabIndex = 6;
			this.label3.Text = "本アプリは電子処方箋契約情報から運用開始日の更新、PCA売上明細データを作成して出力します。※サイレイトモードでの実行は引数に \"Auto\" を指定してください。" +
    "";
			// 
			// textBoxExportFolder
			// 
			this.textBoxExportFolder.Location = new System.Drawing.Point(117, 60);
			this.textBoxExportFolder.Name = "textBoxExportFolder";
			this.textBoxExportFolder.Size = new System.Drawing.Size(473, 24);
			this.textBoxExportFolder.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 63);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "出力先フォルダ";
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(441, 182);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(149, 49);
			this.buttonExec.TabIndex = 7;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// EarningsFileOutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(603, 246);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.dateTimePickerMonth);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxPcaVer);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxExportFolder);
			this.Controls.Add(this.label2);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EarningsFileOutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "電子処方箋管理サービス 売上明細ファイル出力";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dateTimePickerMonth;
		private System.Windows.Forms.Label label5;
		private MwsLib.Component.NumericTextBox textBoxPcaVer;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxExportFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonExec;
	}
}

