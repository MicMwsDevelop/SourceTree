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
			this.textBoxOrderNo = new System.Windows.Forms.TextBox();
			this.buttonSearch = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewControl)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewControl
			// 
			this.dataGridViewControl.AllowUserToAddRows = false;
			this.dataGridViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewControl.Location = new System.Drawing.Point(12, 41);
			this.dataGridViewControl.MultiSelect = false;
			this.dataGridViewControl.Name = "dataGridViewControl";
			this.dataGridViewControl.ReadOnly = true;
			this.dataGridViewControl.RowHeadersVisible = false;
			this.dataGridViewControl.RowTemplate.Height = 21;
			this.dataGridViewControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewControl.Size = new System.Drawing.Size(1176, 694);
			this.dataGridViewControl.TabIndex = 3;
			this.dataGridViewControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewControl_MouseDoubleClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■受注No";
			// 
			// textBoxOrderNo
			// 
			this.textBoxOrderNo.Location = new System.Drawing.Point(73, 12);
			this.textBoxOrderNo.MaxLength = 8;
			this.textBoxOrderNo.Name = "textBoxOrderNo";
			this.textBoxOrderNo.Size = new System.Drawing.Size(156, 19);
			this.textBoxOrderNo.TabIndex = 1;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(235, 10);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// PcSupportControlListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1201, 750);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBoxOrderNo);
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
		private System.Windows.Forms.TextBox textBoxOrderNo;
		private System.Windows.Forms.Button buttonSearch;
	}
}