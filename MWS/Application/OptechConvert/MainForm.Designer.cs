
namespace OptechConvert
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
			this.buttonReadOptech = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonImportFile = new System.Windows.Forms.Button();
			this.textBoxClinicCode = new MwsLib.Component.NumericTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxOptechFolder = new System.Windows.Forms.TextBox();
			this.listBoxPatient = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBoxExceptRezeptCheck = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// buttonReadOptech
			// 
			this.buttonReadOptech.Location = new System.Drawing.Point(349, 28);
			this.buttonReadOptech.Name = "buttonReadOptech";
			this.buttonReadOptech.Size = new System.Drawing.Size(118, 34);
			this.buttonReadOptech.TabIndex = 4;
			this.buttonReadOptech.Text = "読込";
			this.buttonReadOptech.UseVisualStyleBackColor = true;
			this.buttonReadOptech.Click += new System.EventHandler(this.buttonReadOptech_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■医療機関コード";
			// 
			// buttonImportFile
			// 
			this.buttonImportFile.Location = new System.Drawing.Point(349, 93);
			this.buttonImportFile.Name = "buttonImportFile";
			this.buttonImportFile.Size = new System.Drawing.Size(118, 34);
			this.buttonImportFile.TabIndex = 8;
			this.buttonImportFile.Text = "インポートファイル出力";
			this.buttonImportFile.UseVisualStyleBackColor = true;
			this.buttonImportFile.Click += new System.EventHandler(this.buttonImportFile_Click);
			// 
			// textBoxClinicCode
			// 
			this.textBoxClinicCode.Location = new System.Drawing.Point(111, 17);
			this.textBoxClinicCode.MaxLength = 7;
			this.textBoxClinicCode.Name = "textBoxClinicCode";
			this.textBoxClinicCode.Size = new System.Drawing.Size(100, 19);
			this.textBoxClinicCode.TabIndex = 1;
			this.textBoxClinicCode.Text = "1234567";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(167, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "■オプテックコンバートデータフォルダ";
			// 
			// textBoxOptechFolder
			// 
			this.textBoxOptechFolder.BackColor = System.Drawing.Color.White;
			this.textBoxOptechFolder.Location = new System.Drawing.Point(15, 65);
			this.textBoxOptechFolder.Name = "textBoxOptechFolder";
			this.textBoxOptechFolder.ReadOnly = true;
			this.textBoxOptechFolder.Size = new System.Drawing.Size(452, 19);
			this.textBoxOptechFolder.TabIndex = 3;
			this.textBoxOptechFolder.Text = "D:\\SourceTree\\MWS\\Application\\OptechConvert\\Doc\\Optechコンバート\\OptechXML";
			// 
			// listBoxPatient
			// 
			this.listBoxPatient.FormattingEnabled = true;
			this.listBoxPatient.ItemHeight = 12;
			this.listBoxPatient.Location = new System.Drawing.Point(15, 133);
			this.listBoxPatient.Name = "listBoxPatient";
			this.listBoxPatient.Size = new System.Drawing.Size(452, 532);
			this.listBoxPatient.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 115);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "■患者フォルダリスト";
			// 
			// checkBoxExceptRezeptCheck
			// 
			this.checkBoxExceptRezeptCheck.AutoSize = true;
			this.checkBoxExceptRezeptCheck.Location = new System.Drawing.Point(173, 103);
			this.checkBoxExceptRezeptCheck.Name = "checkBoxExceptRezeptCheck";
			this.checkBoxExceptRezeptCheck.Size = new System.Drawing.Size(152, 16);
			this.checkBoxExceptRezeptCheck.TabIndex = 7;
			this.checkBoxExceptRezeptCheck.Text = "レセプトチェックデータを除外";
			this.checkBoxExceptRezeptCheck.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 680);
			this.Controls.Add(this.checkBoxExceptRezeptCheck);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listBoxPatient);
			this.Controls.Add(this.textBoxOptechFolder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonImportFile);
			this.Controls.Add(this.textBoxClinicCode);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonReadOptech);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MainForm";
			this.Text = "オプテックカルテコンバータ";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonReadOptech;
		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox textBoxClinicCode;
		private System.Windows.Forms.Button buttonImportFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxOptechFolder;
		private System.Windows.Forms.ListBox listBoxPatient;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxExceptRezeptCheck;
	}
}

