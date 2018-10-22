namespace MwsSimulation.Forms
{
	partial class SelectRemarkForm
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
			this.listBoxRemark = new System.Windows.Forms.ListBox();
			this.buttonRegist = new System.Windows.Forms.Button();
			this.textBoxRemark = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(325, 294);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(217, 294);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 36);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// listBoxRemark
			// 
			this.listBoxRemark.FormattingEnabled = true;
			this.listBoxRemark.ItemHeight = 17;
			this.listBoxRemark.Location = new System.Drawing.Point(12, 46);
			this.listBoxRemark.Name = "listBoxRemark";
			this.listBoxRemark.Size = new System.Drawing.Size(415, 242);
			this.listBoxRemark.TabIndex = 1;
			this.listBoxRemark.SelectedIndexChanged += new System.EventHandler(this.listBoxRemark_SelectedIndexChanged);
			this.listBoxRemark.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxRemark_MouseDoubleClick);
			// 
			// buttonRegist
			// 
			this.buttonRegist.Location = new System.Drawing.Point(12, 294);
			this.buttonRegist.Name = "buttonRegist";
			this.buttonRegist.Size = new System.Drawing.Size(61, 36);
			this.buttonRegist.TabIndex = 2;
			this.buttonRegist.Text = "登録";
			this.buttonRegist.UseVisualStyleBackColor = true;
			this.buttonRegist.Click += new System.EventHandler(this.buttonRegist_Click);
			// 
			// textBoxRemark
			// 
			this.textBoxRemark.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark.Location = new System.Drawing.Point(13, 13);
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(414, 24);
			this.textBoxRemark.TabIndex = 0;
			// 
			// SelectRemarkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(443, 342);
			this.Controls.Add(this.textBoxRemark);
			this.Controls.Add(this.buttonRegist);
			this.Controls.Add(this.listBoxRemark);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectRemarkForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "備考の入力";
			this.Load += new System.EventHandler(this.SelectRemarkForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.ListBox listBoxRemark;
		private System.Windows.Forms.Button buttonRegist;
		private System.Windows.Forms.TextBox textBoxRemark;
	}
}