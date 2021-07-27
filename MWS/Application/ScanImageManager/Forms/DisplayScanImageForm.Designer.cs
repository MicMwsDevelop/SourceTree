namespace ScanImageManager.Forms
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
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBoxImage = new System.Windows.Forms.PictureBox();
			this.textBoxTextLine = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelFilename = new System.Windows.Forms.Label();
			this.axAcroPDF_Image = new AxAcroPDFLib.AxAcroPDF();
			this.textBoxTokuisaki = new MwsLib.Component.NumericTextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axAcroPDF_Image)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "■ファイル名";
			// 
			// pictureBoxImage
			// 
			this.pictureBoxImage.Location = new System.Drawing.Point(18, 71);
			this.pictureBoxImage.Name = "pictureBoxImage";
			this.pictureBoxImage.Size = new System.Drawing.Size(682, 769);
			this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxImage.TabIndex = 5;
			this.pictureBoxImage.TabStop = false;
			this.pictureBoxImage.Visible = false;
			// 
			// textBoxTextLine
			// 
			this.textBoxTextLine.Location = new System.Drawing.Point(18, 71);
			this.textBoxTextLine.Multiline = true;
			this.textBoxTextLine.Name = "textBoxTextLine";
			this.textBoxTextLine.ReadOnly = true;
			this.textBoxTextLine.Size = new System.Drawing.Size(682, 769);
			this.textBoxTextLine.TabIndex = 5;
			this.textBoxTextLine.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "■得意先番号";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(625, 9);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(625, 36);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelFilename
			// 
			this.labelFilename.BackColor = System.Drawing.Color.White;
			this.labelFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelFilename.Location = new System.Drawing.Point(100, 9);
			this.labelFilename.Name = "labelFilename";
			this.labelFilename.Size = new System.Drawing.Size(432, 23);
			this.labelFilename.TabIndex = 9;
			this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// axAcroPDF_Image
			// 
			this.axAcroPDF_Image.Enabled = true;
			this.axAcroPDF_Image.Location = new System.Drawing.Point(18, 71);
			this.axAcroPDF_Image.Name = "axAcroPDF_Image";
			this.axAcroPDF_Image.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF_Image.OcxState")));
			this.axAcroPDF_Image.Size = new System.Drawing.Size(682, 769);
			this.axAcroPDF_Image.TabIndex = 6;
			this.axAcroPDF_Image.Visible = false;
			// 
			// textBoxTokuisaki
			// 
			this.textBoxTokuisaki.BackColor = System.Drawing.Color.White;
			this.textBoxTokuisaki.Location = new System.Drawing.Point(100, 41);
			this.textBoxTokuisaki.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxTokuisaki.MaxLength = 6;
			this.textBoxTokuisaki.Name = "textBoxTokuisaki";
			this.textBoxTokuisaki.Size = new System.Drawing.Size(118, 23);
			this.textBoxTokuisaki.TabIndex = 3;
			this.textBoxTokuisaki.TabStop = false;
			// 
			// DisplayScanImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(717, 853);
			this.Controls.Add(this.labelFilename);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxTokuisaki);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxTextLine);
			this.Controls.Add(this.pictureBoxImage);
			this.Controls.Add(this.axAcroPDF_Image);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "DisplayScanImageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "得意先番号の登録";
			this.Load += new System.EventHandler(this.DisplayScanImageForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axAcroPDF_Image)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		//private AxAcroPDFLib.AxAcroPDF axAcroPDF;
		private System.Windows.Forms.Label label1;
		private AxAcroPDFLib.AxAcroPDF axAcroPDF_Image;
		private System.Windows.Forms.PictureBox pictureBoxImage;
		private System.Windows.Forms.TextBox textBoxTextLine;
		private System.Windows.Forms.Label label3;
		private MwsLib.Component.NumericTextBox textBoxTokuisaki;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelFilename;
	}
}