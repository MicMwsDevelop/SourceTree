namespace MwsSimulation.Forms
{
	partial class VersionInfoForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelProgramVersion = new System.Windows.Forms.Label();
			this.labelDataVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "■プログラムバージョン";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(139, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "■データファイルバージョン";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(234, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "MIC WEB SERVICE 課金シミュレーション";
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(367, 118);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 32);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// labelProgramVersion
			// 
			this.labelProgramVersion.BackColor = System.Drawing.SystemColors.Window;
			this.labelProgramVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelProgramVersion.Location = new System.Drawing.Point(157, 50);
			this.labelProgramVersion.Name = "labelProgramVersion";
			this.labelProgramVersion.Size = new System.Drawing.Size(109, 24);
			this.labelProgramVersion.TabIndex = 2;
			// 
			// labelDataVersion
			// 
			this.labelDataVersion.BackColor = System.Drawing.SystemColors.Window;
			this.labelDataVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelDataVersion.Location = new System.Drawing.Point(157, 80);
			this.labelDataVersion.Name = "labelDataVersion";
			this.labelDataVersion.Size = new System.Drawing.Size(285, 24);
			this.labelDataVersion.TabIndex = 4;
			// 
			// VersionInfoForm
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(460, 162);
			this.Controls.Add(this.labelDataVersion);
			this.Controls.Add(this.labelProgramVersion);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VersionInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "バージョン情報";
			this.Load += new System.EventHandler(this.VersionInfoForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label labelProgramVersion;
		private System.Windows.Forms.Label labelDataVersion;
	}
}