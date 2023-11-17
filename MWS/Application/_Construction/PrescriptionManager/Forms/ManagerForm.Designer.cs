namespace PrescriptionManager.Forms
{
	partial class ManagerForm
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
			this.textBoxCount = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dataGridViewUser = new System.Windows.Forms.DataGridView();
			this.textBoxCustomerNo = new MwsLib.Component.NumericTextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxCount
			// 
			this.textBoxCount.BackColor = System.Drawing.Color.White;
			this.textBoxCount.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxCount.Location = new System.Drawing.Point(355, 19);
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.ReadOnly = true;
			this.textBoxCount.Size = new System.Drawing.Size(100, 23);
			this.textBoxCount.TabIndex = 9;
			this.textBoxCount.TabStop = false;
			this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(302, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 8;
			this.label2.Text = "■件数";
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(221, 16);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 26);
			this.buttonSearch.TabIndex = 7;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "■顧客No";
			// 
			// dataGridViewUser
			// 
			this.dataGridViewUser.AllowUserToAddRows = false;
			this.dataGridViewUser.AllowUserToDeleteRows = false;
			this.dataGridViewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewUser.Location = new System.Drawing.Point(15, 60);
			this.dataGridViewUser.MultiSelect = false;
			this.dataGridViewUser.Name = "dataGridViewUser";
			this.dataGridViewUser.ReadOnly = true;
			this.dataGridViewUser.RowHeadersVisible = false;
			this.dataGridViewUser.RowTemplate.Height = 21;
			this.dataGridViewUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewUser.Size = new System.Drawing.Size(1210, 697);
			this.dataGridViewUser.TabIndex = 10;
			this.dataGridViewUser.BindingContextChanged += new System.EventHandler(this.dataGridViewUser_BindingContextChanged);
			this.dataGridViewUser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewUser_MouseDoubleClick);
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxCustomerNo.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.textBoxCustomerNo.Location = new System.Drawing.Point(83, 16);
			this.textBoxCustomerNo.MaxLength = 8;
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.Size = new System.Drawing.Size(132, 26);
			this.textBoxCustomerNo.TabIndex = 6;
			// 
			// ManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1237, 769);
			this.Controls.Add(this.dataGridViewUser);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ManagerForm";
			this.Text = "電子処方箋契約情報管理";
			this.Load += new System.EventHandler(this.ManagerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxCount;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonSearch;
		private MwsLib.Component.NumericTextBox textBoxCustomerNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dataGridViewUser;
	}
}