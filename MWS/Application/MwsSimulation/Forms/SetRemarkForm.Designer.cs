namespace MwsSimulation.Forms
{
	partial class SetRemarkForm
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxRemark = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(546, 59);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(438, 59);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 36);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "■備考";
			// 
			// textBoxRemark
			// 
			this.textBoxRemark.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark.Location = new System.Drawing.Point(15, 29);
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(633, 24);
			this.textBoxRemark.TabIndex = 1;
			// 
			// SetRemarkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(660, 106);
			this.Controls.Add(this.textBoxRemark);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetRemarkForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "備考の入力";
			this.Load += new System.EventHandler(this.SetRemarkForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxRemark;
	}
}