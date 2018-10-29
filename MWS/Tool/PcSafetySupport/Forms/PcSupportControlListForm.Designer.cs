namespace PcSafetySupport.Forms
{
	partial class PcSupportControlListForm
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
			this.dataGridViewControl = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCustomerID = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewControl)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewControl
			// 
			this.dataGridViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewControl.Location = new System.Drawing.Point(12, 37);
			this.dataGridViewControl.Name = "dataGridViewControl";
			this.dataGridViewControl.RowTemplate.Height = 21;
			this.dataGridViewControl.Size = new System.Drawing.Size(1176, 698);
			this.dataGridViewControl.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "■顧客No";
			// 
			// textBoxCustomerID
			// 
			this.textBoxCustomerID.Location = new System.Drawing.Point(73, 12);
			this.textBoxCustomerID.Name = "textBoxCustomerID";
			this.textBoxCustomerID.Size = new System.Drawing.Size(156, 19);
			this.textBoxCustomerID.TabIndex = 3;
			this.textBoxCustomerID.TextChanged += new System.EventHandler(this.textBoxCustomerID_TextChanged);
			// 
			// PcSupportControlListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1201, 750);
			this.Controls.Add(this.textBoxCustomerID);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewControl);
			this.Name = "PcSupportControlListForm";
			this.Text = "PC安心サポート管理情報登録";
			this.Load += new System.EventHandler(this.PcSupportControlListForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewControl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCustomerID;
	}
}