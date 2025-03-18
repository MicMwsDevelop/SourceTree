namespace MwsServiceCancelTool.Forms
{
	partial class CuiForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGridViewCui = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCustomerNo = new MwsLib.Component.NumericTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelCustomerName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCui)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewCui
			// 
			this.dataGridViewCui.AllowUserToAddRows = false;
			this.dataGridViewCui.AllowUserToDeleteRows = false;
			this.dataGridViewCui.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewCui.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCui.Location = new System.Drawing.Point(12, 29);
			this.dataGridViewCui.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridViewCui.MultiSelect = false;
			this.dataGridViewCui.Name = "dataGridViewCui";
			this.dataGridViewCui.ReadOnly = true;
			this.dataGridViewCui.RowHeadersVisible = false;
			this.dataGridViewCui.RowTemplate.Height = 21;
			this.dataGridViewCui.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewCui.Size = new System.Drawing.Size(1209, 491);
			this.dataGridViewCui.TabIndex = 0;
			this.dataGridViewCui.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewCui_MouseDoubleClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "■顧客No";
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.Location = new System.Drawing.Point(92, 7);
			this.textBoxCustomerNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.textBoxCustomerNo.MaxLength = 8;
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.Size = new System.Drawing.Size(142, 20);
			this.textBoxCustomerNo.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(245, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "■顧客名";
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(320, 7);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(549, 19);
			this.labelCustomerName.TabIndex = 4;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CuiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1233, 528);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewCui);
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "CuiForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "顧客利用情報編集";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCui)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewCui;
		private System.Windows.Forms.Label label1;
		private MwsLib.Component.NumericTextBox textBoxCustomerNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelCustomerName;
	}
}

