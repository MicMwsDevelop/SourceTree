namespace OnlineLicenseSubsidyCustomerList.Forms
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
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxCusomerPathname = new System.Windows.Forms.TextBox();
			this.buttonSelectCustomer = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxDestinationPathname = new System.Windows.Forms.TextBox();
			this.buttonSelectDestination = new System.Windows.Forms.Button();
			this.buttonExec = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 17);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(181, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "■オン資補助金申請書類顧客情報";
			// 
			// textBoxCusomerPathname
			// 
			this.textBoxCusomerPathname.BackColor = System.Drawing.Color.White;
			this.textBoxCusomerPathname.Location = new System.Drawing.Point(13, 41);
			this.textBoxCusomerPathname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.textBoxCusomerPathname.Name = "textBoxCusomerPathname";
			this.textBoxCusomerPathname.ReadOnly = true;
			this.textBoxCusomerPathname.Size = new System.Drawing.Size(777, 23);
			this.textBoxCusomerPathname.TabIndex = 1;
			// 
			// buttonSelectCustomer
			// 
			this.buttonSelectCustomer.Location = new System.Drawing.Point(790, 41);
			this.buttonSelectCustomer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonSelectCustomer.Name = "buttonSelectCustomer";
			this.buttonSelectCustomer.Size = new System.Drawing.Size(32, 26);
			this.buttonSelectCustomer.TabIndex = 2;
			this.buttonSelectCustomer.Text = "▼";
			this.buttonSelectCustomer.UseVisualStyleBackColor = true;
			this.buttonSelectCustomer.Click += new System.EventHandler(this.buttonSelectCustomer_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 86);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "■送付先リスト";
			// 
			// textBoxDestinationPathname
			// 
			this.textBoxDestinationPathname.BackColor = System.Drawing.Color.White;
			this.textBoxDestinationPathname.Location = new System.Drawing.Point(13, 110);
			this.textBoxDestinationPathname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.textBoxDestinationPathname.Name = "textBoxDestinationPathname";
			this.textBoxDestinationPathname.ReadOnly = true;
			this.textBoxDestinationPathname.Size = new System.Drawing.Size(777, 23);
			this.textBoxDestinationPathname.TabIndex = 4;
			// 
			// buttonSelectDestination
			// 
			this.buttonSelectDestination.Location = new System.Drawing.Point(790, 110);
			this.buttonSelectDestination.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonSelectDestination.Name = "buttonSelectDestination";
			this.buttonSelectDestination.Size = new System.Drawing.Size(32, 26);
			this.buttonSelectDestination.TabIndex = 5;
			this.buttonSelectDestination.Text = "▼";
			this.buttonSelectDestination.UseVisualStyleBackColor = true;
			this.buttonSelectDestination.Click += new System.EventHandler(this.buttonSelectDestination_Click);
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(556, 145);
			this.buttonExec.Margin = new System.Windows.Forms.Padding(4);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(266, 84);
			this.buttonExec.TabIndex = 6;
			this.buttonExec.Text = "オン資補助金申請書類顧客情報の出力";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 247);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxCusomerPathname);
			this.Controls.Add(this.buttonSelectCustomer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxDestinationPathname);
			this.Controls.Add(this.buttonSelectDestination);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オン資補助金申請書類顧客情報抽出";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxCusomerPathname;
		private System.Windows.Forms.Button buttonSelectCustomer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxDestinationPathname;
		private System.Windows.Forms.Button buttonSelectDestination;
		private System.Windows.Forms.Button buttonExec;
	}
}

