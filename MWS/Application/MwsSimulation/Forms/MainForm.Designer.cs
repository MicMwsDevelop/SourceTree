namespace MwsSimulation.Forms
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonDestinationChange = new System.Windows.Forms.Button();
			this.listBoxEstimate = new System.Windows.Forms.ListBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonPrintEstimate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxBranch = new System.Windows.Forms.ComboBox();
			this.comboBoxStaff = new System.Windows.Forms.ComboBox();
			this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEnvironmant = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonPrintOrder = new System.Windows.Forms.Button();
			this.menuStripMainForm.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonNew
			// 
			this.buttonNew.Location = new System.Drawing.Point(394, 51);
			this.buttonNew.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(99, 33);
			this.buttonNew.TabIndex = 3;
			this.buttonNew.Text = "新規追加";
			this.buttonNew.UseVisualStyleBackColor = true;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Location = new System.Drawing.Point(394, 94);
			this.buttonModify.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(99, 33);
			this.buttonModify.TabIndex = 4;
			this.buttonModify.Text = "変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(394, 137);
			this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(99, 33);
			this.buttonDelete.TabIndex = 5;
			this.buttonDelete.Text = "削除";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonDestinationChange
			// 
			this.buttonDestinationChange.Location = new System.Drawing.Point(394, 180);
			this.buttonDestinationChange.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonDestinationChange.Name = "buttonDestinationChange";
			this.buttonDestinationChange.Size = new System.Drawing.Size(99, 33);
			this.buttonDestinationChange.TabIndex = 6;
			this.buttonDestinationChange.Text = "宛先変更";
			this.buttonDestinationChange.UseVisualStyleBackColor = true;
			this.buttonDestinationChange.Click += new System.EventHandler(this.buttonNameChange_Click);
			// 
			// listBoxEstimate
			// 
			this.listBoxEstimate.FormattingEnabled = true;
			this.listBoxEstimate.ItemHeight = 17;
			this.listBoxEstimate.Location = new System.Drawing.Point(12, 51);
			this.listBoxEstimate.Name = "listBoxEstimate";
			this.listBoxEstimate.Size = new System.Drawing.Size(377, 395);
			this.listBoxEstimate.TabIndex = 2;
			this.listBoxEstimate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxEstimate_MouseDoubleClick);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(394, 518);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(99, 33);
			this.buttonClose.TabIndex = 13;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonPrintEstimate
			// 
			this.buttonPrintEstimate.Location = new System.Drawing.Point(394, 223);
			this.buttonPrintEstimate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonPrintEstimate.Name = "buttonPrintEstimate";
			this.buttonPrintEstimate.Size = new System.Drawing.Size(99, 33);
			this.buttonPrintEstimate.TabIndex = 7;
			this.buttonPrintEstimate.Text = "見積書印刷";
			this.buttonPrintEstimate.UseVisualStyleBackColor = true;
			this.buttonPrintEstimate.Click += new System.EventHandler(this.buttonPrintEstimate_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 457);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 17);
			this.label1.TabIndex = 9;
			this.label1.Text = "■拠点名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 488);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "■担当者名";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "■見積書";
			// 
			// comboBoxBranch
			// 
			this.comboBoxBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBranch.FormattingEnabled = true;
			this.comboBoxBranch.Location = new System.Drawing.Point(95, 454);
			this.comboBoxBranch.Name = "comboBoxBranch";
			this.comboBoxBranch.Size = new System.Drawing.Size(399, 25);
			this.comboBoxBranch.TabIndex = 10;
			this.comboBoxBranch.SelectedIndexChanged += new System.EventHandler(this.comboBoxBranch_SelectedIndexChanged);
			// 
			// comboBoxStaff
			// 
			this.comboBoxStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxStaff.FormattingEnabled = true;
			this.comboBoxStaff.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.comboBoxStaff.Location = new System.Drawing.Point(94, 485);
			this.comboBoxStaff.Name = "comboBoxStaff";
			this.comboBoxStaff.Size = new System.Drawing.Size(399, 25);
			this.comboBoxStaff.TabIndex = 12;
			this.comboBoxStaff.SelectedIndexChanged += new System.EventHandler(this.comboBoxStaff_SelectedIndexChanged);
			// 
			// menuStripMainForm
			// 
			this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
			this.menuStripMainForm.Name = "menuStripMainForm";
			this.menuStripMainForm.Size = new System.Drawing.Size(507, 24);
			this.menuStripMainForm.TabIndex = 0;
			this.menuStripMainForm.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEnvironmant,
            this.toolStripSeparator1,
            this.toolStripMenuItemVersion});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
			this.toolStripMenuItem1.Text = "設定";
			// 
			// toolStripMenuItemEnvironmant
			// 
			this.toolStripMenuItemEnvironmant.Name = "toolStripMenuItemEnvironmant";
			this.toolStripMenuItemEnvironmant.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemEnvironmant.Text = "担当者の登録";
			this.toolStripMenuItemEnvironmant.Click += new System.EventHandler(this.toolStripMenuItemEnvironmant_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
			// 
			// toolStripMenuItemVersion
			// 
			this.toolStripMenuItemVersion.Name = "toolStripMenuItemVersion";
			this.toolStripMenuItemVersion.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemVersion.Text = "バージョン情報";
			this.toolStripMenuItemVersion.Click += new System.EventHandler(this.toolStripMenuItemVersion_Click);
			// 
			// buttonPrintOrder
			// 
			this.buttonPrintOrder.Location = new System.Drawing.Point(394, 266);
			this.buttonPrintOrder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonPrintOrder.Name = "buttonPrintOrder";
			this.buttonPrintOrder.Size = new System.Drawing.Size(99, 45);
			this.buttonPrintOrder.TabIndex = 8;
			this.buttonPrintOrder.Text = "注文書/注文請書印刷";
			this.buttonPrintOrder.UseVisualStyleBackColor = true;
			this.buttonPrintOrder.Click += new System.EventHandler(this.buttonPrintOrder_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(507, 561);
			this.Controls.Add(this.buttonPrintOrder);
			this.Controls.Add(this.comboBoxStaff);
			this.Controls.Add(this.comboBoxBranch);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonPrintEstimate);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.listBoxEstimate);
			this.Controls.Add(this.buttonDestinationChange);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonNew);
			this.Controls.Add(this.menuStripMainForm);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStripMainForm;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "MIC WEB SERVICE 課金シミュレーション";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStripMainForm.ResumeLayout(false);
			this.menuStripMainForm.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonDestinationChange;
		private System.Windows.Forms.ListBox listBoxEstimate;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonPrintEstimate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxBranch;
		private System.Windows.Forms.ComboBox comboBoxStaff;
		private System.Windows.Forms.MenuStrip menuStripMainForm;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnvironmant;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersion;
		private System.Windows.Forms.Button buttonPrintOrder;
	}
}

