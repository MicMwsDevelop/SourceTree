﻿namespace EntryFinishedUser
{
	partial class ShowListForm
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
			this.dataGridViewUser = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewUser
			// 
			this.dataGridViewUser.AllowUserToAddRows = false;
			this.dataGridViewUser.AllowUserToDeleteRows = false;
			this.dataGridViewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewUser.Location = new System.Drawing.Point(13, 13);
			this.dataGridViewUser.Name = "dataGridViewUser";
			this.dataGridViewUser.RowHeadersVisible = false;
			this.dataGridViewUser.RowTemplate.Height = 21;
			this.dataGridViewUser.Size = new System.Drawing.Size(1239, 784);
			this.dataGridViewUser.TabIndex = 0;
			// 
			// ShowListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 809);
			this.Controls.Add(this.dataGridViewUser);
			this.Name = "ShowListForm";
			this.Text = "リスト参照";
			this.Load += new System.EventHandler(this.ShowListForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewUser;
	}
}