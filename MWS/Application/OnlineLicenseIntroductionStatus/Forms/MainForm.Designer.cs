
namespace OnlineLicenseIntroductionStatus.Forms
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
			this.buttonFileOut = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxProgress = new System.Windows.Forms.TextBox();
			this.buttonProgress = new System.Windows.Forms.Button();
			this.checkBoxDatabase = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonFileOut
			// 
			this.buttonFileOut.Location = new System.Drawing.Point(385, 71);
			this.buttonFileOut.Margin = new System.Windows.Forms.Padding(4);
			this.buttonFileOut.Name = "buttonFileOut";
			this.buttonFileOut.Size = new System.Drawing.Size(228, 67);
			this.buttonFileOut.TabIndex = 5;
			this.buttonFileOut.Text = "実行";
			this.buttonFileOut.UseVisualStyleBackColor = true;
			this.buttonFileOut.Click += new System.EventHandler(this.buttonFileOut_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "■進捗管理ファイル";
			// 
			// textBoxProgress
			// 
			this.textBoxProgress.BackColor = System.Drawing.Color.White;
			this.textBoxProgress.Location = new System.Drawing.Point(17, 39);
			this.textBoxProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxProgress.Name = "textBoxProgress";
			this.textBoxProgress.ReadOnly = true;
			this.textBoxProgress.Size = new System.Drawing.Size(566, 24);
			this.textBoxProgress.TabIndex = 1;
			// 
			// buttonProgress
			// 
			this.buttonProgress.Location = new System.Drawing.Point(583, 39);
			this.buttonProgress.Name = "buttonProgress";
			this.buttonProgress.Size = new System.Drawing.Size(30, 24);
			this.buttonProgress.TabIndex = 2;
			this.buttonProgress.Text = "▼";
			this.buttonProgress.UseVisualStyleBackColor = true;
			this.buttonProgress.Click += new System.EventHandler(this.buttonProgress_Click);
			// 
			// checkBoxDatabase
			// 
			this.checkBoxDatabase.AutoSize = true;
			this.checkBoxDatabase.Location = new System.Drawing.Point(17, 71);
			this.checkBoxDatabase.Name = "checkBoxDatabase";
			this.checkBoxDatabase.Size = new System.Drawing.Size(235, 21);
			this.checkBoxDatabase.TabIndex = 3;
			this.checkBoxDatabase.Text = "進捗管理情報をデータベースに登録する";
			this.checkBoxDatabase.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(14, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(364, 39);
			this.label2.TabIndex = 4;
			this.label2.Text = "※導入状況を参照するだけの場合にはデータベースには登録しない";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(625, 158);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBoxDatabase);
			this.Controls.Add(this.buttonProgress);
			this.Controls.Add(this.textBoxProgress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonFileOut);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オンライン資格確認導入状況";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonFileOut;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxProgress;
		private System.Windows.Forms.Button buttonProgress;
		private System.Windows.Forms.CheckBox checkBoxDatabase;
		private System.Windows.Forms.Label label2;
	}
}

