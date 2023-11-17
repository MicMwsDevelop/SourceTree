namespace MwsKakinBatchTool.Forms
{
	partial class MainForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.dateTimePickerExecDate = new System.Windows.Forms.DateTimePicker();
			this.buttonExec = new System.Windows.Forms.Button();
			this.checkBoxUse = new System.Windows.Forms.CheckBox();
			this.checkBoxCancel = new System.Windows.Forms.CheckBox();
			this.checkBoxMonthly = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "実行日";
			// 
			// dateTimePickerExecDate
			// 
			this.dateTimePickerExecDate.Location = new System.Drawing.Point(74, 13);
			this.dateTimePickerExecDate.Name = "dateTimePickerExecDate";
			this.dateTimePickerExecDate.Size = new System.Drawing.Size(158, 27);
			this.dateTimePickerExecDate.TabIndex = 1;
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(135, 188);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(214, 59);
			this.buttonExec.TabIndex = 6;
			this.buttonExec.Text = "実行";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// checkBoxUse
			// 
			this.checkBoxUse.AutoSize = true;
			this.checkBoxUse.Checked = true;
			this.checkBoxUse.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxUse.Location = new System.Drawing.Point(74, 81);
			this.checkBoxUse.Name = "checkBoxUse";
			this.checkBoxUse.Size = new System.Drawing.Size(131, 23);
			this.checkBoxUse.TabIndex = 3;
			this.checkBoxUse.Text = "利用申込み反映";
			this.checkBoxUse.UseVisualStyleBackColor = true;
			// 
			// checkBoxCancel
			// 
			this.checkBoxCancel.AutoSize = true;
			this.checkBoxCancel.Checked = true;
			this.checkBoxCancel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxCancel.Location = new System.Drawing.Point(74, 110);
			this.checkBoxCancel.Name = "checkBoxCancel";
			this.checkBoxCancel.Size = new System.Drawing.Size(131, 23);
			this.checkBoxCancel.TabIndex = 4;
			this.checkBoxCancel.Text = "解約申込み反映";
			this.checkBoxCancel.UseVisualStyleBackColor = true;
			// 
			// checkBoxMonthly
			// 
			this.checkBoxMonthly.AutoSize = true;
			this.checkBoxMonthly.Location = new System.Drawing.Point(74, 139);
			this.checkBoxMonthly.Name = "checkBoxMonthly";
			this.checkBoxMonthly.Size = new System.Drawing.Size(118, 23);
			this.checkBoxMonthly.TabIndex = 5;
			this.checkBoxMonthly.Text = "月額利用更新";
			this.checkBoxMonthly.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(70, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(223, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "※実行日の先月分の申込みが対象";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(361, 259);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBoxMonthly);
			this.Controls.Add(this.checkBoxCancel);
			this.Controls.Add(this.checkBoxUse);
			this.Controls.Add(this.buttonExec);
			this.Controls.Add(this.dateTimePickerExecDate);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "課金代替バッチツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePickerExecDate;
		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.CheckBox checkBoxUse;
		private System.Windows.Forms.CheckBox checkBoxCancel;
		private System.Windows.Forms.CheckBox checkBoxMonthly;
		private System.Windows.Forms.Label label2;
	}
}

