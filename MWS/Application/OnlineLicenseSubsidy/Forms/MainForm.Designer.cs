
namespace OnlineLicenseSubsidy.Forms
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
			this.buttonExport = new System.Windows.Forms.Button();
			this.buttonWorkbook = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxYearMonth = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxWorkbook = new System.Windows.Forms.TextBox();
			this.buttonInputWorkbook = new System.Windows.Forms.Button();
			this.radioButtonEast = new System.Windows.Forms.RadioButton();
			this.radioButtonWest = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonExport
			// 
			this.buttonExport.Enabled = false;
			this.buttonExport.Location = new System.Drawing.Point(463, 76);
			this.buttonExport.Margin = new System.Windows.Forms.Padding(4);
			this.buttonExport.Name = "buttonExport";
			this.buttonExport.Size = new System.Drawing.Size(160, 56);
			this.buttonExport.TabIndex = 3;
			this.buttonExport.Text = "書類l出力";
			this.buttonExport.UseVisualStyleBackColor = true;
			this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
			// 
			// buttonWorkbook
			// 
			this.buttonWorkbook.Location = new System.Drawing.Point(463, 72);
			this.buttonWorkbook.Margin = new System.Windows.Forms.Padding(4);
			this.buttonWorkbook.Name = "buttonWorkbook";
			this.buttonWorkbook.Size = new System.Drawing.Size(160, 56);
			this.buttonWorkbook.TabIndex = 4;
			this.buttonWorkbook.Text = "作業リスト出力";
			this.buttonWorkbook.UseVisualStyleBackColor = true;
			this.buttonWorkbook.Click += new System.EventHandler(this.buttonWorkbook_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "■出力対象月";
			// 
			// comboBoxYearMonth
			// 
			this.comboBoxYearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxYearMonth.FormattingEnabled = true;
			this.comboBoxYearMonth.Location = new System.Drawing.Point(121, 89);
			this.comboBoxYearMonth.Name = "comboBoxYearMonth";
			this.comboBoxYearMonth.Size = new System.Drawing.Size(242, 25);
			this.comboBoxYearMonth.TabIndex = 3;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonWest);
			this.groupBox1.Controls.Add(this.radioButtonEast);
			this.groupBox1.Controls.Add(this.comboBoxYearMonth);
			this.groupBox1.Controls.Add(this.buttonWorkbook);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(640, 141);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "完了報告書/領収内訳書読込";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonInputWorkbook);
			this.groupBox2.Controls.Add(this.textBoxWorkbook);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.buttonExport);
			this.groupBox2.Location = new System.Drawing.Point(13, 161);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(640, 149);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "補助金申請書類出力";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "■作業リスト";
			// 
			// textBoxWorkbook
			// 
			this.textBoxWorkbook.BackColor = System.Drawing.Color.White;
			this.textBoxWorkbook.Location = new System.Drawing.Point(10, 45);
			this.textBoxWorkbook.Name = "textBoxWorkbook";
			this.textBoxWorkbook.ReadOnly = true;
			this.textBoxWorkbook.Size = new System.Drawing.Size(579, 24);
			this.textBoxWorkbook.TabIndex = 1;
			// 
			// buttonInputWorkbook
			// 
			this.buttonInputWorkbook.Location = new System.Drawing.Point(590, 45);
			this.buttonInputWorkbook.Name = "buttonInputWorkbook";
			this.buttonInputWorkbook.Size = new System.Drawing.Size(33, 24);
			this.buttonInputWorkbook.TabIndex = 2;
			this.buttonInputWorkbook.Text = "▼";
			this.buttonInputWorkbook.UseVisualStyleBackColor = true;
			this.buttonInputWorkbook.Click += new System.EventHandler(this.buttonInputWorkbook_Click);
			// 
			// radioButtonEast
			// 
			this.radioButtonEast.AutoSize = true;
			this.radioButtonEast.Checked = true;
			this.radioButtonEast.Location = new System.Drawing.Point(32, 35);
			this.radioButtonEast.Name = "radioButtonEast";
			this.radioButtonEast.Size = new System.Drawing.Size(91, 21);
			this.radioButtonEast.TabIndex = 0;
			this.radioButtonEast.TabStop = true;
			this.radioButtonEast.Text = "NTT東日本";
			this.radioButtonEast.UseVisualStyleBackColor = true;
			// 
			// radioButtonWest
			// 
			this.radioButtonWest.AutoSize = true;
			this.radioButtonWest.Location = new System.Drawing.Point(129, 35);
			this.radioButtonWest.Name = "radioButtonWest";
			this.radioButtonWest.Size = new System.Drawing.Size(91, 21);
			this.radioButtonWest.TabIndex = 1;
			this.radioButtonWest.Text = "NTT西日本";
			this.radioButtonWest.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(673, 326);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オン資補助金申請書類出力";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonExport;
		private System.Windows.Forms.Button buttonWorkbook;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxYearMonth;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonInputWorkbook;
		private System.Windows.Forms.TextBox textBoxWorkbook;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radioButtonWest;
		private System.Windows.Forms.RadioButton radioButtonEast;
	}
}

