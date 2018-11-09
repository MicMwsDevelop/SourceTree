namespace PcSupportTool.Forms
{
	partial class ManagementForm
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
			this.dataGridViewManager = new System.Windows.Forms.DataGridView();
			this.buttonReadOrderInfo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewManager)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewManager
			// 
			this.dataGridViewManager.AllowUserToAddRows = false;
			this.dataGridViewManager.AllowUserToDeleteRows = false;
			this.dataGridViewManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewManager.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewManager.Location = new System.Drawing.Point(12, 63);
			this.dataGridViewManager.Name = "dataGridViewManager";
			this.dataGridViewManager.ReadOnly = true;
			this.dataGridViewManager.RowHeadersVisible = false;
			this.dataGridViewManager.RowTemplate.Height = 21;
			this.dataGridViewManager.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewManager.Size = new System.Drawing.Size(1111, 591);
			this.dataGridViewManager.TabIndex = 0;
			this.dataGridViewManager.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewManager_MouseDoubleClick);
			// 
			// buttonReadOrderInfo
			// 
			this.buttonReadOrderInfo.Location = new System.Drawing.Point(12, 12);
			this.buttonReadOrderInfo.Name = "buttonReadOrderInfo";
			this.buttonReadOrderInfo.Size = new System.Drawing.Size(157, 45);
			this.buttonReadOrderInfo.TabIndex = 1;
			this.buttonReadOrderInfo.Text = "受注情報からの読込み";
			this.buttonReadOrderInfo.UseVisualStyleBackColor = true;
			this.buttonReadOrderInfo.Click += new System.EventHandler(this.buttonReadOrderInfo_Click);
			// 
			// ManagementForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1135, 666);
			this.Controls.Add(this.buttonReadOrderInfo);
			this.Controls.Add(this.dataGridViewManager);
			this.Name = "ManagementForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "管理情報登録";
			this.Load += new System.EventHandler(this.ManagementForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewManager)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewManager;
		private System.Windows.Forms.Button buttonReadOrderInfo;
	}
}