namespace MwsServiceFrequency
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
			this.buttonAdd = new System.Windows.Forms.Button();
			this.listBoxFrequency = new System.Windows.Forms.ListBox();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonShowList = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(378, 15);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(86, 26);
			this.buttonAdd.TabIndex = 0;
			this.buttonAdd.Text = "追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// listBoxFrequency
			// 
			this.listBoxFrequency.FormattingEnabled = true;
			this.listBoxFrequency.ItemHeight = 17;
			this.listBoxFrequency.Location = new System.Drawing.Point(15, 15);
			this.listBoxFrequency.Name = "listBoxFrequency";
			this.listBoxFrequency.Size = new System.Drawing.Size(357, 633);
			this.listBoxFrequency.TabIndex = 1;
			// 
			// buttonRemove
			// 
			this.buttonRemove.Location = new System.Drawing.Point(378, 47);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(86, 26);
			this.buttonRemove.TabIndex = 2;
			this.buttonRemove.Text = "削除";
			this.buttonRemove.UseVisualStyleBackColor = true;
			// 
			// buttonShowList
			// 
			this.buttonShowList.Location = new System.Drawing.Point(378, 165);
			this.buttonShowList.Name = "buttonShowList";
			this.buttonShowList.Size = new System.Drawing.Size(86, 26);
			this.buttonShowList.TabIndex = 3;
			this.buttonShowList.Text = "利用頻度";
			this.buttonShowList.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(378, 197);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(86, 26);
			this.button1.TabIndex = 4;
			this.button1.Text = "推移表";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(378, 229);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 26);
			this.button2.TabIndex = 5;
			this.button2.Text = "集計表";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(378, 261);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(86, 26);
			this.button3.TabIndex = 6;
			this.button3.Text = "営業部別";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 658);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.buttonShowList);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.listBoxFrequency);
			this.Controls.Add(this.buttonAdd);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.Name = "MainForm";
			this.Text = "paletteサービス利用頻度";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ListBox listBoxFrequency;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonShowList;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}

