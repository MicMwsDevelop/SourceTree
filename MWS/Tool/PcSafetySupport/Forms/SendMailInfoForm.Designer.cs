namespace PcSafetySupport.Forms
{
	partial class SendMailInfoForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonUpdateMail = new System.Windows.Forms.RadioButton();
			this.radioButtonGuideMail = new System.Windows.Forms.RadioButton();
			this.radioButtonStartMail = new System.Windows.Forms.RadioButton();
			this.dataGridViewMail = new System.Windows.Forms.DataGridView();
			this.buttonSendMail = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonUpdateMail);
			this.groupBox1.Controls.Add(this.radioButtonGuideMail);
			this.groupBox1.Controls.Add(this.radioButtonStartMail);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(580, 54);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "メール種別";
			// 
			// radioButtonUpdateMail
			// 
			this.radioButtonUpdateMail.AutoSize = true;
			this.radioButtonUpdateMail.Location = new System.Drawing.Point(378, 22);
			this.radioButtonUpdateMail.Name = "radioButtonUpdateMail";
			this.radioButtonUpdateMail.Size = new System.Drawing.Size(99, 16);
			this.radioButtonUpdateMail.TabIndex = 2;
			this.radioButtonUpdateMail.Text = "契約更新メール";
			this.radioButtonUpdateMail.UseVisualStyleBackColor = true;
			this.radioButtonUpdateMail.CheckedChanged += new System.EventHandler(this.radioButtonUpdateMail_CheckedChanged);
			// 
			// radioButtonGuideMail
			// 
			this.radioButtonGuideMail.AutoSize = true;
			this.radioButtonGuideMail.Location = new System.Drawing.Point(249, 22);
			this.radioButtonGuideMail.Name = "radioButtonGuideMail";
			this.radioButtonGuideMail.Size = new System.Drawing.Size(123, 16);
			this.radioButtonGuideMail.TabIndex = 1;
			this.radioButtonGuideMail.Text = "契約更新案内メール";
			this.radioButtonGuideMail.UseVisualStyleBackColor = true;
			this.radioButtonGuideMail.CheckedChanged += new System.EventHandler(this.radioButtonGuideMail_CheckedChanged);
			// 
			// radioButtonStartMail
			// 
			this.radioButtonStartMail.AutoSize = true;
			this.radioButtonStartMail.Location = new System.Drawing.Point(168, 22);
			this.radioButtonStartMail.Name = "radioButtonStartMail";
			this.radioButtonStartMail.Size = new System.Drawing.Size(75, 16);
			this.radioButtonStartMail.TabIndex = 0;
			this.radioButtonStartMail.Text = "開始メール";
			this.radioButtonStartMail.UseVisualStyleBackColor = true;
			this.radioButtonStartMail.CheckedChanged += new System.EventHandler(this.radioButtonStartMail_CheckedChanged);
			// 
			// dataGridViewMail
			// 
			this.dataGridViewMail.AllowUserToAddRows = false;
			this.dataGridViewMail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMail.Location = new System.Drawing.Point(13, 73);
			this.dataGridViewMail.MultiSelect = false;
			this.dataGridViewMail.Name = "dataGridViewMail";
			this.dataGridViewMail.ReadOnly = true;
			this.dataGridViewMail.RowHeadersVisible = false;
			this.dataGridViewMail.RowTemplate.Height = 21;
			this.dataGridViewMail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewMail.Size = new System.Drawing.Size(1313, 725);
			this.dataGridViewMail.TabIndex = 4;
			// 
			// buttonSendMail
			// 
			this.buttonSendMail.Location = new System.Drawing.Point(771, 16);
			this.buttonSendMail.Name = "buttonSendMail";
			this.buttonSendMail.Size = new System.Drawing.Size(75, 54);
			this.buttonSendMail.TabIndex = 5;
			this.buttonSendMail.Text = "メール送信";
			this.buttonSendMail.UseVisualStyleBackColor = true;
			this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
			// 
			// SendMailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1338, 810);
			this.Controls.Add(this.buttonSendMail);
			this.Controls.Add(this.dataGridViewMail);
			this.Controls.Add(this.groupBox1);
			this.Name = "SendMailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PC安心サポート送信メール情報";
			this.Load += new System.EventHandler(this.SendMailForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonStartMail;
		private System.Windows.Forms.DataGridView dataGridViewMail;
		private System.Windows.Forms.Button buttonSendMail;
		private System.Windows.Forms.RadioButton radioButtonUpdateMail;
		private System.Windows.Forms.RadioButton radioButtonGuideMail;
	}
}