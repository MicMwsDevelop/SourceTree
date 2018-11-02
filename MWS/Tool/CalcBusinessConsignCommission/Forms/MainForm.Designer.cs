namespace CalcBusinessConsignCommission.Forms
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
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this.dataGridViewSale = new System.Windows.Forms.DataGridView();
			this.buttonReadFile = new System.Windows.Forms.Button();
			this.dataGridViewStock = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonCalc = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewSale
			// 
			this.dataGridViewSale.AllowUserToAddRows = false;
			this.dataGridViewSale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dataGridViewSale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewSale.Location = new System.Drawing.Point(13, 32);
			this.dataGridViewSale.Name = "dataGridViewSale";
			this.dataGridViewSale.ReadOnly = true;
			this.dataGridViewSale.RowHeadersVisible = false;
			this.dataGridViewSale.RowTemplate.Height = 21;
			this.dataGridViewSale.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewSale.Size = new System.Drawing.Size(782, 660);
			this.dataGridViewSale.TabIndex = 0;
			// 
			// buttonReadFile
			// 
			this.buttonReadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonReadFile.Location = new System.Drawing.Point(12, 698);
			this.buttonReadFile.Name = "buttonReadFile";
			this.buttonReadFile.Size = new System.Drawing.Size(75, 30);
			this.buttonReadFile.TabIndex = 2;
			this.buttonReadFile.Text = "ファイル読込";
			this.buttonReadFile.UseVisualStyleBackColor = true;
			this.buttonReadFile.Click += new System.EventHandler(this.buttonReadFile_Click);
			// 
			// dataGridViewStock
			// 
			this.dataGridViewStock.AllowUserToAddRows = false;
			this.dataGridViewStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewStock.Location = new System.Drawing.Point(801, 32);
			this.dataGridViewStock.Name = "dataGridViewStock";
			this.dataGridViewStock.ReadOnly = true;
			this.dataGridViewStock.RowHeadersVisible = false;
			this.dataGridViewStock.RowTemplate.Height = 21;
			this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewStock.Size = new System.Drawing.Size(581, 660);
			this.dataGridViewStock.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "■PCA売上データ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(799, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "■PCA仕入データ";
			// 
			// buttonCalc
			// 
			this.buttonCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCalc.Location = new System.Drawing.Point(1307, 698);
			this.buttonCalc.Name = "buttonCalc";
			this.buttonCalc.Size = new System.Drawing.Size(75, 30);
			this.buttonCalc.TabIndex = 6;
			this.buttonCalc.Text = "再計算";
			this.buttonCalc.UseVisualStyleBackColor = true;
			this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1394, 736);
			this.Controls.Add(this.buttonCalc);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewStock);
			this.Controls.Add(this.buttonReadFile);
			this.Controls.Add(this.dataGridViewSale);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "業務委託手数料計算";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewSale;
		private System.Windows.Forms.Button buttonReadFile;
		private System.Windows.Forms.DataGridView dataGridViewStock;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonCalc;
	}
}

