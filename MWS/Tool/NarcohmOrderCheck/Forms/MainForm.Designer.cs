namespace NarcohmOrderCheck.Forms
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
			this.listViewApplicate = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listViewApplicate
			// 
			this.listViewApplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewApplicate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader6,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader9,
            this.columnHeader8,
            this.columnHeader7,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
			this.listViewApplicate.FullRowSelect = true;
			this.listViewApplicate.Location = new System.Drawing.Point(12, 14);
			this.listViewApplicate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.listViewApplicate.MultiSelect = false;
			this.listViewApplicate.Name = "listViewApplicate";
			this.listViewApplicate.Size = new System.Drawing.Size(1291, 665);
			this.listViewApplicate.TabIndex = 0;
			this.listViewApplicate.UseCompatibleStateImageBehavior = false;
			this.listViewApplicate.View = System.Windows.Forms.View.Details;
			this.listViewApplicate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewApplicate_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "申込番号";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "受注番号";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "受注日";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "顧客No";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "医院名";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "件名";
			this.columnHeader6.Width = 250;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "拠店名";
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "担当者";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "サービス開始日";
			this.columnHeader9.Width = 90;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "販売種別";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "商品名";
			this.columnHeader7.Width = 200;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "金額";
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "数量";
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "メール送信日";
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Location = new System.Drawing.Point(12, 687);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 33);
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonModify.Location = new System.Drawing.Point(93, 687);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(75, 33);
			this.buttonModify.TabIndex = 2;
			this.buttonModify.Text = "変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemove.Location = new System.Drawing.Point(174, 687);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(75, 33);
			this.buttonRemove.TabIndex = 3;
			this.buttonRemove.Text = "削除";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1319, 728);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.listViewApplicate);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.Name = "MainForm";
			this.Text = "サービス開始製品申込情報管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.ListView listViewApplicate;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.Button buttonRemove;
	}
}

