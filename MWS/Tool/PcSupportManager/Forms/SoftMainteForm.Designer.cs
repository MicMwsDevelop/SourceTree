namespace PcSupportManager.Forms
{
	partial class SoftMainteForm
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
			this.dataGridViewSoft = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonSoftMainte = new System.Windows.Forms.RadioButton();
			this.radioButtonPcSupport = new System.Windows.Forms.RadioButton();
			this.buttonUpdateSoftMainte = new System.Windows.Forms.Button();
			this.textBoxSpan = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCount = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoft)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewSoft
			// 
			this.dataGridViewSoft.AllowUserToAddRows = false;
			this.dataGridViewSoft.AllowUserToDeleteRows = false;
			this.dataGridViewSoft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewSoft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewSoft.Location = new System.Drawing.Point(12, 62);
			this.dataGridViewSoft.Name = "dataGridViewSoft";
			this.dataGridViewSoft.RowTemplate.Height = 21;
			this.dataGridViewSoft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewSoft.Size = new System.Drawing.Size(1235, 667);
			this.dataGridViewSoft.TabIndex = 6;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonSoftMainte);
			this.groupBox1.Controls.Add(this.radioButtonPcSupport);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(330, 44);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "表示種別";
			// 
			// radioButtonSoftMainte
			// 
			this.radioButtonSoftMainte.AutoSize = true;
			this.radioButtonSoftMainte.Location = new System.Drawing.Point(169, 18);
			this.radioButtonSoftMainte.Name = "radioButtonSoftMainte";
			this.radioButtonSoftMainte.Size = new System.Drawing.Size(158, 16);
			this.radioButtonSoftMainte.TabIndex = 1;
			this.radioButtonSoftMainte.Text = "製品サポート情報ソフト保守";
			this.radioButtonSoftMainte.UseVisualStyleBackColor = true;
			this.radioButtonSoftMainte.CheckedChanged += new System.EventHandler(this.radioButtonSoftMainte_CheckedChanged);
			// 
			// radioButtonPcSupport
			// 
			this.radioButtonPcSupport.AutoSize = true;
			this.radioButtonPcSupport.Location = new System.Drawing.Point(15, 18);
			this.radioButtonPcSupport.Name = "radioButtonPcSupport";
			this.radioButtonPcSupport.Size = new System.Drawing.Size(148, 16);
			this.radioButtonPcSupport.TabIndex = 0;
			this.radioButtonPcSupport.Text = "PC安心サポート管理情報";
			this.radioButtonPcSupport.UseVisualStyleBackColor = true;
			this.radioButtonPcSupport.CheckedChanged += new System.EventHandler(this.radioButtonPcSupport_CheckedChanged);
			// 
			// buttonUpdateSoftMainte
			// 
			this.buttonUpdateSoftMainte.Location = new System.Drawing.Point(348, 12);
			this.buttonUpdateSoftMainte.Name = "buttonUpdateSoftMainte";
			this.buttonUpdateSoftMainte.Size = new System.Drawing.Size(189, 44);
			this.buttonUpdateSoftMainte.TabIndex = 1;
			this.buttonUpdateSoftMainte.Text = "製品サポート情報ソフト保守更新";
			this.buttonUpdateSoftMainte.UseVisualStyleBackColor = true;
			this.buttonUpdateSoftMainte.Click += new System.EventHandler(this.buttonUpdateSoftMainte_Click);
			// 
			// textBoxSpan
			// 
			this.textBoxSpan.BackColor = System.Drawing.Color.White;
			this.textBoxSpan.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxSpan.Location = new System.Drawing.Point(623, 25);
			this.textBoxSpan.Name = "textBoxSpan";
			this.textBoxSpan.ReadOnly = true;
			this.textBoxSpan.Size = new System.Drawing.Size(242, 23);
			this.textBoxSpan.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(552, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "■検索対象";
			// 
			// textBoxCount
			// 
			this.textBoxCount.BackColor = System.Drawing.Color.White;
			this.textBoxCount.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxCount.Location = new System.Drawing.Point(918, 25);
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.ReadOnly = true;
			this.textBoxCount.Size = new System.Drawing.Size(100, 23);
			this.textBoxCount.TabIndex = 5;
			this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(871, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "■件数";
			// 
			// SoftMainteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1259, 741);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxSpan);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonUpdateSoftMainte);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dataGridViewSoft);
			this.Name = "SoftMainteForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "製品サポート情報ソフト保守更新";
			this.Load += new System.EventHandler(this.SoftMainteForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoft)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewSoft;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonPcSupport;
		private System.Windows.Forms.Button buttonUpdateSoftMainte;
		private System.Windows.Forms.RadioButton radioButtonSoftMainte;
		private System.Windows.Forms.TextBox textBoxSpan;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCount;
		private System.Windows.Forms.Label label2;
	}
}