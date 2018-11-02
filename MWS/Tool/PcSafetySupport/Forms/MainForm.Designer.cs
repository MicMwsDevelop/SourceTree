namespace PcSafetySupport.Forms
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
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this.buttonDaily = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dateTimePickerSystemDate = new System.Windows.Forms.DateTimePicker();
			this.buttonMonthly = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonControl = new System.Windows.Forms.Button();
			this.buttonMale = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonDaily
			// 
			this.buttonDaily.Location = new System.Drawing.Point(19, 172);
			this.buttonDaily.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonDaily.Name = "buttonDaily";
			this.buttonDaily.Size = new System.Drawing.Size(365, 48);
			this.buttonDaily.TabIndex = 0;
			this.buttonDaily.Text = "日時処理";
			this.buttonDaily.UseVisualStyleBackColor = true;
			this.buttonDaily.Click += new System.EventHandler(this.buttonDaily_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "■システム日付";
			// 
			// dateTimePickerSystemDate
			// 
			this.dateTimePickerSystemDate.Location = new System.Drawing.Point(115, 16);
			this.dateTimePickerSystemDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dateTimePickerSystemDate.Name = "dateTimePickerSystemDate";
			this.dateTimePickerSystemDate.Size = new System.Drawing.Size(143, 23);
			this.dateTimePickerSystemDate.TabIndex = 2;
			this.dateTimePickerSystemDate.ValueChanged += new System.EventHandler(this.dateTimePickerSystemDate_ValueChanged);
			// 
			// buttonMonthly
			// 
			this.buttonMonthly.Location = new System.Drawing.Point(19, 228);
			this.buttonMonthly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonMonthly.Name = "buttonMonthly";
			this.buttonMonthly.Size = new System.Drawing.Size(365, 48);
			this.buttonMonthly.TabIndex = 3;
			this.buttonMonthly.Text = "月時処理";
			this.buttonMonthly.UseVisualStyleBackColor = true;
			this.buttonMonthly.Click += new System.EventHandler(this.buttonMonthly_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(296, 282);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(87, 48);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonControl
			// 
			this.buttonControl.Location = new System.Drawing.Point(19, 62);
			this.buttonControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonControl.Name = "buttonControl";
			this.buttonControl.Size = new System.Drawing.Size(365, 48);
			this.buttonControl.TabIndex = 5;
			this.buttonControl.Text = "管理情報登録";
			this.buttonControl.UseVisualStyleBackColor = true;
			this.buttonControl.Click += new System.EventHandler(this.buttonControl_Click);
			// 
			// buttonMale
			// 
			this.buttonMale.Location = new System.Drawing.Point(19, 118);
			this.buttonMale.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonMale.Name = "buttonMale";
			this.buttonMale.Size = new System.Drawing.Size(365, 48);
			this.buttonMale.TabIndex = 6;
			this.buttonMale.Text = "送信メール情報";
			this.buttonMale.UseVisualStyleBackColor = true;
			this.buttonMale.Click += new System.EventHandler(this.buttonMale_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(405, 348);
			this.Controls.Add(this.buttonMale);
			this.Controls.Add(this.buttonControl);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonMonthly);
			this.Controls.Add(this.dateTimePickerSystemDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonDaily);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ＰＣ安心サポート管理ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDaily;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePickerSystemDate;
		private System.Windows.Forms.Button buttonMonthly;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonControl;
		private System.Windows.Forms.Button buttonMale;
	}
}

