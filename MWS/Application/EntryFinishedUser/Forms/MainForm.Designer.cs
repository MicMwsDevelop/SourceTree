namespace EntryFinishedUser.Forms
{
	partial class MainForm
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
			this.dataGridViewFinishedUser = new System.Windows.Forms.DataGridView();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxCount = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonExcel = new System.Windows.Forms.Button();
			this.textBoxTokuisakiNo = new MwsLib.Component.NumericTextBox();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonCheckContractService = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFinishedUser)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewFinishedUser
			// 
			this.dataGridViewFinishedUser.AllowUserToAddRows = false;
			this.dataGridViewFinishedUser.AllowUserToDeleteRows = false;
			this.dataGridViewFinishedUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewFinishedUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewFinishedUser.Location = new System.Drawing.Point(13, 44);
			this.dataGridViewFinishedUser.MultiSelect = false;
			this.dataGridViewFinishedUser.Name = "dataGridViewFinishedUser";
			this.dataGridViewFinishedUser.ReadOnly = true;
			this.dataGridViewFinishedUser.RowHeadersVisible = false;
			this.dataGridViewFinishedUser.RowTemplate.Height = 21;
			this.dataGridViewFinishedUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewFinishedUser.Size = new System.Drawing.Size(1239, 810);
			this.dataGridViewFinishedUser.TabIndex = 8;
			this.dataGridViewFinishedUser.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewFinishedUser_DataBindingComplete);
			this.dataGridViewFinishedUser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewFinishedUser_MouseDoubleClick);
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(220, 11);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 26);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "■得意先No";
			// 
			// textBoxCount
			// 
			this.textBoxCount.BackColor = System.Drawing.Color.White;
			this.textBoxCount.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxCount.Location = new System.Drawing.Point(352, 12);
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.ReadOnly = true;
			this.textBoxCount.Size = new System.Drawing.Size(100, 23);
			this.textBoxCount.TabIndex = 4;
			this.textBoxCount.TabStop = false;
			this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(305, 18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "■件数";
			// 
			// buttonExcel
			// 
			this.buttonExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExcel.Location = new System.Drawing.Point(1177, 11);
			this.buttonExcel.Name = "buttonExcel";
			this.buttonExcel.Size = new System.Drawing.Size(75, 26);
			this.buttonExcel.TabIndex = 7;
			this.buttonExcel.Text = "EXCEL出力";
			this.buttonExcel.UseVisualStyleBackColor = true;
			this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
			// 
			// textBoxTokuisakiNo
			// 
			this.textBoxTokuisakiNo.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxTokuisakiNo.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.textBoxTokuisakiNo.Location = new System.Drawing.Point(82, 12);
			this.textBoxTokuisakiNo.MaxLength = 6;
			this.textBoxTokuisakiNo.Name = "textBoxTokuisakiNo";
			this.textBoxTokuisakiNo.Size = new System.Drawing.Size(132, 26);
			this.textBoxTokuisakiNo.TabIndex = 1;
			// 
			// buttonRemove
			// 
			this.buttonRemove.Location = new System.Drawing.Point(469, 12);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(75, 26);
			this.buttonRemove.TabIndex = 5;
			this.buttonRemove.Text = "削除";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonCheckContractService
			// 
			this.buttonCheckContractService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCheckContractService.Location = new System.Drawing.Point(1061, 11);
			this.buttonCheckContractService.Name = "buttonCheckContractService";
			this.buttonCheckContractService.Size = new System.Drawing.Size(110, 26);
			this.buttonCheckContractService.TabIndex = 6;
			this.buttonCheckContractService.Text = "契約中サービス確認";
			this.buttonCheckContractService.UseVisualStyleBackColor = true;
			this.buttonCheckContractService.Click += new System.EventHandler(this.buttonCheckContractService_Click);
			// 
			// MainForm
			// 
			this.AcceptButton = this.buttonSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 866);
			this.Controls.Add(this.buttonCheckContractService);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonExcel);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBoxTokuisakiNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewFinishedUser);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "終了ユーザーリスト参照";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFinishedUser)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewFinishedUser;
		private System.Windows.Forms.Button buttonSearch;
		private MwsLib.Component.NumericTextBox textBoxTokuisakiNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCount;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonExcel;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonCheckContractService;
	}
}