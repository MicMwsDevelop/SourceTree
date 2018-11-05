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
			this.buttonReadSaleFile = new System.Windows.Forms.Button();
			this.dataGridViewStock = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonRecalcResult = new System.Windows.Forms.Button();
			this.labelSaleFilename = new System.Windows.Forms.Label();
			this.labelStockFilename = new System.Windows.Forms.Label();
			this.buttonReadStockFile = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewSale
			// 
			this.dataGridViewSale.AllowUserToAddRows = false;
			this.dataGridViewSale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewSale.Location = new System.Drawing.Point(13, 38);
			this.dataGridViewSale.Name = "dataGridViewSale";
			this.dataGridViewSale.ReadOnly = true;
			this.dataGridViewSale.RowHeadersVisible = false;
			this.dataGridViewSale.RowTemplate.Height = 21;
			this.dataGridViewSale.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewSale.Size = new System.Drawing.Size(782, 676);
			this.dataGridViewSale.TabIndex = 3;
			// 
			// buttonReadSaleFile
			// 
			this.buttonReadSaleFile.Location = new System.Drawing.Point(518, 9);
			this.buttonReadSaleFile.Name = "buttonReadSaleFile";
			this.buttonReadSaleFile.Size = new System.Drawing.Size(75, 23);
			this.buttonReadSaleFile.TabIndex = 2;
			this.buttonReadSaleFile.Text = "ファイル読込";
			this.buttonReadSaleFile.UseVisualStyleBackColor = true;
			this.buttonReadSaleFile.Click += new System.EventHandler(this.buttonReadSaleFile_Click);
			// 
			// dataGridViewStock
			// 
			this.dataGridViewStock.AllowUserToAddRows = false;
			this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewStock.Location = new System.Drawing.Point(801, 38);
			this.dataGridViewStock.Name = "dataGridViewStock";
			this.dataGridViewStock.ReadOnly = true;
			this.dataGridViewStock.RowHeadersVisible = false;
			this.dataGridViewStock.RowTemplate.Height = 21;
			this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewStock.Size = new System.Drawing.Size(581, 676);
			this.dataGridViewStock.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■PCA売上データ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(799, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "■PCA仕入データ";
			// 
			// buttonRecalcResult
			// 
			this.buttonRecalcResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRecalcResult.Location = new System.Drawing.Point(1134, 720);
			this.buttonRecalcResult.Name = "buttonRecalcResult";
			this.buttonRecalcResult.Size = new System.Drawing.Size(248, 40);
			this.buttonRecalcResult.TabIndex = 8;
			this.buttonRecalcResult.Text = "PCA仕入データ 業務委託手数料再計算";
			this.buttonRecalcResult.UseVisualStyleBackColor = true;
			this.buttonRecalcResult.Click += new System.EventHandler(this.buttonRecalcResult_Click);
			// 
			// labelSaleFilename
			// 
			this.labelSaleFilename.BackColor = System.Drawing.Color.White;
			this.labelSaleFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelSaleFilename.Location = new System.Drawing.Point(109, 9);
			this.labelSaleFilename.Name = "labelSaleFilename";
			this.labelSaleFilename.Size = new System.Drawing.Size(408, 23);
			this.labelSaleFilename.TabIndex = 1;
			this.labelSaleFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelStockFilename
			// 
			this.labelStockFilename.BackColor = System.Drawing.Color.White;
			this.labelStockFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelStockFilename.Location = new System.Drawing.Point(897, 9);
			this.labelStockFilename.Name = "labelStockFilename";
			this.labelStockFilename.Size = new System.Drawing.Size(408, 23);
			this.labelStockFilename.TabIndex = 5;
			this.labelStockFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonReadStockFile
			// 
			this.buttonReadStockFile.Location = new System.Drawing.Point(1306, 9);
			this.buttonReadStockFile.Name = "buttonReadStockFile";
			this.buttonReadStockFile.Size = new System.Drawing.Size(75, 23);
			this.buttonReadStockFile.TabIndex = 6;
			this.buttonReadStockFile.Text = "ファイル読込";
			this.buttonReadStockFile.UseVisualStyleBackColor = true;
			this.buttonReadStockFile.Click += new System.EventHandler(this.buttonReadStockFile_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1394, 772);
			this.Controls.Add(this.labelStockFilename);
			this.Controls.Add(this.buttonReadStockFile);
			this.Controls.Add(this.labelSaleFilename);
			this.Controls.Add(this.buttonRecalcResult);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewStock);
			this.Controls.Add(this.buttonReadSaleFile);
			this.Controls.Add(this.dataGridViewSale);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PCA仕入データ業務委託手数料再計算ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewSale;
		private System.Windows.Forms.Button buttonReadSaleFile;
		private System.Windows.Forms.DataGridView dataGridViewStock;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonRecalcResult;
		private System.Windows.Forms.Label labelSaleFilename;
		private System.Windows.Forms.Label labelStockFilename;
		private System.Windows.Forms.Button buttonReadStockFile;
	}
}

