namespace PcSupportManager.Forms
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCustomerNo = new System.Windows.Forms.TextBox();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonInputAll = new System.Windows.Forms.RadioButton();
			this.radioButtonInputed = new System.Windows.Forms.RadioButton();
			this.radioButtonNotInput = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewManager)).BeginInit();
			this.groupBox1.SuspendLayout();
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
			this.dataGridViewManager.MultiSelect = false;
			this.dataGridViewManager.Name = "dataGridViewManager";
			this.dataGridViewManager.ReadOnly = true;
			this.dataGridViewManager.RowHeadersVisible = false;
			this.dataGridViewManager.RowTemplate.Height = 21;
			this.dataGridViewManager.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewManager.Size = new System.Drawing.Size(1111, 591);
			this.dataGridViewManager.TabIndex = 4;
			this.dataGridViewManager.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewManager_MouseDoubleClick);
			// 
			// buttonReadOrderInfo
			// 
			this.buttonReadOrderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReadOrderInfo.Location = new System.Drawing.Point(966, 12);
			this.buttonReadOrderInfo.Name = "buttonReadOrderInfo";
			this.buttonReadOrderInfo.Size = new System.Drawing.Size(157, 45);
			this.buttonReadOrderInfo.TabIndex = 5;
			this.buttonReadOrderInfo.Text = "受注情報からの読込み";
			this.buttonReadOrderInfo.UseVisualStyleBackColor = true;
			this.buttonReadOrderInfo.Click += new System.EventHandler(this.buttonReadOrderInfo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■顧客No";
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.textBoxCustomerNo.Location = new System.Drawing.Point(75, 34);
			this.textBoxCustomerNo.MaxLength = 8;
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.Size = new System.Drawing.Size(132, 19);
			this.textBoxCustomerNo.TabIndex = 1;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(214, 34);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonInputAll);
			this.groupBox1.Controls.Add(this.radioButtonInputed);
			this.groupBox1.Controls.Add(this.radioButtonNotInput);
			this.groupBox1.Location = new System.Drawing.Point(312, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(261, 44);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "入力状態絞り込み";
			// 
			// radioButtonInputAll
			// 
			this.radioButtonInputAll.AutoSize = true;
			this.radioButtonInputAll.Checked = true;
			this.radioButtonInputAll.Location = new System.Drawing.Point(17, 18);
			this.radioButtonInputAll.Name = "radioButtonInputAll";
			this.radioButtonInputAll.Size = new System.Drawing.Size(44, 16);
			this.radioButtonInputAll.TabIndex = 0;
			this.radioButtonInputAll.TabStop = true;
			this.radioButtonInputAll.Text = "全て";
			this.radioButtonInputAll.UseVisualStyleBackColor = true;
			this.radioButtonInputAll.CheckedChanged += new System.EventHandler(this.radioButtonInputAll_CheckedChanged);
			// 
			// radioButtonInputed
			// 
			this.radioButtonInputed.AutoSize = true;
			this.radioButtonInputed.Location = new System.Drawing.Point(159, 18);
			this.radioButtonInputed.Name = "radioButtonInputed";
			this.radioButtonInputed.Size = new System.Drawing.Size(70, 16);
			this.radioButtonInputed.TabIndex = 2;
			this.radioButtonInputed.Text = "入力済み";
			this.radioButtonInputed.UseVisualStyleBackColor = true;
			this.radioButtonInputed.CheckedChanged += new System.EventHandler(this.radioButtonInputed_CheckedChanged);
			// 
			// radioButtonNotInput
			// 
			this.radioButtonNotInput.AutoSize = true;
			this.radioButtonNotInput.Location = new System.Drawing.Point(82, 18);
			this.radioButtonNotInput.Name = "radioButtonNotInput";
			this.radioButtonNotInput.Size = new System.Drawing.Size(71, 16);
			this.radioButtonNotInput.TabIndex = 1;
			this.radioButtonNotInput.Text = "入力途中";
			this.radioButtonNotInput.UseVisualStyleBackColor = true;
			this.radioButtonNotInput.CheckedChanged += new System.EventHandler(this.radioButtonNotInput_CheckedChanged);
			// 
			// ManagementForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1135, 666);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonReadOrderInfo);
			this.Controls.Add(this.dataGridViewManager);
			this.Name = "ManagementForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "管理情報登録";
			this.Load += new System.EventHandler(this.ManagementForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewManager)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewManager;
		private System.Windows.Forms.Button buttonReadOrderInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCustomerNo;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonInputed;
		private System.Windows.Forms.RadioButton radioButtonNotInput;
		private System.Windows.Forms.RadioButton radioButtonInputAll;
	}
}