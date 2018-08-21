namespace MwsSimulation.Forms
{
	partial class SimulationForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationForm));
			this.listViewService = new System.Windows.Forms.ListView();
			this.columnHeaderServiceTypeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderServiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTotalPrice = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxStandardPrice = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.listViewSetPlan = new System.Windows.Forms.ListView();
			this.columnHeaderSetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSetPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxFree36 = new System.Windows.Forms.TextBox();
			this.textBoxPrice36 = new System.Windows.Forms.TextBox();
			this.textBoxFree24 = new System.Windows.Forms.TextBox();
			this.textBoxPrice24 = new System.Windows.Forms.TextBox();
			this.textBoxFree12 = new System.Windows.Forms.TextBox();
			this.textBoxPrice12 = new System.Windows.Forms.TextBox();
			this.radioButtonGroup36 = new System.Windows.Forms.RadioButton();
			this.radioButtonGroup24 = new System.Windows.Forms.RadioButton();
			this.radioButtonGroup12 = new System.Windows.Forms.RadioButton();
			this.radioButtonGroupNone = new System.Windows.Forms.RadioButton();
			this.buttonInitGroupPlan3 = new System.Windows.Forms.Button();
			this.buttonInitGroupPlan2 = new System.Windows.Forms.Button();
			this.buttonInitGroupPlan1 = new System.Windows.Forms.Button();
			this.labelGroupPlanMessage = new System.Windows.Forms.Label();
			this.labelDebugNormal = new System.Windows.Forms.Label();
			this.textBoxDebugNormal = new System.Windows.Forms.TextBox();
			this.labelDebugSet = new System.Windows.Forms.Label();
			this.labelDebugGroup = new System.Windows.Forms.Label();
			this.textBoxDebugSet = new System.Windows.Forms.TextBox();
			this.textBoxDebugGroup = new System.Windows.Forms.TextBox();
			this.textBoxDebugTotal = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxDestination = new System.Windows.Forms.TextBox();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxRemark = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.dateTimePickerPrintDate = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
			this.comboBoxTerm = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
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
			this.listViewService.Size = new System.Drawing.Size(676, 721);
			this.listViewService.TabIndex = 4;
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(699, 679);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(177, 17);
			this.label1.TabIndex = 26;
			this.label1.Text = "ご利用のサービスの月額利用額";
			// 
			// textBoxTotalPrice
			// 
			this.textBoxTotalPrice.BackColor = System.Drawing.Color.White;
			this.textBoxTotalPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxTotalPrice.Location = new System.Drawing.Point(958, 676);
			this.textBoxTotalPrice.Name = "textBoxTotalPrice";
			this.textBoxTotalPrice.ReadOnly = true;
			this.textBoxTotalPrice.Size = new System.Drawing.Size(159, 24);
			this.textBoxTotalPrice.TabIndex = 27;
			this.textBoxTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxTotalPrice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxTotalPrice_MouseDoubleClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(699, 646);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(254, 17);
			this.label2.TabIndex = 24;
			this.label2.Text = "MIC WEB SERVICEプラットフォーム利用料";
			// 
			// textBoxStandardPrice
			// 
			this.textBoxStandardPrice.BackColor = System.Drawing.Color.White;
			this.textBoxStandardPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxStandardPrice.Location = new System.Drawing.Point(958, 643);
			this.textBoxStandardPrice.Name = "textBoxStandardPrice";
			this.textBoxStandardPrice.ReadOnly = true;
			this.textBoxStandardPrice.Size = new System.Drawing.Size(159, 24);
			this.textBoxStandardPrice.TabIndex = 25;
			this.textBoxStandardPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOK.Location = new System.Drawing.Point(907, 729);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 36);
			this.buttonOK.TabIndex = 30;
			this.buttonOK.Text = "保存";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(1015, 729);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 31;
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
			this.listViewSetPlan.Location = new System.Drawing.Point(702, 388);
			this.listViewSetPlan.Name = "listViewSetPlan";
			this.listViewSetPlan.Size = new System.Drawing.Size(415, 94);
			this.listViewSetPlan.TabIndex = 14;
			this.listViewSetPlan.UseCompatibleStateImageBehavior = false;
			this.listViewSetPlan.View = System.Windows.Forms.View.Details;
			this.listViewSetPlan.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewSet_ItemChecked);
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
			this.label3.Location = new System.Drawing.Point(703, 368);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(141, 17);
			this.label3.TabIndex = 13;
			this.label3.Text = "■セット割サービス申込み";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxFree36);
			this.groupBox1.Controls.Add(this.textBoxPrice36);
			this.groupBox1.Controls.Add(this.textBoxFree24);
			this.groupBox1.Controls.Add(this.textBoxPrice24);
			this.groupBox1.Controls.Add(this.textBoxFree12);
			this.groupBox1.Controls.Add(this.textBoxPrice12);
			this.groupBox1.Controls.Add(this.radioButtonGroup36);
			this.groupBox1.Controls.Add(this.radioButtonGroup24);
			this.groupBox1.Controls.Add(this.radioButtonGroup12);
			this.groupBox1.Controls.Add(this.radioButtonGroupNone);
			this.groupBox1.Location = new System.Drawing.Point(702, 191);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(415, 166);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "■おまとめプラン申込み";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(295, 37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 17);
			this.label6.TabIndex = 4;
			this.label6.Text = "プラン割引";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(190, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "料金";
			// 
			// textBoxFree36
			// 
			this.textBoxFree36.BackColor = System.Drawing.Color.White;
			this.textBoxFree36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxFree36.Location = new System.Drawing.Point(277, 126);
			this.textBoxFree36.Name = "textBoxFree36";
			this.textBoxFree36.ReadOnly = true;
			this.textBoxFree36.Size = new System.Drawing.Size(125, 24);
			this.textBoxFree36.TabIndex = 11;
			this.textBoxFree36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxFree36.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFree36_MouseDoubleClick);
			// 
			// textBoxPrice36
			// 
			this.textBoxPrice36.BackColor = System.Drawing.Color.White;
			this.textBoxPrice36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPrice36.Location = new System.Drawing.Point(147, 127);
			this.textBoxPrice36.Name = "textBoxPrice36";
			this.textBoxPrice36.ReadOnly = true;
			this.textBoxPrice36.Size = new System.Drawing.Size(125, 24);
			this.textBoxPrice36.TabIndex = 10;
			this.textBoxPrice36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPrice36.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPrice36_MouseDoubleClick);
			// 
			// textBoxFree24
			// 
			this.textBoxFree24.BackColor = System.Drawing.Color.White;
			this.textBoxFree24.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxFree24.Location = new System.Drawing.Point(277, 92);
			this.textBoxFree24.Name = "textBoxFree24";
			this.textBoxFree24.ReadOnly = true;
			this.textBoxFree24.Size = new System.Drawing.Size(125, 24);
			this.textBoxFree24.TabIndex = 8;
			this.textBoxFree24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxFree24.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFree24_MouseDoubleClick);
			// 
			// textBoxPrice24
			// 
			this.textBoxPrice24.BackColor = System.Drawing.Color.White;
			this.textBoxPrice24.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPrice24.Location = new System.Drawing.Point(147, 93);
			this.textBoxPrice24.Name = "textBoxPrice24";
			this.textBoxPrice24.ReadOnly = true;
			this.textBoxPrice24.Size = new System.Drawing.Size(125, 24);
			this.textBoxPrice24.TabIndex = 7;
			this.textBoxPrice24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPrice24.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPrice24_MouseDoubleClick);
			// 
			// textBoxFree12
			// 
			this.textBoxFree12.BackColor = System.Drawing.Color.White;
			this.textBoxFree12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxFree12.Location = new System.Drawing.Point(277, 57);
			this.textBoxFree12.Name = "textBoxFree12";
			this.textBoxFree12.ReadOnly = true;
			this.textBoxFree12.Size = new System.Drawing.Size(125, 24);
			this.textBoxFree12.TabIndex = 5;
			this.textBoxFree12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxFree12.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFree12_MouseDoubleClick);
			// 
			// textBoxPrice12
			// 
			this.textBoxPrice12.BackColor = System.Drawing.Color.White;
			this.textBoxPrice12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPrice12.Location = new System.Drawing.Point(147, 58);
			this.textBoxPrice12.Name = "textBoxPrice12";
			this.textBoxPrice12.ReadOnly = true;
			this.textBoxPrice12.Size = new System.Drawing.Size(125, 24);
			this.textBoxPrice12.TabIndex = 3;
			this.textBoxPrice12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPrice12.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPrice12_MouseDoubleClick);
			// 
			// radioButtonGroup36
			// 
			this.radioButtonGroup36.AutoSize = true;
			this.radioButtonGroup36.Location = new System.Drawing.Point(16, 128);
			this.radioButtonGroup36.Name = "radioButtonGroup36";
			this.radioButtonGroup36.Size = new System.Drawing.Size(132, 21);
			this.radioButtonGroup36.TabIndex = 9;
			this.radioButtonGroup36.Text = "おまとめプラン36ヵ月";
			this.radioButtonGroup36.UseVisualStyleBackColor = true;
			this.radioButtonGroup36.CheckedChanged += new System.EventHandler(this.radioButtonGroup36_CheckedChanged);
			// 
			// radioButtonGroup24
			// 
			this.radioButtonGroup24.AutoSize = true;
			this.radioButtonGroup24.Location = new System.Drawing.Point(16, 94);
			this.radioButtonGroup24.Name = "radioButtonGroup24";
			this.radioButtonGroup24.Size = new System.Drawing.Size(132, 21);
			this.radioButtonGroup24.TabIndex = 6;
			this.radioButtonGroup24.Text = "おまとめプラン24ヵ月";
			this.radioButtonGroup24.UseVisualStyleBackColor = true;
			this.radioButtonGroup24.CheckedChanged += new System.EventHandler(this.radioButtonGroup24_CheckedChanged);
			// 
			// radioButtonGroup12
			// 
			this.radioButtonGroup12.AutoSize = true;
			this.radioButtonGroup12.Location = new System.Drawing.Point(16, 59);
			this.radioButtonGroup12.Name = "radioButtonGroup12";
			this.radioButtonGroup12.Size = new System.Drawing.Size(132, 21);
			this.radioButtonGroup12.TabIndex = 1;
			this.radioButtonGroup12.Text = "おまとめプラン12ヵ月";
			this.radioButtonGroup12.UseVisualStyleBackColor = true;
			this.radioButtonGroup12.CheckedChanged += new System.EventHandler(this.radioButtonGroup12_CheckedChanged);
			// 
			// radioButtonGroupNone
			// 
			this.radioButtonGroupNone.AutoSize = true;
			this.radioButtonGroupNone.Checked = true;
			this.radioButtonGroupNone.Location = new System.Drawing.Point(16, 23);
			this.radioButtonGroupNone.Name = "radioButtonGroupNone";
			this.radioButtonGroupNone.Size = new System.Drawing.Size(115, 21);
			this.radioButtonGroupNone.TabIndex = 0;
			this.radioButtonGroupNone.TabStop = true;
			this.radioButtonGroupNone.Text = "おまとめプランなし";
			this.radioButtonGroupNone.UseVisualStyleBackColor = true;
			this.radioButtonGroupNone.CheckedChanged += new System.EventHandler(this.radioButtonGroupNone_CheckedChanged);
			// 
			// buttonInitGroupPlan3
			// 
			this.buttonInitGroupPlan3.Location = new System.Drawing.Point(702, 145);
			this.buttonInitGroupPlan3.Name = "buttonInitGroupPlan3";
			this.buttonInitGroupPlan3.Size = new System.Drawing.Size(381, 35);
			this.buttonInitGroupPlan3.TabIndex = 11;
			this.buttonInitGroupPlan3.Text = "button3";
			this.buttonInitGroupPlan3.UseVisualStyleBackColor = true;
			this.buttonInitGroupPlan3.Click += new System.EventHandler(this.buttonInitGroupPlan3_Click);
			// 
			// buttonInitGroupPlan2
			// 
			this.buttonInitGroupPlan2.Location = new System.Drawing.Point(702, 104);
			this.buttonInitGroupPlan2.Name = "buttonInitGroupPlan2";
			this.buttonInitGroupPlan2.Size = new System.Drawing.Size(381, 35);
			this.buttonInitGroupPlan2.TabIndex = 10;
			this.buttonInitGroupPlan2.Text = "button2";
			this.buttonInitGroupPlan2.UseVisualStyleBackColor = true;
			this.buttonInitGroupPlan2.Click += new System.EventHandler(this.buttonInitGroupPlan2_Click);
			// 
			// buttonInitGroupPlan1
			// 
			this.buttonInitGroupPlan1.Location = new System.Drawing.Point(702, 63);
			this.buttonInitGroupPlan1.Name = "buttonInitGroupPlan1";
			this.buttonInitGroupPlan1.Size = new System.Drawing.Size(381, 35);
			this.buttonInitGroupPlan1.TabIndex = 9;
			this.buttonInitGroupPlan1.Text = "button1";
			this.buttonInitGroupPlan1.UseVisualStyleBackColor = true;
			this.buttonInitGroupPlan1.Click += new System.EventHandler(this.buttonInitGroupPlan1_Click);
			// 
			// labelGroupPlanMessage
			// 
			this.labelGroupPlanMessage.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelGroupPlanMessage.ForeColor = System.Drawing.Color.Red;
			this.labelGroupPlanMessage.Location = new System.Drawing.Point(699, 703);
			this.labelGroupPlanMessage.Name = "labelGroupPlanMessage";
			this.labelGroupPlanMessage.Size = new System.Drawing.Size(418, 17);
			this.labelGroupPlanMessage.TabIndex = 28;
			this.labelGroupPlanMessage.Text = "※おまとめプランが適用できます。";
			this.labelGroupPlanMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelDebugNormal
			// 
			this.labelDebugNormal.AutoSize = true;
			this.labelDebugNormal.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelDebugNormal.Location = new System.Drawing.Point(704, 600);
			this.labelDebugNormal.Name = "labelDebugNormal";
			this.labelDebugNormal.Size = new System.Drawing.Size(18, 7);
			this.labelDebugNormal.TabIndex = 17;
			this.labelDebugNormal.Text = "通常";
			this.labelDebugNormal.Visible = false;
			// 
			// textBoxDebugNormal
			// 
			this.textBoxDebugNormal.BackColor = System.Drawing.Color.White;
			this.textBoxDebugNormal.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxDebugNormal.Location = new System.Drawing.Point(725, 595);
			this.textBoxDebugNormal.Name = "textBoxDebugNormal";
			this.textBoxDebugNormal.ReadOnly = true;
			this.textBoxDebugNormal.Size = new System.Drawing.Size(58, 16);
			this.textBoxDebugNormal.TabIndex = 18;
			this.textBoxDebugNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxDebugNormal.Visible = false;
			// 
			// labelDebugSet
			// 
			this.labelDebugSet.AutoSize = true;
			this.labelDebugSet.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelDebugSet.Location = new System.Drawing.Point(789, 600);
			this.labelDebugSet.Name = "labelDebugSet";
			this.labelDebugSet.Size = new System.Drawing.Size(27, 7);
			this.labelDebugSet.TabIndex = 19;
			this.labelDebugSet.Text = "セット割";
			this.labelDebugSet.Visible = false;
			// 
			// labelDebugGroup
			// 
			this.labelDebugGroup.AutoSize = true;
			this.labelDebugGroup.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelDebugGroup.Location = new System.Drawing.Point(886, 600);
			this.labelDebugGroup.Name = "labelDebugGroup";
			this.labelDebugGroup.Size = new System.Drawing.Size(50, 7);
			this.labelDebugGroup.TabIndex = 21;
			this.labelDebugGroup.Text = "おまとめプラン";
			this.labelDebugGroup.Visible = false;
			// 
			// textBoxDebugSet
			// 
			this.textBoxDebugSet.BackColor = System.Drawing.Color.White;
			this.textBoxDebugSet.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxDebugSet.Location = new System.Drawing.Point(822, 595);
			this.textBoxDebugSet.Name = "textBoxDebugSet";
			this.textBoxDebugSet.ReadOnly = true;
			this.textBoxDebugSet.Size = new System.Drawing.Size(58, 16);
			this.textBoxDebugSet.TabIndex = 20;
			this.textBoxDebugSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxDebugSet.Visible = false;
			// 
			// textBoxDebugGroup
			// 
			this.textBoxDebugGroup.BackColor = System.Drawing.Color.White;
			this.textBoxDebugGroup.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxDebugGroup.Location = new System.Drawing.Point(942, 595);
			this.textBoxDebugGroup.Name = "textBoxDebugGroup";
			this.textBoxDebugGroup.ReadOnly = true;
			this.textBoxDebugGroup.Size = new System.Drawing.Size(58, 16);
			this.textBoxDebugGroup.TabIndex = 22;
			this.textBoxDebugGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxDebugGroup.Visible = false;
			// 
			// textBoxDebugTotal
			// 
			this.textBoxDebugTotal.BackColor = System.Drawing.Color.White;
			this.textBoxDebugTotal.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxDebugTotal.Location = new System.Drawing.Point(1006, 595);
			this.textBoxDebugTotal.Name = "textBoxDebugTotal";
			this.textBoxDebugTotal.ReadOnly = true;
			this.textBoxDebugTotal.Size = new System.Drawing.Size(58, 16);
			this.textBoxDebugTotal.TabIndex = 23;
			this.textBoxDebugTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxDebugTotal.Visible = false;
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
			this.textBoxDestination.Size = new System.Drawing.Size(392, 24);
			this.textBoxDestination.TabIndex = 1;
			// 
			// buttonPrint
			// 
			this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonPrint.Location = new System.Drawing.Point(702, 729);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(102, 36);
			this.buttonPrint.TabIndex = 29;
			this.buttonPrint.Text = "見積書印刷";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(702, 43);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 17);
			this.label7.TabIndex = 8;
			this.label7.Text = "■おススメセット";
			// 
			// textBoxRemark
			// 
			this.textBoxRemark.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark.Location = new System.Drawing.Point(702, 510);
			this.textBoxRemark.Multiline = true;
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(415, 81);
			this.textBoxRemark.TabIndex = 16;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(703, 490);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 17);
			this.label8.TabIndex = 15;
			this.label8.Text = "■備考";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(483, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 17);
			this.label9.TabIndex = 2;
			this.label9.Text = "■発行日";
			// 
			// dateTimePickerPrintDate
			// 
			this.dateTimePickerPrintDate.Location = new System.Drawing.Point(549, 14);
			this.dateTimePickerPrintDate.Name = "dateTimePickerPrintDate";
			this.dateTimePickerPrintDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerPrintDate.TabIndex = 3;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(703, 17);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 17);
			this.label10.TabIndex = 5;
			this.label10.Text = "■契約期間";
			// 
			// dateTimePickerStartDate
			// 
			this.dateTimePickerStartDate.Location = new System.Drawing.Point(782, 14);
			this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			this.dateTimePickerStartDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerStartDate.TabIndex = 6;
			// 
			// comboBoxTerm
			// 
			this.comboBoxTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTerm.FormattingEnabled = true;
			this.comboBoxTerm.Location = new System.Drawing.Point(929, 13);
			this.comboBoxTerm.Name = "comboBoxTerm";
			this.comboBoxTerm.Size = new System.Drawing.Size(80, 25);
			this.comboBoxTerm.TabIndex = 7;
			// 
			// SimulationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 781);
			this.Controls.Add(this.comboBoxTerm);
			this.Controls.Add(this.dateTimePickerStartDate);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.dateTimePickerPrintDate);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBoxRemark);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.textBoxDestination);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxDebugTotal);
			this.Controls.Add(this.textBoxDebugGroup);
			this.Controls.Add(this.textBoxDebugSet);
			this.Controls.Add(this.labelDebugGroup);
			this.Controls.Add(this.labelDebugSet);
			this.Controls.Add(this.textBoxDebugNormal);
			this.Controls.Add(this.buttonInitGroupPlan3);
			this.Controls.Add(this.labelDebugNormal);
			this.Controls.Add(this.buttonInitGroupPlan2);
			this.Controls.Add(this.labelGroupPlanMessage);
			this.Controls.Add(this.buttonInitGroupPlan1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listViewSetPlan);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxStandardPrice);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxTotalPrice);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listViewService);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SimulationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MIC WEB SERVICE お見積書作成";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimulationForm_FormClosed);
			this.Load += new System.EventHandler(this.SimulationForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listViewService;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceTypeName;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceName;
		private System.Windows.Forms.ColumnHeader columnHeaderPrice;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxTotalPrice;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxStandardPrice;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ListView listViewSetPlan;
		private System.Windows.Forms.ColumnHeader columnHeaderSetName;
		private System.Windows.Forms.ColumnHeader columnHeaderSetPrice;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonInitGroupPlan3;
		private System.Windows.Forms.Button buttonInitGroupPlan2;
		private System.Windows.Forms.Button buttonInitGroupPlan1;
		private System.Windows.Forms.RadioButton radioButtonGroup24;
		private System.Windows.Forms.RadioButton radioButtonGroup12;
		private System.Windows.Forms.RadioButton radioButtonGroupNone;
		private System.Windows.Forms.RadioButton radioButtonGroup36;
		private System.Windows.Forms.Label labelGroupPlanMessage;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxFree36;
		private System.Windows.Forms.TextBox textBoxPrice36;
		private System.Windows.Forms.TextBox textBoxFree24;
		private System.Windows.Forms.TextBox textBoxPrice24;
		private System.Windows.Forms.TextBox textBoxFree12;
		private System.Windows.Forms.TextBox textBoxPrice12;
		private System.Windows.Forms.Label labelDebugNormal;
		private System.Windows.Forms.TextBox textBoxDebugNormal;
		private System.Windows.Forms.Label labelDebugSet;
		private System.Windows.Forms.Label labelDebugGroup;
		private System.Windows.Forms.TextBox textBoxDebugSet;
		private System.Windows.Forms.TextBox textBoxDebugGroup;
		private System.Windows.Forms.TextBox textBoxDebugTotal;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxDestination;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxRemark;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DateTimePicker dateTimePickerPrintDate;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
		private System.Windows.Forms.ComboBox comboBoxTerm;
	}
}