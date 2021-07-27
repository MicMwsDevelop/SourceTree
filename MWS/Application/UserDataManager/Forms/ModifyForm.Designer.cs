
namespace UserDataManager.Forms
{
	partial class ModifyForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox備考 = new System.Windows.Forms.TextBox();
			this.radioButton未作業 = new System.Windows.Forms.RadioButton();
			this.radioButton終了 = new System.Windows.Forms.RadioButton();
			this.buttonステータス変更 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageCheckOut = new System.Windows.Forms.TabPage();
			this.tabPageCheckIn = new System.Windows.Forms.TabPage();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxCheckOutPath = new System.Windows.Forms.TextBox();
			this.buttonSelectFolder = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxCheckOutMarks = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.dataGridViewCheckOutFiles = new System.Windows.Forms.DataGridView();
			this.buttonClearCheckOut = new System.Windows.Forms.Button();
			this.buttonCheckOut = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.comboBoxStatus = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.label11 = new System.Windows.Forms.Label();
			this.buttonCheckIn = new System.Windows.Forms.Button();
			this.buttonClearCheckIn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageCheckOut.SuspendLayout();
			this.tabPageCheckIn.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCheckOutFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "作業No";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(17, 27);
			this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(87, 24);
			this.textBox1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(107, 8);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "データ名称";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(110, 27);
			this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(641, 24);
			this.textBox2.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(756, 8);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "作業報告書No";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(755, 27);
			this.textBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(133, 24);
			this.textBox3.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 62);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "現在のステータス";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(17, 80);
			this.textBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(123, 24);
			this.textBox4.TabIndex = 7;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonステータス変更);
			this.groupBox1.Controls.Add(this.radioButton終了);
			this.groupBox1.Controls.Add(this.radioButton未作業);
			this.groupBox1.Controls.Add(this.textBox備考);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(155, 56);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox1.Size = new System.Drawing.Size(733, 91);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ステータス変更";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(27, 22);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "備考";
			// 
			// textBox備考
			// 
			this.textBox備考.Location = new System.Drawing.Point(65, 22);
			this.textBox備考.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBox備考.Multiline = true;
			this.textBox備考.Name = "textBox備考";
			this.textBox備考.Size = new System.Drawing.Size(519, 61);
			this.textBox備考.TabIndex = 10;
			// 
			// radioButton未作業
			// 
			this.radioButton未作業.AutoSize = true;
			this.radioButton未作業.Location = new System.Drawing.Point(600, 13);
			this.radioButton未作業.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.radioButton未作業.Name = "radioButton未作業";
			this.radioButton未作業.Size = new System.Drawing.Size(65, 21);
			this.radioButton未作業.TabIndex = 11;
			this.radioButton未作業.TabStop = true;
			this.radioButton未作業.Text = "未作業";
			this.radioButton未作業.UseVisualStyleBackColor = true;
			// 
			// radioButton終了
			// 
			this.radioButton終了.AutoSize = true;
			this.radioButton終了.Location = new System.Drawing.Point(600, 32);
			this.radioButton終了.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.radioButton終了.Name = "radioButton終了";
			this.radioButton終了.Size = new System.Drawing.Size(52, 21);
			this.radioButton終了.TabIndex = 12;
			this.radioButton終了.TabStop = true;
			this.radioButton終了.Text = "終了";
			this.radioButton終了.UseVisualStyleBackColor = true;
			// 
			// buttonステータス変更
			// 
			this.buttonステータス変更.Location = new System.Drawing.Point(600, 56);
			this.buttonステータス変更.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonステータス変更.Name = "buttonステータス変更";
			this.buttonステータス変更.Size = new System.Drawing.Size(104, 29);
			this.buttonステータス変更.TabIndex = 13;
			this.buttonステータス変更.Text = "ステータス変更";
			this.buttonステータス変更.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageCheckOut);
			this.tabControl1.Controls.Add(this.tabPageCheckIn);
			this.tabControl1.Location = new System.Drawing.Point(17, 162);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(871, 429);
			this.tabControl1.TabIndex = 9;
			// 
			// tabPageCheckOut
			// 
			this.tabPageCheckOut.Controls.Add(this.buttonCheckOut);
			this.tabPageCheckOut.Controls.Add(this.buttonClearCheckOut);
			this.tabPageCheckOut.Controls.Add(this.dataGridViewCheckOutFiles);
			this.tabPageCheckOut.Controls.Add(this.label8);
			this.tabPageCheckOut.Controls.Add(this.textBoxCheckOutMarks);
			this.tabPageCheckOut.Controls.Add(this.label7);
			this.tabPageCheckOut.Controls.Add(this.buttonSelectFolder);
			this.tabPageCheckOut.Controls.Add(this.textBoxCheckOutPath);
			this.tabPageCheckOut.Controls.Add(this.label6);
			this.tabPageCheckOut.Location = new System.Drawing.Point(4, 26);
			this.tabPageCheckOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPageCheckOut.Name = "tabPageCheckOut";
			this.tabPageCheckOut.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPageCheckOut.Size = new System.Drawing.Size(863, 399);
			this.tabPageCheckOut.TabIndex = 0;
			this.tabPageCheckOut.Text = "チェックアウト";
			this.tabPageCheckOut.UseVisualStyleBackColor = true;
			// 
			// tabPageCheckIn
			// 
			this.tabPageCheckIn.Controls.Add(this.buttonCheckIn);
			this.tabPageCheckIn.Controls.Add(this.buttonClearCheckIn);
			this.tabPageCheckIn.Controls.Add(this.label11);
			this.tabPageCheckIn.Controls.Add(this.dataGridView1);
			this.tabPageCheckIn.Controls.Add(this.textBox5);
			this.tabPageCheckIn.Controls.Add(this.label10);
			this.tabPageCheckIn.Controls.Add(this.comboBoxStatus);
			this.tabPageCheckIn.Controls.Add(this.label9);
			this.tabPageCheckIn.Location = new System.Drawing.Point(4, 26);
			this.tabPageCheckIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPageCheckIn.Name = "tabPageCheckIn";
			this.tabPageCheckIn.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPageCheckIn.Size = new System.Drawing.Size(863, 399);
			this.tabPageCheckIn.TabIndex = 1;
			this.tabPageCheckIn.Text = "チェックイン";
			this.tabPageCheckIn.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(780, 595);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(104, 29);
			this.buttonCancel.TabIndex = 14;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 18);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(106, 17);
			this.label6.TabIndex = 15;
			this.label6.Text = "チェックアウト先パス";
			// 
			// textBoxCheckOutPath
			// 
			this.textBoxCheckOutPath.Location = new System.Drawing.Point(18, 37);
			this.textBoxCheckOutPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxCheckOutPath.Name = "textBoxCheckOutPath";
			this.textBoxCheckOutPath.ReadOnly = true;
			this.textBoxCheckOutPath.Size = new System.Drawing.Size(420, 24);
			this.textBoxCheckOutPath.TabIndex = 15;
			// 
			// buttonSelectFolder
			// 
			this.buttonSelectFolder.Location = new System.Drawing.Point(443, 41);
			this.buttonSelectFolder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.buttonSelectFolder.Name = "buttonSelectFolder";
			this.buttonSelectFolder.Size = new System.Drawing.Size(30, 20);
			this.buttonSelectFolder.TabIndex = 16;
			this.buttonSelectFolder.Text = "…";
			this.buttonSelectFolder.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.buttonSelectFolder.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(509, 18);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(34, 17);
			this.label7.TabIndex = 17;
			this.label7.Text = "備考";
			// 
			// textBoxCheckOutMarks
			// 
			this.textBoxCheckOutMarks.Location = new System.Drawing.Point(512, 37);
			this.textBoxCheckOutMarks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxCheckOutMarks.Multiline = true;
			this.textBoxCheckOutMarks.Name = "textBoxCheckOutMarks";
			this.textBoxCheckOutMarks.ReadOnly = true;
			this.textBoxCheckOutMarks.Size = new System.Drawing.Size(308, 83);
			this.textBoxCheckOutMarks.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(15, 112);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(109, 17);
			this.label8.TabIndex = 18;
			this.label8.Text = "チェックアウトファイル";
			// 
			// dataGridViewCheckOutFiles
			// 
			this.dataGridViewCheckOutFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCheckOutFiles.Location = new System.Drawing.Point(18, 132);
			this.dataGridViewCheckOutFiles.Name = "dataGridViewCheckOutFiles";
			this.dataGridViewCheckOutFiles.RowTemplate.Height = 21;
			this.dataGridViewCheckOutFiles.Size = new System.Drawing.Size(802, 219);
			this.dataGridViewCheckOutFiles.TabIndex = 19;
			// 
			// buttonClearCheckOut
			// 
			this.buttonClearCheckOut.Location = new System.Drawing.Point(623, 357);
			this.buttonClearCheckOut.Name = "buttonClearCheckOut";
			this.buttonClearCheckOut.Size = new System.Drawing.Size(95, 36);
			this.buttonClearCheckOut.TabIndex = 20;
			this.buttonClearCheckOut.Text = "クリア";
			this.buttonClearCheckOut.UseVisualStyleBackColor = true;
			// 
			// buttonCheckOut
			// 
			this.buttonCheckOut.Location = new System.Drawing.Point(725, 357);
			this.buttonCheckOut.Name = "buttonCheckOut";
			this.buttonCheckOut.Size = new System.Drawing.Size(95, 36);
			this.buttonCheckOut.TabIndex = 21;
			this.buttonCheckOut.Text = "チェックアウト";
			this.buttonCheckOut.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(19, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(58, 17);
			this.label9.TabIndex = 0;
			this.label9.Text = "ステータス";
			// 
			// comboBoxStatus
			// 
			this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxStatus.FormattingEnabled = true;
			this.comboBoxStatus.Location = new System.Drawing.Point(22, 38);
			this.comboBoxStatus.Name = "comboBoxStatus";
			this.comboBoxStatus.Size = new System.Drawing.Size(121, 25);
			this.comboBoxStatus.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(509, 18);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(34, 17);
			this.label10.TabIndex = 2;
			this.label10.Text = "備考";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(512, 37);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(308, 83);
			this.textBox5.TabIndex = 3;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(18, 132);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 21;
			this.dataGridView1.Size = new System.Drawing.Size(822, 209);
			this.dataGridView1.TabIndex = 4;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(19, 112);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(71, 17);
			this.label11.TabIndex = 5;
			this.label11.Text = "登録ファイル";
			// 
			// buttonCheckIn
			// 
			this.buttonCheckIn.Location = new System.Drawing.Point(743, 347);
			this.buttonCheckIn.Name = "buttonCheckIn";
			this.buttonCheckIn.Size = new System.Drawing.Size(95, 36);
			this.buttonCheckIn.TabIndex = 23;
			this.buttonCheckIn.Text = "チェックイン";
			this.buttonCheckIn.UseVisualStyleBackColor = true;
			// 
			// buttonClearCheckIn
			// 
			this.buttonClearCheckIn.Location = new System.Drawing.Point(641, 347);
			this.buttonClearCheckIn.Name = "buttonClearCheckIn";
			this.buttonClearCheckIn.Size = new System.Drawing.Size(95, 36);
			this.buttonClearCheckIn.TabIndex = 22;
			this.buttonClearCheckIn.Text = "クリア";
			this.buttonClearCheckIn.UseVisualStyleBackColor = true;
			// 
			// ModifyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(901, 636);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.Name = "ModifyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "編集";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageCheckOut.ResumeLayout(false);
			this.tabPageCheckOut.PerformLayout();
			this.tabPageCheckIn.ResumeLayout(false);
			this.tabPageCheckIn.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCheckOutFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonステータス変更;
		private System.Windows.Forms.RadioButton radioButton終了;
		private System.Windows.Forms.RadioButton radioButton未作業;
		private System.Windows.Forms.TextBox textBox備考;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageCheckOut;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxCheckOutMarks;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonSelectFolder;
		private System.Windows.Forms.TextBox textBoxCheckOutPath;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tabPageCheckIn;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonCheckOut;
		private System.Windows.Forms.Button buttonClearCheckOut;
		private System.Windows.Forms.DataGridView dataGridViewCheckOutFiles;
		private System.Windows.Forms.Button buttonCheckIn;
		private System.Windows.Forms.Button buttonClearCheckIn;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboBoxStatus;
		private System.Windows.Forms.Label label9;
	}
}