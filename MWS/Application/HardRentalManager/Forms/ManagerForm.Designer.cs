namespace HardRentalManager
{
	partial class ManagerForm
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
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelOffice = new System.Windows.Forms.Label();
			this.labelClinicName = new System.Windows.Forms.Label();
			this.labelClinickKana = new System.Windows.Forms.Label();
			this.labelAddress = new System.Windows.Forms.Label();
			this.labelTel = new System.Windows.Forms.Label();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.listViewHeader = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label7 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelVersion = new System.Windows.Forms.Label();
			this.buttonAddNew = new System.Windows.Forms.Button();
			this.labelEndFlag = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.buttonDeleteHeader = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.numericTextBoxCustomerID = new MwsLib.Component.NumericTextBox();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.LightCyan;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(14, 10);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 25);
			this.label5.TabIndex = 0;
			this.label5.Text = "顧客No ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightCyan;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(14, 36);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 25);
			this.label1.TabIndex = 4;
			this.label1.Text = "担当オフィス ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.LightCyan;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(14, 62);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 25);
			this.label2.TabIndex = 6;
			this.label2.Text = "顧客名 ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.LightCyan;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(14, 88);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 25);
			this.label3.TabIndex = 8;
			this.label3.Text = "顧客名カナ ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.LightCyan;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(14, 114);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 25);
			this.label4.TabIndex = 10;
			this.label4.Text = "住所 ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.LightCyan;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(14, 140);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(99, 25);
			this.label6.TabIndex = 12;
			this.label6.Text = "電話番号 ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelOffice
			// 
			this.labelOffice.BackColor = System.Drawing.Color.White;
			this.labelOffice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelOffice.Location = new System.Drawing.Point(114, 36);
			this.labelOffice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelOffice.Name = "labelOffice";
			this.labelOffice.Size = new System.Drawing.Size(634, 25);
			this.labelOffice.TabIndex = 5;
			this.labelOffice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClinicName
			// 
			this.labelClinicName.BackColor = System.Drawing.Color.White;
			this.labelClinicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelClinicName.Location = new System.Drawing.Point(114, 62);
			this.labelClinicName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelClinicName.Name = "labelClinicName";
			this.labelClinicName.Size = new System.Drawing.Size(634, 25);
			this.labelClinicName.TabIndex = 7;
			this.labelClinicName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClinickKana
			// 
			this.labelClinickKana.BackColor = System.Drawing.Color.White;
			this.labelClinickKana.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelClinickKana.Location = new System.Drawing.Point(114, 88);
			this.labelClinickKana.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelClinickKana.Name = "labelClinickKana";
			this.labelClinickKana.Size = new System.Drawing.Size(634, 25);
			this.labelClinickKana.TabIndex = 9;
			this.labelClinickKana.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelAddress
			// 
			this.labelAddress.BackColor = System.Drawing.Color.White;
			this.labelAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAddress.Location = new System.Drawing.Point(114, 114);
			this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(634, 25);
			this.labelAddress.TabIndex = 11;
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelTel
			// 
			this.labelTel.BackColor = System.Drawing.Color.White;
			this.labelTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTel.Location = new System.Drawing.Point(114, 140);
			this.labelTel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTel.Name = "labelTel";
			this.labelTel.Size = new System.Drawing.Size(317, 25);
			this.labelTel.TabIndex = 13;
			this.labelTel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(226, 10);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(63, 25);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// listViewHeader
			// 
			this.listViewHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader11});
			this.listViewHeader.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewHeader.FullRowSelect = true;
			this.listViewHeader.HideSelection = false;
			this.listViewHeader.Location = new System.Drawing.Point(14, 225);
			this.listViewHeader.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.listViewHeader.Name = "listViewHeader";
			this.listViewHeader.Size = new System.Drawing.Size(827, 266);
			this.listViewHeader.TabIndex = 17;
			this.listViewHeader.UseCompatibleStateImageBehavior = false;
			this.listViewHeader.View = System.Windows.Forms.View.Details;
			this.listViewHeader.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewHeader_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "契約番号";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "受付日";
			this.columnHeader2.Width = 75;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "利用月数";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "月額利用料";
			this.columnHeader3.Width = 75;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "利用開始日";
			this.columnHeader5.Width = 75;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "利用終了日";
			this.columnHeader6.Width = 75;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "課金開始日";
			this.columnHeader7.Width = 75;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "課金終了日";
			this.columnHeader8.Width = 75;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "解約日";
			this.columnHeader9.Width = 75;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "ｻｰﾋﾞｽ終了";
			this.columnHeader11.Width = 90;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 209);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "■契約情報";
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(728, 497);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(113, 31);
			this.buttonClose.TabIndex = 21;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(708, 9);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(133, 13);
			this.labelVersion.TabIndex = 3;
			this.labelVersion.Text = "Ver1.00 2025/05/20";
			// 
			// buttonAddNew
			// 
			this.buttonAddNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonAddNew.Location = new System.Drawing.Point(14, 497);
			this.buttonAddNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonAddNew.Name = "buttonAddNew";
			this.buttonAddNew.Size = new System.Drawing.Size(113, 31);
			this.buttonAddNew.TabIndex = 18;
			this.buttonAddNew.Text = "新規申込";
			this.buttonAddNew.UseVisualStyleBackColor = true;
			this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
			// 
			// labelEndFlag
			// 
			this.labelEndFlag.BackColor = System.Drawing.Color.White;
			this.labelEndFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelEndFlag.ForeColor = System.Drawing.Color.Red;
			this.labelEndFlag.Location = new System.Drawing.Point(114, 166);
			this.labelEndFlag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelEndFlag.Name = "labelEndFlag";
			this.labelEndFlag.Size = new System.Drawing.Size(73, 25);
			this.labelEndFlag.TabIndex = 15;
			this.labelEndFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.LightCyan;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(14, 166);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(99, 25);
			this.label9.TabIndex = 14;
			this.label9.Text = "終了届 ";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonDeleteHeader
			// 
			this.buttonDeleteHeader.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonDeleteHeader.Location = new System.Drawing.Point(256, 497);
			this.buttonDeleteHeader.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonDeleteHeader.Name = "buttonDeleteHeader";
			this.buttonDeleteHeader.Size = new System.Drawing.Size(113, 31);
			this.buttonDeleteHeader.TabIndex = 20;
			this.buttonDeleteHeader.Text = "情報削除";
			this.buttonDeleteHeader.UseVisualStyleBackColor = true;
			this.buttonDeleteHeader.Click += new System.EventHandler(this.buttonDeleteHeader_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonModify.Location = new System.Drawing.Point(135, 497);
			this.buttonModify.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(113, 31);
			this.buttonModify.TabIndex = 19;
			this.buttonModify.Text = "情報変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// numericTextBoxCustomerID
			// 
			this.numericTextBoxCustomerID.Location = new System.Drawing.Point(114, 12);
			this.numericTextBoxCustomerID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxCustomerID.MaxLength = 8;
			this.numericTextBoxCustomerID.Name = "numericTextBoxCustomerID";
			this.numericTextBoxCustomerID.Size = new System.Drawing.Size(112, 20);
			this.numericTextBoxCustomerID.TabIndex = 1;
			// 
			// ManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(853, 537);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonDeleteHeader);
			this.Controls.Add(this.labelEndFlag);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.buttonAddNew);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listViewHeader);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.numericTextBoxCustomerID);
			this.Controls.Add(this.labelTel);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.labelClinickKana);
			this.Controls.Add(this.labelClinicName);
			this.Controls.Add(this.labelOffice);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "ManagerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ハードレンタル契約管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelOffice;
		private System.Windows.Forms.Label labelClinicName;
		private System.Windows.Forms.Label labelClinickKana;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.Label labelTel;
		private MwsLib.Component.NumericTextBox numericTextBoxCustomerID;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.ListView listViewHeader;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.Button buttonAddNew;
		private System.Windows.Forms.Label labelEndFlag;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button buttonDeleteHeader;
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.ColumnHeader columnHeader11;
	}
}

