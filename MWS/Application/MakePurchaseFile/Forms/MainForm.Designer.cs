
namespace MakePurchaseFile.Forms
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
			this.label1 = new System.Windows.Forms.Label();
			this.dateTimePickerTarget = new System.Windows.Forms.DateTimePicker();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.labelversion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
			this.textBoxListonFilename = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxMicrosoft365Filename = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxMonshindenFilename = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxCurlineFilename = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxNarcohmFilename = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxCloudBackupFilename = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxAlmexFilename = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPcaVersion = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 25);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "対象年月";
			// 
			// dateTimePickerTarget
			// 
			this.dateTimePickerTarget.CustomFormat = "yyyy年MM月";
			this.dateTimePickerTarget.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerTarget.Location = new System.Drawing.Point(96, 21);
			this.dateTimePickerTarget.Name = "dateTimePickerTarget";
			this.dateTimePickerTarget.Size = new System.Drawing.Size(160, 27);
			this.dateTimePickerTarget.TabIndex = 1;
			this.dateTimePickerTarget.DropDown += new System.EventHandler(this.dateTimePickerTarget_DropDown);
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(500, 68);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(232, 107);
			this.buttonStart.TabIndex = 20;
			this.buttonStart.Text = "START";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(610, 434);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(122, 49);
			this.buttonExit.TabIndex = 23;
			this.buttonExit.Text = "終了";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// labelversion
			// 
			this.labelversion.Location = new System.Drawing.Point(473, 21);
			this.labelversion.Name = "labelversion";
			this.labelversion.Size = new System.Drawing.Size(259, 26);
			this.labelversion.TabIndex = 2;
			this.labelversion.Text = "Ver1.00(2022/02/18)";
			this.labelversion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 19);
			this.label3.TabIndex = 3;
			this.label3.Text = "出力先フォルダ";
			// 
			// textBoxOutputFolder
			// 
			this.textBoxOutputFolder.BackColor = System.Drawing.Color.White;
			this.textBoxOutputFolder.Location = new System.Drawing.Point(124, 68);
			this.textBoxOutputFolder.Name = "textBoxOutputFolder";
			this.textBoxOutputFolder.ReadOnly = true;
			this.textBoxOutputFolder.Size = new System.Drawing.Size(348, 27);
			this.textBoxOutputFolder.TabIndex = 4;
			// 
			// textBoxListonFilename
			// 
			this.textBoxListonFilename.BackColor = System.Drawing.Color.White;
			this.textBoxListonFilename.Location = new System.Drawing.Point(21, 122);
			this.textBoxListonFilename.Name = "textBoxListonFilename";
			this.textBoxListonFilename.ReadOnly = true;
			this.textBoxListonFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxListonFilename.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(203, 19);
			this.label4.TabIndex = 5;
			this.label4.Text = "りすとん月額仕入データファイル名";
			// 
			// textBoxMicrosoft365Filename
			// 
			this.textBoxMicrosoft365Filename.BackColor = System.Drawing.Color.White;
			this.textBoxMicrosoft365Filename.Location = new System.Drawing.Point(21, 174);
			this.textBoxMicrosoft365Filename.Name = "textBoxMicrosoft365Filename";
			this.textBoxMicrosoft365Filename.ReadOnly = true;
			this.textBoxMicrosoft365Filename.Size = new System.Drawing.Size(447, 27);
			this.textBoxMicrosoft365Filename.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(21, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(223, 19);
			this.label5.TabIndex = 7;
			this.label5.Text = "Microsoft365仕入データファイル名";
			// 
			// textBoxMonshindenFilename
			// 
			this.textBoxMonshindenFilename.BackColor = System.Drawing.Color.White;
			this.textBoxMonshindenFilename.Location = new System.Drawing.Point(21, 226);
			this.textBoxMonshindenFilename.Name = "textBoxMonshindenFilename";
			this.textBoxMonshindenFilename.ReadOnly = true;
			this.textBoxMonshindenFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxMonshindenFilename.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(21, 204);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(205, 19);
			this.label6.TabIndex = 9;
			this.label6.Text = "問心伝月額仕入データファイル名";
			// 
			// textBoxCurlineFilename
			// 
			this.textBoxCurlineFilename.BackColor = System.Drawing.Color.White;
			this.textBoxCurlineFilename.Location = new System.Drawing.Point(21, 278);
			this.textBoxCurlineFilename.Name = "textBoxCurlineFilename";
			this.textBoxCurlineFilename.ReadOnly = true;
			this.textBoxCurlineFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxCurlineFilename.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(21, 256);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(242, 19);
			this.label7.TabIndex = 11;
			this.label7.Text = "Curline本体アプリ仕入データファイル名";
			// 
			// textBoxNarcohmFilename
			// 
			this.textBoxNarcohmFilename.BackColor = System.Drawing.Color.White;
			this.textBoxNarcohmFilename.Location = new System.Drawing.Point(21, 330);
			this.textBoxNarcohmFilename.Name = "textBoxNarcohmFilename";
			this.textBoxNarcohmFilename.ReadOnly = true;
			this.textBoxNarcohmFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxNarcohmFilename.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(21, 308);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(188, 19);
			this.label8.TabIndex = 13;
			this.label8.Text = "ナルコーム仕入データファイル名";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.Red;
			this.label9.Location = new System.Drawing.Point(21, 464);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(351, 19);
			this.label9.TabIndex = 19;
			this.label9.Text = "※出力物に関する変更はXMLファイルを変更してください。";
			// 
			// textBoxCloudBackupFilename
			// 
			this.textBoxCloudBackupFilename.BackColor = System.Drawing.Color.White;
			this.textBoxCloudBackupFilename.Location = new System.Drawing.Point(21, 382);
			this.textBoxCloudBackupFilename.Name = "textBoxCloudBackupFilename";
			this.textBoxCloudBackupFilename.ReadOnly = true;
			this.textBoxCloudBackupFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxCloudBackupFilename.TabIndex = 16;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(21, 360);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(235, 19);
			this.label10.TabIndex = 15;
			this.label10.Text = "クラウドバックアップ仕入データファイル名";
			// 
			// textBoxAlmexFilename
			// 
			this.textBoxAlmexFilename.BackColor = System.Drawing.Color.White;
			this.textBoxAlmexFilename.Location = new System.Drawing.Point(21, 434);
			this.textBoxAlmexFilename.Name = "textBoxAlmexFilename";
			this.textBoxAlmexFilename.ReadOnly = true;
			this.textBoxAlmexFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxAlmexFilename.TabIndex = 18;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(21, 412);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(223, 19);
			this.label11.TabIndex = 17;
			this.label11.Text = "アルメックス保守仕入データファイル名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(565, 182);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124, 19);
			this.label2.TabIndex = 21;
			this.label2.Text = "PCAバージョン番号";
			// 
			// textBoxPcaVersion
			// 
			this.textBoxPcaVersion.BackColor = System.Drawing.Color.White;
			this.textBoxPcaVersion.Location = new System.Drawing.Point(695, 179);
			this.textBoxPcaVersion.Name = "textBoxPcaVersion";
			this.textBoxPcaVersion.ReadOnly = true;
			this.textBoxPcaVersion.Size = new System.Drawing.Size(37, 27);
			this.textBoxPcaVersion.TabIndex = 22;
			this.textBoxPcaVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(751, 506);
			this.Controls.Add(this.textBoxPcaVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxAlmexFilename);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textBoxCloudBackupFilename);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textBoxNarcohmFilename);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBoxCurlineFilename);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxMonshindenFilename);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxMicrosoft365Filename);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxListonFilename);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxOutputFolder);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelversion);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.dateTimePickerTarget);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "仕入データ作成";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePickerTarget;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label labelversion;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxOutputFolder;
		private System.Windows.Forms.TextBox textBoxListonFilename;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxMicrosoft365Filename;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxMonshindenFilename;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxCurlineFilename;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxNarcohmFilename;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxCloudBackupFilename;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxAlmexFilename;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPcaVersion;
	}
}

