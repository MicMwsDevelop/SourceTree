namespace CalcBusinessConsignCommission.Forms
{
	partial class OutputStockForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.dataGridViewStock = new System.Windows.Forms.DataGridView();
			this.buttonOutput = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "■PCA仕入データ 業務委託手数料再計算結果";
			// 
			// dataGridViewStock
			// 
			this.dataGridViewStock.AllowUserToAddRows = false;
			this.dataGridViewStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewStock.Location = new System.Drawing.Point(12, 51);
			this.dataGridViewStock.Name = "dataGridViewStock";
			this.dataGridViewStock.ReadOnly = true;
			this.dataGridViewStock.RowHeadersVisible = false;
			this.dataGridViewStock.RowTemplate.Height = 21;
			this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewStock.Size = new System.Drawing.Size(1353, 816);
			this.dataGridViewStock.TabIndex = 1;
			// 
			// buttonOutput
			// 
			this.buttonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOutput.Location = new System.Drawing.Point(1189, 12);
			this.buttonOutput.Name = "buttonOutput";
			this.buttonOutput.Size = new System.Drawing.Size(176, 33);
			this.buttonOutput.TabIndex = 2;
			this.buttonOutput.Text = "PCA仕入データ出力";
			this.buttonOutput.UseVisualStyleBackColor = true;
			this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
			// 
			// OutputStockForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1377, 880);
			this.Controls.Add(this.buttonOutput);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dataGridViewStock);
			this.Name = "OutputStockForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PCA仕入データ出力";
			this.Load += new System.EventHandler(this.OutputStockForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dataGridViewStock;
		private System.Windows.Forms.Button buttonOutput;
	}
}