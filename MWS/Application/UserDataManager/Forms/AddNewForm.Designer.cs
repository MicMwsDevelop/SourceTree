
namespace UserDataManager.Forms
{
	partial class AddNewForm
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
			this.textBoxデータ名称 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker作業終了予定日 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox作業者情報 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox作業報告書No = new MwsLib.Component.NumericTextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox備考 = new System.Windows.Forms.TextBox();
			this.buttonクリア = new System.Windows.Forms.Button();
			this.dataGridViewFileList = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileList)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "データ名称";
			// 
			// textBoxデータ名称
			// 
			this.textBoxデータ名称.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxデータ名称.Location = new System.Drawing.Point(11, 28);
			this.textBoxデータ名称.Name = "textBoxデータ名称";
			this.textBoxデータ名称.Size = new System.Drawing.Size(282, 24);
			this.textBoxデータ名称.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "作業終了予定日";
			// 
			// dateTimePicker作業終了予定日
			// 
			this.dateTimePicker作業終了予定日.Location = new System.Drawing.Point(11, 80);
			this.dateTimePicker作業終了予定日.Name = "dateTimePicker作業終了予定日";
			this.dateTimePicker作業終了予定日.Size = new System.Drawing.Size(178, 24);
			this.dateTimePicker作業終了予定日.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "作業者情報";
			// 
			// textBox作業者情報
			// 
			this.textBox作業者情報.Location = new System.Drawing.Point(11, 132);
			this.textBox作業者情報.Name = "textBox作業者情報";
			this.textBox作業者情報.ReadOnly = true;
			this.textBox作業者情報.Size = new System.Drawing.Size(182, 24);
			this.textBox作業者情報.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(311, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(91, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "作業報告者No";
			// 
			// textBox作業報告書No
			// 
			this.textBox作業報告書No.Location = new System.Drawing.Point(315, 28);
			this.textBox作業報告書No.MaxLength = 8;
			this.textBox作業報告書No.Name = "textBox作業報告書No";
			this.textBox作業報告書No.Size = new System.Drawing.Size(96, 24);
			this.textBox作業報告書No.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(311, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 17);
			this.label5.TabIndex = 8;
			this.label5.Text = "備考";
			// 
			// textBox備考
			// 
			this.textBox備考.Location = new System.Drawing.Point(315, 84);
			this.textBox備考.Multiline = true;
			this.textBox備考.Name = "textBox備考";
			this.textBox備考.Size = new System.Drawing.Size(347, 72);
			this.textBox備考.TabIndex = 9;
			// 
			// buttonクリア
			// 
			this.buttonクリア.Location = new System.Drawing.Point(667, 132);
			this.buttonクリア.Name = "buttonクリア";
			this.buttonクリア.Size = new System.Drawing.Size(67, 24);
			this.buttonクリア.TabIndex = 10;
			this.buttonクリア.Text = "クリア";
			this.buttonクリア.UseVisualStyleBackColor = true;
			this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
			// 
			// dataGridViewFileList
			// 
			this.dataGridViewFileList.AllowDrop = true;
			this.dataGridViewFileList.AllowUserToAddRows = false;
			this.dataGridViewFileList.AllowUserToDeleteRows = false;
			this.dataGridViewFileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewFileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
			this.dataGridViewFileList.Location = new System.Drawing.Point(11, 174);
			this.dataGridViewFileList.Name = "dataGridViewFileList";
			this.dataGridViewFileList.ReadOnly = true;
			this.dataGridViewFileList.RowTemplate.Height = 21;
			this.dataGridViewFileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewFileList.Size = new System.Drawing.Size(723, 220);
			this.dataGridViewFileList.TabIndex = 11;
			this.dataGridViewFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewFileList_DragDrop);
			this.dataGridViewFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewFileList_DragEnter);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "登録ファイル";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 650;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "ファイルサイズ";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 120;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(595, 401);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(67, 30);
			this.buttonOK.TabIndex = 12;
			this.buttonOK.Text = "登録";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(667, 401);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(67, 30);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// AddNewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(746, 438);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.dataGridViewFileList);
			this.Controls.Add(this.buttonクリア);
			this.Controls.Add(this.textBox備考);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox作業報告書No);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox作業者情報);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dateTimePicker作業終了予定日);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxデータ名称);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.Name = "AddNewForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "新規登録";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxデータ名称;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePicker作業終了予定日;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox作業者情報;
		private System.Windows.Forms.Label label4;
		private MwsLib.Component.NumericTextBox textBox作業報告書No;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox備考;
		private System.Windows.Forms.Button buttonクリア;
		private System.Windows.Forms.DataGridView dataGridViewFileList;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
	}
}