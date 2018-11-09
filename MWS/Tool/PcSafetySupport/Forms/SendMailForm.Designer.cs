namespace PcSafetySupport.Forms
{
	partial class SendMailForm
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
			this.dataGridViewMail = new System.Windows.Forms.DataGridView();
			this.buttonSendMail = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonStart = new System.Windows.Forms.RadioButton();
			this.radioButtonGuide = new System.Windows.Forms.RadioButton();
			this.radioButtonUpdate = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewMail
			// 
			this.dataGridViewMail.AllowUserToAddRows = false;
			this.dataGridViewMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMail.Location = new System.Drawing.Point(13, 116);
			this.dataGridViewMail.Name = "dataGridViewMail";
			this.dataGridViewMail.RowHeadersVisible = false;
			this.dataGridViewMail.RowTemplate.Height = 21;
			this.dataGridViewMail.Size = new System.Drawing.Size(1261, 512);
			this.dataGridViewMail.TabIndex = 0;
			// 
			// buttonSendMail
			// 
			this.buttonSendMail.Location = new System.Drawing.Point(365, 12);
			this.buttonSendMail.Name = "buttonSendMail";
			this.buttonSendMail.Size = new System.Drawing.Size(75, 53);
			this.buttonSendMail.TabIndex = 1;
			this.buttonSendMail.Text = "メール送信";
			this.buttonSendMail.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonUpdate);
			this.groupBox1.Controls.Add(this.radioButtonGuide);
			this.groupBox1.Controls.Add(this.radioButtonStart);
			this.groupBox1.Location = new System.Drawing.Point(13, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(346, 53);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "送信対象";
			// 
			// radioButtonStart
			// 
			this.radioButtonStart.AutoSize = true;
			this.radioButtonStart.Location = new System.Drawing.Point(18, 19);
			this.radioButtonStart.Name = "radioButtonStart";
			this.radioButtonStart.Size = new System.Drawing.Size(75, 16);
			this.radioButtonStart.TabIndex = 0;
			this.radioButtonStart.TabStop = true;
			this.radioButtonStart.Text = "開始メール";
			this.radioButtonStart.UseVisualStyleBackColor = true;
			// 
			// radioButtonGuide
			// 
			this.radioButtonGuide.AutoSize = true;
			this.radioButtonGuide.Location = new System.Drawing.Point(99, 19);
			this.radioButtonGuide.Name = "radioButtonGuide";
			this.radioButtonGuide.Size = new System.Drawing.Size(123, 16);
			this.radioButtonGuide.TabIndex = 1;
			this.radioButtonGuide.TabStop = true;
			this.radioButtonGuide.Text = "契約更新案内メール";
			this.radioButtonGuide.UseVisualStyleBackColor = true;
			// 
			// radioButtonUpdate
			// 
			this.radioButtonUpdate.AutoSize = true;
			this.radioButtonUpdate.Location = new System.Drawing.Point(228, 19);
			this.radioButtonUpdate.Name = "radioButtonUpdate";
			this.radioButtonUpdate.Size = new System.Drawing.Size(99, 16);
			this.radioButtonUpdate.TabIndex = 2;
			this.radioButtonUpdate.TabStop = true;
			this.radioButtonUpdate.Text = "契約更新メール";
			this.radioButtonUpdate.UseVisualStyleBackColor = true;
			// 
			// SendMailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1286, 668);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonSendMail);
			this.Controls.Add(this.dataGridViewMail);
			this.Name = "SendMailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "メール送信";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewMail;
		private System.Windows.Forms.Button buttonSendMail;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonUpdate;
		private System.Windows.Forms.RadioButton radioButtonGuide;
		private System.Windows.Forms.RadioButton radioButtonStart;
	}
}