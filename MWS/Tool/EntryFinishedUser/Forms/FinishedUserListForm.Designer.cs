namespace EntryFinishedUser.Forms
{
	partial class FinishedUserListForm
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
			this.buttonShowList = new System.Windows.Forms.Button();
			this.textBoxTokuisakiNo = new MwsLib.Component.NumericTextBox();
			this.buttonRemove = new System.Windows.Forms.Button();
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
			this.dataGridViewFinishedUser.TabIndex = 6;
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
			// buttonShowList
			// 
			this.buttonShowList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonShowList.Location = new System.Drawing.Point(1177, 11);
			this.buttonShowList.Name = "buttonShowList";
			this.buttonShowList.Size = new System.Drawing.Size(75, 26);
			this.buttonShowList.TabIndex = 5;
			this.buttonShowList.Text = "リスト参照";
			this.buttonShowList.UseVisualStyleBackColor = true;
			this.buttonShowList.Click += new System.EventHandler(this.buttonShowList_Click);
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
			this.buttonRemove.TabIndex = 7;
			this.buttonRemove.Text = "削除";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// FinishedUserListForm
			// 
			this.AcceptButton = this.buttonSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 866);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonShowList);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBoxTokuisakiNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewFinishedUser);
			this.Name = "FinishedUserListForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "終了ユーザーリスト参照";
			this.Load += new System.EventHandler(this.FinishedUserListForm_Load);
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
		private System.Windows.Forms.Button buttonShowList;
		private System.Windows.Forms.Button buttonRemove;
	}
}