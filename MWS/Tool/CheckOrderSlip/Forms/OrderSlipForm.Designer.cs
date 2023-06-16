namespace CheckOrderSlip.Forms
{
	partial class OrderSlipForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxOrderNo = new System.Windows.Forms.TextBox();
			this.textBoxCustomerNo = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxCustomerName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "受注番号";
			// 
			// textBoxOrderNo
			// 
			this.textBoxOrderNo.Location = new System.Drawing.Point(71, 17);
			this.textBoxOrderNo.Name = "textBoxOrderNo";
			this.textBoxOrderNo.Size = new System.Drawing.Size(100, 19);
			this.textBoxOrderNo.TabIndex = 1;
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.Location = new System.Drawing.Point(71, 47);
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.Size = new System.Drawing.Size(100, 19);
			this.textBoxCustomerNo.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "顧客No";
			// 
			// textBoxCustomerName
			// 
			this.textBoxCustomerName.Location = new System.Drawing.Point(71, 77);
			this.textBoxCustomerName.Name = "textBoxCustomerName";
			this.textBoxCustomerName.Size = new System.Drawing.Size(335, 19);
			this.textBoxCustomerName.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "顧客名";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "件名";
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Location = new System.Drawing.Point(71, 106);
			this.textBoxTitle.Multiline = true;
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.Size = new System.Drawing.Size(335, 92);
			this.textBoxTitle.TabIndex = 7;
			// 
			// OrderSlipForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(436, 218);
			this.Controls.Add(this.textBoxTitle);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxCustomerName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxOrderNo);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OrderSlipForm";
			this.Text = "伝票情報";
			this.Load += new System.EventHandler(this.OrderSlipForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxOrderNo;
		private System.Windows.Forms.TextBox textBoxCustomerNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxCustomerName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxTitle;
	}
}