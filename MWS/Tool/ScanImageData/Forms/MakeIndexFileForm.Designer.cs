namespace ScanImageData.Forms
{
	partial class MakeIndexFileForm
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
			this.dataGridViewIndex = new System.Windows.Forms.DataGridView();
			this.buttonClear = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOutput = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewIndex
			// 
			this.dataGridViewIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dataGridViewIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewIndex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
			this.dataGridViewIndex.Location = new System.Drawing.Point(13, 35);
			this.dataGridViewIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dataGridViewIndex.Name = "dataGridViewIndex";
			this.dataGridViewIndex.RowTemplate.Height = 21;
			this.dataGridViewIndex.Size = new System.Drawing.Size(512, 464);
			this.dataGridViewIndex.TabIndex = 0;
			// 
			// buttonClear
			// 
			this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClear.Location = new System.Drawing.Point(13, 508);
			this.buttonClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(125, 44);
			this.buttonClear.TabIndex = 1;
			this.buttonClear.Text = "出力対象リスト初期化";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "出力対象リスト";
			// 
			// buttonOutput
			// 
			this.buttonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOutput.Location = new System.Drawing.Point(144, 508);
			this.buttonOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOutput.Name = "buttonOutput";
			this.buttonOutput.Size = new System.Drawing.Size(125, 44);
			this.buttonOutput.TabIndex = 3;
			this.buttonOutput.Text = "インデックスファイル出力";
			this.buttonOutput.UseVisualStyleBackColor = true;
			this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(400, 508);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(125, 44);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// Column1
			// 
			this.Column1.DataPropertyName = "TOKUISAKI_NO";
			this.Column1.HeaderText = "得意先No";
			this.Column1.MaxInputLength = 6;
			this.Column1.Name = "Column1";
			// 
			// Column2
			// 
			this.Column2.DataPropertyName = "CUSTOMER_NO";
			this.Column2.HeaderText = "顧客No";
			this.Column2.Name = "Column2";
			// 
			// Column3
			// 
			this.Column3.DataPropertyName = "CLINIC_NAME";
			this.Column3.HeaderText = "医院名";
			this.Column3.Name = "Column3";
			this.Column3.Width = 250;
			// 
			// MakeIndexFileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(541, 561);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonOutput);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonClear);
			this.Controls.Add(this.dataGridViewIndex);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MakeIndexFileForm";
			this.Text = "インデックスファイル作成";
			this.Load += new System.EventHandler(this.MakeIndexFileForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewIndex;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOutput;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
	}
}