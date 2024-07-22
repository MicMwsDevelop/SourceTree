namespace MwsServiceCancelTool.Forms
{
	partial class PcSupportEndForm
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
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelCustomerName = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.dataGridViewPcSupport = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dataGridViewCui = new System.Windows.Forms.DataGridView();
			this.labelCustomerNo = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelContractEndDate = new System.Windows.Forms.Label();
			this.labelGoodsID = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.labelBillingEndDate = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelPeriodEndDate = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.labelUseEndDate = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label1Warning = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPcSupport)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCui)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Enabled = false;
			this.buttonOK.Location = new System.Drawing.Point(901, 9);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(156, 32);
			this.buttonOK.TabIndex = 21;
			this.buttonOK.Text = "PC安心サポート終了処理";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(1063, 9);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 32);
			this.buttonCancel.TabIndex = 22;
			this.buttonCancel.Text = "閉じる";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(282, 9);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(488, 24);
			this.labelCustomerName.TabIndex = 3;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(216, 12);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60, 17);
			this.label12.TabIndex = 2;
			this.label12.Text = "■顧客名";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(12, 12);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(65, 17);
			this.label13.TabIndex = 0;
			this.label13.Text = "■顧客No";
			// 
			// dataGridViewPcSupport
			// 
			this.dataGridViewPcSupport.AllowUserToAddRows = false;
			this.dataGridViewPcSupport.AllowUserToDeleteRows = false;
			this.dataGridViewPcSupport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewPcSupport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPcSupport.Location = new System.Drawing.Point(12, 68);
			this.dataGridViewPcSupport.MultiSelect = false;
			this.dataGridViewPcSupport.Name = "dataGridViewPcSupport";
			this.dataGridViewPcSupport.ReadOnly = true;
			this.dataGridViewPcSupport.RowHeadersVisible = false;
			this.dataGridViewPcSupport.RowTemplate.Height = 21;
			this.dataGridViewPcSupport.Size = new System.Drawing.Size(1126, 65);
			this.dataGridViewPcSupport.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(319, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "■PC安心サポート契約情報（T_USE_PCCSUPPORT）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 181);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(582, 17);
			this.label3.TabIndex = 14;
			this.label3.Text = "■顧客管理用情報（T_CUSSTOMER_USE_INFOMATION）クラウドバックアップ(PC安心サポート Plus) ";
			// 
			// dataGridViewCui
			// 
			this.dataGridViewCui.AllowUserToAddRows = false;
			this.dataGridViewCui.AllowUserToDeleteRows = false;
			this.dataGridViewCui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewCui.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCui.Location = new System.Drawing.Point(12, 201);
			this.dataGridViewCui.MultiSelect = false;
			this.dataGridViewCui.Name = "dataGridViewCui";
			this.dataGridViewCui.ReadOnly = true;
			this.dataGridViewCui.RowHeadersVisible = false;
			this.dataGridViewCui.RowTemplate.Height = 21;
			this.dataGridViewCui.Size = new System.Drawing.Size(1126, 87);
			this.dataGridViewCui.TabIndex = 15;
			// 
			// labelCustomerNo
			// 
			this.labelCustomerNo.BackColor = System.Drawing.Color.White;
			this.labelCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerNo.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelCustomerNo.Location = new System.Drawing.Point(83, 9);
			this.labelCustomerNo.Name = "labelCustomerNo";
			this.labelCustomerNo.Size = new System.Drawing.Size(127, 24);
			this.labelCustomerNo.TabIndex = 1;
			this.labelCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(282, 145);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 17);
			this.label2.TabIndex = 10;
			this.label2.Text = "契約終了日";
			// 
			// labelContractEndDate
			// 
			this.labelContractEndDate.BackColor = System.Drawing.Color.White;
			this.labelContractEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelContractEndDate.Location = new System.Drawing.Point(361, 141);
			this.labelContractEndDate.Name = "labelContractEndDate";
			this.labelContractEndDate.Size = new System.Drawing.Size(99, 24);
			this.labelContractEndDate.TabIndex = 11;
			this.labelContractEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelGoodsID
			// 
			this.labelGoodsID.BackColor = System.Drawing.Color.White;
			this.labelGoodsID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelGoodsID.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelGoodsID.Location = new System.Drawing.Point(186, 141);
			this.labelGoodsID.Name = "labelGoodsID";
			this.labelGoodsID.Size = new System.Drawing.Size(90, 24);
			this.labelGoodsID.TabIndex = 9;
			this.labelGoodsID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(117, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 17);
			this.label6.TabIndex = 8;
			this.label6.Text = "商品コード";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(13, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 17);
			this.label7.TabIndex = 7;
			this.label7.Text = "※処理後の情報";
			// 
			// labelBillingEndDate
			// 
			this.labelBillingEndDate.BackColor = System.Drawing.Color.White;
			this.labelBillingEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelBillingEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelBillingEndDate.Location = new System.Drawing.Point(545, 141);
			this.labelBillingEndDate.Name = "labelBillingEndDate";
			this.labelBillingEndDate.Size = new System.Drawing.Size(99, 24);
			this.labelBillingEndDate.TabIndex = 13;
			this.labelBillingEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(466, 145);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(73, 17);
			this.label9.TabIndex = 12;
			this.label9.Text = "課金終了日";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(13, 301);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(98, 17);
			this.label10.TabIndex = 16;
			this.label10.Text = "※処理後の情報";
			// 
			// labelPeriodEndDate
			// 
			this.labelPeriodEndDate.BackColor = System.Drawing.Color.White;
			this.labelPeriodEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelPeriodEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelPeriodEndDate.Location = new System.Drawing.Point(379, 299);
			this.labelPeriodEndDate.Name = "labelPeriodEndDate";
			this.labelPeriodEndDate.Size = new System.Drawing.Size(99, 24);
			this.labelPeriodEndDate.TabIndex = 20;
			this.labelPeriodEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(300, 301);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(73, 17);
			this.label14.TabIndex = 19;
			this.label14.Text = "利用期限日";
			// 
			// labelUseEndDate
			// 
			this.labelUseEndDate.BackColor = System.Drawing.Color.White;
			this.labelUseEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelUseEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelUseEndDate.Location = new System.Drawing.Point(195, 297);
			this.labelUseEndDate.Name = "labelUseEndDate";
			this.labelUseEndDate.Size = new System.Drawing.Size(99, 24);
			this.labelUseEndDate.TabIndex = 18;
			this.labelUseEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(117, 301);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73, 17);
			this.label16.TabIndex = 17;
			this.label16.Text = "課金終了日";
			// 
			// label1Warning
			// 
			this.label1Warning.AutoSize = true;
			this.label1Warning.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1Warning.ForeColor = System.Drawing.Color.Red;
			this.label1Warning.Location = new System.Drawing.Point(337, 48);
			this.label1Warning.Name = "label1Warning";
			this.label1Warning.Size = new System.Drawing.Size(361, 17);
			this.label1Warning.TabIndex = 6;
			this.label1Warning.Text = "※PC安心サポートが更新されていないので、終了処理はできません";
			this.label1Warning.Visible = false;
			// 
			// PcSupportEndForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1152, 332);
			this.Controls.Add(this.label1Warning);
			this.Controls.Add(this.labelPeriodEndDate);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.labelUseEndDate);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.labelBillingEndDate);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.labelGoodsID);
			this.Controls.Add(this.labelContractEndDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelCustomerNo);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dataGridViewCui);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewPcSupport);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "PcSupportEndForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PC安心サポート 自動更新後の終了処理";
			this.Load += new System.EventHandler(this.PcSupportCancelForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPcSupport)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCui)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelCustomerName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DataGridView dataGridViewPcSupport;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dataGridViewCui;
		private System.Windows.Forms.Label labelCustomerNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelContractEndDate;
		private System.Windows.Forms.Label labelGoodsID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label labelBillingEndDate;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label labelPeriodEndDate;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label labelUseEndDate;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label1Warning;
	}
}