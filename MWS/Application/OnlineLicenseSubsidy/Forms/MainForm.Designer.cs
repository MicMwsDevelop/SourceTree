
namespace OnlineLicenseSubsidy.Forms
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
			this.buttonExport = new System.Windows.Forms.Button();
			this.buttonWorkbook = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxYearMonth = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonInputDestinationFile = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxDestinationPathname = new System.Windows.Forms.TextBox();
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.textBoxInputFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.radioButtonWest = new System.Windows.Forms.RadioButton();
			this.radioButtonEast = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxNotPDF = new System.Windows.Forms.CheckBox();
			this.buttonInputWorkList = new System.Windows.Forms.Button();
			this.textBoxWorkList = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonExport
			// 
			this.buttonExport.Enabled = false;
			this.buttonExport.Location = new System.Drawing.Point(469, 76);
			this.buttonExport.Margin = new System.Windows.Forms.Padding(4);
			this.buttonExport.Name = "buttonExport";
			this.buttonExport.Size = new System.Drawing.Size(160, 56);
			this.buttonExport.TabIndex = 4;
			this.buttonExport.Text = "申請書類出力";
			this.buttonExport.UseVisualStyleBackColor = true;
			this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
			// 
			// buttonWorkbook
			// 
			this.buttonWorkbook.Location = new System.Drawing.Point(469, 204);
			this.buttonWorkbook.Margin = new System.Windows.Forms.Padding(4);
			this.buttonWorkbook.Name = "buttonWorkbook";
			this.buttonWorkbook.Size = new System.Drawing.Size(160, 56);
			this.buttonWorkbook.TabIndex = 9;
			this.buttonWorkbook.Text = "作業リスト出力";
			this.buttonWorkbook.UseVisualStyleBackColor = true;
			this.buttonWorkbook.Click += new System.EventHandler(this.buttonWorkbook_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "■作業対象月";
			// 
			// comboBoxYearMonth
			// 
			this.comboBoxYearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxYearMonth.FormattingEnabled = true;
			this.comboBoxYearMonth.Location = new System.Drawing.Point(112, 117);
			this.comboBoxYearMonth.Name = "comboBoxYearMonth";
			this.comboBoxYearMonth.Size = new System.Drawing.Size(277, 25);
			this.comboBoxYearMonth.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonInputDestinationFile);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxDestinationPathname);
			this.groupBox1.Controls.Add(this.buttonSelectFile);
			this.groupBox1.Controls.Add(this.textBoxInputFolder);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.radioButtonWest);
			this.groupBox1.Controls.Add(this.radioButtonEast);
			this.groupBox1.Controls.Add(this.comboBoxYearMonth);
			this.groupBox1.Controls.Add(this.buttonWorkbook);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(648, 277);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "事業完了報告書/領収書内訳書読込";
			// 
			// buttonInputDestinationFile
			// 
			this.buttonInputDestinationFile.Location = new System.Drawing.Point(601, 172);
			this.buttonInputDestinationFile.Margin = new System.Windows.Forms.Padding(4);
			this.buttonInputDestinationFile.Name = "buttonInputDestinationFile";
			this.buttonInputDestinationFile.Size = new System.Drawing.Size(28, 24);
			this.buttonInputDestinationFile.TabIndex = 8;
			this.buttonInputDestinationFile.Text = "▼";
			this.buttonInputDestinationFile.UseVisualStyleBackColor = true;
			this.buttonInputDestinationFile.Click += new System.EventHandler(this.buttonInputDestinationFile_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 151);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "■送付先リスト";
			// 
			// textBoxDestinationPathname
			// 
			this.textBoxDestinationPathname.BackColor = System.Drawing.Color.White;
			this.textBoxDestinationPathname.Location = new System.Drawing.Point(21, 172);
			this.textBoxDestinationPathname.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxDestinationPathname.Name = "textBoxDestinationPathname";
			this.textBoxDestinationPathname.ReadOnly = true;
			this.textBoxDestinationPathname.Size = new System.Drawing.Size(577, 24);
			this.textBoxDestinationPathname.TabIndex = 7;
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(689, 186);
			this.buttonSelectFile.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(10, 21);
			this.buttonSelectFile.TabIndex = 9;
			this.buttonSelectFile.Text = "▼";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			// 
			// textBoxInputFolder
			// 
			this.textBoxInputFolder.BackColor = System.Drawing.Color.White;
			this.textBoxInputFolder.Location = new System.Drawing.Point(21, 87);
			this.textBoxInputFolder.Name = "textBoxInputFolder";
			this.textBoxInputFolder.ReadOnly = true;
			this.textBoxInputFolder.Size = new System.Drawing.Size(579, 24);
			this.textBoxInputFolder.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(137, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "■補助金額資料フォルダ";
			// 
			// radioButtonWest
			// 
			this.radioButtonWest.AutoSize = true;
			this.radioButtonWest.Location = new System.Drawing.Point(121, 34);
			this.radioButtonWest.Name = "radioButtonWest";
			this.radioButtonWest.Size = new System.Drawing.Size(91, 21);
			this.radioButtonWest.TabIndex = 1;
			this.radioButtonWest.Text = "NTT西日本";
			this.radioButtonWest.UseVisualStyleBackColor = true;
			this.radioButtonWest.CheckedChanged += new System.EventHandler(this.radioButtonWest_CheckedChanged);
			// 
			// radioButtonEast
			// 
			this.radioButtonEast.AutoSize = true;
			this.radioButtonEast.Location = new System.Drawing.Point(21, 34);
			this.radioButtonEast.Name = "radioButtonEast";
			this.radioButtonEast.Size = new System.Drawing.Size(91, 21);
			this.radioButtonEast.TabIndex = 0;
			this.radioButtonEast.Text = "NTT東日本";
			this.radioButtonEast.UseVisualStyleBackColor = true;
			this.radioButtonEast.CheckedChanged += new System.EventHandler(this.radioButtonEast_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxNotPDF);
			this.groupBox2.Controls.Add(this.buttonInputWorkList);
			this.groupBox2.Controls.Add(this.textBoxWorkList);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.buttonExport);
			this.groupBox2.Location = new System.Drawing.Point(13, 300);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(648, 148);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "補助金申請書類出力";
			// 
			// checkBoxNotPDF
			// 
			this.checkBoxNotPDF.AutoSize = true;
			this.checkBoxNotPDF.Location = new System.Drawing.Point(306, 95);
			this.checkBoxNotPDF.Name = "checkBoxNotPDF";
			this.checkBoxNotPDF.Size = new System.Drawing.Size(156, 21);
			this.checkBoxNotPDF.TabIndex = 3;
			this.checkBoxNotPDF.Text = "PDFファイルを作成しない";
			this.checkBoxNotPDF.UseVisualStyleBackColor = true;
			this.checkBoxNotPDF.Visible = false;
			// 
			// buttonInputWorkList
			// 
			this.buttonInputWorkList.Location = new System.Drawing.Point(601, 45);
			this.buttonInputWorkList.Name = "buttonInputWorkList";
			this.buttonInputWorkList.Size = new System.Drawing.Size(28, 24);
			this.buttonInputWorkList.TabIndex = 2;
			this.buttonInputWorkList.Text = "▼";
			this.buttonInputWorkList.UseVisualStyleBackColor = true;
			this.buttonInputWorkList.Click += new System.EventHandler(this.buttonInputWorkList_Click);
			// 
			// textBoxWorkList
			// 
			this.textBoxWorkList.BackColor = System.Drawing.Color.White;
			this.textBoxWorkList.Location = new System.Drawing.Point(21, 45);
			this.textBoxWorkList.Name = "textBoxWorkList";
			this.textBoxWorkList.ReadOnly = true;
			this.textBoxWorkList.Size = new System.Drawing.Size(577, 24);
			this.textBoxWorkList.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "■作業リストファイル";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(687, 463);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オン資補助金申請書類出力";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonExport;
		private System.Windows.Forms.Button buttonWorkbook;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxYearMonth;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonInputWorkList;
		private System.Windows.Forms.TextBox textBoxWorkList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radioButtonWest;
		private System.Windows.Forms.RadioButton radioButtonEast;
		private System.Windows.Forms.CheckBox checkBoxNotPDF;
		private System.Windows.Forms.TextBox textBoxInputFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonInputDestinationFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxDestinationPathname;
		private System.Windows.Forms.Button buttonSelectFile;
	}
}

