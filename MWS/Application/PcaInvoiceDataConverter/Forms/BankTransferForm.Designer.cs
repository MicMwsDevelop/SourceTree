namespace PcaInvoiceDataConverter.Forms
{
	partial class BankTransferForm
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
			this.buttonReadCustomerInfo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonReadInvoiceHeaderData = new System.Windows.Forms.Button();
			this.buttonReadInvoiceDetailData = new System.Windows.Forms.Button();
			this.buttonMakeInvoice = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox請求書番号基数 = new MwsLib.Component.NumericTextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.dateTimePicker銀行振込請求書請求日 = new System.Windows.Forms.DateTimePicker();
			this.label17 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.dateTimePicker銀行振込請求期間終了日 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker銀行振込請求期間開始日 = new System.Windows.Forms.DateTimePicker();
			this.label18 = new System.Windows.Forms.Label();
			this.dateTimePicker銀行振込入金期限日 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxPCA請求一覧11読込みファイル = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxPCA請求明細11読込みファイル = new System.Windows.Forms.TextBox();
			this.label45 = new System.Windows.Forms.Label();
			this.buttonAGREX請求書ファイル出力フォルダ = new System.Windows.Forms.Button();
			this.labelAGREX請求書ファイル出力フォルダ = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.textBoxAGREX請求書ファイル = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.label銀行振込請求一覧請求金額 = new System.Windows.Forms.Label();
			this.label銀行振込請求一覧件数 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label銀行振込請求金額 = new System.Windows.Forms.Label();
			this.label銀行振込請求書件数 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label銀行振込マイナス請求金額 = new System.Windows.Forms.Label();
			this.label銀行振込マイナス請求件数 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label銀行振込0円請求件数 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label銀行振込請求書発行金額 = new System.Windows.Forms.Label();
			this.label銀行振込請求書発行件数 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonReadCustomerInfo
			// 
			this.buttonReadCustomerInfo.Location = new System.Drawing.Point(12, 63);
			this.buttonReadCustomerInfo.Name = "buttonReadCustomerInfo";
			this.buttonReadCustomerInfo.Size = new System.Drawing.Size(368, 57);
			this.buttonReadCustomerInfo.TabIndex = 1;
			this.buttonReadCustomerInfo.Text = "顧客情報読込み";
			this.buttonReadCustomerInfo.UseVisualStyleBackColor = true;
			this.buttonReadCustomerInfo.Click += new System.EventHandler(this.buttonReadCustomerInfo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(53, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(286, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "銀行振替請求書データ作成";
			// 
			// buttonReadInvoiceHeaderData
			// 
			this.buttonReadInvoiceHeaderData.Location = new System.Drawing.Point(12, 126);
			this.buttonReadInvoiceHeaderData.Name = "buttonReadInvoiceHeaderData";
			this.buttonReadInvoiceHeaderData.Size = new System.Drawing.Size(368, 57);
			this.buttonReadInvoiceHeaderData.TabIndex = 2;
			this.buttonReadInvoiceHeaderData.Text = "請求一覧データ読込み";
			this.buttonReadInvoiceHeaderData.UseVisualStyleBackColor = true;
			this.buttonReadInvoiceHeaderData.Click += new System.EventHandler(this.buttonReadInvoiceHeaderData_Click);
			// 
			// buttonReadInvoiceDetailData
			// 
			this.buttonReadInvoiceDetailData.Location = new System.Drawing.Point(12, 189);
			this.buttonReadInvoiceDetailData.Name = "buttonReadInvoiceDetailData";
			this.buttonReadInvoiceDetailData.Size = new System.Drawing.Size(368, 57);
			this.buttonReadInvoiceDetailData.TabIndex = 3;
			this.buttonReadInvoiceDetailData.Text = "請求明細データ読込み";
			this.buttonReadInvoiceDetailData.UseVisualStyleBackColor = true;
			this.buttonReadInvoiceDetailData.Click += new System.EventHandler(this.buttonReadInvoiceDetailData_Click);
			// 
			// buttonMakeInvoice
			// 
			this.buttonMakeInvoice.Location = new System.Drawing.Point(12, 252);
			this.buttonMakeInvoice.Name = "buttonMakeInvoice";
			this.buttonMakeInvoice.Size = new System.Drawing.Size(368, 57);
			this.buttonMakeInvoice.TabIndex = 4;
			this.buttonMakeInvoice.Text = "請求書データ作成";
			this.buttonMakeInvoice.UseVisualStyleBackColor = true;
			this.buttonMakeInvoice.Click += new System.EventHandler(this.buttonMakeInvoice_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Location = new System.Drawing.Point(12, 315);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(368, 57);
			this.buttonExit.TabIndex = 5;
			this.buttonExit.Text = "閉じる";
			this.buttonExit.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Yellow;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Location = new System.Drawing.Point(398, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(210, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "銀行振込請求書発行関連基本データ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox請求書番号基数
			// 
			this.textBox請求書番号基数.Location = new System.Drawing.Point(608, 71);
			this.textBox請求書番号基数.Name = "textBox請求書番号基数";
			this.textBox請求書番号基数.Size = new System.Drawing.Size(180, 23);
			this.textBox請求書番号基数.TabIndex = 8;
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.LightGray;
			this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label16.Location = new System.Drawing.Point(398, 71);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(210, 23);
			this.label16.TabIndex = 7;
			this.label16.Text = "請求書番号基数";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePicker銀行振込請求書請求日
			// 
			this.dateTimePicker銀行振込請求書請求日.Location = new System.Drawing.Point(608, 94);
			this.dateTimePicker銀行振込請求書請求日.Name = "dateTimePicker銀行振込請求書請求日";
			this.dateTimePicker銀行振込請求書請求日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker銀行振込請求書請求日.TabIndex = 10;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.LightGray;
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Location = new System.Drawing.Point(398, 94);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(210, 23);
			this.label17.TabIndex = 9;
			this.label17.Text = "振替日";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(744, 121);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(19, 15);
			this.label19.TabIndex = 13;
			this.label19.Text = "～";
			// 
			// dateTimePicker銀行振込請求期間終了日
			// 
			this.dateTimePicker銀行振込請求期間終了日.Location = new System.Drawing.Point(767, 117);
			this.dateTimePicker銀行振込請求期間終了日.Name = "dateTimePicker銀行振込請求期間終了日";
			this.dateTimePicker銀行振込請求期間終了日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker銀行振込請求期間終了日.TabIndex = 14;
			// 
			// dateTimePicker銀行振込請求期間開始日
			// 
			this.dateTimePicker銀行振込請求期間開始日.Location = new System.Drawing.Point(608, 117);
			this.dateTimePicker銀行振込請求期間開始日.Name = "dateTimePicker銀行振込請求期間開始日";
			this.dateTimePicker銀行振込請求期間開始日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker銀行振込請求期間開始日.TabIndex = 12;
			// 
			// label18
			// 
			this.label18.BackColor = System.Drawing.Color.LightGray;
			this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label18.Location = new System.Drawing.Point(398, 117);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(210, 23);
			this.label18.TabIndex = 11;
			this.label18.Text = "請求期間";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePicker銀行振込入金期限日
			// 
			this.dateTimePicker銀行振込入金期限日.Location = new System.Drawing.Point(608, 140);
			this.dateTimePicker銀行振込入金期限日.Name = "dateTimePicker銀行振込入金期限日";
			this.dateTimePicker銀行振込入金期限日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker銀行振込入金期限日.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.LightGray;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Location = new System.Drawing.Point(398, 140);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(210, 23);
			this.label3.TabIndex = 15;
			this.label3.Text = "入金期限日";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxPCA請求一覧11読込みファイル
			// 
			this.textBoxPCA請求一覧11読込みファイル.Location = new System.Drawing.Point(608, 163);
			this.textBoxPCA請求一覧11読込みファイル.Name = "textBoxPCA請求一覧11読込みファイル";
			this.textBoxPCA請求一覧11読込みファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxPCA請求一覧11読込みファイル.TabIndex = 18;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.LightGray;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Location = new System.Drawing.Point(398, 163);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(210, 23);
			this.label4.TabIndex = 17;
			this.label4.Text = "PCA請求一覧読込みファイル";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxPCA請求明細11読込みファイル
			// 
			this.textBoxPCA請求明細11読込みファイル.Location = new System.Drawing.Point(608, 186);
			this.textBoxPCA請求明細11読込みファイル.Name = "textBoxPCA請求明細11読込みファイル";
			this.textBoxPCA請求明細11読込みファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxPCA請求明細11読込みファイル.TabIndex = 20;
			// 
			// label45
			// 
			this.label45.BackColor = System.Drawing.Color.LightGray;
			this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label45.Location = new System.Drawing.Point(398, 186);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(210, 23);
			this.label45.TabIndex = 19;
			this.label45.Text = "PCA請求明細読込みファイル";
			this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonAGREX請求書ファイル出力フォルダ
			// 
			this.buttonAGREX請求書ファイル出力フォルダ.Location = new System.Drawing.Point(1079, 209);
			this.buttonAGREX請求書ファイル出力フォルダ.Name = "buttonAGREX請求書ファイル出力フォルダ";
			this.buttonAGREX請求書ファイル出力フォルダ.Size = new System.Drawing.Size(26, 23);
			this.buttonAGREX請求書ファイル出力フォルダ.TabIndex = 23;
			this.buttonAGREX請求書ファイル出力フォルダ.Text = "▼";
			this.buttonAGREX請求書ファイル出力フォルダ.UseVisualStyleBackColor = true;
			this.buttonAGREX請求書ファイル出力フォルダ.Click += new System.EventHandler(this.buttonAGREX請求書ファイル出力フォルダ_Click);
			// 
			// labelAGREX請求書ファイル出力フォルダ
			// 
			this.labelAGREX請求書ファイル出力フォルダ.BackColor = System.Drawing.Color.White;
			this.labelAGREX請求書ファイル出力フォルダ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAGREX請求書ファイル出力フォルダ.Location = new System.Drawing.Point(608, 209);
			this.labelAGREX請求書ファイル出力フォルダ.Name = "labelAGREX請求書ファイル出力フォルダ";
			this.labelAGREX請求書ファイル出力フォルダ.Size = new System.Drawing.Size(471, 23);
			this.labelAGREX請求書ファイル出力フォルダ.TabIndex = 22;
			this.labelAGREX請求書ファイル出力フォルダ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label52
			// 
			this.label52.BackColor = System.Drawing.Color.LightGray;
			this.label52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label52.Location = new System.Drawing.Point(398, 209);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(210, 23);
			this.label52.TabIndex = 21;
			this.label52.Text = "AGREX請求書ファイル出力フォルダ";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxAGREX請求書ファイル
			// 
			this.textBoxAGREX請求書ファイル.Location = new System.Drawing.Point(608, 232);
			this.textBoxAGREX請求書ファイル.Name = "textBoxAGREX請求書ファイル";
			this.textBoxAGREX請求書ファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxAGREX請求書ファイル.TabIndex = 25;
			// 
			// label26
			// 
			this.label26.BackColor = System.Drawing.Color.LightGray;
			this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label26.Location = new System.Drawing.Point(398, 232);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(210, 23);
			this.label26.TabIndex = 24;
			this.label26.Text = "AGREX請求書ファイル";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込請求一覧請求金額
			// 
			this.label銀行振込請求一覧請求金額.BackColor = System.Drawing.Color.White;
			this.label銀行振込請求一覧請求金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込請求一覧請求金額.Location = new System.Drawing.Point(919, 255);
			this.label銀行振込請求一覧請求金額.Name = "label銀行振込請求一覧請求金額";
			this.label銀行振込請求一覧請求金額.Size = new System.Drawing.Size(160, 23);
			this.label銀行振込請求一覧請求金額.TabIndex = 29;
			this.label銀行振込請求一覧請求金額.Text = "0 円 ";
			this.label銀行振込請求一覧請求金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込請求一覧件数
			// 
			this.label銀行振込請求一覧件数.BackColor = System.Drawing.Color.White;
			this.label銀行振込請求一覧件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込請求一覧件数.Location = new System.Drawing.Point(608, 255);
			this.label銀行振込請求一覧件数.Name = "label銀行振込請求一覧件数";
			this.label銀行振込請求一覧件数.Size = new System.Drawing.Size(180, 23);
			this.label銀行振込請求一覧件数.TabIndex = 27;
			this.label銀行振込請求一覧件数.Text = "0 件 ";
			this.label銀行振込請求一覧件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Location = new System.Drawing.Point(790, 255);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(130, 23);
			this.label8.TabIndex = 28;
			this.label8.Text = "請求一覧請求金額";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label7.Location = new System.Drawing.Point(398, 255);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(210, 23);
			this.label7.TabIndex = 26;
			this.label7.Text = "請求一覧件数";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込請求金額
			// 
			this.label銀行振込請求金額.BackColor = System.Drawing.Color.White;
			this.label銀行振込請求金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込請求金額.Location = new System.Drawing.Point(919, 278);
			this.label銀行振込請求金額.Name = "label銀行振込請求金額";
			this.label銀行振込請求金額.Size = new System.Drawing.Size(160, 23);
			this.label銀行振込請求金額.TabIndex = 33;
			this.label銀行振込請求金額.Text = "0 円 ";
			this.label銀行振込請求金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込請求書件数
			// 
			this.label銀行振込請求書件数.BackColor = System.Drawing.Color.White;
			this.label銀行振込請求書件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込請求書件数.Location = new System.Drawing.Point(608, 278);
			this.label銀行振込請求書件数.Name = "label銀行振込請求書件数";
			this.label銀行振込請求書件数.Size = new System.Drawing.Size(180, 23);
			this.label銀行振込請求書件数.TabIndex = 31;
			this.label銀行振込請求書件数.Text = "0 件 ";
			this.label銀行振込請求書件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Location = new System.Drawing.Point(790, 278);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(130, 23);
			this.label10.TabIndex = 32;
			this.label10.Text = "銀行振込請求金額";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Location = new System.Drawing.Point(398, 278);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(210, 23);
			this.label9.TabIndex = 30;
			this.label9.Text = "銀行振込請求書件数";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込マイナス請求金額
			// 
			this.label銀行振込マイナス請求金額.BackColor = System.Drawing.Color.White;
			this.label銀行振込マイナス請求金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込マイナス請求金額.Location = new System.Drawing.Point(919, 301);
			this.label銀行振込マイナス請求金額.Name = "label銀行振込マイナス請求金額";
			this.label銀行振込マイナス請求金額.Size = new System.Drawing.Size(160, 23);
			this.label銀行振込マイナス請求金額.TabIndex = 37;
			this.label銀行振込マイナス請求金額.Text = "0 円 ";
			this.label銀行振込マイナス請求金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込マイナス請求件数
			// 
			this.label銀行振込マイナス請求件数.BackColor = System.Drawing.Color.White;
			this.label銀行振込マイナス請求件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込マイナス請求件数.Location = new System.Drawing.Point(608, 301);
			this.label銀行振込マイナス請求件数.Name = "label銀行振込マイナス請求件数";
			this.label銀行振込マイナス請求件数.Size = new System.Drawing.Size(180, 23);
			this.label銀行振込マイナス請求件数.TabIndex = 35;
			this.label銀行振込マイナス請求件数.Text = "0 件 ";
			this.label銀行振込マイナス請求件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Location = new System.Drawing.Point(790, 301);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(130, 23);
			this.label11.TabIndex = 36;
			this.label11.Text = "マイナス請求金額";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Location = new System.Drawing.Point(398, 301);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(210, 23);
			this.label12.TabIndex = 34;
			this.label12.Text = "マイナス請求件数";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込0円請求件数
			// 
			this.label銀行振込0円請求件数.BackColor = System.Drawing.Color.White;
			this.label銀行振込0円請求件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込0円請求件数.Location = new System.Drawing.Point(608, 324);
			this.label銀行振込0円請求件数.Name = "label銀行振込0円請求件数";
			this.label銀行振込0円請求件数.Size = new System.Drawing.Size(180, 23);
			this.label銀行振込0円請求件数.TabIndex = 39;
			this.label銀行振込0円請求件数.Text = "0 件 ";
			this.label銀行振込0円請求件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Location = new System.Drawing.Point(398, 324);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(210, 23);
			this.label6.TabIndex = 38;
			this.label6.Text = "０円請求件数";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込請求書発行金額
			// 
			this.label銀行振込請求書発行金額.BackColor = System.Drawing.Color.White;
			this.label銀行振込請求書発行金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込請求書発行金額.Location = new System.Drawing.Point(919, 347);
			this.label銀行振込請求書発行金額.Name = "label銀行振込請求書発行金額";
			this.label銀行振込請求書発行金額.Size = new System.Drawing.Size(160, 23);
			this.label銀行振込請求書発行金額.TabIndex = 43;
			this.label銀行振込請求書発行金額.Text = "0 円 ";
			this.label銀行振込請求書発行金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label銀行振込請求書発行件数
			// 
			this.label銀行振込請求書発行件数.BackColor = System.Drawing.Color.White;
			this.label銀行振込請求書発行件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label銀行振込請求書発行件数.Location = new System.Drawing.Point(608, 347);
			this.label銀行振込請求書発行件数.Name = "label銀行振込請求書発行件数";
			this.label銀行振込請求書発行件数.Size = new System.Drawing.Size(180, 23);
			this.label銀行振込請求書発行件数.TabIndex = 41;
			this.label銀行振込請求書発行件数.Text = "0 件 ";
			this.label銀行振込請求書発行件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label30
			// 
			this.label30.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label30.Location = new System.Drawing.Point(790, 347);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(130, 23);
			this.label30.TabIndex = 42;
			this.label30.Text = "WEB請求書請求金額";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label31
			// 
			this.label31.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label31.Location = new System.Drawing.Point(398, 347);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(210, 23);
			this.label31.TabIndex = 40;
			this.label31.Text = "請求書発行件数";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BankTransferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1118, 385);
			this.Controls.Add(this.label銀行振込請求書発行金額);
			this.Controls.Add(this.label銀行振込請求書発行件数);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label31);
			this.Controls.Add(this.label銀行振込0円請求件数);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label銀行振込マイナス請求金額);
			this.Controls.Add(this.label銀行振込マイナス請求件数);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label銀行振込請求金額);
			this.Controls.Add(this.label銀行振込請求書件数);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label銀行振込請求一覧請求金額);
			this.Controls.Add(this.label銀行振込請求一覧件数);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxAGREX請求書ファイル);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.buttonAGREX請求書ファイル出力フォルダ);
			this.Controls.Add(this.labelAGREX請求書ファイル出力フォルダ);
			this.Controls.Add(this.label52);
			this.Controls.Add(this.textBoxPCA請求明細11読込みファイル);
			this.Controls.Add(this.label45);
			this.Controls.Add(this.textBoxPCA請求一覧11読込みファイル);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePicker銀行振込入金期限日);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.dateTimePicker銀行振込請求期間終了日);
			this.Controls.Add(this.dateTimePicker銀行振込請求期間開始日);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.dateTimePicker銀行振込請求書請求日);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.textBox請求書番号基数);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonMakeInvoice);
			this.Controls.Add(this.buttonReadInvoiceDetailData);
			this.Controls.Add(this.buttonReadInvoiceHeaderData);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonReadCustomerInfo);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BankTransferForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "銀行振替請求書データ作成";
			this.Load += new System.EventHandler(this.BankTransferForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonReadCustomerInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonReadInvoiceHeaderData;
		private System.Windows.Forms.Button buttonReadInvoiceDetailData;
		private System.Windows.Forms.Button buttonMakeInvoice;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label label2;
		private MwsLib.Component.NumericTextBox textBox請求書番号基数;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.DateTimePicker dateTimePicker銀行振込請求書請求日;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.DateTimePicker dateTimePicker銀行振込請求期間終了日;
		private System.Windows.Forms.DateTimePicker dateTimePicker銀行振込請求期間開始日;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.DateTimePicker dateTimePicker銀行振込入金期限日;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxPCA請求一覧11読込みファイル;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxPCA請求明細11読込みファイル;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.Button buttonAGREX請求書ファイル出力フォルダ;
		private System.Windows.Forms.Label labelAGREX請求書ファイル出力フォルダ;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.TextBox textBoxAGREX請求書ファイル;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label銀行振込請求一覧請求金額;
		private System.Windows.Forms.Label label銀行振込請求一覧件数;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label銀行振込請求金額;
		private System.Windows.Forms.Label label銀行振込請求書件数;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label銀行振込マイナス請求金額;
		private System.Windows.Forms.Label label銀行振込マイナス請求件数;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label銀行振込0円請求件数;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label銀行振込請求書発行金額;
		private System.Windows.Forms.Label label銀行振込請求書発行件数;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
	}
}