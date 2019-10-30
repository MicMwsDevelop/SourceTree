namespace DemoUserPaletteES.Forms
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
			this.labelPathname = new System.Windows.Forms.Label();
			this.buttonFolder = new System.Windows.Forms.Button();
			this.buttonExec = new System.Windows.Forms.Button();
			this.listViewDemoUser = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label2 = new System.Windows.Forms.Label();
			this.listBoxError = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "■MWS-IDファイル";
			// 
			// labelPathname
			// 
			this.labelPathname.BackColor = System.Drawing.Color.White;
			this.labelPathname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelPathname.Location = new System.Drawing.Point(11, 26);
			this.labelPathname.Name = "labelPathname";
			this.labelPathname.Size = new System.Drawing.Size(488, 32);
			this.labelPathname.TabIndex = 1;
			this.labelPathname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonFolder
			// 
			this.buttonFolder.Location = new System.Drawing.Point(505, 26);
			this.buttonFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonFolder.Name = "buttonFolder";
			this.buttonFolder.Size = new System.Drawing.Size(34, 32);
			this.buttonFolder.TabIndex = 2;
			this.buttonFolder.Text = "▼";
			this.buttonFolder.UseVisualStyleBackColor = true;
			this.buttonFolder.Click += new System.EventHandler(this.buttonFolder_Click);
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(452, 590);
			this.buttonExec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(87, 49);
			this.buttonExec.TabIndex = 7;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// listViewDemoUser
			// 
			this.listViewDemoUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.listViewDemoUser.FullRowSelect = true;
			this.listViewDemoUser.HideSelection = false;
			this.listViewDemoUser.Location = new System.Drawing.Point(11, 84);
			this.listViewDemoUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.listViewDemoUser.Name = "listViewDemoUser";
			this.listViewDemoUser.Size = new System.Drawing.Size(528, 498);
			this.listViewDemoUser.TabIndex = 4;
			this.listViewDemoUser.UseCompatibleStateImageBehavior = false;
			this.listViewDemoUser.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "MWS-ID";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "顧客No";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "顧客名";
			this.columnHeader3.Width = 280;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 586);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "■カプラー未登録";
			// 
			// listBoxError
			// 
			this.listBoxError.FormattingEnabled = true;
			this.listBoxError.ItemHeight = 17;
			this.listBoxError.Location = new System.Drawing.Point(11, 607);
			this.listBoxError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.listBoxError.Name = "listBoxError";
			this.listBoxError.Size = new System.Drawing.Size(277, 106);
			this.listBoxError.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(198, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "■palette ES サービス設定対象リスト";
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(347, 696);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(193, 17);
			this.labelVersion.TabIndex = 8;
			this.labelVersion.Text = "バージョン：Ver1.00(2019/10/25)";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(552, 726);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listBoxError);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listViewDemoUser);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.buttonFolder);
			this.Controls.Add(this.labelPathname);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "デモユーザー palette ESサービス設定";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPathname;
        private System.Windows.Forms.Button buttonFolder;
        private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.ListView listViewDemoUser;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listBoxError;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelVersion;
	}
}

