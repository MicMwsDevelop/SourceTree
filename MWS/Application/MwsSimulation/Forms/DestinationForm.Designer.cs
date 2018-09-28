namespace MwsSimulation.Forms
{
	partial class DestinationForm
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
			this.textBoxDestination = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.radioOnchu = new System.Windows.Forms.RadioButton();
			this.radioSama = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "■宛先";
			// 
			// textBoxDestination
			// 
			this.textBoxDestination.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxDestination.Location = new System.Drawing.Point(16, 32);
			this.textBoxDestination.Name = "textBoxDestination";
			this.textBoxDestination.Size = new System.Drawing.Size(389, 23);
			this.textBoxDestination.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(249, 61);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 32);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(330, 61);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 32);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// radioOnchu
			// 
			this.radioOnchu.AutoSize = true;
			this.radioOnchu.Checked = true;
			this.radioOnchu.Location = new System.Drawing.Point(15, 68);
			this.radioOnchu.Name = "radioOnchu";
			this.radioOnchu.Size = new System.Drawing.Size(49, 19);
			this.radioOnchu.TabIndex = 2;
			this.radioOnchu.TabStop = true;
			this.radioOnchu.Text = "御中";
			this.radioOnchu.UseVisualStyleBackColor = true;
			// 
			// radioSama
			// 
			this.radioSama.AutoSize = true;
			this.radioSama.Location = new System.Drawing.Point(70, 68);
			this.radioSama.Name = "radioSama";
			this.radioSama.Size = new System.Drawing.Size(37, 19);
			this.radioSama.TabIndex = 3;
			this.radioSama.TabStop = true;
			this.radioSama.Text = "様";
			this.radioSama.UseVisualStyleBackColor = true;
			// 
			// DestinationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(417, 106);
			this.Controls.Add(this.radioSama);
			this.Controls.Add(this.radioOnchu);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxDestination);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DestinationForm";
			this.Text = "宛先の設定";
			this.Load += new System.EventHandler(this.EstimateNameForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxDestination;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.RadioButton radioOnchu;
		private System.Windows.Forms.RadioButton radioSama;
	}
}