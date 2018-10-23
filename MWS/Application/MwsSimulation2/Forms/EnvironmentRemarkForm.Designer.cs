namespace MwsSimulation.Forms
{
	partial class EnvironmentRemarkForm
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.listBoxRemark = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(248, 294);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(140, 294);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 36);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// listBoxRemark
			// 
			this.listBoxRemark.FormattingEnabled = true;
			this.listBoxRemark.ItemHeight = 17;
			this.listBoxRemark.Location = new System.Drawing.Point(12, 29);
			this.listBoxRemark.Name = "listBoxRemark";
			this.listBoxRemark.Size = new System.Drawing.Size(338, 259);
			this.listBoxRemark.TabIndex = 1;
			this.listBoxRemark.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxRemark_MouseDoubleClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "■備考";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(356, 101);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(61, 30);
			this.buttonDelete.TabIndex = 4;
			this.buttonDelete.Text = "削除";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(356, 29);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(61, 30);
			this.buttonAdd.TabIndex = 2;
			this.buttonAdd.Text = "追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Location = new System.Drawing.Point(356, 65);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(61, 30);
			this.buttonModify.TabIndex = 3;
			this.buttonModify.Text = "変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// EnvironmentRemarkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(431, 342);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBoxRemark);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EnvironmentRemarkForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "備考の登録";
			this.Load += new System.EventHandler(this.EnvironmentForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.ListBox listBoxRemark;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonModify;
	}
}