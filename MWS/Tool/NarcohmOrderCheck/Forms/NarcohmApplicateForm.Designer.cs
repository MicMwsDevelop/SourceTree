namespace NarcohmOrderCheck.Forms
{
    partial class NarcohmApplicateForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTel = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.textBoxCustomerNo = new System.Windows.Forms.TextBox();
			this.textBoxToluisakiNo = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxClinicName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxBranch = new System.Windows.Forms.TextBox();
			this.textBoxSalesman = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBoxNarcohm = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.dateTimePickerServiceStartDate = new System.Windows.Forms.DateTimePicker();
			this.listViewApplicate = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label10 = new System.Windows.Forms.Label();
			this.comboBoxSaleType = new System.Windows.Forms.ComboBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonModify = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonLoadOrder = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxSubject = new System.Windows.Forms.TextBox();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCalendarColumn1 = new MwsLib.Component.DataGridViewCalendarColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCalendarColumn2 = new MwsLib.Component.DataGridViewCalendarColumn();
			this.dataGridViewCalendarColumn3 = new MwsLib.Component.DataGridViewCalendarColumn();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 27);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "電話番号";
			// 
			// textBoxTel
			// 
			this.textBoxTel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.textBoxTel.Location = new System.Drawing.Point(83, 24);
			this.textBoxTel.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxTel.Name = "textBoxTel";
			this.textBoxTel.Size = new System.Drawing.Size(209, 24);
			this.textBoxTel.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 59);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "顧客No";
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(292, 24);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(55, 24);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.BackColor = System.Drawing.Color.White;
			this.textBoxCustomerNo.Location = new System.Drawing.Point(83, 56);
			this.textBoxCustomerNo.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.ReadOnly = true;
			this.textBoxCustomerNo.Size = new System.Drawing.Size(209, 24);
			this.textBoxCustomerNo.TabIndex = 4;
			// 
			// textBoxToluisakiNo
			// 
			this.textBoxToluisakiNo.BackColor = System.Drawing.Color.White;
			this.textBoxToluisakiNo.Location = new System.Drawing.Point(383, 57);
			this.textBoxToluisakiNo.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxToluisakiNo.Name = "textBoxToluisakiNo";
			this.textBoxToluisakiNo.ReadOnly = true;
			this.textBoxToluisakiNo.Size = new System.Drawing.Size(209, 24);
			this.textBoxToluisakiNo.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(310, 60);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "得意先No";
			// 
			// textBoxClinicName
			// 
			this.textBoxClinicName.BackColor = System.Drawing.Color.White;
			this.textBoxClinicName.Location = new System.Drawing.Point(83, 88);
			this.textBoxClinicName.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxClinicName.Name = "textBoxClinicName";
			this.textBoxClinicName.ReadOnly = true;
			this.textBoxClinicName.Size = new System.Drawing.Size(509, 24);
			this.textBoxClinicName.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(28, 91);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "医院名";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(28, 123);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "拠店名";
			// 
			// textBoxBranch
			// 
			this.textBoxBranch.BackColor = System.Drawing.Color.White;
			this.textBoxBranch.Location = new System.Drawing.Point(83, 120);
			this.textBoxBranch.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxBranch.Name = "textBoxBranch";
			this.textBoxBranch.ReadOnly = true;
			this.textBoxBranch.Size = new System.Drawing.Size(209, 24);
			this.textBoxBranch.TabIndex = 10;
			// 
			// textBoxSalesman
			// 
			this.textBoxSalesman.BackColor = System.Drawing.Color.White;
			this.textBoxSalesman.Location = new System.Drawing.Point(383, 120);
			this.textBoxSalesman.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxSalesman.Name = "textBoxSalesman";
			this.textBoxSalesman.ReadOnly = true;
			this.textBoxSalesman.Size = new System.Drawing.Size(209, 24);
			this.textBoxSalesman.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(315, 123);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 17);
			this.label6.TabIndex = 11;
			this.label6.Text = "担当者名";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(54, 181);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 17);
			this.label7.TabIndex = 1;
			this.label7.Text = "商品名";
			// 
			// comboBoxNarcohm
			// 
			this.comboBoxNarcohm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxNarcohm.Enabled = false;
			this.comboBoxNarcohm.FormattingEnabled = true;
			this.comboBoxNarcohm.Location = new System.Drawing.Point(108, 179);
			this.comboBoxNarcohm.Name = "comboBoxNarcohm";
			this.comboBoxNarcohm.Size = new System.Drawing.Size(346, 25);
			this.comboBoxNarcohm.TabIndex = 2;
			this.comboBoxNarcohm.SelectedIndexChanged += new System.EventHandler(this.comboBoxNarcohm_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxClinicName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxCustomerNo);
			this.groupBox1.Controls.Add(this.buttonSearch);
			this.groupBox1.Controls.Add(this.textBoxSalesman);
			this.groupBox1.Controls.Add(this.textBoxTel);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBoxToluisakiNo);
			this.groupBox1.Controls.Add(this.textBoxBranch);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(25, 14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(614, 159);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "医院情報";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(11, 212);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 17);
			this.label8.TabIndex = 3;
			this.label8.Text = "サービス開始日";
			// 
			// dateTimePickerServiceStartDate
			// 
			this.dateTimePickerServiceStartDate.Checked = false;
			this.dateTimePickerServiceStartDate.Enabled = false;
			this.dateTimePickerServiceStartDate.Location = new System.Drawing.Point(106, 210);
			this.dateTimePickerServiceStartDate.Name = "dateTimePickerServiceStartDate";
			this.dateTimePickerServiceStartDate.ShowCheckBox = true;
			this.dateTimePickerServiceStartDate.Size = new System.Drawing.Size(164, 24);
			this.dateTimePickerServiceStartDate.TabIndex = 4;
			// 
			// listViewApplicate
			// 
			this.listViewApplicate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
			this.listViewApplicate.FullRowSelect = true;
			this.listViewApplicate.Location = new System.Drawing.Point(12, 304);
			this.listViewApplicate.MultiSelect = false;
			this.listViewApplicate.Name = "listViewApplicate";
			this.listViewApplicate.Size = new System.Drawing.Size(902, 196);
			this.listViewApplicate.TabIndex = 10;
			this.listViewApplicate.UseCompatibleStateImageBehavior = false;
			this.listViewApplicate.View = System.Windows.Forms.View.Details;
			this.listViewApplicate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewApplicate_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "受注番号";
			this.columnHeader1.Width = 70;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "受注日";
			this.columnHeader2.Width = 90;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "商品コード";
			this.columnHeader3.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "商品名";
			this.columnHeader4.Width = 250;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "金額";
			this.columnHeader5.Width = 80;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "数量";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "合計";
			this.columnHeader7.Width = 90;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "利用開始月";
			this.columnHeader8.Width = 80;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "利用終了月";
			this.columnHeader9.Width = 80;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(307, 212);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 17);
			this.label10.TabIndex = 5;
			this.label10.Text = "販売種別";
			// 
			// comboBoxSaleType
			// 
			this.comboBoxSaleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSaleType.Enabled = false;
			this.comboBoxSaleType.FormattingEnabled = true;
			this.comboBoxSaleType.Location = new System.Drawing.Point(374, 210);
			this.comboBoxSaleType.Name = "comboBoxSaleType";
			this.comboBoxSaleType.Size = new System.Drawing.Size(80, 25);
			this.comboBoxSaleType.TabIndex = 6;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Enabled = false;
			this.buttonAdd.Location = new System.Drawing.Point(175, 507);
			this.buttonAdd.Margin = new System.Windows.Forms.Padding(4);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(55, 33);
			this.buttonAdd.TabIndex = 12;
			this.buttonAdd.Text = "追加";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonModify
			// 
			this.buttonModify.Enabled = false;
			this.buttonModify.Location = new System.Drawing.Point(238, 507);
			this.buttonModify.Margin = new System.Windows.Forms.Padding(4);
			this.buttonModify.Name = "buttonModify";
			this.buttonModify.Size = new System.Drawing.Size(55, 33);
			this.buttonModify.TabIndex = 13;
			this.buttonModify.Text = "変更";
			this.buttonModify.UseVisualStyleBackColor = true;
			this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Enabled = false;
			this.buttonRemove.Location = new System.Drawing.Point(301, 507);
			this.buttonRemove.Margin = new System.Windows.Forms.Padding(4);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(55, 33);
			this.buttonRemove.TabIndex = 14;
			this.buttonRemove.Text = "削除";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonLoadOrder
			// 
			this.buttonLoadOrder.Enabled = false;
			this.buttonLoadOrder.Location = new System.Drawing.Point(12, 507);
			this.buttonLoadOrder.Margin = new System.Windows.Forms.Padding(4);
			this.buttonLoadOrder.Name = "buttonLoadOrder";
			this.buttonLoadOrder.Size = new System.Drawing.Size(155, 33);
			this.buttonLoadOrder.TabIndex = 11;
			this.buttonLoadOrder.Text = "受注伝票からの読込";
			this.buttonLoadOrder.UseVisualStyleBackColor = true;
			this.buttonLoadOrder.Click += new System.EventHandler(this.buttonLoadOrder_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(839, 507);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonCancel.TabIndex = 16;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(758, 507);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 33);
			this.buttonOK.TabIndex = 15;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(11, 284);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(86, 17);
			this.label9.TabIndex = 9;
			this.label9.Text = "申込詳細情報";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(67, 244);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(34, 17);
			this.label11.TabIndex = 7;
			this.label11.Text = "件名";
			// 
			// textBoxSubject
			// 
			this.textBoxSubject.Enabled = false;
			this.textBoxSubject.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
			this.textBoxSubject.Location = new System.Drawing.Point(106, 241);
			this.textBoxSubject.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxSubject.Name = "textBoxSubject";
			this.textBoxSubject.Size = new System.Drawing.Size(533, 24);
			this.textBoxSubject.TabIndex = 8;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "受注番号";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// dataGridViewCalendarColumn1
			// 
			this.dataGridViewCalendarColumn1.HeaderText = "受注日";
			this.dataGridViewCalendarColumn1.Name = "dataGridViewCalendarColumn1";
			this.dataGridViewCalendarColumn1.ReadOnly = true;
			this.dataGridViewCalendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCalendarColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "商品コード";
			this.dataGridViewTextBoxColumn2.MaxInputLength = 6;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.HeaderText = "商品名";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.HeaderText = "金額";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.HeaderText = "数量";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.HeaderText = "合計";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewCalendarColumn2
			// 
			this.dataGridViewCalendarColumn2.HeaderText = "利用開始月";
			this.dataGridViewCalendarColumn2.Name = "dataGridViewCalendarColumn2";
			this.dataGridViewCalendarColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCalendarColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// dataGridViewCalendarColumn3
			// 
			this.dataGridViewCalendarColumn3.HeaderText = "利用終了月";
			this.dataGridViewCalendarColumn3.Name = "dataGridViewCalendarColumn3";
			this.dataGridViewCalendarColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCalendarColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// NarcohmApplicateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(931, 556);
			this.Controls.Add(this.textBoxSubject);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonLoadOrder);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonModify);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.comboBoxSaleType);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.listViewApplicate);
			this.Controls.Add(this.dateTimePickerServiceStartDate);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.comboBoxNarcohm);
			this.Controls.Add(this.label7);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "NarcohmApplicateForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ナルコーム製品 サービス開始製品の登録";
			this.Load += new System.EventHandler(this.NarcohmApplicateForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxCustomerNo;
        private System.Windows.Forms.TextBox textBoxToluisakiNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxClinicName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxBranch;
        private System.Windows.Forms.TextBox textBoxSalesman;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxNarcohm;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.DateTimePicker dateTimePickerServiceStartDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private MwsLib.Component.DataGridViewCalendarColumn dataGridViewCalendarColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private MwsLib.Component.DataGridViewCalendarColumn dataGridViewCalendarColumn2;
		private MwsLib.Component.DataGridViewCalendarColumn dataGridViewCalendarColumn3;
		private System.Windows.Forms.ListView listViewApplicate;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboBoxSaleType;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonLoadOrder;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxSubject;
	}
}