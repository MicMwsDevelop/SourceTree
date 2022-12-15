
namespace OnlineLicenseProgressEntry.Forms
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
			this.buttonExecProgress = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxProgress = new System.Windows.Forms.TextBox();
			this.buttonProgress = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxClinicList = new System.Windows.Forms.TextBox();
			this.buttonClinicList = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonExecProgress
			// 
			this.buttonExecProgress.Location = new System.Drawing.Point(387, 141);
			this.buttonExecProgress.Margin = new System.Windows.Forms.Padding(4);
			this.buttonExecProgress.Name = "buttonExecProgress";
			this.buttonExecProgress.Size = new System.Drawing.Size(228, 67);
			this.buttonExecProgress.TabIndex = 6;
			this.buttonExecProgress.Text = "進捗管理情報の登録";
			this.buttonExecProgress.UseVisualStyleBackColor = true;
			this.buttonExecProgress.Click += new System.EventHandler(this.buttonExecProgress_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "■進捗管理ファイル";
			// 
			// textBoxProgress
			// 
			this.textBoxProgress.BackColor = System.Drawing.Color.White;
			this.textBoxProgress.Location = new System.Drawing.Point(18, 48);
			this.textBoxProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxProgress.Name = "textBoxProgress";
			this.textBoxProgress.ReadOnly = true;
			this.textBoxProgress.Size = new System.Drawing.Size(566, 24);
			this.textBoxProgress.TabIndex = 1;
			// 
			// buttonProgress
			// 
			this.buttonProgress.Location = new System.Drawing.Point(585, 48);
			this.buttonProgress.Name = "buttonProgress";
			this.buttonProgress.Size = new System.Drawing.Size(30, 24);
			this.buttonProgress.TabIndex = 2;
			this.buttonProgress.Text = "▼";
			this.buttonProgress.UseVisualStyleBackColor = true;
			this.buttonProgress.Click += new System.EventHandler(this.buttonProgress_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxClinicList);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBoxProgress);
			this.groupBox1.Controls.Add(this.buttonClinicList);
			this.groupBox1.Controls.Add(this.buttonExecProgress);
			this.groupBox1.Controls.Add(this.buttonProgress);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(630, 223);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "進捗管理情報登録";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(458, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "■マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）フォルダ";
			// 
			// textBoxClinicList
			// 
			this.textBoxClinicList.BackColor = System.Drawing.Color.White;
			this.textBoxClinicList.Location = new System.Drawing.Point(18, 109);
			this.textBoxClinicList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBoxClinicList.Name = "textBoxClinicList";
			this.textBoxClinicList.ReadOnly = true;
			this.textBoxClinicList.Size = new System.Drawing.Size(566, 24);
			this.textBoxClinicList.TabIndex = 4;
			// 
			// buttonClinicList
			// 
			this.buttonClinicList.Location = new System.Drawing.Point(585, 109);
			this.buttonClinicList.Name = "buttonClinicList";
			this.buttonClinicList.Size = new System.Drawing.Size(30, 24);
			this.buttonClinicList.TabIndex = 5;
			this.buttonClinicList.Text = "▼";
			this.buttonClinicList.UseVisualStyleBackColor = true;
			this.buttonClinicList.Click += new System.EventHandler(this.buttonClinicList_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 252);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "オンライン資格確認進捗管理情報登録";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonExecProgress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxProgress;
		private System.Windows.Forms.Button buttonProgress;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxClinicList;
		private System.Windows.Forms.Button buttonClinicList;
	}
}

