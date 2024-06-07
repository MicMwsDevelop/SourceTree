namespace MwsSimulation.Forms
{
	partial class SimulationMatomeForm201907
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationMatomeForm201907));
			this.listViewService = new System.Windows.Forms.ListView();
			this.columnHeaderServiceTypeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderServiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxServicePrice = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPlatformPrice = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonMatome60 = new System.Windows.Forms.RadioButton();
			this.label15 = new System.Windows.Forms.Label();
			this.textBoxMatomeMonthlyPrice60 = new System.Windows.Forms.TextBox();
			this.radioButtonMatome36 = new System.Windows.Forms.RadioButton();
			this.label14 = new System.Windows.Forms.Label();
			this.textBoxMatomeMonthlyPrice36 = new System.Windows.Forms.TextBox();
			this.textBoxNormalMonthlyPrice = new System.Windows.Forms.TextBox();
			this.textBoxMatomeMonthlyPrice12 = new System.Windows.Forms.TextBox();
			this.radioButtonMatome12 = new System.Windows.Forms.RadioButton();
			this.radioButtonNormal60 = new System.Windows.Forms.RadioButton();
			this.radioButtonNormal36 = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxNormalTotalPrice60 = new System.Windows.Forms.TextBox();
			this.textBoxNormalTotalPrice36 = new System.Windows.Forms.TextBox();
			this.radioButtonNormal12 = new System.Windows.Forms.RadioButton();
			this.textBoxNormalTotalPrice12 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxMatomeFree60 = new System.Windows.Forms.TextBox();
			this.textBoxMatomeTotalPrice60 = new System.Windows.Forms.TextBox();
			this.textBoxMatomeFree36 = new System.Windows.Forms.TextBox();
			this.textBoxMatomeTotalPrice36 = new System.Windows.Forms.TextBox();
			this.textBoxMatomeFree12 = new System.Windows.Forms.TextBox();
			this.textBoxMatomeTotalPrice12 = new System.Windows.Forms.TextBox();
			this.labelMatomeMessage = new System.Windows.Forms.Label();
			this.buttonInitMatomePlan3 = new System.Windows.Forms.Button();
			this.buttonInitMatomePlan2 = new System.Windows.Forms.Button();
			this.buttonInitMatomePlan1 = new System.Windows.Forms.Button();
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
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxMonthlyPrice = new System.Windows.Forms.TextBox();
			this.buttonRemarkTemplate = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonAllOn = new System.Windows.Forms.Button();
			this.buttonAllOff = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.dateTimePickerLimitDate = new System.Windows.Forms.DateTimePicker();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			this.listViewService.Location = new System.Drawing.Point(14, 45);
			this.listViewService.Name = "listViewService";
			this.listViewService.Size = new System.Drawing.Size(546, 556);
			this.listViewService.TabIndex = 4;
			this.listViewService.UseCompatibleStateImageBehavior = false;
			this.listViewService.View = System.Windows.Forms.View.Details;
			this.listViewService.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewService_ItemCheck);
			this.listViewService.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewService_ItemChecked);
			// 
			// columnHeaderServiceTypeName
			// 
			this.columnHeaderServiceTypeName.Text = "サービス種別";
			this.columnHeaderServiceTypeName.Width = 120;
			// 
			// columnHeaderServiceName
			// 
			this.columnHeaderServiceName.Text = "サービス名";
			this.columnHeaderServiceName.Width = 300;
			// 
			// columnHeaderPrice
			// 
			this.columnHeaderPrice.Text = "価格";
			this.columnHeaderPrice.Width = 100;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(863, 450);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 17);
			this.label1.TabIndex = 19;
			this.label1.Text = "サービス利用料";
			// 
			// textBoxServicePrice
			// 
			this.textBoxServicePrice.BackColor = System.Drawing.Color.White;
			this.textBoxServicePrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxServicePrice.Location = new System.Drawing.Point(866, 470);
			this.textBoxServicePrice.Name = "textBoxServicePrice";
			this.textBoxServicePrice.ReadOnly = true;
			this.textBoxServicePrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxServicePrice.TabIndex = 20;
			this.textBoxServicePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(707, 450);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 17);
			this.label2.TabIndex = 16;
			this.label2.Text = "プラットフォーム利用料";
			// 
			// textBoxPlatformPrice
			// 
			this.textBoxPlatformPrice.BackColor = System.Drawing.Color.White;
			this.textBoxPlatformPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPlatformPrice.Location = new System.Drawing.Point(740, 470);
			this.textBoxPlatformPrice.Name = "textBoxPlatformPrice";
			this.textBoxPlatformPrice.ReadOnly = true;
			this.textBoxPlatformPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxPlatformPrice.TabIndex = 17;
			this.textBoxPlatformPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOK.Location = new System.Drawing.Point(875, 565);
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
			this.buttonCancel.Location = new System.Drawing.Point(983, 565);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 28;
			this.buttonCancel.Text = "破棄";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonMatome60);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.textBoxMatomeMonthlyPrice60);
			this.groupBox1.Controls.Add(this.radioButtonMatome36);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.textBoxMatomeMonthlyPrice36);
			this.groupBox1.Controls.Add(this.textBoxNormalMonthlyPrice);
			this.groupBox1.Controls.Add(this.textBoxMatomeMonthlyPrice12);
			this.groupBox1.Controls.Add(this.radioButtonMatome12);
			this.groupBox1.Controls.Add(this.radioButtonNormal60);
			this.groupBox1.Controls.Add(this.radioButtonNormal36);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxNormalTotalPrice60);
			this.groupBox1.Controls.Add(this.textBoxNormalTotalPrice36);
			this.groupBox1.Controls.Add(this.radioButtonNormal12);
			this.groupBox1.Controls.Add(this.textBoxNormalTotalPrice12);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxMatomeFree60);
			this.groupBox1.Controls.Add(this.textBoxMatomeTotalPrice60);
			this.groupBox1.Controls.Add(this.textBoxMatomeFree36);
			this.groupBox1.Controls.Add(this.textBoxMatomeTotalPrice36);
			this.groupBox1.Controls.Add(this.textBoxMatomeFree12);
			this.groupBox1.Controls.Add(this.textBoxMatomeTotalPrice12);
			this.groupBox1.Controls.Add(this.labelMatomeMessage);
			this.groupBox1.Location = new System.Drawing.Point(566, 227);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(519, 214);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "■おまとめプラン";
			// 
			// radioButtonMatome60
			// 
			this.radioButtonMatome60.AutoSize = true;
			this.radioButtonMatome60.Enabled = false;
			this.radioButtonMatome60.Location = new System.Drawing.Point(14, 157);
			this.radioButtonMatome60.Name = "radioButtonMatome60";
			this.radioButtonMatome60.Size = new System.Drawing.Size(92, 21);
			this.radioButtonMatome60.TabIndex = 20;
			this.radioButtonMatome60.Text = "60ヵ月プラン";
			this.radioButtonMatome60.UseVisualStyleBackColor = true;
			this.radioButtonMatome60.CheckedChanged += new System.EventHandler(this.radioButtonMatome60_CheckedChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(362, 77);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(73, 17);
			this.label15.TabIndex = 14;
			this.label15.Text = "月額利用料";
			// 
			// textBoxMatomeMonthlyPrice60
			// 
			this.textBoxMatomeMonthlyPrice60.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeMonthlyPrice60.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeMonthlyPrice60.Location = new System.Drawing.Point(357, 157);
			this.textBoxMatomeMonthlyPrice60.Name = "textBoxMatomeMonthlyPrice60";
			this.textBoxMatomeMonthlyPrice60.ReadOnly = true;
			this.textBoxMatomeMonthlyPrice60.Size = new System.Drawing.Size(93, 24);
			this.textBoxMatomeMonthlyPrice60.TabIndex = 23;
			this.textBoxMatomeMonthlyPrice60.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// radioButtonMatome36
			// 
			this.radioButtonMatome36.AutoSize = true;
			this.radioButtonMatome36.Enabled = false;
			this.radioButtonMatome36.Location = new System.Drawing.Point(14, 128);
			this.radioButtonMatome36.Name = "radioButtonMatome36";
			this.radioButtonMatome36.Size = new System.Drawing.Size(92, 21);
			this.radioButtonMatome36.TabIndex = 16;
			this.radioButtonMatome36.Text = "36ヵ月プラン";
			this.radioButtonMatome36.UseVisualStyleBackColor = true;
			this.radioButtonMatome36.CheckedChanged += new System.EventHandler(this.radioButtonMatome36_CheckedChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(416, 18);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(73, 17);
			this.label14.TabIndex = 7;
			this.label14.Text = "月額利用料";
			// 
			// textBoxMatomeMonthlyPrice36
			// 
			this.textBoxMatomeMonthlyPrice36.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeMonthlyPrice36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeMonthlyPrice36.Location = new System.Drawing.Point(357, 127);
			this.textBoxMatomeMonthlyPrice36.Name = "textBoxMatomeMonthlyPrice36";
			this.textBoxMatomeMonthlyPrice36.ReadOnly = true;
			this.textBoxMatomeMonthlyPrice36.Size = new System.Drawing.Size(93, 24);
			this.textBoxMatomeMonthlyPrice36.TabIndex = 19;
			this.textBoxMatomeMonthlyPrice36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxNormalMonthlyPrice
			// 
			this.textBoxNormalMonthlyPrice.BackColor = System.Drawing.Color.White;
			this.textBoxNormalMonthlyPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalMonthlyPrice.Location = new System.Drawing.Point(408, 40);
			this.textBoxNormalMonthlyPrice.Name = "textBoxNormalMonthlyPrice";
			this.textBoxNormalMonthlyPrice.ReadOnly = true;
			this.textBoxNormalMonthlyPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalMonthlyPrice.TabIndex = 8;
			this.textBoxNormalMonthlyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxMatomeMonthlyPrice12
			// 
			this.textBoxMatomeMonthlyPrice12.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeMonthlyPrice12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeMonthlyPrice12.Location = new System.Drawing.Point(357, 97);
			this.textBoxMatomeMonthlyPrice12.Name = "textBoxMatomeMonthlyPrice12";
			this.textBoxMatomeMonthlyPrice12.ReadOnly = true;
			this.textBoxMatomeMonthlyPrice12.Size = new System.Drawing.Size(93, 24);
			this.textBoxMatomeMonthlyPrice12.TabIndex = 15;
			this.textBoxMatomeMonthlyPrice12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// radioButtonMatome12
			// 
			this.radioButtonMatome12.AutoSize = true;
			this.radioButtonMatome12.Enabled = false;
			this.radioButtonMatome12.Location = new System.Drawing.Point(13, 98);
			this.radioButtonMatome12.Name = "radioButtonMatome12";
			this.radioButtonMatome12.Size = new System.Drawing.Size(92, 21);
			this.radioButtonMatome12.TabIndex = 9;
			this.radioButtonMatome12.Text = "12ヵ月プラン";
			this.radioButtonMatome12.UseVisualStyleBackColor = true;
			this.radioButtonMatome12.CheckedChanged += new System.EventHandler(this.radioButtonMatome12_CheckedChanged);
			// 
			// radioButtonNormal60
			// 
			this.radioButtonNormal60.AutoSize = true;
			this.radioButtonNormal60.Enabled = false;
			this.radioButtonNormal60.Location = new System.Drawing.Point(321, 18);
			this.radioButtonNormal60.Name = "radioButtonNormal60";
			this.radioButtonNormal60.Size = new System.Drawing.Size(63, 21);
			this.radioButtonNormal60.TabIndex = 5;
			this.radioButtonNormal60.Text = "60ヵ月";
			this.radioButtonNormal60.UseVisualStyleBackColor = true;
			this.radioButtonNormal60.CheckedChanged += new System.EventHandler(this.radioButtonNormal60_CheckedChanged);
			// 
			// radioButtonNormal36
			// 
			this.radioButtonNormal36.AutoSize = true;
			this.radioButtonNormal36.Checked = true;
			this.radioButtonNormal36.Enabled = false;
			this.radioButtonNormal36.Location = new System.Drawing.Point(222, 18);
			this.radioButtonNormal36.Name = "radioButtonNormal36";
			this.radioButtonNormal36.Size = new System.Drawing.Size(63, 21);
			this.radioButtonNormal36.TabIndex = 3;
			this.radioButtonNormal36.TabStop = true;
			this.radioButtonNormal36.Text = "36ヵ月";
			this.radioButtonNormal36.UseVisualStyleBackColor = true;
			this.radioButtonNormal36.CheckedChanged += new System.EventHandler(this.radioButtonNormal36_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(51, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "契約なし";
			// 
			// textBoxNormalTotalPrice60
			// 
			this.textBoxNormalTotalPrice60.BackColor = System.Drawing.Color.White;
			this.textBoxNormalTotalPrice60.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalTotalPrice60.Location = new System.Drawing.Point(309, 40);
			this.textBoxNormalTotalPrice60.Name = "textBoxNormalTotalPrice60";
			this.textBoxNormalTotalPrice60.ReadOnly = true;
			this.textBoxNormalTotalPrice60.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalTotalPrice60.TabIndex = 6;
			this.textBoxNormalTotalPrice60.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxNormalTotalPrice36
			// 
			this.textBoxNormalTotalPrice36.BackColor = System.Drawing.Color.White;
			this.textBoxNormalTotalPrice36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalTotalPrice36.Location = new System.Drawing.Point(210, 40);
			this.textBoxNormalTotalPrice36.Name = "textBoxNormalTotalPrice36";
			this.textBoxNormalTotalPrice36.ReadOnly = true;
			this.textBoxNormalTotalPrice36.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalTotalPrice36.TabIndex = 4;
			this.textBoxNormalTotalPrice36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// radioButtonNormal12
			// 
			this.radioButtonNormal12.AutoSize = true;
			this.radioButtonNormal12.Enabled = false;
			this.radioButtonNormal12.Location = new System.Drawing.Point(129, 18);
			this.radioButtonNormal12.Name = "radioButtonNormal12";
			this.radioButtonNormal12.Size = new System.Drawing.Size(63, 21);
			this.radioButtonNormal12.TabIndex = 1;
			this.radioButtonNormal12.Text = "12ヵ月";
			this.radioButtonNormal12.UseVisualStyleBackColor = true;
			this.radioButtonNormal12.CheckedChanged += new System.EventHandler(this.radioButtonNormal36_CheckedChanged);
			// 
			// textBoxNormalTotalPrice12
			// 
			this.textBoxNormalTotalPrice12.BackColor = System.Drawing.Color.White;
			this.textBoxNormalTotalPrice12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalTotalPrice12.Location = new System.Drawing.Point(111, 40);
			this.textBoxNormalTotalPrice12.Name = "textBoxNormalTotalPrice12";
			this.textBoxNormalTotalPrice12.ReadOnly = true;
			this.textBoxNormalTotalPrice12.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalTotalPrice12.TabIndex = 2;
			this.textBoxNormalTotalPrice12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(217, 77);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(125, 17);
			this.label6.TabIndex = 12;
			this.label6.Text = "割引額（無償月数）";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(117, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 17);
			this.label5.TabIndex = 10;
			this.label5.Text = "おまとめ料金";
			// 
			// textBoxMatomeFree60
			// 
			this.textBoxMatomeFree60.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeFree60.Enabled = false;
			this.textBoxMatomeFree60.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeFree60.Location = new System.Drawing.Point(210, 157);
			this.textBoxMatomeFree60.Name = "textBoxMatomeFree60";
			this.textBoxMatomeFree60.ReadOnly = true;
			this.textBoxMatomeFree60.Size = new System.Drawing.Size(141, 24);
			this.textBoxMatomeFree60.TabIndex = 22;
			this.textBoxMatomeFree60.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxMatomeTotalPrice60
			// 
			this.textBoxMatomeTotalPrice60.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeTotalPrice60.Enabled = false;
			this.textBoxMatomeTotalPrice60.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeTotalPrice60.Location = new System.Drawing.Point(111, 157);
			this.textBoxMatomeTotalPrice60.Name = "textBoxMatomeTotalPrice60";
			this.textBoxMatomeTotalPrice60.ReadOnly = true;
			this.textBoxMatomeTotalPrice60.Size = new System.Drawing.Size(93, 24);
			this.textBoxMatomeTotalPrice60.TabIndex = 21;
			this.textBoxMatomeTotalPrice60.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxMatomeTotalPrice60.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatomeTotalPrice60_MouseDoubleClick);
			// 
			// textBoxMatomeFree36
			// 
			this.textBoxMatomeFree36.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeFree36.Enabled = false;
			this.textBoxMatomeFree36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeFree36.Location = new System.Drawing.Point(210, 127);
			this.textBoxMatomeFree36.Name = "textBoxMatomeFree36";
			this.textBoxMatomeFree36.ReadOnly = true;
			this.textBoxMatomeFree36.Size = new System.Drawing.Size(141, 24);
			this.textBoxMatomeFree36.TabIndex = 18;
			this.textBoxMatomeFree36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxMatomeTotalPrice36
			// 
			this.textBoxMatomeTotalPrice36.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeTotalPrice36.Enabled = false;
			this.textBoxMatomeTotalPrice36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeTotalPrice36.Location = new System.Drawing.Point(111, 127);
			this.textBoxMatomeTotalPrice36.Name = "textBoxMatomeTotalPrice36";
			this.textBoxMatomeTotalPrice36.ReadOnly = true;
			this.textBoxMatomeTotalPrice36.Size = new System.Drawing.Size(93, 24);
			this.textBoxMatomeTotalPrice36.TabIndex = 17;
			this.textBoxMatomeTotalPrice36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxMatomeTotalPrice36.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatomeTotalPrice36_MouseDoubleClick);
			// 
			// textBoxMatomeFree12
			// 
			this.textBoxMatomeFree12.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeFree12.Enabled = false;
			this.textBoxMatomeFree12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeFree12.Location = new System.Drawing.Point(210, 97);
			this.textBoxMatomeFree12.Name = "textBoxMatomeFree12";
			this.textBoxMatomeFree12.ReadOnly = true;
			this.textBoxMatomeFree12.Size = new System.Drawing.Size(141, 24);
			this.textBoxMatomeFree12.TabIndex = 13;
			this.textBoxMatomeFree12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxMatomeTotalPrice12
			// 
			this.textBoxMatomeTotalPrice12.BackColor = System.Drawing.Color.White;
			this.textBoxMatomeTotalPrice12.Enabled = false;
			this.textBoxMatomeTotalPrice12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMatomeTotalPrice12.Location = new System.Drawing.Point(111, 97);
			this.textBoxMatomeTotalPrice12.Name = "textBoxMatomeTotalPrice12";
			this.textBoxMatomeTotalPrice12.ReadOnly = true;
			this.textBoxMatomeTotalPrice12.Size = new System.Drawing.Size(93, 24);
			this.textBoxMatomeTotalPrice12.TabIndex = 11;
			this.textBoxMatomeTotalPrice12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxMatomeTotalPrice12.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatomeTotalPrice12_MouseDoubleClick);
			// 
			// labelMatomeMessage
			// 
			this.labelMatomeMessage.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelMatomeMessage.ForeColor = System.Drawing.Color.Red;
			this.labelMatomeMessage.Location = new System.Drawing.Point(111, 184);
			this.labelMatomeMessage.Name = "labelMatomeMessage";
			this.labelMatomeMessage.Size = new System.Drawing.Size(339, 17);
			this.labelMatomeMessage.TabIndex = 24;
			this.labelMatomeMessage.Text = "※おまとめプラン割引が適用できます。";
			this.labelMatomeMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonInitMatomePlan3
			// 
			this.buttonInitMatomePlan3.Location = new System.Drawing.Point(9, 101);
			this.buttonInitMatomePlan3.Name = "buttonInitMatomePlan3";
			this.buttonInitMatomePlan3.Size = new System.Drawing.Size(157, 35);
			this.buttonInitMatomePlan3.TabIndex = 2;
			this.buttonInitMatomePlan3.Text = "button3";
			this.buttonInitMatomePlan3.UseVisualStyleBackColor = true;
			this.buttonInitMatomePlan3.Click += new System.EventHandler(this.buttonInitMatomePlan3_Click);
			// 
			// buttonInitMatomePlan2
			// 
			this.buttonInitMatomePlan2.Location = new System.Drawing.Point(9, 60);
			this.buttonInitMatomePlan2.Name = "buttonInitMatomePlan2";
			this.buttonInitMatomePlan2.Size = new System.Drawing.Size(157, 35);
			this.buttonInitMatomePlan2.TabIndex = 1;
			this.buttonInitMatomePlan2.Text = "button2";
			this.buttonInitMatomePlan2.UseVisualStyleBackColor = true;
			this.buttonInitMatomePlan2.Click += new System.EventHandler(this.buttonInitMatomePlan2_Click);
			// 
			// buttonInitMatomePlan1
			// 
			this.buttonInitMatomePlan1.Location = new System.Drawing.Point(9, 19);
			this.buttonInitMatomePlan1.Name = "buttonInitMatomePlan1";
			this.buttonInitMatomePlan1.Size = new System.Drawing.Size(157, 35);
			this.buttonInitMatomePlan1.TabIndex = 0;
			this.buttonInitMatomePlan1.Text = "button1";
			this.buttonInitMatomePlan1.UseVisualStyleBackColor = true;
			this.buttonInitMatomePlan1.Click += new System.EventHandler(this.buttonInitMatomePlan1_Click);
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
			this.buttonPrint.Location = new System.Drawing.Point(566, 565);
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
			this.textBoxRemark.Location = new System.Drawing.Point(747, 98);
			this.textBoxRemark.Multiline = true;
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(338, 81);
			this.textBoxRemark.TabIndex = 13;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(748, 78);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 17);
			this.label8.TabIndex = 12;
			this.label8.Text = "■備考";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(572, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 17);
			this.label9.TabIndex = 5;
			this.label9.Text = "■発行日";
			// 
			// dateTimePickerPrintDate
			// 
			this.dateTimePickerPrintDate.Location = new System.Drawing.Point(647, 14);
			this.dateTimePickerPrintDate.Name = "dateTimePickerPrintDate";
			this.dateTimePickerPrintDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerPrintDate.TabIndex = 6;
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
			this.buttonChangeAgreeSpan.Location = new System.Drawing.Point(809, 14);
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
			this.labelAgreeSpan.Location = new System.Drawing.Point(902, 14);
			this.labelAgreeSpan.Name = "labelAgreeSpan";
			this.labelAgreeSpan.Size = new System.Drawing.Size(177, 25);
			this.labelAgreeSpan.TabIndex = 8;
			this.labelAgreeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(989, 450);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 17);
			this.label10.TabIndex = 22;
			this.label10.Text = "月額利用料";
			// 
			// textBoxMonthlyPrice
			// 
			this.textBoxMonthlyPrice.BackColor = System.Drawing.Color.White;
			this.textBoxMonthlyPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxMonthlyPrice.Location = new System.Drawing.Point(992, 470);
			this.textBoxMonthlyPrice.Name = "textBoxMonthlyPrice";
			this.textBoxMonthlyPrice.ReadOnly = true;
			this.textBoxMonthlyPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxMonthlyPrice.TabIndex = 23;
			this.textBoxMonthlyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonRemarkTemplate
			// 
			this.buttonRemarkTemplate.Location = new System.Drawing.Point(1013, 71);
			this.buttonRemarkTemplate.Name = "buttonRemarkTemplate";
			this.buttonRemarkTemplate.Size = new System.Drawing.Size(72, 27);
			this.buttonRemarkTemplate.TabIndex = 14;
			this.buttonRemarkTemplate.Text = "定型文";
			this.buttonRemarkTemplate.UseVisualStyleBackColor = true;
			this.buttonRemarkTemplate.Click += new System.EventHandler(this.buttonRemarkTemplate_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonInitMatomePlan1);
			this.groupBox2.Controls.Add(this.buttonInitMatomePlan2);
			this.groupBox2.Controls.Add(this.buttonInitMatomePlan3);
			this.groupBox2.Location = new System.Drawing.Point(566, 77);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(175, 144);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "■オススメセット";
			// 
			// buttonAllOn
			// 
			this.buttonAllOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAllOn.Location = new System.Drawing.Point(566, 481);
			this.buttonAllOn.Name = "buttonAllOn";
			this.buttonAllOn.Size = new System.Drawing.Size(102, 36);
			this.buttonAllOn.TabIndex = 24;
			this.buttonAllOn.Text = "全選択";
			this.buttonAllOn.UseVisualStyleBackColor = true;
			this.buttonAllOn.Click += new System.EventHandler(this.buttonAllOn_Click);
			// 
			// buttonAllOff
			// 
			this.buttonAllOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAllOff.Location = new System.Drawing.Point(566, 523);
			this.buttonAllOff.Name = "buttonAllOff";
			this.buttonAllOff.Size = new System.Drawing.Size(102, 36);
			this.buttonAllOff.TabIndex = 25;
			this.buttonAllOff.Text = "全解除";
			this.buttonAllOff.UseVisualStyleBackColor = true;
			this.buttonAllOff.Click += new System.EventHandler(this.buttonAllOff_Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(839, 473);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(21, 17);
			this.label16.TabIndex = 18;
			this.label16.Text = "＋";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label17.Location = new System.Drawing.Point(965, 473);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(21, 17);
			this.label17.TabIndex = 21;
			this.label17.Text = "＝";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(572, 47);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 17);
			this.label7.TabIndex = 9;
			this.label7.Text = "■有効期限";
			// 
			// dateTimePickerLimitDate
			// 
			this.dateTimePickerLimitDate.Location = new System.Drawing.Point(647, 44);
			this.dateTimePickerLimitDate.Name = "dateTimePickerLimitDate";
			this.dateTimePickerLimitDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerLimitDate.TabIndex = 10;
			// 
			// SimulationMatomeForm201907
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1098, 613);
			this.Controls.Add(this.dateTimePickerLimitDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.buttonAllOff);
			this.Controls.Add(this.buttonAllOn);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonRemarkTemplate);
			this.Controls.Add(this.textBoxMonthlyPrice);
			this.Controls.Add(this.label10);
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
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxPlatformPrice);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxServicePrice);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listViewService);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SimulationMatomeForm201907";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MIC WEB SERVICE 御見積書作成（おまとめプラン）";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimulationMatomeNewForm_FormClosed);
			this.Load += new System.EventHandler(this.SimulationMatomeNewForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listViewService;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceTypeName;
		private System.Windows.Forms.ColumnHeader columnHeaderServiceName;
		private System.Windows.Forms.ColumnHeader columnHeaderPrice;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxServicePrice;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPlatformPrice;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonInitMatomePlan3;
		private System.Windows.Forms.Button buttonInitMatomePlan2;
		private System.Windows.Forms.Button buttonInitMatomePlan1;
		private System.Windows.Forms.Label labelMatomeMessage;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxMatomeFree60;
		private System.Windows.Forms.TextBox textBoxMatomeTotalPrice60;
		private System.Windows.Forms.TextBox textBoxMatomeFree36;
		private System.Windows.Forms.TextBox textBoxMatomeTotalPrice36;
		private System.Windows.Forms.TextBox textBoxMatomeFree12;
		private System.Windows.Forms.TextBox textBoxMatomeTotalPrice12;
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
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxMonthlyPrice;
		private System.Windows.Forms.Button buttonRemarkTemplate;
		private System.Windows.Forms.RadioButton radioButtonMatome12;
		private System.Windows.Forms.RadioButton radioButtonNormal12;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonAllOn;
		private System.Windows.Forms.Button buttonAllOff;
		private System.Windows.Forms.TextBox textBoxNormalTotalPrice60;
		private System.Windows.Forms.TextBox textBoxNormalTotalPrice36;
		private System.Windows.Forms.TextBox textBoxNormalTotalPrice12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateTimePickerLimitDate;
		private System.Windows.Forms.RadioButton radioButtonMatome60;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBoxMatomeMonthlyPrice60;
		private System.Windows.Forms.RadioButton radioButtonMatome36;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBoxMatomeMonthlyPrice36;
		private System.Windows.Forms.TextBox textBoxNormalMonthlyPrice;
		private System.Windows.Forms.TextBox textBoxMatomeMonthlyPrice12;
		private System.Windows.Forms.RadioButton radioButtonNormal60;
		private System.Windows.Forms.RadioButton radioButtonNormal36;
	}
}