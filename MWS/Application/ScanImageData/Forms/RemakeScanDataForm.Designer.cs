namespace ScanImageData.Forms
{
	partial class RemakeScanDataForm
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
			this.textBoxScanImageDataPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonInputPath = new System.Windows.Forms.Button();
			this.dataGridViewUserRegister = new System.Windows.Forms.DataGridView();
			this.buttonRemake = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserRegister)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxScanImageDataPath
			// 
			this.textBoxScanImageDataPath.BackColor = System.Drawing.Color.White;
			this.textBoxScanImageDataPath.Location = new System.Drawing.Point(152, 12);
			this.textBoxScanImageDataPath.Name = "textBoxScanImageDataPath";
			this.textBoxScanImageDataPath.ReadOnly = true;
			this.textBoxScanImageDataPath.Size = new System.Drawing.Size(525, 19);
			this.textBoxScanImageDataPath.TabIndex = 3;
			this.textBoxScanImageDataPath.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "■スキャンデータ登録パス";
			// 
			// buttonInputPath
			// 
			this.buttonInputPath.Location = new System.Drawing.Point(683, 8);
			this.buttonInputPath.Name = "buttonInputPath";
			this.buttonInputPath.Size = new System.Drawing.Size(30, 23);
			this.buttonInputPath.TabIndex = 4;
			this.buttonInputPath.Text = "...";
			this.buttonInputPath.UseVisualStyleBackColor = true;
			this.buttonInputPath.Click += new System.EventHandler(this.buttonInputPath_Click);
			// 
			// dataGridViewUserRegister
			// 
			this.dataGridViewUserRegister.AllowUserToAddRows = false;
			this.dataGridViewUserRegister.AllowUserToDeleteRows = false;
			this.dataGridViewUserRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewUserRegister.Location = new System.Drawing.Point(24, 40);
			this.dataGridViewUserRegister.Name = "dataGridViewUserRegister";
			this.dataGridViewUserRegister.RowHeadersVisible = false;
			this.dataGridViewUserRegister.RowTemplate.Height = 21;
			this.dataGridViewUserRegister.Size = new System.Drawing.Size(977, 704);
			this.dataGridViewUserRegister.TabIndex = 5;
			// 
			// buttonRemake
			// 
			this.buttonRemake.Location = new System.Drawing.Point(882, 12);
			this.buttonRemake.Name = "buttonRemake";
			this.buttonRemake.Size = new System.Drawing.Size(119, 23);
			this.buttonRemake.TabIndex = 6;
			this.buttonRemake.Text = "再作成";
			this.buttonRemake.UseVisualStyleBackColor = true;
			this.buttonRemake.Click += new System.EventHandler(this.buttonRemake_Click);
			// 
			// RemakeScanDataForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1013, 756);
			this.Controls.Add(this.buttonRemake);
			this.Controls.Add(this.dataGridViewUserRegister);
			this.Controls.Add(this.buttonInputPath);
			this.Controls.Add(this.textBoxScanImageDataPath);
			this.Controls.Add(this.label1);
			this.Name = "RemakeScanDataForm";
			this.Text = "スキャンデータ登録情報再作成";
			this.Load += new System.EventHandler(this.RemakeScanDataForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserRegister)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxScanImageDataPath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonInputPath;
		private System.Windows.Forms.DataGridView dataGridViewUserRegister;
		private System.Windows.Forms.Button buttonRemake;
	}
}