namespace MwsServiceCancelTool.Forms
{
	partial class OnlineDemandCancelForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineDemandCancelForm));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelCustomerName = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.dataGridViewOnlineDemand = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.labelCustomerNo = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnlineDemand)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOK.Location = new System.Drawing.Point(806, 10);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(162, 63);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "作業済申請取消";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(806, 77);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(162, 63);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "閉じる";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(260, 10);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(450, 20);
			this.labelCustomerName.TabIndex = 3;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(205, 14);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 11);
			this.label12.TabIndex = 2;
			this.label12.Text = "■顧客名";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label13.Location = new System.Drawing.Point(12, 14);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(54, 11);
			this.label13.TabIndex = 0;
			this.label13.Text = "■顧客No";
			// 
			// dataGridViewOnlineDemand
			// 
			this.dataGridViewOnlineDemand.AllowUserToAddRows = false;
			this.dataGridViewOnlineDemand.AllowUserToDeleteRows = false;
			this.dataGridViewOnlineDemand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewOnlineDemand.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridViewOnlineDemand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewOnlineDemand.Location = new System.Drawing.Point(15, 190);
			this.dataGridViewOnlineDemand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridViewOnlineDemand.MultiSelect = false;
			this.dataGridViewOnlineDemand.Name = "dataGridViewOnlineDemand";
			this.dataGridViewOnlineDemand.ReadOnly = true;
			this.dataGridViewOnlineDemand.RowHeadersVisible = false;
			this.dataGridViewOnlineDemand.RowTemplate.Height = 21;
			this.dataGridViewOnlineDemand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewOnlineDemand.Size = new System.Drawing.Size(953, 157);
			this.dataGridViewOnlineDemand.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(15, 174);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(303, 11);
			this.label1.TabIndex = 5;
			this.label1.Text = "■各種作業料作業済申請情報（T_USE_ONLINE_DEMAND）";
			// 
			// labelCustomerNo
			// 
			this.labelCustomerNo.BackColor = System.Drawing.Color.White;
			this.labelCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerNo.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelCustomerNo.Location = new System.Drawing.Point(72, 10);
			this.labelCustomerNo.Name = "labelCustomerNo";
			this.labelCustomerNo.Size = new System.Drawing.Size(127, 20);
			this.labelCustomerNo.TabIndex = 1;
			this.labelCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.White;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Location = new System.Drawing.Point(14, 41);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(528, 119);
			this.label5.TabIndex = 4;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// OnlineDemandCancelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(980, 359);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelCustomerNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewOnlineDemand);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "OnlineDemandCancelForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "各種作業料 作業済申請取消";
			this.Load += new System.EventHandler(this.OnlineDemandCancelForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnlineDemand)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelCustomerName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DataGridView dataGridViewOnlineDemand;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelCustomerNo;
		private System.Windows.Forms.Label label5;
	}
}