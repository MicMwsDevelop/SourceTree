namespace WonderWebMemo.Forms
{
	partial class BankTransferForm
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
			this.buttonReadTable = new System.Windows.Forms.Button();
			this.dataGridViewMemo = new System.Windows.Forms.DataGridView();
			this.buttonAddMemo = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMemo)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonReadTable
			// 
			this.buttonReadTable.Location = new System.Drawing.Point(12, 18);
			this.buttonReadTable.Name = "buttonReadTable";
			this.buttonReadTable.Size = new System.Drawing.Size(167, 36);
			this.buttonReadTable.TabIndex = 0;
			this.buttonReadTable.Text = "読込";
			this.buttonReadTable.UseVisualStyleBackColor = true;
			// 
			// dataGridViewMemo
			// 
			this.dataGridViewMemo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMemo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
			this.dataGridViewMemo.Location = new System.Drawing.Point(13, 60);
			this.dataGridViewMemo.Name = "dataGridViewMemo";
			this.dataGridViewMemo.RowTemplate.Height = 21;
			this.dataGridViewMemo.Size = new System.Drawing.Size(1149, 378);
			this.dataGridViewMemo.TabIndex = 1;
			// 
			// buttonAddMemo
			// 
			this.buttonAddMemo.Location = new System.Drawing.Point(995, 18);
			this.buttonAddMemo.Name = "buttonAddMemo";
			this.buttonAddMemo.Size = new System.Drawing.Size(167, 36);
			this.buttonAddMemo.TabIndex = 2;
			this.buttonAddMemo.Text = "メモ追加";
			this.buttonAddMemo.UseVisualStyleBackColor = true;
			this.buttonAddMemo.Click += new System.EventHandler(this.buttonAddMemo_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "得意先No";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "メモタイプ";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.HeaderText = "メモ";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "得意先No";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "メモタイプ";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "メモ";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			// 
			// BankTransferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1177, 450);
			this.Controls.Add(this.buttonAddMemo);
			this.Controls.Add(this.dataGridViewMemo);
			this.Controls.Add(this.buttonReadTable);
			this.Name = "BankTransferForm";
			this.Text = "銀行振込請求書発行先メモ追加";
			this.Load += new System.EventHandler(this.BankTransferForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMemo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonReadTable;
		private System.Windows.Forms.DataGridView dataGridViewMemo;
		private System.Windows.Forms.Button buttonAddMemo;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
	}
}