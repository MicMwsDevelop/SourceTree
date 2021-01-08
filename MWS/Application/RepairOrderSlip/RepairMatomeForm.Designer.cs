
namespace RepairOrderSlip
{
	partial class RepairMatomeForm
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
			this.dataGridViewContractHeader = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.numericTextBoxCustomerNo = new MwsLib.Component.NumericTextBox();
			this.labelCustomerName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.dataGridViewContractDetail = new System.Windows.Forms.DataGridView();
			this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.listViewApply = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewContractHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewContractDetail)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewContractHeader
			// 
			this.dataGridViewContractHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewContractHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
			this.dataGridViewContractHeader.Location = new System.Drawing.Point(12, 63);
			this.dataGridViewContractHeader.Name = "dataGridViewContractHeader";
			this.dataGridViewContractHeader.RowTemplate.Height = 21;
			this.dataGridViewContractHeader.Size = new System.Drawing.Size(1372, 84);
			this.dataGridViewContractHeader.TabIndex = 0;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "fContractID";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "fContractType";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "fMonths";
			this.Column3.Name = "Column3";
			// 
			// Column4
			// 
			this.Column4.HeaderText = "fGoodsID";
			this.Column4.Name = "Column4";
			// 
			// Column5
			// 
			this.Column5.HeaderText = "fApplyDate";
			this.Column5.Name = "Column5";
			// 
			// Column6
			// 
			this.Column6.HeaderText = "fContractStartDate";
			this.Column6.Name = "Column6";
			// 
			// Column7
			// 
			this.Column7.HeaderText = "fContractEndDate";
			this.Column7.Name = "Column7";
			// 
			// Column8
			// 
			this.Column8.HeaderText = "fBillingStartDate";
			this.Column8.Name = "Column8";
			// 
			// Column9
			// 
			this.Column9.HeaderText = "fBillingEndDate";
			this.Column9.Name = "Column9";
			// 
			// Column10
			// 
			this.Column10.HeaderText = "fCreateDate";
			this.Column10.Name = "Column10";
			this.Column10.ReadOnly = true;
			// 
			// Column11
			// 
			this.Column11.HeaderText = "fCreatePerson";
			this.Column11.Name = "Column11";
			this.Column11.ReadOnly = true;
			// 
			// Column12
			// 
			this.Column12.HeaderText = "fUpdateDate";
			this.Column12.Name = "Column12";
			// 
			// Column13
			// 
			this.Column13.HeaderText = "fUpdatePerson";
			this.Column13.Name = "Column13";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "顧客No";
			// 
			// numericTextBoxCustomerNo
			// 
			this.numericTextBoxCustomerNo.Location = new System.Drawing.Point(62, 13);
			this.numericTextBoxCustomerNo.MaxLength = 8;
			this.numericTextBoxCustomerNo.Name = "numericTextBoxCustomerNo";
			this.numericTextBoxCustomerNo.Size = new System.Drawing.Size(100, 19);
			this.numericTextBoxCustomerNo.TabIndex = 2;
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(249, 13);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(565, 23);
			this.labelCustomerName.TabIndex = 3;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(267, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "まとめ契約ヘッダ情報（T_USE_CONTRACT_HEADER）";
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(168, 11);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 5;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 160);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(258, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "まとめ契約詳細情報（T_USE_CONTRACT_DETAIL）";
			// 
			// dataGridViewContractDetail
			// 
			this.dataGridViewContractDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewContractDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column14,
            this.Column15,
            this.Column16});
			this.dataGridViewContractDetail.Location = new System.Drawing.Point(12, 176);
			this.dataGridViewContractDetail.Name = "dataGridViewContractDetail";
			this.dataGridViewContractDetail.RowTemplate.Height = 21;
			this.dataGridViewContractDetail.Size = new System.Drawing.Size(517, 566);
			this.dataGridViewContractDetail.TabIndex = 7;
			// 
			// Column14
			// 
			this.Column14.HeaderText = "fContractID";
			this.Column14.Name = "Column14";
			this.Column14.ReadOnly = true;
			// 
			// Column15
			// 
			this.Column15.HeaderText = "fSERVICE_ID";
			this.Column15.Name = "Column15";
			// 
			// Column16
			// 
			this.Column16.HeaderText = "fSERVICE_NAME";
			this.Column16.Name = "Column16";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(540, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 12);
			this.label4.TabIndex = 8;
			this.label4.Text = "申込情報（APPLY）";
			// 
			// listViewApply
			// 
			this.listViewApply.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.listViewApply.HideSelection = false;
			this.listViewApply.Location = new System.Drawing.Point(542, 176);
			this.listViewApply.Name = "listViewApply";
			this.listViewApply.Size = new System.Drawing.Size(680, 566);
			this.listViewApply.TabIndex = 9;
			this.listViewApply.UseCompatibleStateImageBehavior = false;
			this.listViewApply.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "cp_id";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "customer_id";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "service_id";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "apply_date";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "apply_type";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "system_flg";
			// 
			// RepairMatomeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1445, 779);
			this.Controls.Add(this.listViewApply);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dataGridViewContractDetail);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.numericTextBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewContractHeader);
			this.Name = "RepairMatomeForm";
			this.Text = "おまとめプラン契約情報修正";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewContractHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewContractDetail)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewContractHeader;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox numericTextBoxCustomerNo;
		private System.Windows.Forms.Label labelCustomerName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dataGridViewContractDetail;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView listViewApply;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
	}
}

