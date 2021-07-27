
namespace UserDataManager.Forms
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
			this.comboBox登録部署 = new System.Windows.Forms.ComboBox();
			this.button抽出 = new System.Windows.Forms.Button();
			this.textBox検索文字列 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button検索 = new System.Windows.Forms.Button();
			this.buttonクリア = new System.Windows.Forms.Button();
			this.dataGridViewDataList = new System.Windows.Forms.DataGridView();
			this.button新規登録 = new System.Windows.Forms.Button();
			this.button編集 = new System.Windows.Forms.Button();
			this.button終了 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox部署名 = new System.Windows.Forms.TextBox();
			this.textBox利用者 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataList)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 8);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "登録部署抽出";
			// 
			// comboBox登録部署
			// 
			this.comboBox登録部署.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox登録部署.FormattingEnabled = true;
			this.comboBox登録部署.Location = new System.Drawing.Point(15, 29);
			this.comboBox登録部署.Name = "comboBox登録部署";
			this.comboBox登録部署.Size = new System.Drawing.Size(251, 25);
			this.comboBox登録部署.TabIndex = 1;
			// 
			// button抽出
			// 
			this.button抽出.Location = new System.Drawing.Point(272, 29);
			this.button抽出.Name = "button抽出";
			this.button抽出.Size = new System.Drawing.Size(44, 24);
			this.button抽出.TabIndex = 2;
			this.button抽出.Text = "抽出";
			this.button抽出.UseVisualStyleBackColor = true;
			this.button抽出.Click += new System.EventHandler(this.button抽出_Click);
			// 
			// textBox検索文字列
			// 
			this.textBox検索文字列.Location = new System.Drawing.Point(322, 29);
			this.textBox検索文字列.Name = "textBox検索文字列";
			this.textBox検索文字列.Size = new System.Drawing.Size(164, 24);
			this.textBox検索文字列.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(322, 9);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(179, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "[名称　ステータス　更新者]検索";
			// 
			// button検索
			// 
			this.button検索.Location = new System.Drawing.Point(491, 29);
			this.button検索.Name = "button検索";
			this.button検索.Size = new System.Drawing.Size(44, 24);
			this.button検索.TabIndex = 5;
			this.button検索.Text = "検索";
			this.button検索.UseVisualStyleBackColor = true;
			this.button検索.Click += new System.EventHandler(this.button検索_Click);
			// 
			// buttonクリア
			// 
			this.buttonクリア.Location = new System.Drawing.Point(540, 29);
			this.buttonクリア.Name = "buttonクリア";
			this.buttonクリア.Size = new System.Drawing.Size(44, 24);
			this.buttonクリア.TabIndex = 6;
			this.buttonクリア.Text = "クリア";
			this.buttonクリア.UseVisualStyleBackColor = true;
			this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
			// 
			// dataGridViewDataList
			// 
			this.dataGridViewDataList.AllowUserToAddRows = false;
			this.dataGridViewDataList.AllowUserToDeleteRows = false;
			this.dataGridViewDataList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dataGridViewDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewDataList.Location = new System.Drawing.Point(15, 59);
			this.dataGridViewDataList.Name = "dataGridViewDataList";
			this.dataGridViewDataList.ReadOnly = true;
			this.dataGridViewDataList.RowTemplate.Height = 21;
			this.dataGridViewDataList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewDataList.Size = new System.Drawing.Size(957, 430);
			this.dataGridViewDataList.TabIndex = 7;
			// 
			// button新規登録
			// 
			this.button新規登録.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button新規登録.Location = new System.Drawing.Point(714, 495);
			this.button新規登録.Name = "button新規登録";
			this.button新規登録.Size = new System.Drawing.Size(83, 44);
			this.button新規登録.TabIndex = 8;
			this.button新規登録.Text = "新規登録";
			this.button新規登録.UseVisualStyleBackColor = true;
			this.button新規登録.Click += new System.EventHandler(this.button新規登録_Click);
			// 
			// button編集
			// 
			this.button編集.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button編集.Location = new System.Drawing.Point(802, 495);
			this.button編集.Name = "button編集";
			this.button編集.Size = new System.Drawing.Size(83, 44);
			this.button編集.TabIndex = 9;
			this.button編集.Text = "編集";
			this.button編集.UseVisualStyleBackColor = true;
			this.button編集.Click += new System.EventHandler(this.button編集_Click);
			// 
			// button終了
			// 
			this.button終了.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button終了.Location = new System.Drawing.Point(890, 495);
			this.button終了.Name = "button終了";
			this.button終了.Size = new System.Drawing.Size(83, 44);
			this.button終了.TabIndex = 10;
			this.button終了.Text = "終了";
			this.button終了.UseVisualStyleBackColor = true;
			this.button終了.Click += new System.EventHandler(this.button終了_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(710, 8);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 17);
			this.label3.TabIndex = 11;
			this.label3.Text = "利用部署：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(710, 29);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 17);
			this.label4.TabIndex = 12;
			this.label4.Text = "利 用 者 ：";
			// 
			// textBox部署名
			// 
			this.textBox部署名.BackColor = System.Drawing.SystemColors.Control;
			this.textBox部署名.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox部署名.Location = new System.Drawing.Point(780, 5);
			this.textBox部署名.Name = "textBox部署名";
			this.textBox部署名.ReadOnly = true;
			this.textBox部署名.Size = new System.Drawing.Size(193, 17);
			this.textBox部署名.TabIndex = 13;
			// 
			// textBox利用者
			// 
			this.textBox利用者.BackColor = System.Drawing.SystemColors.Control;
			this.textBox利用者.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox利用者.Location = new System.Drawing.Point(780, 30);
			this.textBox利用者.Name = "textBox利用者";
			this.textBox利用者.ReadOnly = true;
			this.textBox利用者.Size = new System.Drawing.Size(193, 17);
			this.textBox利用者.TabIndex = 14;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(983, 549);
			this.Controls.Add(this.textBox利用者);
			this.Controls.Add(this.textBox部署名);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button終了);
			this.Controls.Add(this.button編集);
			this.Controls.Add(this.button新規登録);
			this.Controls.Add(this.dataGridViewDataList);
			this.Controls.Add(this.buttonクリア);
			this.Controls.Add(this.button検索);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox検索文字列);
			this.Controls.Add(this.button抽出);
			this.Controls.Add(this.comboBox登録部署);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainForm";
			this.Text = "ユーザーデータ管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox登録部署;
		private System.Windows.Forms.Button button抽出;
		private System.Windows.Forms.TextBox textBox検索文字列;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button検索;
		private System.Windows.Forms.Button buttonクリア;
		private System.Windows.Forms.DataGridView dataGridViewDataList;
		private System.Windows.Forms.Button button新規登録;
		private System.Windows.Forms.Button button編集;
		private System.Windows.Forms.Button button終了;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox部署名;
		private System.Windows.Forms.TextBox textBox利用者;
	}
}

