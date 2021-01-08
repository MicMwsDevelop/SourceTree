
namespace AnalysisRecommendService
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
			this.label1 = new System.Windows.Forms.Label();
			this.listViewTargetService = new System.Windows.Forms.ListView();
			this.columnHeaderServiceID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderServiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonSearch = new System.Windows.Forms.Button();
			this.numericTextBoxTargetCustomerNo = new MwsLib.Component.NumericTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listViewRecommendService = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.comboBoxRecommend = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "顧客No";
			// 
			// listViewTargetService
			// 
			this.listViewTargetService.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderServiceID,
            this.columnHeaderServiceName});
			this.listViewTargetService.FullRowSelect = true;
			this.listViewTargetService.HideSelection = false;
			this.listViewTargetService.Location = new System.Drawing.Point(15, 38);
			this.listViewTargetService.MultiSelect = false;
			this.listViewTargetService.Name = "listViewTargetService";
			this.listViewTargetService.Size = new System.Drawing.Size(380, 616);
			this.listViewTargetService.TabIndex = 3;
			this.listViewTargetService.UseCompatibleStateImageBehavior = false;
			this.listViewTargetService.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderServiceID
			// 
			this.columnHeaderServiceID.Text = "サービスID";
			// 
			// columnHeaderServiceName
			// 
			this.columnHeaderServiceName.Text = "サービス名";
			this.columnHeaderServiceName.Width = 300;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(168, 10);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 4;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// numericTextBoxTargetCustomerNo
			// 
			this.numericTextBoxTargetCustomerNo.Location = new System.Drawing.Point(62, 10);
			this.numericTextBoxTargetCustomerNo.MaxLength = 8;
			this.numericTextBoxTargetCustomerNo.Name = "numericTextBoxTargetCustomerNo";
			this.numericTextBoxTargetCustomerNo.Size = new System.Drawing.Size(100, 19);
			this.numericTextBoxTargetCustomerNo.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(414, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "顧客No";
			// 
			// listViewRecommendService
			// 
			this.listViewRecommendService.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listViewRecommendService.FullRowSelect = true;
			this.listViewRecommendService.HideSelection = false;
			this.listViewRecommendService.Location = new System.Drawing.Point(416, 38);
			this.listViewRecommendService.MultiSelect = false;
			this.listViewRecommendService.Name = "listViewRecommendService";
			this.listViewRecommendService.Size = new System.Drawing.Size(380, 616);
			this.listViewRecommendService.TabIndex = 7;
			this.listViewRecommendService.UseCompatibleStateImageBehavior = false;
			this.listViewRecommendService.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "サービスID";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "サービス名";
			this.columnHeader2.Width = 300;
			// 
			// comboBoxRecommend
			// 
			this.comboBoxRecommend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRecommend.FormattingEnabled = true;
			this.comboBoxRecommend.Location = new System.Drawing.Point(463, 10);
			this.comboBoxRecommend.Name = "comboBoxRecommend";
			this.comboBoxRecommend.Size = new System.Drawing.Size(121, 20);
			this.comboBoxRecommend.TabIndex = 8;
			this.comboBoxRecommend.SelectedIndexChanged += new System.EventHandler(this.comboBoxRecommend_SelectedIndexChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(813, 666);
			this.Controls.Add(this.comboBoxRecommend);
			this.Controls.Add(this.listViewRecommendService);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.listViewTargetService);
			this.Controls.Add(this.numericTextBoxTargetCustomerNo);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "協調フィルタリング";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox numericTextBoxTargetCustomerNo;
		private System.Windows.Forms.ListView listViewTargetService;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceID;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceName;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView listViewRecommendService;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ComboBox comboBoxRecommend;
	}
}

