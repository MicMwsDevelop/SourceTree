
namespace WonderWebEntryMemo.Forms
{
	partial class BankForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dateTimePickerBank = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.buttonInputFile = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonAddMemo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// dateTimePickerBank
			// 
			this.dateTimePickerBank.Location = new System.Drawing.Point(64, 85);
			this.dateTimePickerBank.Name = "dateTimePickerBank";
			this.dateTimePickerBank.Size = new System.Drawing.Size(143, 24);
			this.dateTimePickerBank.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 88);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "締日";
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.BackColor = System.Drawing.Color.White;
			this.textBoxFilename.Location = new System.Drawing.Point(26, 50);
			this.textBoxFilename.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.ReadOnly = true;
			this.textBoxFilename.Size = new System.Drawing.Size(548, 24);
			this.textBoxFilename.TabIndex = 1;
			this.textBoxFilename.TabStop = false;
			// 
			// buttonInputFile
			// 
			this.buttonInputFile.Location = new System.Drawing.Point(574, 49);
			this.buttonInputFile.Margin = new System.Windows.Forms.Padding(4);
			this.buttonInputFile.Name = "buttonInputFile";
			this.buttonInputFile.Size = new System.Drawing.Size(34, 25);
			this.buttonInputFile.TabIndex = 2;
			this.buttonInputFile.Text = "▼";
			this.buttonInputFile.UseVisualStyleBackColor = true;
			this.buttonInputFile.Click += new System.EventHandler(this.buttonInputFile_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 29);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(177, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "■請求書発行先エクセルファイル";
			// 
			// buttonAddMemo
			// 
			this.buttonAddMemo.Location = new System.Drawing.Point(410, 121);
			this.buttonAddMemo.Name = "buttonAddMemo";
			this.buttonAddMemo.Size = new System.Drawing.Size(198, 37);
			this.buttonAddMemo.TabIndex = 5;
			this.buttonAddMemo.Text = "メモ追加";
			this.buttonAddMemo.UseVisualStyleBackColor = true;
			this.buttonAddMemo.Click += new System.EventHandler(this.buttonAddMemo_Click);
			// 
			// BankForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 170);
			this.Controls.Add(this.buttonAddMemo);
			this.Controls.Add(this.dateTimePickerBank);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxFilename);
			this.Controls.Add(this.buttonInputFile);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BankForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "銀行振込請求書発行メモ追加";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dateTimePickerBank;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.Button buttonInputFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAddMemo;
	}
}