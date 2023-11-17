
namespace ProspectProgressAutoAggregate.Forms
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
			this.buttonExec = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonAddNew = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.comboBoxPeriod = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// buttonExec
			// 
			this.buttonExec.Location = new System.Drawing.Point(24, 52);
			this.buttonExec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonExec.Name = "buttonExec";
			this.buttonExec.Size = new System.Drawing.Size(371, 183);
			this.buttonExec.TabIndex = 2;
			this.buttonExec.Text = "見込進捗エクセル出力";
			this.buttonExec.UseVisualStyleBackColor = true;
			this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 21);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "■決算期";
			// 
			// buttonAddNew
			// 
			this.buttonAddNew.Enabled = false;
			this.buttonAddNew.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonAddNew.Location = new System.Drawing.Point(171, 253);
			this.buttonAddNew.Name = "buttonAddNew";
			this.buttonAddNew.Size = new System.Drawing.Size(109, 27);
			this.buttonAddNew.TabIndex = 3;
			this.buttonAddNew.Text = "来期追加";
			this.buttonAddNew.UseVisualStyleBackColor = true;
			this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Enabled = false;
			this.buttonModify.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonModify.Location = new System.Drawing.Point(286, 253);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(109, 27);
			this.buttonModify.TabIndex = 4;
			this.buttonModify.Text = "実績設定";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// comboBoxPeriod
			// 
			this.comboBoxPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPeriod.FormattingEnabled = true;
			this.comboBoxPeriod.Location = new System.Drawing.Point(96, 18);
			this.comboBoxPeriod.Name = "comboBoxPeriod";
			this.comboBoxPeriod.Size = new System.Drawing.Size(80, 27);
			this.comboBoxPeriod.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(417, 296);
			this.Controls.Add(this.comboBoxPeriod);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonAddNew);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonExec);
			this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "見込進捗自動集計";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonExec;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAddNew;
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.ComboBox comboBoxPeriod;
	}
}

