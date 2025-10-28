namespace HardSubscManager.Forms
{
	partial class InputCustomerNoForm
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
			this.label5 = new System.Windows.Forms.Label();
			this.numericTextBoxCustomerNo = new MwsLib.Component.NumericTextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.LightCyan;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(15, 18);
			this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(79, 27);
			this.label5.TabIndex = 0;
			this.label5.Text = "顧客No ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numericTextBoxCustomerNo
			// 
			this.numericTextBoxCustomerNo.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.numericTextBoxCustomerNo.Location = new System.Drawing.Point(95, 26);
			this.numericTextBoxCustomerNo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxCustomerNo.MaxLength = 8;
			this.numericTextBoxCustomerNo.Name = "numericTextBoxCustomerNo";
			this.numericTextBoxCustomerNo.Size = new System.Drawing.Size(111, 20);
			this.numericTextBoxCustomerNo.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.Font = new System.Drawing.Font("BIZ UDゴシック", 9F);
			this.buttonOK.Location = new System.Drawing.Point(15, 72);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(95, 34);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Font = new System.Drawing.Font("BIZ UDゴシック", 9F);
			this.buttonCancel.Location = new System.Drawing.Point(120, 72);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(95, 34);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// InputCustomerNoForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(238, 121);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.numericTextBoxCustomerNo);
			this.Controls.Add(this.label5);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputCustomerNoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "顧客No入力";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private MwsLib.Component.NumericTextBox numericTextBoxCustomerNo;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
	}
}