namespace MwsServiceCancelTool.Forms
{
	partial class SetServiceCancelForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetServiceCancelForm));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelCustomerName = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.dataGridViewHeader = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dataGridViewApply = new System.Windows.Forms.DataGridView();
			this.label4 = new System.Windows.Forms.Label();
			this.labelCustomerNo = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewApply)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOK.Location = new System.Drawing.Point(926, 6);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(162, 63);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "利用申込取消";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(926, 73);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(162, 63);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "閉じる";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(258, 6);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(450, 20);
			this.labelCustomerName.TabIndex = 3;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(203, 12);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 11);
			this.label12.TabIndex = 2;
			this.label12.Text = "■顧客名";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label13.Location = new System.Drawing.Point(10, 11);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(54, 11);
			this.label13.TabIndex = 0;
			this.label13.Text = "■顧客No";
			// 
			// dataGridViewHeader
			// 
			this.dataGridViewHeader.AllowUserToAddRows = false;
			this.dataGridViewHeader.AllowUserToDeleteRows = false;
			this.dataGridViewHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewHeader.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridViewHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewHeader.Location = new System.Drawing.Point(12, 190);
			this.dataGridViewHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridViewHeader.MultiSelect = false;
			this.dataGridViewHeader.Name = "dataGridViewHeader";
			this.dataGridViewHeader.ReadOnly = true;
			this.dataGridViewHeader.RowHeadersVisible = false;
			this.dataGridViewHeader.RowTemplate.Height = 21;
			this.dataGridViewHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewHeader.Size = new System.Drawing.Size(1076, 145);
			this.dataGridViewHeader.TabIndex = 6;
			this.dataGridViewHeader.SelectionChanged += new System.EventHandler(this.dataGridViewHeader_SelectionChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 174);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(262, 11);
			this.label1.TabIndex = 5;
			this.label1.Text = "■契約ヘッダ情報（T_USE_CONTRACT_HEADER）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(13, 349);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(153, 11);
			this.label3.TabIndex = 7;
			this.label3.Text = "■カプラー申込情報（APPLY）";
			// 
			// dataGridViewApply
			// 
			this.dataGridViewApply.AllowUserToAddRows = false;
			this.dataGridViewApply.AllowUserToDeleteRows = false;
			this.dataGridViewApply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewApply.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridViewApply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewApply.Location = new System.Drawing.Point(12, 365);
			this.dataGridViewApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridViewApply.MultiSelect = false;
			this.dataGridViewApply.Name = "dataGridViewApply";
			this.dataGridViewApply.ReadOnly = true;
			this.dataGridViewApply.RowHeadersVisible = false;
			this.dataGridViewApply.RowTemplate.Height = 21;
			this.dataGridViewApply.Size = new System.Drawing.Size(1076, 231);
			this.dataGridViewApply.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(172, 349);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(515, 11);
			this.label4.TabIndex = 8;
			this.label4.Text = "※セット割サービス利用申込時に追加されたサービス。カプラー管理画面にて利用申込を取消してください";
			// 
			// labelCustomerNo
			// 
			this.labelCustomerNo.BackColor = System.Drawing.Color.White;
			this.labelCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerNo.Location = new System.Drawing.Point(70, 7);
			this.labelCustomerNo.Name = "labelCustomerNo";
			this.labelCustomerNo.Size = new System.Drawing.Size(127, 20);
			this.labelCustomerNo.TabIndex = 1;
			this.labelCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.White;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Location = new System.Drawing.Point(12, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(528, 119);
			this.label5.TabIndex = 4;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// SetServiceCancelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1102, 607);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelCustomerNo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dataGridViewApply);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewHeader);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "SetServiceCancelForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "セット割サービス 利用申込取消";
			this.Load += new System.EventHandler(this.SetServiceCancelForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewApply)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelCustomerName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.DataGridView dataGridViewHeader;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dataGridViewApply;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelCustomerNo;
		private System.Windows.Forms.Label label5;
	}
}