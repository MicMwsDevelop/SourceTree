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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PcSupportEndForm));
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
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPcSupport)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCui)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Enabled = false;
			this.buttonOK.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOK.Location = new System.Drawing.Point(806, 6);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(162, 63);
			this.buttonOK.TabIndex = 22;
			this.buttonOK.Text = "終了処理";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(806, 73);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(160, 63);
			this.buttonCancel.TabIndex = 23;
			this.buttonCancel.Text = "閉じる";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(261, 6);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(450, 20);
			this.labelCustomerName.TabIndex = 3;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(206, 10);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 11);
			this.label12.TabIndex = 2;
			this.label12.Text = "■顧客名";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label13.Location = new System.Drawing.Point(13, 10);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(54, 11);
			this.label13.TabIndex = 0;
			this.label13.Text = "■顧客No";
			// 
			// dataGridViewPcSupport
			// 
			this.dataGridViewPcSupport.AllowUserToAddRows = false;
			this.dataGridViewPcSupport.AllowUserToDeleteRows = false;
			this.dataGridViewPcSupport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewPcSupport.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridViewPcSupport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPcSupport.Location = new System.Drawing.Point(12, 187);
			this.dataGridViewPcSupport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridViewPcSupport.MultiSelect = false;
			this.dataGridViewPcSupport.Name = "dataGridViewPcSupport";
			this.dataGridViewPcSupport.ReadOnly = true;
			this.dataGridViewPcSupport.RowHeadersVisible = false;
			this.dataGridViewPcSupport.RowTemplate.Height = 21;
			this.dataGridViewPcSupport.Size = new System.Drawing.Size(954, 62);
			this.dataGridViewPcSupport.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(13, 172);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(275, 11);
			this.label1.TabIndex = 5;
			this.label1.Text = "■PC安心サポート契約情報（T_USE_PCCSUPPORT）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(13, 284);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(528, 11);
			this.label3.TabIndex = 15;
			this.label3.Text = "■顧客管理用情報（T_CUSSTOMER_USE_INFOMATION）クラウドバックアップ(PC安心サポート Plus) ";
			// 
			// dataGridViewCui
			// 
			this.dataGridViewCui.AllowUserToAddRows = false;
			this.dataGridViewCui.AllowUserToDeleteRows = false;
			this.dataGridViewCui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewCui.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridViewCui.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCui.Location = new System.Drawing.Point(12, 299);
			this.dataGridViewCui.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridViewCui.MultiSelect = false;
			this.dataGridViewCui.Name = "dataGridViewCui";
			this.dataGridViewCui.ReadOnly = true;
			this.dataGridViewCui.RowHeadersVisible = false;
			this.dataGridViewCui.RowTemplate.Height = 21;
			this.dataGridViewCui.Size = new System.Drawing.Size(954, 62);
			this.dataGridViewCui.TabIndex = 16;
			// 
			// labelCustomerNo
			// 
			this.labelCustomerNo.BackColor = System.Drawing.Color.White;
			this.labelCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerNo.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelCustomerNo.Location = new System.Drawing.Point(73, 6);
			this.labelCustomerNo.Name = "labelCustomerNo";
			this.labelCustomerNo.Size = new System.Drawing.Size(127, 20);
			this.labelCustomerNo.TabIndex = 1;
			this.labelCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(265, 258);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 11);
			this.label2.TabIndex = 11;
			this.label2.Text = "契約終了日";
			// 
			// labelContractEndDate
			// 
			this.labelContractEndDate.BackColor = System.Drawing.Color.White;
			this.labelContractEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelContractEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelContractEndDate.Location = new System.Drawing.Point(331, 254);
			this.labelContractEndDate.Name = "labelContractEndDate";
			this.labelContractEndDate.Size = new System.Drawing.Size(99, 20);
			this.labelContractEndDate.TabIndex = 12;
			this.labelContractEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelGoodsID
			// 
			this.labelGoodsID.BackColor = System.Drawing.Color.White;
			this.labelGoodsID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelGoodsID.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelGoodsID.Location = new System.Drawing.Point(169, 254);
			this.labelGoodsID.Name = "labelGoodsID";
			this.labelGoodsID.Size = new System.Drawing.Size(90, 20);
			this.labelGoodsID.TabIndex = 10;
			this.labelGoodsID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(106, 257);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57, 11);
			this.label6.TabIndex = 9;
			this.label6.Text = "商品コード";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(13, 258);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(82, 11);
			this.label7.TabIndex = 8;
			this.label7.Text = "※処理後の情報";
			// 
			// labelBillingEndDate
			// 
			this.labelBillingEndDate.BackColor = System.Drawing.Color.White;
			this.labelBillingEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelBillingEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelBillingEndDate.Location = new System.Drawing.Point(502, 254);
			this.labelBillingEndDate.Name = "labelBillingEndDate";
			this.labelBillingEndDate.Size = new System.Drawing.Size(99, 20);
			this.labelBillingEndDate.TabIndex = 14;
			this.labelBillingEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(436, 257);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 11);
			this.label9.TabIndex = 13;
			this.label9.Text = "課金終了日";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(13, 370);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(82, 11);
			this.label10.TabIndex = 17;
			this.label10.Text = "※処理後の情報";
			// 
			// labelPeriodEndDate
			// 
			this.labelPeriodEndDate.BackColor = System.Drawing.Color.White;
			this.labelPeriodEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelPeriodEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelPeriodEndDate.Location = new System.Drawing.Point(343, 366);
			this.labelPeriodEndDate.Name = "labelPeriodEndDate";
			this.labelPeriodEndDate.Size = new System.Drawing.Size(99, 20);
			this.labelPeriodEndDate.TabIndex = 21;
			this.labelPeriodEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label14.Location = new System.Drawing.Point(277, 370);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(60, 11);
			this.label14.TabIndex = 20;
			this.label14.Text = "利用期限日";
			// 
			// labelUseEndDate
			// 
			this.labelUseEndDate.BackColor = System.Drawing.Color.White;
			this.labelUseEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelUseEndDate.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelUseEndDate.Location = new System.Drawing.Point(172, 366);
			this.labelUseEndDate.Name = "labelUseEndDate";
			this.labelUseEndDate.Size = new System.Drawing.Size(99, 20);
			this.labelUseEndDate.TabIndex = 19;
			this.labelUseEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(106, 370);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60, 11);
			this.label16.TabIndex = 18;
			this.label16.Text = "課金終了日";
			// 
			// label1Warning
			// 
			this.label1Warning.AutoSize = true;
			this.label1Warning.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1Warning.ForeColor = System.Drawing.Color.Red;
			this.label1Warning.Location = new System.Drawing.Point(294, 172);
			this.label1Warning.Name = "label1Warning";
			this.label1Warning.Size = new System.Drawing.Size(332, 11);
			this.label1Warning.TabIndex = 6;
			this.label1Warning.Text = "※PC安心サポートが更新されていないので、終了処理はできません";
			this.label1Warning.Visible = false;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.White;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Location = new System.Drawing.Point(12, 41);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(528, 119);
			this.label5.TabIndex = 4;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// PcSupportEndForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(980, 398);
			this.Controls.Add(this.label5);
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
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
		private System.Windows.Forms.Label label5;
	}
}