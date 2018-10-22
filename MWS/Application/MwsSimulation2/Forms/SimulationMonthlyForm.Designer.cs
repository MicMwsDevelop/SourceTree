namespace MwsSimulation.Forms
{
	partial class SimulationMonthlyForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationMonthlyForm));
			this.listViewService = new System.Windows.Forms.ListView();
			this.columnHeaderServiceTypeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderServiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.listViewSetPlan = new System.Windows.Forms.ListView();
			this.columnHeaderSetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSetPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxDestination = new System.Windows.Forms.TextBox();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.textBoxRemark = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.dateTimePickerPrintDate = new System.Windows.Forms.DateTimePicker();
			this.radioSama = new System.Windows.Forms.RadioButton();
			this.radioOnchu = new System.Windows.Forms.RadioButton();
			this.buttonChangeAgreeSpan = new System.Windows.Forms.Button();
			this.labelAgreeSpan = new System.Windows.Forms.Label();
			this.buttonRemarkTemplate = new System.Windows.Forms.Button();
			this.buttonAllOff = new System.Windows.Forms.Button();
			this.buttonAllOn = new System.Windows.Forms.Button();
			this.dateTimePickerLimitDate = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textBoxTotalPrice = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxPlatformPrice = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxServicePrice = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listViewService
			// 
			this.listViewService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewService.CheckBoxes = true;
			this.listViewService.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderServiceTypeName,
            this.columnHeaderServiceName,
            this.columnHeaderPrice});
			this.listViewService.FullRowSelect = true;
			this.listViewService.GridLines = true;
			this.listViewService.HideSelection = false;
			this.listViewService.Location = new System.Drawing.Point(14, 44);
			this.listViewService.Name = "listViewService";
			this.listViewService.Size = new System.Drawing.Size(676, 598);
			this.listViewService.TabIndex = 6;
			this.listViewService.UseCompatibleStateImageBehavior = false;
			this.listViewService.View = System.Windows.Forms.View.Details;
			this.listViewService.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewService_ItemCheck);
			this.listViewService.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewService_ItemChecked);
			// 
			// columnHeaderServiceTypeName
			// 
			this.columnHeaderServiceTypeName.Text = "サービス種別";
			this.columnHeaderServiceTypeName.Width = 180;
			// 
			// columnHeaderServiceName
			// 
			this.columnHeaderServiceName.Text = "サービス名";
			this.columnHeaderServiceName.Width = 350;
			// 
			// columnHeaderPrice
			// 
			this.columnHeaderPrice.Text = "価格";
			this.columnHeaderPrice.Width = 120;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOK.Location = new System.Drawing.Point(907, 606);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 36);
			this.buttonOK.TabIndex = 27;
			this.buttonOK.Text = "保存";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(1015, 606);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 28;
			this.buttonCancel.Text = "破棄";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// listViewSetPlan
			// 
			this.listViewSetPlan.CheckBoxes = true;
			this.listViewSetPlan.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSetName,
            this.columnHeaderSetPrice});
			this.listViewSetPlan.FullRowSelect = true;
			this.listViewSetPlan.HideSelection = false;
			this.listViewSetPlan.Location = new System.Drawing.Point(699, 98);
			this.listViewSetPlan.Name = "listViewSetPlan";
			this.listViewSetPlan.Size = new System.Drawing.Size(415, 94);
			this.listViewSetPlan.TabIndex = 12;
			this.listViewSetPlan.UseCompatibleStateImageBehavior = false;
			this.listViewSetPlan.View = System.Windows.Forms.View.Details;
			this.listViewSetPlan.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewSetPlan_ItemChecked);
			// 
			// columnHeaderSetName
			// 
			this.columnHeaderSetName.Text = "セット名";
			this.columnHeaderSetName.Width = 300;
			// 
			// columnHeaderSetPrice
			// 
			this.columnHeaderSetPrice.Text = "価格";
			this.columnHeaderSetPrice.Width = 100;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(700, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103, 17);
			this.label3.TabIndex = 11;
			this.label3.Text = "■セット割サービス";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "■宛先";
			// 
			// textBoxDestination
			// 
			this.textBoxDestination.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxDestination.Location = new System.Drawing.Point(67, 14);
			this.textBoxDestination.Name = "textBoxDestination";
			this.textBoxDestination.Size = new System.Drawing.Size(301, 24);
			this.textBoxDestination.TabIndex = 1;
			// 
			// buttonPrint
			// 
			this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonPrint.Location = new System.Drawing.Point(696, 606);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(102, 36);
			this.buttonPrint.TabIndex = 26;
			this.buttonPrint.Text = "見積書印刷";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// textBoxRemark
			// 
			this.textBoxRemark.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark.Location = new System.Drawing.Point(699, 234);
			this.textBoxRemark.Multiline = true;
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(415, 81);
			this.textBoxRemark.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(700, 214);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 17);
			this.label8.TabIndex = 13;
			this.label8.Text = "■備考";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(483, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 17);
			this.label9.TabIndex = 4;
			this.label9.Text = "■発行日";
			// 
			// dateTimePickerPrintDate
			// 
			this.dateTimePickerPrintDate.Location = new System.Drawing.Point(549, 14);
			this.dateTimePickerPrintDate.Name = "dateTimePickerPrintDate";
			this.dateTimePickerPrintDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerPrintDate.TabIndex = 5;
			this.dateTimePickerPrintDate.ValueChanged += new System.EventHandler(this.dateTimePickerPrintDate_ValueChanged);
			// 
			// radioSama
			// 
			this.radioSama.AutoSize = true;
			this.radioSama.Location = new System.Drawing.Point(429, 15);
			this.radioSama.Name = "radioSama";
			this.radioSama.Size = new System.Drawing.Size(39, 21);
			this.radioSama.TabIndex = 3;
			this.radioSama.TabStop = true;
			this.radioSama.Text = "様";
			this.radioSama.UseVisualStyleBackColor = true;
			// 
			// radioOnchu
			// 
			this.radioOnchu.AutoSize = true;
			this.radioOnchu.Checked = true;
			this.radioOnchu.Location = new System.Drawing.Point(374, 15);
			this.radioOnchu.Name = "radioOnchu";
			this.radioOnchu.Size = new System.Drawing.Size(52, 21);
			this.radioOnchu.TabIndex = 2;
			this.radioOnchu.TabStop = true;
			this.radioOnchu.Text = "御中";
			this.radioOnchu.UseVisualStyleBackColor = true;
			// 
			// buttonChangeAgreeSpan
			// 
			this.buttonChangeAgreeSpan.Location = new System.Drawing.Point(699, 14);
			this.buttonChangeAgreeSpan.Name = "buttonChangeAgreeSpan";
			this.buttonChangeAgreeSpan.Size = new System.Drawing.Size(92, 25);
			this.buttonChangeAgreeSpan.TabIndex = 7;
			this.buttonChangeAgreeSpan.Text = "■契約期間";
			this.buttonChangeAgreeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonChangeAgreeSpan.UseVisualStyleBackColor = true;
			this.buttonChangeAgreeSpan.Click += new System.EventHandler(this.buttonChangeAgreeSpan_Click);
			// 
			// labelAgreeSpan
			// 
			this.labelAgreeSpan.BackColor = System.Drawing.Color.White;
			this.labelAgreeSpan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAgreeSpan.Location = new System.Drawing.Point(797, 14);
			this.labelAgreeSpan.Name = "labelAgreeSpan";
			this.labelAgreeSpan.Size = new System.Drawing.Size(177, 25);
			this.labelAgreeSpan.TabIndex = 8;
			this.labelAgreeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonRemarkTemplate
			// 
			this.buttonRemarkTemplate.Location = new System.Drawing.Point(1039, 207);
			this.buttonRemarkTemplate.Name = "buttonRemarkTemplate";
			this.buttonRemarkTemplate.Size = new System.Drawing.Size(75, 27);
			this.buttonRemarkTemplate.TabIndex = 15;
			this.buttonRemarkTemplate.Text = "定型文";
			this.buttonRemarkTemplate.UseVisualStyleBackColor = true;
			this.buttonRemarkTemplate.Click += new System.EventHandler(this.buttonRemarkTemplate_Click);
			// 
			// buttonAllOff
			// 
			this.buttonAllOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAllOff.Location = new System.Drawing.Point(696, 564);
			this.buttonAllOff.Name = "buttonAllOff";
			this.buttonAllOff.Size = new System.Drawing.Size(102, 36);
			this.buttonAllOff.TabIndex = 25;
			this.buttonAllOff.Text = "全解除";
			this.buttonAllOff.UseVisualStyleBackColor = true;
			this.buttonAllOff.Click += new System.EventHandler(this.buttonAllOff_Click);
			// 
			// buttonAllOn
			// 
			this.buttonAllOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAllOn.Location = new System.Drawing.Point(696, 522);
			this.buttonAllOn.Name = "buttonAllOn";
			this.buttonAllOn.Size = new System.Drawing.Size(102, 36);
			this.buttonAllOn.TabIndex = 24;
			this.buttonAllOn.Text = "全選択";
			this.buttonAllOn.UseVisualStyleBackColor = true;
			this.buttonAllOn.Click += new System.EventHandler(this.buttonAllOn_Click);
			// 
			// dateTimePickerLimitDate
			// 
			this.dateTimePickerLimitDate.Location = new System.Drawing.Point(797, 44);
			this.dateTimePickerLimitDate.Name = "dateTimePickerLimitDate";
			this.dateTimePickerLimitDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerLimitDate.TabIndex = 10;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(700, 50);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 17);
			this.label7.TabIndex = 9;
			this.label7.Text = "■有効期限";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label17.Location = new System.Drawing.Point(986, 355);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(21, 17);
			this.label17.TabIndex = 21;
			this.label17.Text = "＝";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(860, 355);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(21, 17);
			this.label16.TabIndex = 18;
			this.label16.Text = "＋";
			// 
			// textBoxTotalPrice
			// 
			this.textBoxTotalPrice.BackColor = System.Drawing.Color.White;
			this.textBoxTotalPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxTotalPrice.Location = new System.Drawing.Point(1013, 352);
			this.textBoxTotalPrice.Name = "textBoxTotalPrice";
			this.textBoxTotalPrice.ReadOnly = true;
			this.textBoxTotalPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxTotalPrice.TabIndex = 23;
			this.textBoxTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(1010, 332);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 17);
			this.label5.TabIndex = 22;
			this.label5.Text = "月額利用料";
			// 
			// textBoxPlatformPrice
			// 
			this.textBoxPlatformPrice.BackColor = System.Drawing.Color.White;
			this.textBoxPlatformPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPlatformPrice.Location = new System.Drawing.Point(761, 352);
			this.textBoxPlatformPrice.Name = "textBoxPlatformPrice";
			this.textBoxPlatformPrice.ReadOnly = true;
			this.textBoxPlatformPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxPlatformPrice.TabIndex = 17;
			this.textBoxPlatformPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(728, 332);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(126, 17);
			this.label6.TabIndex = 16;
			this.label6.Text = "プラットフォーム利用料";
			// 
			// textBoxServicePrice
			// 
			this.textBoxServicePrice.BackColor = System.Drawing.Color.White;
			this.textBoxServicePrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxServicePrice.Location = new System.Drawing.Point(887, 352);
			this.textBoxServicePrice.Name = "textBoxServicePrice";
			this.textBoxServicePrice.ReadOnly = true;
			this.textBoxServicePrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxServicePrice.TabIndex = 20;
			this.textBoxServicePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxServicePrice.DoubleClick += new System.EventHandler(this.textBoxServicePrice_DoubleClick);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(884, 332);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(90, 17);
			this.label11.TabIndex = 19;
			this.label11.Text = "サービス利用料";
			// 
			// SimulationMonthlyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 652);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.textBoxTotalPrice);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxPlatformPrice);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxServicePrice);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.dateTimePickerLimitDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.buttonAllOff);
			this.Controls.Add(this.buttonAllOn);
			this.Controls.Add(this.buttonRemarkTemplate);
			this.Controls.Add(this.labelAgreeSpan);
			this.Controls.Add(this.buttonChangeAgreeSpan);
			this.Controls.Add(this.radioSama);
			this.Controls.Add(this.radioOnchu);
			this.Controls.Add(this.dateTimePickerPrintDate);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBoxRemark);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.textBoxDestination);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listViewSetPlan);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.listViewService);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SimulationMonthlyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MIC WEB SERVICE 御見積書作成（月額課金/セット割サービス）";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimulationMonthlyForm_FormClosed);
			this.Load += new System.EventHandler(this.SimulationMonthlyForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listViewService;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceTypeName;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceName;
		private System.Windows.Forms.ColumnHeader columnHeaderPrice;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ListView listViewSetPlan;
		private System.Windows.Forms.ColumnHeader columnHeaderSetName;
		private System.Windows.Forms.ColumnHeader columnHeaderSetPrice;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxDestination;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.TextBox textBoxRemark;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DateTimePicker dateTimePickerPrintDate;
		private System.Windows.Forms.RadioButton radioSama;
		private System.Windows.Forms.RadioButton radioOnchu;
		private System.Windows.Forms.Button buttonChangeAgreeSpan;
		private System.Windows.Forms.Label labelAgreeSpan;
		private System.Windows.Forms.Button buttonRemarkTemplate;
		private System.Windows.Forms.Button buttonAllOff;
		private System.Windows.Forms.Button buttonAllOn;
		private System.Windows.Forms.DateTimePicker dateTimePickerLimitDate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBoxTotalPrice;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxPlatformPrice;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxServicePrice;
		private System.Windows.Forms.Label label11;
	}
}