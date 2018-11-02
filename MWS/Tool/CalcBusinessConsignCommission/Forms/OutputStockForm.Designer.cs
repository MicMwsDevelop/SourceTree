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
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 12);
			this.label2.TabIndex = 7;
			this.label2.Text = "■PCA仕入データ";
			// 
			// dataGridViewStock
			// 
			this.dataGridViewStock.AllowUserToAddRows = false;
			this.dataGridViewStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewStock.Location = new System.Drawing.Point(12, 29);
			this.dataGridViewStock.Name = "dataGridViewStock";
			this.dataGridViewStock.ReadOnly = true;
			this.dataGridViewStock.RowHeadersVisible = false;
			this.dataGridViewStock.RowTemplate.Height = 21;
			this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewStock.Size = new System.Drawing.Size(1353, 821);
			this.dataGridViewStock.TabIndex = 6;
			// 
			// OutputStockForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1377, 862);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dataGridViewStock);
			this.Name = "OutputStockForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "仕入データ（再計算）";
			this.Load += new System.EventHandler(this.OutputStockForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dataGridViewStock;
	}
}