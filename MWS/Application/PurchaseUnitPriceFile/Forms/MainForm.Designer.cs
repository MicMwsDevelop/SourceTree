
namespace PurchaseUnitPriceFile
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
			this.textBoxZaikoFilename = new System.Windows.Forms.TextBox();
			this.buttonInputZaikoFile = new System.Windows.Forms.Button();
			this.buttonChangeFolder = new System.Windows.Forms.Button();
			this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonOpenExcel = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePickerTarget = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 79);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(171, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "在庫一覧表入力ファイル名";
			// 
			// textBoxZaikoFilename
			// 
			this.textBoxZaikoFilename.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxZaikoFilename.Location = new System.Drawing.Point(24, 102);
			this.textBoxZaikoFilename.Name = "textBoxZaikoFilename";
			this.textBoxZaikoFilename.ReadOnly = true;
			this.textBoxZaikoFilename.Size = new System.Drawing.Size(552, 27);
			this.textBoxZaikoFilename.TabIndex = 4;
			// 
			// buttonInputZaikoFile
			// 
			this.buttonInputZaikoFile.Location = new System.Drawing.Point(582, 102);
			this.buttonInputZaikoFile.Name = "buttonInputZaikoFile";
			this.buttonInputZaikoFile.Size = new System.Drawing.Size(75, 27);
			this.buttonInputZaikoFile.TabIndex = 5;
			this.buttonInputZaikoFile.Text = "変更";
			this.buttonInputZaikoFile.UseVisualStyleBackColor = true;
			this.buttonInputZaikoFile.Click += new System.EventHandler(this.buttonInputZaikoFile_Click);
			// 
			// buttonChangeFolder
			// 
			this.buttonChangeFolder.Location = new System.Drawing.Point(582, 167);
			this.buttonChangeFolder.Name = "buttonChangeFolder";
			this.buttonChangeFolder.Size = new System.Drawing.Size(75, 27);
			this.buttonChangeFolder.TabIndex = 8;
			this.buttonChangeFolder.Text = "変更";
			this.buttonChangeFolder.UseVisualStyleBackColor = true;
			this.buttonChangeFolder.Click += new System.EventHandler(this.buttonChangeFolder_Click);
			// 
			// textBoxOutputFolder
			// 
			this.textBoxOutputFolder.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxOutputFolder.Location = new System.Drawing.Point(24, 167);
			this.textBoxOutputFolder.Name = "textBoxOutputFolder";
			this.textBoxOutputFolder.ReadOnly = true;
			this.textBoxOutputFolder.Size = new System.Drawing.Size(552, 27);
			this.textBoxOutputFolder.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 144);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(204, 19);
			this.label2.TabIndex = 6;
			this.label2.Text = "振替出力ファイル 出力先フォルダ";
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(24, 213);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(242, 99);
			this.buttonStart.TabIndex = 9;
			this.buttonStart.Text = "START";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonOpenExcel
			// 
			this.buttonOpenExcel.Location = new System.Drawing.Point(272, 213);
			this.buttonOpenExcel.Name = "buttonOpenExcel";
			this.buttonOpenExcel.Size = new System.Drawing.Size(136, 99);
			this.buttonOpenExcel.TabIndex = 10;
			this.buttonOpenExcel.Text = "社内使用分振替出力ファイルを開く";
			this.buttonOpenExcel.UseVisualStyleBackColor = true;
			this.buttonOpenExcel.Click += new System.EventHandler(this.buttonOpenExcel_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(582, 262);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(75, 50);
			this.buttonExit.TabIndex = 11;
			this.buttonExit.Text = "終了";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(486, 9);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(175, 19);
			this.label4.TabIndex = 0;
			this.label4.Text = "Ver 1.00 (2022/01/11)";
			// 
			// dateTimePickerTarget
			// 
			this.dateTimePickerTarget.CustomFormat = "yyyy年MM月";
			this.dateTimePickerTarget.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerTarget.Location = new System.Drawing.Point(100, 36);
			this.dateTimePickerTarget.Name = "dateTimePickerTarget";
			this.dateTimePickerTarget.Size = new System.Drawing.Size(160, 27);
			this.dateTimePickerTarget.TabIndex = 2;
			this.dateTimePickerTarget.DropDown += new System.EventHandler(this.dateTimePickerTarget_DropDown);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 42);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 19);
			this.label3.TabIndex = 1;
			this.label3.Text = "対象年月";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 335);
			this.Controls.Add(this.dateTimePickerTarget);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonOpenExcel);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.buttonChangeFolder);
			this.Controls.Add(this.textBoxOutputFolder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonInputZaikoFile);
			this.Controls.Add(this.textBoxZaikoFilename);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "社内使用分振替出力ファイル出力";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxZaikoFilename;
		private System.Windows.Forms.Button buttonInputZaikoFile;
		private System.Windows.Forms.Button buttonChangeFolder;
		private System.Windows.Forms.TextBox textBoxOutputFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonOpenExcel;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerTarget;
		private System.Windows.Forms.Label label3;
	}
}

