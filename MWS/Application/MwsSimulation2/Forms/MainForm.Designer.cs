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
			this.listBoxMatome = new System.Windows.Forms.ListBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonPrintEstimate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxBranch = new System.Windows.Forms.ComboBox();
			this.comboBoxStaff = new System.Windows.Forms.ComboBox();
			this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEnvStaff = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEnvRemark = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonPrintOrder = new System.Windows.Forms.Button();
			this.buttonCopy = new System.Windows.Forms.Button();
			this.tabControlEstimate = new System.Windows.Forms.TabControl();
			this.tabPageMatome = new System.Windows.Forms.TabPage();
			this.tabPageMonthly = new System.Windows.Forms.TabPage();
			this.listBoxMonthly = new System.Windows.Forms.ListBox();
			this.menuStripMainForm.SuspendLayout();
			this.tabControlEstimate.SuspendLayout();
			this.tabPageMatome.SuspendLayout();
			this.tabPageMonthly.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonNew
			// 
			this.buttonNew.Location = new System.Drawing.Point(425, 68);
			this.buttonNew.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(99, 33);
			this.buttonNew.TabIndex = 2;
			this.buttonNew.Text = "新規追加";
			this.buttonNew.UseVisualStyleBackColor = true;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Location = new System.Drawing.Point(425, 111);
			this.buttonModify.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(99, 33);
			this.buttonModify.TabIndex = 3;
			this.buttonModify.Text = "変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(425, 154);
			this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(99, 33);
			this.buttonDelete.TabIndex = 4;
			this.buttonDelete.Text = "削除";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonDestinationChange
			// 
			this.buttonDestinationChange.Location = new System.Drawing.Point(425, 197);
			this.buttonDestinationChange.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonDestinationChange.Name = "buttonDestinationChange";
			this.buttonDestinationChange.Size = new System.Drawing.Size(99, 33);
			this.buttonDestinationChange.TabIndex = 5;
			this.buttonDestinationChange.Text = "宛先変更";
			this.buttonDestinationChange.UseVisualStyleBackColor = true;
			this.buttonDestinationChange.Click += new System.EventHandler(this.buttonNameChange_Click);
			// 
			// listBoxMatome
			// 
			this.listBoxMatome.FormattingEnabled = true;
			this.listBoxMatome.ItemHeight = 17;
			this.listBoxMatome.Location = new System.Drawing.Point(6, 6);
			this.listBoxMatome.Name = "listBoxMatome";
			this.listBoxMatome.Size = new System.Drawing.Size(377, 378);
			this.listBoxMatome.TabIndex = 0;
			this.listBoxMatome.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxMatome_MouseDoubleClick);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(425, 543);
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
			this.buttonPrintEstimate.Location = new System.Drawing.Point(426, 283);
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
			this.label1.Location = new System.Drawing.Point(14, 479);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 17);
			this.label1.TabIndex = 9;
			this.label1.Text = "■拠点名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 510);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "■担当者名";
			// 
			// comboBoxBranch
			// 
			this.comboBoxBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBranch.FormattingEnabled = true;
			this.comboBoxBranch.Location = new System.Drawing.Point(124, 476);
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
			this.comboBoxStaff.Location = new System.Drawing.Point(123, 507);
			this.comboBoxStaff.Name = "comboBoxStaff";
			this.comboBoxStaff.Size = new System.Drawing.Size(399, 25);
			this.comboBoxStaff.TabIndex = 12;
			this.comboBoxStaff.SelectedIndexChanged += new System.EventHandler(this.comboBoxStaff_SelectedIndexChanged);
			// 
			// menuStripMainForm
			// 
			this.menuStripMainForm.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
			this.menuStripMainForm.Name = "menuStripMainForm";
			this.menuStripMainForm.Size = new System.Drawing.Size(545, 24);
			this.menuStripMainForm.TabIndex = 0;
			this.menuStripMainForm.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEnvStaff,
            this.toolStripMenuItemEnvRemark,
            this.toolStripSeparator1,
            this.toolStripMenuItemVersion});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
			this.toolStripMenuItem1.Text = "設定";
			// 
			// toolStripMenuItemEnvStaff
			// 
			this.toolStripMenuItemEnvStaff.Name = "toolStripMenuItemEnvStaff";
			this.toolStripMenuItemEnvStaff.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemEnvStaff.Text = "担当者の登録";
			this.toolStripMenuItemEnvStaff.Click += new System.EventHandler(this.toolStripMenuItemEnvStaff_Click);
			// 
			// toolStripMenuItemEnvRemark
			// 
			this.toolStripMenuItemEnvRemark.Name = "toolStripMenuItemEnvRemark";
			this.toolStripMenuItemEnvRemark.Size = new System.Drawing.Size(144, 22);
			this.toolStripMenuItemEnvRemark.Text = "備考の登録";
			this.toolStripMenuItemEnvRemark.Click += new System.EventHandler(this.toolStripMenuItemEnvRemark_Click);
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
			this.buttonPrintOrder.Location = new System.Drawing.Point(426, 326);
			this.buttonPrintOrder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonPrintOrder.Name = "buttonPrintOrder";
			this.buttonPrintOrder.Size = new System.Drawing.Size(99, 45);
			this.buttonPrintOrder.TabIndex = 8;
			this.buttonPrintOrder.Text = "注文書/注文請書印刷";
			this.buttonPrintOrder.UseVisualStyleBackColor = true;
			this.buttonPrintOrder.Click += new System.EventHandler(this.buttonPrintOrder_Click);
			// 
			// buttonCopy
			// 
			this.buttonCopy.Location = new System.Drawing.Point(425, 240);
			this.buttonCopy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(99, 33);
			this.buttonCopy.TabIndex = 6;
			this.buttonCopy.Text = "見積書コピー";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// tabControlEstimate
			// 
			this.tabControlEstimate.Controls.Add(this.tabPageMatome);
			this.tabControlEstimate.Controls.Add(this.tabPageMonthly);
			this.tabControlEstimate.Location = new System.Drawing.Point(18, 39);
			this.tabControlEstimate.Name = "tabControlEstimate";
			this.tabControlEstimate.SelectedIndex = 0;
			this.tabControlEstimate.Size = new System.Drawing.Size(400, 431);
			this.tabControlEstimate.TabIndex = 1;
			// 
			// tabPageMatome
			// 
			this.tabPageMatome.Controls.Add(this.listBoxMatome);
			this.tabPageMatome.Location = new System.Drawing.Point(4, 26);
			this.tabPageMatome.Name = "tabPageMatome";
			this.tabPageMatome.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMatome.Size = new System.Drawing.Size(392, 401);
			this.tabPageMatome.TabIndex = 0;
			this.tabPageMatome.Text = "おまとめプラン";
			this.tabPageMatome.UseVisualStyleBackColor = true;
			// 
			// tabPageMonthly
			// 
			this.tabPageMonthly.Controls.Add(this.listBoxMonthly);
			this.tabPageMonthly.Location = new System.Drawing.Point(4, 26);
			this.tabPageMonthly.Name = "tabPageMonthly";
			this.tabPageMonthly.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMonthly.Size = new System.Drawing.Size(392, 401);
			this.tabPageMonthly.TabIndex = 1;
			this.tabPageMonthly.Text = "月額課金";
			this.tabPageMonthly.UseVisualStyleBackColor = true;
			// 
			// listBoxMonthly
			// 
			this.listBoxMonthly.FormattingEnabled = true;
			this.listBoxMonthly.ItemHeight = 17;
			this.listBoxMonthly.Location = new System.Drawing.Point(8, 7);
			this.listBoxMonthly.Name = "listBoxMonthly";
			this.listBoxMonthly.Size = new System.Drawing.Size(377, 378);
			this.listBoxMonthly.TabIndex = 2;
			this.listBoxMonthly.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxMonthly_MouseDoubleClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(545, 588);
			this.Controls.Add(this.tabControlEstimate);
			this.Controls.Add(this.buttonCopy);
			this.Controls.Add(this.buttonPrintOrder);
			this.Controls.Add(this.comboBoxStaff);
			this.Controls.Add(this.comboBoxBranch);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonPrintEstimate);
			this.Controls.Add(this.buttonClose);
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
			this.tabControlEstimate.ResumeLayout(false);
			this.tabPageMatome.ResumeLayout(false);
			this.tabPageMonthly.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonDestinationChange;
		private System.Windows.Forms.ListBox listBoxMatome;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonPrintEstimate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxBranch;
		private System.Windows.Forms.ComboBox comboBoxStaff;
		private System.Windows.Forms.MenuStrip menuStripMainForm;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnvStaff;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersion;
		private System.Windows.Forms.Button buttonPrintOrder;
		private System.Windows.Forms.Button buttonCopy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnvRemark;
		private System.Windows.Forms.TabControl tabControlEstimate;
		private System.Windows.Forms.TabPage tabPageMatome;
		private System.Windows.Forms.TabPage tabPageMonthly;
		private System.Windows.Forms.ListBox listBoxMonthly;
	}
}

