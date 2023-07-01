namespace PcaInvoiceDataConverter.Forms
{
	partial class AccountTransferForm
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
			this.buttonReadCustomerInfo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonReadList = new System.Windows.Forms.Button();
			this.buttonReadDetail = new System.Windows.Forms.Button();
			this.buttonMakeAccountTransfer = new System.Windows.Forms.Button();
			this.buttonMakeInvoice = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonReadCustomerInfo
			// 
			this.buttonReadCustomerInfo.Location = new System.Drawing.Point(12, 63);
			this.buttonReadCustomerInfo.Name = "buttonReadCustomerInfo";
			this.buttonReadCustomerInfo.Size = new System.Drawing.Size(368, 57);
			this.buttonReadCustomerInfo.TabIndex = 1;
			this.buttonReadCustomerInfo.Text = "顧客情報読込み";
			this.buttonReadCustomerInfo.UseVisualStyleBackColor = true;
			this.buttonReadCustomerInfo.Click += new System.EventHandler(this.buttonReadCustomerInfo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(88, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(214, 30);
			this.label1.TabIndex = 2;
			this.label1.Text = "口座振替データ作成";
			// 
			// buttonReadList
			// 
			this.buttonReadList.Location = new System.Drawing.Point(12, 126);
			this.buttonReadList.Name = "buttonReadList";
			this.buttonReadList.Size = new System.Drawing.Size(368, 57);
			this.buttonReadList.TabIndex = 3;
			this.buttonReadList.Text = "請求一覧データ読込み";
			this.buttonReadList.UseVisualStyleBackColor = true;
			this.buttonReadList.Click += new System.EventHandler(this.buttonReadList_Click);
			// 
			// buttonReadDetail
			// 
			this.buttonReadDetail.Location = new System.Drawing.Point(12, 189);
			this.buttonReadDetail.Name = "buttonReadDetail";
			this.buttonReadDetail.Size = new System.Drawing.Size(368, 57);
			this.buttonReadDetail.TabIndex = 4;
			this.buttonReadDetail.Text = "請求明細データ読込み";
			this.buttonReadDetail.UseVisualStyleBackColor = true;
			this.buttonReadDetail.Click += new System.EventHandler(this.buttonReadDetail_Click);
			// 
			// buttonMakeAccountTransfer
			// 
			this.buttonMakeAccountTransfer.Location = new System.Drawing.Point(12, 252);
			this.buttonMakeAccountTransfer.Name = "buttonMakeAccountTransfer";
			this.buttonMakeAccountTransfer.Size = new System.Drawing.Size(368, 57);
			this.buttonMakeAccountTransfer.TabIndex = 5;
			this.buttonMakeAccountTransfer.Text = "口座振替データ作成";
			this.buttonMakeAccountTransfer.UseVisualStyleBackColor = true;
			this.buttonMakeAccountTransfer.Click += new System.EventHandler(this.buttonMakeAccountTransfer_Click);
			// 
			// buttonMakeInvoice
			// 
			this.buttonMakeInvoice.Location = new System.Drawing.Point(12, 315);
			this.buttonMakeInvoice.Name = "buttonMakeInvoice";
			this.buttonMakeInvoice.Size = new System.Drawing.Size(368, 57);
			this.buttonMakeInvoice.TabIndex = 6;
			this.buttonMakeInvoice.Text = "請求書データ作成";
			this.buttonMakeInvoice.UseVisualStyleBackColor = true;
			this.buttonMakeInvoice.Click += new System.EventHandler(this.buttonMakeInvoice_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Location = new System.Drawing.Point(12, 380);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(368, 57);
			this.buttonExit.TabIndex = 7;
			this.buttonExit.Text = "閉じる";
			this.buttonExit.UseVisualStyleBackColor = true;
			// 
			// AccountTransferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(396, 449);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonMakeInvoice);
			this.Controls.Add(this.buttonMakeAccountTransfer);
			this.Controls.Add(this.buttonReadDetail);
			this.Controls.Add(this.buttonReadList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonReadCustomerInfo);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AccountTransferForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "口座振替データ作成";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonReadCustomerInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonReadList;
		private System.Windows.Forms.Button buttonReadDetail;
		private System.Windows.Forms.Button buttonMakeAccountTransfer;
		private System.Windows.Forms.Button buttonMakeInvoice;
		private System.Windows.Forms.Button buttonExit;
	}
}