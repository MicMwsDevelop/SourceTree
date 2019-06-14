namespace ScanImageData.Forms
{
	partial class DisplayScanImageForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayScanImageForm));
			this.axAcroPDF = new AxAcroPDFLib.AxAcroPDF();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.textBoxTextLine = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// axAcroPDF
			// 
			this.axAcroPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.axAcroPDF.Enabled = true;
			this.axAcroPDF.Location = new System.Drawing.Point(15, 35);
			this.axAcroPDF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.axAcroPDF.Name = "axAcroPDF";
			this.axAcroPDF.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF.OcxState")));
			this.axAcroPDF.Size = new System.Drawing.Size(685, 647);
			this.axAcroPDF.TabIndex = 0;
			this.axAcroPDF.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "■ファイル名";
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.BackColor = System.Drawing.Color.White;
			this.textBoxFilename.Location = new System.Drawing.Point(97, 12);
			this.textBoxFilename.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.ReadOnly = true;
			this.textBoxFilename.Size = new System.Drawing.Size(603, 23);
			this.textBoxFilename.TabIndex = 2;
			this.textBoxFilename.TabStop = false;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(15, 44);
			this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(685, 809);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			this.pictureBox.Visible = false;
			// 
			// textBoxTextLine
			// 
			this.textBoxTextLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTextLine.Location = new System.Drawing.Point(15, 44);
			this.textBoxTextLine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxTextLine.Multiline = true;
			this.textBoxTextLine.Name = "textBoxTextLine";
			this.textBoxTextLine.ReadOnly = true;
			this.textBoxTextLine.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxTextLine.Size = new System.Drawing.Size(685, 808);
			this.textBoxTextLine.TabIndex = 4;
			this.textBoxTextLine.Visible = false;
			// 
			// DisplayScanImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(720, 868);
			this.Controls.Add(this.textBoxTextLine);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.textBoxFilename);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.axAcroPDF);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DisplayScanImageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "スキャナーイメージの表示";
			this.Load += new System.EventHandler(this.DisplayScanImageForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AxAcroPDFLib.AxAcroPDF axAcroPDF;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.TextBox textBoxTextLine;
	}
}