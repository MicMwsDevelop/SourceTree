namespace PcSupportManager.Forms
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonAll = new System.Windows.Forms.RadioButton();
			this.radioButtonStartMail = new System.Windows.Forms.RadioButton();
			this.radioButtonMailUpdate = new System.Windows.Forms.RadioButton();
			this.radioButtonMailGuide = new System.Windows.Forms.RadioButton();
			this.buttonSend = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxSpan = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewMail
			// 
			this.dataGridViewMail.AllowUserToAddRows = false;
			this.dataGridViewMail.AllowUserToDeleteRows = false;
			this.dataGridViewMail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMail.Location = new System.Drawing.Point(12, 62);
			this.dataGridViewMail.Name = "dataGridViewMail";
			this.dataGridViewMail.RowTemplate.Height = 21;
			this.dataGridViewMail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewMail.Size = new System.Drawing.Size(1235, 667);
			this.dataGridViewMail.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonAll);
			this.groupBox1.Controls.Add(this.radioButtonStartMail);
			this.groupBox1.Controls.Add(this.radioButtonMailUpdate);
			this.groupBox1.Controls.Add(this.radioButtonMailGuide);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(389, 44);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "メール送信種別";
			// 
			// radioButtonAll
			// 
			this.radioButtonAll.AutoSize = true;
			this.radioButtonAll.Location = new System.Drawing.Point(15, 18);
			this.radioButtonAll.Name = "radioButtonAll";
			this.radioButtonAll.Size = new System.Drawing.Size(44, 16);
			this.radioButtonAll.TabIndex = 0;
			this.radioButtonAll.Text = "全て";
			this.radioButtonAll.UseVisualStyleBackColor = true;
			this.radioButtonAll.CheckedChanged += new System.EventHandler(this.radioButtonAll_CheckedChanged);
			// 
			// radioButtonStartMail
			// 
			this.radioButtonStartMail.AutoSize = true;
			this.radioButtonStartMail.Location = new System.Drawing.Point(65, 18);
			this.radioButtonStartMail.Name = "radioButtonStartMail";
			this.radioButtonStartMail.Size = new System.Drawing.Size(75, 16);
			this.radioButtonStartMail.TabIndex = 1;
			this.radioButtonStartMail.Text = "開始メール";
			this.radioButtonStartMail.UseVisualStyleBackColor = true;
			this.radioButtonStartMail.CheckedChanged += new System.EventHandler(this.radioButtonStartMail_CheckedChanged);
			// 
			// radioButtonMailUpdate
			// 
			this.radioButtonMailUpdate.AutoSize = true;
			this.radioButtonMailUpdate.Location = new System.Drawing.Point(275, 18);
			this.radioButtonMailUpdate.Name = "radioButtonMailUpdate";
			this.radioButtonMailUpdate.Size = new System.Drawing.Size(99, 16);
			this.radioButtonMailUpdate.TabIndex = 3;
			this.radioButtonMailUpdate.Text = "契約更新メール";
			this.radioButtonMailUpdate.UseVisualStyleBackColor = true;
			this.radioButtonMailUpdate.CheckedChanged += new System.EventHandler(this.radioButtonMailUpdate_CheckedChanged);
			// 
			// radioButtonMailGuide
			// 
			this.radioButtonMailGuide.AutoSize = true;
			this.radioButtonMailGuide.Location = new System.Drawing.Point(146, 18);
			this.radioButtonMailGuide.Name = "radioButtonMailGuide";
			this.radioButtonMailGuide.Size = new System.Drawing.Size(123, 16);
			this.radioButtonMailGuide.TabIndex = 2;
			this.radioButtonMailGuide.Text = "契約更新案内メール";
			this.radioButtonMailGuide.UseVisualStyleBackColor = true;
			this.radioButtonMailGuide.CheckedChanged += new System.EventHandler(this.radioButtonMailGuide_CheckedChanged);
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(407, 12);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(105, 44);
			this.buttonSend.TabIndex = 2;
			this.buttonSend.Text = "送信";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(518, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "■送信対象";
			// 
			// textBoxSpan
			// 
			this.textBoxSpan.BackColor = System.Drawing.Color.White;
			this.textBoxSpan.Location = new System.Drawing.Point(589, 31);
			this.textBoxSpan.Name = "textBoxSpan";
			this.textBoxSpan.ReadOnly = true;
			this.textBoxSpan.Size = new System.Drawing.Size(242, 19);
			this.textBoxSpan.TabIndex = 4;
			// 
			// SendMailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1259, 741);
			this.Controls.Add(this.textBoxSpan);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dataGridViewMail);
			this.Name = "SendMailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "メール送信";
			this.Load += new System.EventHandler(this.SendMailForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewMail;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonStartMail;
		private System.Windows.Forms.RadioButton radioButtonMailUpdate;
		private System.Windows.Forms.RadioButton radioButtonMailGuide;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.RadioButton radioButtonAll;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxSpan;
	}
}