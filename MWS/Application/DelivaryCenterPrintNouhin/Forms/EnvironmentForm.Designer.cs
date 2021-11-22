
namespace DeliveryCenterPrintNouhin.Forms
{
	partial class EnvironmentForm
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
			this.textBoxNouhinDir = new System.Windows.Forms.TextBox();
			this.buttonInputNouhinDir = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxNouhinFile = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxParamFile = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBoxPrintRectangle = new System.Windows.Forms.CheckBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.checkBoxPrintPreview = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pictureBoxOffsetY = new System.Windows.Forms.PictureBox();
			this.buttonPrintTest = new System.Windows.Forms.Button();
			this.pictureBoxOffsetX = new System.Windows.Forms.PictureBox();
			this.numericUpDownOffsetX = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownOffsetY = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOffsetY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOffsetX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffsetX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffsetY)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■納品書ファイルフォルダ";
			// 
			// textBoxNouhinDir
			// 
			this.textBoxNouhinDir.Location = new System.Drawing.Point(141, 21);
			this.textBoxNouhinDir.Name = "textBoxNouhinDir";
			this.textBoxNouhinDir.Size = new System.Drawing.Size(361, 19);
			this.textBoxNouhinDir.TabIndex = 1;
			// 
			// buttonInputNouhinDir
			// 
			this.buttonInputNouhinDir.Location = new System.Drawing.Point(502, 19);
			this.buttonInputNouhinDir.Name = "buttonInputNouhinDir";
			this.buttonInputNouhinDir.Size = new System.Drawing.Size(53, 23);
			this.buttonInputNouhinDir.TabIndex = 2;
			this.buttonInputNouhinDir.Text = "設定";
			this.buttonInputNouhinDir.UseVisualStyleBackColor = true;
			this.buttonInputNouhinDir.Click += new System.EventHandler(this.buttonInputNouhinDir_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "■納品書ファイル名";
			// 
			// textBoxNouhinFile
			// 
			this.textBoxNouhinFile.Location = new System.Drawing.Point(141, 51);
			this.textBoxNouhinFile.Name = "textBoxNouhinFile";
			this.textBoxNouhinFile.Size = new System.Drawing.Size(178, 19);
			this.textBoxNouhinFile.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 127);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "左";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(144, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 12);
			this.label5.TabIndex = 2;
			this.label5.Text = "上";
			// 
			// textBoxParamFile
			// 
			this.textBoxParamFile.Location = new System.Drawing.Point(141, 82);
			this.textBoxParamFile.Name = "textBoxParamFile";
			this.textBoxParamFile.Size = new System.Drawing.Size(178, 19);
			this.textBoxParamFile.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 85);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 12);
			this.label6.TabIndex = 5;
			this.label6.Text = "■パラメタファイル名";
			// 
			// checkBoxPrintRectangle
			// 
			this.checkBoxPrintRectangle.AutoSize = true;
			this.checkBoxPrintRectangle.Location = new System.Drawing.Point(410, 148);
			this.checkBoxPrintRectangle.Name = "checkBoxPrintRectangle";
			this.checkBoxPrintRectangle.Size = new System.Drawing.Size(72, 16);
			this.checkBoxPrintRectangle.TabIndex = 7;
			this.checkBoxPrintRectangle.Text = "矩形印字";
			this.checkBoxPrintRectangle.UseVisualStyleBackColor = true;
			this.checkBoxPrintRectangle.Visible = false;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(403, 338);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 38);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(480, 338);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 38);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// checkBoxPrintPreview
			// 
			this.checkBoxPrintPreview.AutoSize = true;
			this.checkBoxPrintPreview.Location = new System.Drawing.Point(410, 126);
			this.checkBoxPrintPreview.Name = "checkBoxPrintPreview";
			this.checkBoxPrintPreview.Size = new System.Drawing.Size(92, 16);
			this.checkBoxPrintPreview.TabIndex = 8;
			this.checkBoxPrintPreview.Text = "印刷プレビュー";
			this.checkBoxPrintPreview.UseVisualStyleBackColor = true;
			this.checkBoxPrintPreview.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pictureBoxOffsetY);
			this.groupBox1.Controls.Add(this.buttonPrintTest);
			this.groupBox1.Controls.Add(this.pictureBoxOffsetX);
			this.groupBox1.Controls.Add(this.numericUpDownOffsetX);
			this.groupBox1.Controls.Add(this.numericUpDownOffsetY);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(14, 114);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(383, 262);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "開始位置調整（0.1mm単位）";
			// 
			// pictureBoxOffsetY
			// 
			this.pictureBoxOffsetY.Image = global::DeliveryCenterPrintNouhin.Properties.Resources.OffsetY;
			this.pictureBoxOffsetY.InitialImage = null;
			this.pictureBoxOffsetY.Location = new System.Drawing.Point(97, 51);
			this.pictureBoxOffsetY.Name = "pictureBoxOffsetY";
			this.pictureBoxOffsetY.Size = new System.Drawing.Size(178, 188);
			this.pictureBoxOffsetY.TabIndex = 13;
			this.pictureBoxOffsetY.TabStop = false;
			this.pictureBoxOffsetY.Visible = false;
			// 
			// buttonPrintTest
			// 
			this.buttonPrintTest.Location = new System.Drawing.Point(281, 191);
			this.buttonPrintTest.Name = "buttonPrintTest";
			this.buttonPrintTest.Size = new System.Drawing.Size(96, 36);
			this.buttonPrintTest.TabIndex = 4;
			this.buttonPrintTest.Text = "テスト印刷";
			this.buttonPrintTest.UseVisualStyleBackColor = true;
			this.buttonPrintTest.Click += new System.EventHandler(this.buttonPrintTest_Click);
			// 
			// pictureBoxOffsetX
			// 
			this.pictureBoxOffsetX.Image = global::DeliveryCenterPrintNouhin.Properties.Resources.OffsetX;
			this.pictureBoxOffsetX.InitialImage = null;
			this.pictureBoxOffsetX.Location = new System.Drawing.Point(97, 51);
			this.pictureBoxOffsetX.Name = "pictureBoxOffsetX";
			this.pictureBoxOffsetX.Size = new System.Drawing.Size(178, 188);
			this.pictureBoxOffsetX.TabIndex = 12;
			this.pictureBoxOffsetX.TabStop = false;
			// 
			// numericUpDownOffsetX
			// 
			this.numericUpDownOffsetX.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.numericUpDownOffsetX.Location = new System.Drawing.Point(37, 122);
			this.numericUpDownOffsetX.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDownOffsetX.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
			this.numericUpDownOffsetX.Name = "numericUpDownOffsetX";
			this.numericUpDownOffsetX.Size = new System.Drawing.Size(54, 22);
			this.numericUpDownOffsetX.TabIndex = 1;
			this.numericUpDownOffsetX.Enter += new System.EventHandler(this.numericUpDownOffsetX_Enter);
			// 
			// numericUpDownOffsetY
			// 
			this.numericUpDownOffsetY.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.numericUpDownOffsetY.Location = new System.Drawing.Point(167, 23);
			this.numericUpDownOffsetY.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDownOffsetY.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
			this.numericUpDownOffsetY.Name = "numericUpDownOffsetY";
			this.numericUpDownOffsetY.Size = new System.Drawing.Size(54, 22);
			this.numericUpDownOffsetY.TabIndex = 3;
			this.numericUpDownOffsetY.Enter += new System.EventHandler(this.numericUpDownOffsetY_Enter);
			// 
			// EnvironmentForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(570, 390);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBoxPrintPreview);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkBoxPrintRectangle);
			this.Controls.Add(this.textBoxParamFile);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxNouhinFile);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonInputNouhinDir);
			this.Controls.Add(this.textBoxNouhinDir);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EnvironmentForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "環境設定";
			this.Load += new System.EventHandler(this.EnvironmentForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOffsetY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOffsetX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffsetX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffsetY)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNouhinDir;
		private System.Windows.Forms.Button buttonInputNouhinDir;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxNouhinFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxParamFile;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkBoxPrintRectangle;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.CheckBox checkBoxPrintPreview;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox pictureBoxOffsetX;
		private System.Windows.Forms.NumericUpDown numericUpDownOffsetX;
		private System.Windows.Forms.NumericUpDown numericUpDownOffsetY;
		private System.Windows.Forms.Button buttonPrintTest;
		private System.Windows.Forms.PictureBox pictureBoxOffsetY;
	}
}