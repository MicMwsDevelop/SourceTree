﻿namespace MwsSimulation.Forms
{
	partial class SimulationMatomeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationMatomeForm));
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
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxNormalPlanTotalPrice36 = new System.Windows.Forms.TextBox();
			this.radioButtonGroupEnable = new System.Windows.Forms.RadioButton();
			this.label13 = new System.Windows.Forms.Label();
			this.textBoxNormalPlanTotalPrice24 = new System.Windows.Forms.TextBox();
			this.radioButtonGroupDisable = new System.Windows.Forms.RadioButton();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxNormalPlanTotalPrice12 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxFree36 = new System.Windows.Forms.TextBox();
			this.textBoxPrice36 = new System.Windows.Forms.TextBox();
			this.textBoxFree24 = new System.Windows.Forms.TextBox();
			this.textBoxPrice24 = new System.Windows.Forms.TextBox();
			this.textBoxFree12 = new System.Windows.Forms.TextBox();
			this.textBoxPrice12 = new System.Windows.Forms.TextBox();
			this.labelGroupPlanMessage = new System.Windows.Forms.Label();
			this.buttonInitGroupPlan3 = new System.Windows.Forms.Button();
			this.buttonInitGroupPlan2 = new System.Windows.Forms.Button();
			this.buttonInitGroupPlan1 = new System.Windows.Forms.Button();
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
			this.textBoxTotalPrice = new System.Windows.Forms.TextBox();
			this.buttonRemarkTemplate = new System.Windows.Forms.Button();
			this.comboBoxTerm = new System.Windows.Forms.ComboBox();
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
			this.listViewService.Size = new System.Drawing.Size(676, 556);
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(884, 548);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 17);
			this.label1.TabIndex = 23;
			this.label1.Text = "サービス利用料";
			// 
			// textBoxServicePrice
			// 
			this.textBoxServicePrice.BackColor = System.Drawing.Color.White;
			this.textBoxServicePrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxServicePrice.Location = new System.Drawing.Point(887, 568);
			this.textBoxServicePrice.Name = "textBoxServicePrice";
			this.textBoxServicePrice.ReadOnly = true;
			this.textBoxServicePrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxServicePrice.TabIndex = 24;
			this.textBoxServicePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxServicePrice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxTotalPrice_MouseDoubleClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(728, 548);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 17);
			this.label2.TabIndex = 20;
			this.label2.Text = "プラットフォーム利用料";
			// 
			// textBoxPlatformPrice
			// 
			this.textBoxPlatformPrice.BackColor = System.Drawing.Color.White;
			this.textBoxPlatformPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPlatformPrice.Location = new System.Drawing.Point(761, 568);
			this.textBoxPlatformPrice.Name = "textBoxPlatformPrice";
			this.textBoxPlatformPrice.ReadOnly = true;
			this.textBoxPlatformPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxPlatformPrice.TabIndex = 21;
			this.textBoxPlatformPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOK.Location = new System.Drawing.Point(905, 607);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 36);
			this.buttonOK.TabIndex = 28;
			this.buttonOK.Text = "保存";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(1013, 607);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 36);
			this.buttonCancel.TabIndex = 29;
			this.buttonCancel.Text = "破棄";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxNormalPlanTotalPrice36);
			this.groupBox1.Controls.Add(this.radioButtonGroupEnable);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.textBoxNormalPlanTotalPrice24);
			this.groupBox1.Controls.Add(this.radioButtonGroupDisable);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.textBoxNormalPlanTotalPrice12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxFree36);
			this.groupBox1.Controls.Add(this.textBoxPrice36);
			this.groupBox1.Controls.Add(this.textBoxFree24);
			this.groupBox1.Controls.Add(this.textBoxPrice24);
			this.groupBox1.Controls.Add(this.textBoxFree12);
			this.groupBox1.Controls.Add(this.textBoxPrice12);
			this.groupBox1.Controls.Add(this.labelGroupPlanMessage);
			this.groupBox1.Location = new System.Drawing.Point(702, 344);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(415, 191);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "■おまとめプラン";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(72, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "通常料金";
			// 
			// textBoxNormalPlanTotalPrice36
			// 
			this.textBoxNormalPlanTotalPrice36.BackColor = System.Drawing.Color.White;
			this.textBoxNormalPlanTotalPrice36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalPlanTotalPrice36.Location = new System.Drawing.Point(60, 134);
			this.textBoxNormalPlanTotalPrice36.Name = "textBoxNormalPlanTotalPrice36";
			this.textBoxNormalPlanTotalPrice36.ReadOnly = true;
			this.textBoxNormalPlanTotalPrice36.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalPlanTotalPrice36.TabIndex = 14;
			this.textBoxNormalPlanTotalPrice36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// radioButtonGroupEnable
			// 
			this.radioButtonGroupEnable.AutoSize = true;
			this.radioButtonGroupEnable.Location = new System.Drawing.Point(78, 23);
			this.radioButtonGroupEnable.Name = "radioButtonGroupEnable";
			this.radioButtonGroupEnable.Size = new System.Drawing.Size(95, 21);
			this.radioButtonGroupEnable.TabIndex = 1;
			this.radioButtonGroupEnable.Text = "おまとめプラン";
			this.radioButtonGroupEnable.UseVisualStyleBackColor = true;
			this.radioButtonGroupEnable.CheckedChanged += new System.EventHandler(this.radioButtonGroupEnable_CheckedChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(9, 137);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(45, 17);
			this.label13.TabIndex = 13;
			this.label13.Text = "36ヵ月";
			// 
			// textBoxNormalPlanTotalPrice24
			// 
			this.textBoxNormalPlanTotalPrice24.BackColor = System.Drawing.Color.White;
			this.textBoxNormalPlanTotalPrice24.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalPlanTotalPrice24.Location = new System.Drawing.Point(60, 104);
			this.textBoxNormalPlanTotalPrice24.Name = "textBoxNormalPlanTotalPrice24";
			this.textBoxNormalPlanTotalPrice24.ReadOnly = true;
			this.textBoxNormalPlanTotalPrice24.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalPlanTotalPrice24.TabIndex = 10;
			this.textBoxNormalPlanTotalPrice24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// radioButtonGroupDisable
			// 
			this.radioButtonGroupDisable.AutoSize = true;
			this.radioButtonGroupDisable.Checked = true;
			this.radioButtonGroupDisable.Location = new System.Drawing.Point(20, 23);
			this.radioButtonGroupDisable.Name = "radioButtonGroupDisable";
			this.radioButtonGroupDisable.Size = new System.Drawing.Size(52, 21);
			this.radioButtonGroupDisable.TabIndex = 0;
			this.radioButtonGroupDisable.TabStop = true;
			this.radioButtonGroupDisable.Text = "通常";
			this.radioButtonGroupDisable.UseVisualStyleBackColor = true;
			this.radioButtonGroupDisable.CheckedChanged += new System.EventHandler(this.radioButtonGroupDisable_CheckedChanged);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 107);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(45, 17);
			this.label12.TabIndex = 9;
			this.label12.Text = "24ヵ月";
			// 
			// textBoxNormalPlanTotalPrice12
			// 
			this.textBoxNormalPlanTotalPrice12.BackColor = System.Drawing.Color.White;
			this.textBoxNormalPlanTotalPrice12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxNormalPlanTotalPrice12.Location = new System.Drawing.Point(60, 74);
			this.textBoxNormalPlanTotalPrice12.Name = "textBoxNormalPlanTotalPrice12";
			this.textBoxNormalPlanTotalPrice12.ReadOnly = true;
			this.textBoxNormalPlanTotalPrice12.Size = new System.Drawing.Size(93, 24);
			this.textBoxNormalPlanTotalPrice12.TabIndex = 6;
			this.textBoxNormalPlanTotalPrice12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(9, 77);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(45, 17);
			this.label11.TabIndex = 5;
			this.label11.Text = "12ヵ月";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(253, 54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(125, 17);
			this.label6.TabIndex = 4;
			this.label6.Text = "割引額（無償月数）";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(163, 54);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 17);
			this.label5.TabIndex = 3;
			this.label5.Text = "おまとめ料金";
			// 
			// textBoxFree36
			// 
			this.textBoxFree36.BackColor = System.Drawing.Color.White;
			this.textBoxFree36.Enabled = false;
			this.textBoxFree36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxFree36.Location = new System.Drawing.Point(256, 134);
			this.textBoxFree36.Name = "textBoxFree36";
			this.textBoxFree36.ReadOnly = true;
			this.textBoxFree36.Size = new System.Drawing.Size(141, 24);
			this.textBoxFree36.TabIndex = 16;
			this.textBoxFree36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxPrice36
			// 
			this.textBoxPrice36.BackColor = System.Drawing.Color.White;
			this.textBoxPrice36.Enabled = false;
			this.textBoxPrice36.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPrice36.Location = new System.Drawing.Point(158, 134);
			this.textBoxPrice36.Name = "textBoxPrice36";
			this.textBoxPrice36.ReadOnly = true;
			this.textBoxPrice36.Size = new System.Drawing.Size(93, 24);
			this.textBoxPrice36.TabIndex = 15;
			this.textBoxPrice36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPrice36.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPrice36_MouseDoubleClick);
			// 
			// textBoxFree24
			// 
			this.textBoxFree24.BackColor = System.Drawing.Color.White;
			this.textBoxFree24.Enabled = false;
			this.textBoxFree24.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxFree24.Location = new System.Drawing.Point(256, 104);
			this.textBoxFree24.Name = "textBoxFree24";
			this.textBoxFree24.ReadOnly = true;
			this.textBoxFree24.Size = new System.Drawing.Size(141, 24);
			this.textBoxFree24.TabIndex = 12;
			this.textBoxFree24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxPrice24
			// 
			this.textBoxPrice24.BackColor = System.Drawing.Color.White;
			this.textBoxPrice24.Enabled = false;
			this.textBoxPrice24.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPrice24.Location = new System.Drawing.Point(158, 104);
			this.textBoxPrice24.Name = "textBoxPrice24";
			this.textBoxPrice24.ReadOnly = true;
			this.textBoxPrice24.Size = new System.Drawing.Size(93, 24);
			this.textBoxPrice24.TabIndex = 11;
			this.textBoxPrice24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPrice24.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPrice24_MouseDoubleClick);
			// 
			// textBoxFree12
			// 
			this.textBoxFree12.BackColor = System.Drawing.Color.White;
			this.textBoxFree12.Enabled = false;
			this.textBoxFree12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxFree12.Location = new System.Drawing.Point(256, 74);
			this.textBoxFree12.Name = "textBoxFree12";
			this.textBoxFree12.ReadOnly = true;
			this.textBoxFree12.Size = new System.Drawing.Size(141, 24);
			this.textBoxFree12.TabIndex = 8;
			this.textBoxFree12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxPrice12
			// 
			this.textBoxPrice12.BackColor = System.Drawing.Color.White;
			this.textBoxPrice12.Enabled = false;
			this.textBoxPrice12.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPrice12.Location = new System.Drawing.Point(158, 74);
			this.textBoxPrice12.Name = "textBoxPrice12";
			this.textBoxPrice12.ReadOnly = true;
			this.textBoxPrice12.Size = new System.Drawing.Size(93, 24);
			this.textBoxPrice12.TabIndex = 7;
			this.textBoxPrice12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPrice12.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPrice12_MouseDoubleClick);
			// 
			// labelGroupPlanMessage
			// 
			this.labelGroupPlanMessage.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelGroupPlanMessage.ForeColor = System.Drawing.Color.Red;
			this.labelGroupPlanMessage.Location = new System.Drawing.Point(45, 161);
			this.labelGroupPlanMessage.Name = "labelGroupPlanMessage";
			this.labelGroupPlanMessage.Size = new System.Drawing.Size(349, 17);
			this.labelGroupPlanMessage.TabIndex = 17;
			this.labelGroupPlanMessage.Text = "※おまとめプラン割引が適用できます。";
			this.labelGroupPlanMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonInitGroupPlan3
			// 
			this.buttonInitGroupPlan3.Location = new System.Drawing.Point(18, 101);
			this.buttonInitGroupPlan3.Name = "buttonInitGroupPlan3";
			this.buttonInitGroupPlan3.Size = new System.Drawing.Size(378, 35);
			this.buttonInitGroupPlan3.TabIndex = 2;
			this.buttonInitGroupPlan3.Text = "button3";
			this.buttonInitGroupPlan3.UseVisualStyleBackColor = true;
			this.buttonInitGroupPlan3.Click += new System.EventHandler(this.buttonInitGroupPlan3_Click);
			// 
			// buttonInitGroupPlan2
			// 
			this.buttonInitGroupPlan2.Location = new System.Drawing.Point(18, 60);
			this.buttonInitGroupPlan2.Name = "buttonInitGroupPlan2";
			this.buttonInitGroupPlan2.Size = new System.Drawing.Size(378, 35);
			this.buttonInitGroupPlan2.TabIndex = 1;
			this.buttonInitGroupPlan2.Text = "button2";
			this.buttonInitGroupPlan2.UseVisualStyleBackColor = true;
			this.buttonInitGroupPlan2.Click += new System.EventHandler(this.buttonInitGroupPlan2_Click);
			// 
			// buttonInitGroupPlan1
			// 
			this.buttonInitGroupPlan1.Location = new System.Drawing.Point(18, 19);
			this.buttonInitGroupPlan1.Name = "buttonInitGroupPlan1";
			this.buttonInitGroupPlan1.Size = new System.Drawing.Size(378, 35);
			this.buttonInitGroupPlan1.TabIndex = 0;
			this.buttonInitGroupPlan1.Text = "button1";
			this.buttonInitGroupPlan1.UseVisualStyleBackColor = true;
			this.buttonInitGroupPlan1.Click += new System.EventHandler(this.buttonInitGroupPlan1_Click);
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
			this.buttonPrint.Location = new System.Drawing.Point(588, 607);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(102, 36);
			this.buttonPrint.TabIndex = 9;
			this.buttonPrint.Text = "見積書印刷";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// textBoxRemark
			// 
			this.textBoxRemark.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxRemark.Location = new System.Drawing.Point(702, 254);
			this.textBoxRemark.Multiline = true;
			this.textBoxRemark.Name = "textBoxRemark";
			this.textBoxRemark.Size = new System.Drawing.Size(415, 81);
			this.textBoxRemark.TabIndex = 17;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(703, 234);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 17);
			this.label8.TabIndex = 16;
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
			this.buttonChangeAgreeSpan.TabIndex = 10;
			this.buttonChangeAgreeSpan.Text = "■契約期間";
			this.buttonChangeAgreeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonChangeAgreeSpan.UseVisualStyleBackColor = true;
			this.buttonChangeAgreeSpan.Click += new System.EventHandler(this.buttonChangeAgreeSpan_Click);
			// 
			// labelAgreeSpan
			// 
			this.labelAgreeSpan.BackColor = System.Drawing.Color.White;
			this.labelAgreeSpan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAgreeSpan.Location = new System.Drawing.Point(792, 14);
			this.labelAgreeSpan.Name = "labelAgreeSpan";
			this.labelAgreeSpan.Size = new System.Drawing.Size(177, 25);
			this.labelAgreeSpan.TabIndex = 11;
			this.labelAgreeSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(1010, 548);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 17);
			this.label10.TabIndex = 26;
			this.label10.Text = "月額利用料";
			// 
			// textBoxTotalPrice
			// 
			this.textBoxTotalPrice.BackColor = System.Drawing.Color.White;
			this.textBoxTotalPrice.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxTotalPrice.Location = new System.Drawing.Point(1013, 568);
			this.textBoxTotalPrice.Name = "textBoxTotalPrice";
			this.textBoxTotalPrice.ReadOnly = true;
			this.textBoxTotalPrice.Size = new System.Drawing.Size(93, 24);
			this.textBoxTotalPrice.TabIndex = 27;
			this.textBoxTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonRemarkTemplate
			// 
			this.buttonRemarkTemplate.Location = new System.Drawing.Point(1042, 227);
			this.buttonRemarkTemplate.Name = "buttonRemarkTemplate";
			this.buttonRemarkTemplate.Size = new System.Drawing.Size(75, 27);
			this.buttonRemarkTemplate.TabIndex = 18;
			this.buttonRemarkTemplate.Text = "定型文";
			this.buttonRemarkTemplate.UseVisualStyleBackColor = true;
			this.buttonRemarkTemplate.Click += new System.EventHandler(this.buttonRemarkTemplate_Click);
			// 
			// comboBoxTerm
			// 
			this.comboBoxTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTerm.FormattingEnabled = true;
			this.comboBoxTerm.Location = new System.Drawing.Point(972, 14);
			this.comboBoxTerm.Name = "comboBoxTerm";
			this.comboBoxTerm.Size = new System.Drawing.Size(75, 25);
			this.comboBoxTerm.TabIndex = 12;
			this.comboBoxTerm.SelectedIndexChanged += new System.EventHandler(this.comboBoxTerm_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonInitGroupPlan1);
			this.groupBox2.Controls.Add(this.buttonInitGroupPlan2);
			this.groupBox2.Controls.Add(this.buttonInitGroupPlan3);
			this.groupBox2.Location = new System.Drawing.Point(700, 77);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(415, 144);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "■オススメセット";
			// 
			// buttonAllOn
			// 
			this.buttonAllOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAllOn.Location = new System.Drawing.Point(14, 607);
			this.buttonAllOn.Name = "buttonAllOn";
			this.buttonAllOn.Size = new System.Drawing.Size(102, 36);
			this.buttonAllOn.TabIndex = 7;
			this.buttonAllOn.Text = "全選択";
			this.buttonAllOn.UseVisualStyleBackColor = true;
			this.buttonAllOn.Click += new System.EventHandler(this.buttonAllOn_Click);
			// 
			// buttonAllOff
			// 
			this.buttonAllOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAllOff.Location = new System.Drawing.Point(122, 607);
			this.buttonAllOff.Name = "buttonAllOff";
			this.buttonAllOff.Size = new System.Drawing.Size(102, 36);
			this.buttonAllOff.TabIndex = 8;
			this.buttonAllOff.Text = "全解除";
			this.buttonAllOff.UseVisualStyleBackColor = true;
			this.buttonAllOff.Click += new System.EventHandler(this.buttonAllOff_Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(860, 571);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(21, 17);
			this.label16.TabIndex = 22;
			this.label16.Text = "＋";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label17.Location = new System.Drawing.Point(986, 571);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(21, 17);
			this.label17.TabIndex = 25;
			this.label17.Text = "＝";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(703, 51);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 17);
			this.label7.TabIndex = 13;
			this.label7.Text = "■有効期限";
			// 
			// dateTimePickerLimitDate
			// 
			this.dateTimePickerLimitDate.Location = new System.Drawing.Point(792, 45);
			this.dateTimePickerLimitDate.Name = "dateTimePickerLimitDate";
			this.dateTimePickerLimitDate.Size = new System.Drawing.Size(141, 24);
			this.dateTimePickerLimitDate.TabIndex = 14;
			// 
			// SimulationMatomeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 652);
			this.Controls.Add(this.dateTimePickerLimitDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.buttonAllOff);
			this.Controls.Add(this.buttonAllOn);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.comboBoxTerm);
			this.Controls.Add(this.buttonRemarkTemplate);
			this.Controls.Add(this.textBoxTotalPrice);
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
			this.Name = "SimulationMatomeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MIC WEB SERVICE 御見積書作成（おまとめプラン）";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimulationMatomeForm_FormClosed);
			this.Load += new System.EventHandler(this.SimulationMatomeForm_Load);
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
		private System.Windows.Forms.Button buttonInitGroupPlan3;
		private System.Windows.Forms.Button buttonInitGroupPlan2;
		private System.Windows.Forms.Button buttonInitGroupPlan1;
		private System.Windows.Forms.Label labelGroupPlanMessage;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxFree36;
		private System.Windows.Forms.TextBox textBoxPrice36;
		private System.Windows.Forms.TextBox textBoxFree24;
		private System.Windows.Forms.TextBox textBoxPrice24;
		private System.Windows.Forms.TextBox textBoxFree12;
		private System.Windows.Forms.TextBox textBoxPrice12;
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
		private System.Windows.Forms.TextBox textBoxTotalPrice;
		private System.Windows.Forms.Button buttonRemarkTemplate;
		private System.Windows.Forms.ComboBox comboBoxTerm;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.RadioButton radioButtonGroupEnable;
		private System.Windows.Forms.RadioButton radioButtonGroupDisable;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonAllOn;
		private System.Windows.Forms.Button buttonAllOff;
		private System.Windows.Forms.TextBox textBoxNormalPlanTotalPrice36;
		private System.Windows.Forms.TextBox textBoxNormalPlanTotalPrice24;
		private System.Windows.Forms.TextBox textBoxNormalPlanTotalPrice12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateTimePickerLimitDate;
	}
}