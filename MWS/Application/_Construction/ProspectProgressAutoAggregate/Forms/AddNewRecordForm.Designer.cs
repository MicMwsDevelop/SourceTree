
namespace ProspectProgressAutoAggregate.Forms
{
	partial class AddNewRecordForm
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
			this.textBoxPeriod = new MwsLib.Component.NumericTextBox();
			this.buttonAddNew = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "追加する期";
			// 
			// textBoxPeriod
			// 
			this.textBoxPeriod.Location = new System.Drawing.Point(80, 13);
			this.textBoxPeriod.Name = "textBoxPeriod";
			this.textBoxPeriod.Size = new System.Drawing.Size(100, 19);
			this.textBoxPeriod.TabIndex = 1;
			// 
			// buttonAddNew
			// 
			this.buttonAddNew.Location = new System.Drawing.Point(186, 11);
			this.buttonAddNew.Name = "buttonAddNew";
			this.buttonAddNew.Size = new System.Drawing.Size(75, 23);
			this.buttonAddNew.TabIndex = 2;
			this.buttonAddNew.Text = "追加";
			this.buttonAddNew.UseVisualStyleBackColor = true;
			this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
			// 
			// AddNewRecordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(279, 54);
			this.Controls.Add(this.buttonAddNew);
			this.Controls.Add(this.textBoxPeriod);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddNewRecordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "来期売上実績追加";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox textBoxPeriod;
		private System.Windows.Forms.Button buttonAddNew;
	}
}