namespace MwsServiceCancelTool.Forms
{
	partial class PcSupportCancelForm
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
			this.labelMessage = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dataGridViewApply = new System.Windows.Forms.DataGridView();
			this.label4 = new System.Windows.Forms.Label();
			this.labelCustomerNo = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPcSupport)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewApply)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Enabled = false;
			this.buttonOK.Location = new System.Drawing.Point(867, 9);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(140, 32);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "PC安心サポート削除";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(1013, 9);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 32);
			this.buttonCancel.TabIndex = 11;
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
			this.dataGridViewPcSupport.Location = new System.Drawing.Point(11, 66);
			this.dataGridViewPcSupport.MultiSelect = false;
			this.dataGridViewPcSupport.Name = "dataGridViewPcSupport";
			this.dataGridViewPcSupport.ReadOnly = true;
			this.dataGridViewPcSupport.RowHeadersVisible = false;
			this.dataGridViewPcSupport.RowTemplate.Height = 21;
			this.dataGridViewPcSupport.Size = new System.Drawing.Size(1076, 65);
			this.dataGridViewPcSupport.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(319, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "■PC安心サポート契約情報（T_USE_PCCSUPPORT）";
			// 
			// labelMessage
			// 
			this.labelMessage.AutoSize = true;
			this.labelMessage.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelMessage.ForeColor = System.Drawing.Color.Red;
			this.labelMessage.Location = new System.Drawing.Point(336, 46);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(337, 17);
			this.labelMessage.TabIndex = 6;
			this.labelMessage.Text = "※売上データ作成済みのため、利用申込の取消はできません。";
			this.labelMessage.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(177, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "■カプラー申込情報（APPLY）";
			// 
			// dataGridViewApply
			// 
			this.dataGridViewApply.AllowUserToAddRows = false;
			this.dataGridViewApply.AllowUserToDeleteRows = false;
			this.dataGridViewApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewApply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewApply.Location = new System.Drawing.Point(12, 164);
			this.dataGridViewApply.MultiSelect = false;
			this.dataGridViewApply.Name = "dataGridViewApply";
			this.dataGridViewApply.ReadOnly = true;
			this.dataGridViewApply.RowHeadersVisible = false;
			this.dataGridViewApply.RowTemplate.Height = 21;
			this.dataGridViewApply.Size = new System.Drawing.Size(1076, 87);
			this.dataGridViewApply.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(195, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(480, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "※クラウドバックアップ(PC安心サポート Plus) 管理画面で利用申込を取消してください。";
			// 
			// labelCustomerNo
			// 
			this.labelCustomerNo.BackColor = System.Drawing.Color.White;
			this.labelCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerNo.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelCustomerNo.Location = new System.Drawing.Point(80, 9);
			this.labelCustomerNo.Name = "labelCustomerNo";
			this.labelCustomerNo.Size = new System.Drawing.Size(127, 24);
			this.labelCustomerNo.TabIndex = 1;
			this.labelCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PcSupportCancelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1102, 260);
			this.Controls.Add(this.labelCustomerNo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dataGridViewApply);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewPcSupport);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "PcSupportCancelForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PC安心サポート 利用申込取消";
			this.Load += new System.EventHandler(this.PcSupportCancelForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPcSupport)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewApply)).EndInit();
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
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dataGridViewApply;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelCustomerNo;
	}
}