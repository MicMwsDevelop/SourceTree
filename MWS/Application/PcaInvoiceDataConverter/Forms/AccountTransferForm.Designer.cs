namespace PcaInvoiceDataConverter.Forms
{
	partial class AccountTransferForm
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
			this.buttonMakeAccountTransfer = new System.Windows.Forms.Button();
			this.buttonMakeInvoice = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label口座振替請求一覧件数 = new System.Windows.Forms.Label();
			this.label口座振替不可件数 = new System.Windows.Forms.Label();
			this.label口座振替不要件数 = new System.Windows.Forms.Label();
			this.label口座振替請求件数 = new System.Windows.Forms.Label();
			this.label口座振替請求一覧請求金額 = new System.Windows.Forms.Label();
			this.label口座振替不可請求額 = new System.Windows.Forms.Label();
			this.label口座振替不要請求額 = new System.Windows.Forms.Label();
			this.label口座振替請求金額 = new System.Windows.Forms.Label();
			this.labelAPLUS送信ファイル出力フォルダ = new System.Windows.Forms.Label();
			this.labelWEB請求書ファイル出力フォルダ = new System.Windows.Forms.Label();
			this.labelAGREX口振通知書ファイル出力フォルダ = new System.Windows.Forms.Label();
			this.label請求金額あり件数 = new System.Windows.Forms.Label();
			this.label口振請求なし件数 = new System.Windows.Forms.Label();
			this.labelWEB請求書件数 = new System.Windows.Forms.Label();
			this.labelAGREX口振通知書件数 = new System.Windows.Forms.Label();
			this.labelWEB請求書請求金額 = new System.Windows.Forms.Label();
			this.dateTimePicker口座振替日 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker口座振替請求日 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker口座振替請求期間開始日 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker口座振替請求期間終了日 = new System.Windows.Forms.DateTimePicker();
			this.label19 = new System.Windows.Forms.Label();
			this.buttonAPLUS送信ファイル出力フォルダ = new System.Windows.Forms.Button();
			this.buttonWEB請求書ファイル出力フォルダ = new System.Windows.Forms.Button();
			this.buttonAGREX口振通知書ファイル出力フォルダ = new System.Windows.Forms.Button();
			this.textBoxPCA請求一覧10読込みファイル = new System.Windows.Forms.TextBox();
			this.textBoxAPLUS送信ファイル = new System.Windows.Forms.TextBox();
			this.textBoxWEB請求書番号基数 = new MwsLib.Component.NumericTextBox();
			this.textBoxPCA請求明細10読込みファイル = new System.Windows.Forms.TextBox();
			this.textBoxWEB請求書ヘッダファイル = new System.Windows.Forms.TextBox();
			this.textBoxWEB請求書明細売上行ファイル = new System.Windows.Forms.TextBox();
			this.textBoxWEB請求書明細消費税行ファイル = new System.Windows.Forms.TextBox();
			this.textBoxWEB請求書明細記事行ファイル = new System.Windows.Forms.TextBox();
			this.textBoxAGREX口振通知書ファイル = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonReadCustomerInfo
			// 
			this.buttonReadCustomerInfo.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonReadCustomerInfo.Location = new System.Drawing.Point(12, 66);
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
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(214, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "口座振替データ作成";
			// 
			// buttonReadInvoiceHeaderData
			// 
			this.buttonReadInvoiceHeaderData.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonReadInvoiceHeaderData.Location = new System.Drawing.Point(12, 129);
			this.buttonReadInvoiceHeaderData.Name = "buttonReadInvoiceHeaderData";
			this.buttonReadInvoiceHeaderData.Size = new System.Drawing.Size(368, 57);
			this.buttonReadInvoiceHeaderData.TabIndex = 2;
			this.buttonReadInvoiceHeaderData.Text = "請求一覧データ読込み";
			this.buttonReadInvoiceHeaderData.UseVisualStyleBackColor = true;
			this.buttonReadInvoiceHeaderData.Click += new System.EventHandler(this.buttonReadInvoiceHeaderData_Click);
			// 
			// buttonReadInvoiceDetailData
			// 
			this.buttonReadInvoiceDetailData.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonReadInvoiceDetailData.Location = new System.Drawing.Point(12, 192);
			this.buttonReadInvoiceDetailData.Name = "buttonReadInvoiceDetailData";
			this.buttonReadInvoiceDetailData.Size = new System.Drawing.Size(368, 57);
			this.buttonReadInvoiceDetailData.TabIndex = 3;
			this.buttonReadInvoiceDetailData.Text = "請求明細データ読込み";
			this.buttonReadInvoiceDetailData.UseVisualStyleBackColor = true;
			this.buttonReadInvoiceDetailData.Click += new System.EventHandler(this.buttonReadInvoiceDetailData_Click);
			// 
			// buttonMakeAccountTransfer
			// 
			this.buttonMakeAccountTransfer.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonMakeAccountTransfer.Location = new System.Drawing.Point(12, 255);
			this.buttonMakeAccountTransfer.Name = "buttonMakeAccountTransfer";
			this.buttonMakeAccountTransfer.Size = new System.Drawing.Size(368, 57);
			this.buttonMakeAccountTransfer.TabIndex = 4;
			this.buttonMakeAccountTransfer.Text = "口座振替データ作成";
			this.buttonMakeAccountTransfer.UseVisualStyleBackColor = true;
			this.buttonMakeAccountTransfer.Click += new System.EventHandler(this.buttonMakeAccountTransfer_Click);
			// 
			// buttonMakeInvoice
			// 
			this.buttonMakeInvoice.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonMakeInvoice.Location = new System.Drawing.Point(12, 318);
			this.buttonMakeInvoice.Name = "buttonMakeInvoice";
			this.buttonMakeInvoice.Size = new System.Drawing.Size(368, 57);
			this.buttonMakeInvoice.TabIndex = 5;
			this.buttonMakeInvoice.Text = "請求書データ作成";
			this.buttonMakeInvoice.UseVisualStyleBackColor = true;
			this.buttonMakeInvoice.Click += new System.EventHandler(this.buttonMakeInvoice_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonExit.Location = new System.Drawing.Point(12, 381);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(368, 57);
			this.buttonExit.TabIndex = 6;
			this.buttonExit.Text = "閉じる";
			this.buttonExit.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Yellow;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Location = new System.Drawing.Point(396, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(210, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "口座振替関連基本データ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.LightGray;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(396, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(210, 23);
			this.label3.TabIndex = 8;
			this.label3.Text = "振替日";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.LightGray;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(396, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(210, 23);
			this.label4.TabIndex = 10;
			this.label4.Text = "PCA請求一覧読込みファイル";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.LightGray;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(396, 79);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(210, 23);
			this.label5.TabIndex = 12;
			this.label5.Text = "APLUS送信ファイル出力フォルダ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.LightGray;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(396, 101);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(210, 23);
			this.label6.TabIndex = 15;
			this.label6.Text = "APLUS送信ファイル";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label7.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(396, 123);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(210, 23);
			this.label7.TabIndex = 17;
			this.label7.Text = "請求一覧件数";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label8.Location = new System.Drawing.Point(788, 123);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(130, 23);
			this.label8.TabIndex = 19;
			this.label8.Text = "請求一覧請求金額";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label9.Location = new System.Drawing.Point(396, 145);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(210, 23);
			this.label9.TabIndex = 21;
			this.label9.Text = "口座振替不可件数";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label10.Location = new System.Drawing.Point(788, 145);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(130, 23);
			this.label10.TabIndex = 23;
			this.label10.Text = "口座振替不可請求額";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(788, 167);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(130, 23);
			this.label11.TabIndex = 26;
			this.label11.Text = "口座振替不要請求額";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label12.Location = new System.Drawing.Point(396, 167);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(210, 23);
			this.label12.TabIndex = 17;
			this.label12.Text = "口座振替不要件数";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label13.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label13.Location = new System.Drawing.Point(788, 189);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(130, 23);
			this.label13.TabIndex = 30;
			this.label13.Text = "口座振替請求金額";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label14.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label14.Location = new System.Drawing.Point(396, 189);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(210, 23);
			this.label14.TabIndex = 28;
			this.label14.Text = "口座振替請求件数";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.BackColor = System.Drawing.Color.Yellow;
			this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label15.Location = new System.Drawing.Point(396, 222);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(210, 23);
			this.label15.TabIndex = 32;
			this.label15.Text = "WEB請求書発行関連基本データ";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.LightGray;
			this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label16.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label16.Location = new System.Drawing.Point(396, 244);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(210, 23);
			this.label16.TabIndex = 33;
			this.label16.Text = "WEB請求書番号基数";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.BackColor = System.Drawing.Color.LightGray;
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label17.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label17.Location = new System.Drawing.Point(396, 266);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(210, 23);
			this.label17.TabIndex = 35;
			this.label17.Text = "振替日";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.BackColor = System.Drawing.Color.LightGray;
			this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label18.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label18.Location = new System.Drawing.Point(396, 288);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(210, 23);
			this.label18.TabIndex = 37;
			this.label18.Text = "請求期間";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label45
			// 
			this.label45.BackColor = System.Drawing.Color.LightGray;
			this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label45.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label45.Location = new System.Drawing.Point(396, 310);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(210, 23);
			this.label45.TabIndex = 41;
			this.label45.Text = "PCA請求明細読込みファイル";
			this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label52
			// 
			this.label52.BackColor = System.Drawing.Color.LightGray;
			this.label52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label52.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label52.Location = new System.Drawing.Point(396, 332);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(210, 23);
			this.label52.TabIndex = 43;
			this.label52.Text = "WEB請求書ファイル出力フォルダ";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.BackColor = System.Drawing.Color.LightGray;
			this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label21.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label21.Location = new System.Drawing.Point(396, 354);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(210, 23);
			this.label21.TabIndex = 46;
			this.label21.Text = "WEB請求書ヘッダファイル";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label22
			// 
			this.label22.BackColor = System.Drawing.Color.LightGray;
			this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label22.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label22.Location = new System.Drawing.Point(396, 376);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(210, 23);
			this.label22.TabIndex = 48;
			this.label22.Text = "WEB請求書明細売上行ファイル";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label23
			// 
			this.label23.BackColor = System.Drawing.Color.LightGray;
			this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label23.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label23.Location = new System.Drawing.Point(396, 398);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(210, 23);
			this.label23.TabIndex = 50;
			this.label23.Text = "WEB請求書明細消費税行ファイル";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.BackColor = System.Drawing.Color.LightGray;
			this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label24.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label24.Location = new System.Drawing.Point(396, 420);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(210, 23);
			this.label24.TabIndex = 52;
			this.label24.Text = "WEB請求書明細記事行ファイル";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label25
			// 
			this.label25.BackColor = System.Drawing.Color.LightGray;
			this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label25.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label25.Location = new System.Drawing.Point(396, 442);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(210, 23);
			this.label25.TabIndex = 54;
			this.label25.Text = "AGREX口振通知書ファイル出力フォルダ";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.BackColor = System.Drawing.Color.LightGray;
			this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label26.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label26.Location = new System.Drawing.Point(396, 464);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(210, 23);
			this.label26.TabIndex = 57;
			this.label26.Text = "AGREX口振通知書ファイル";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label27
			// 
			this.label27.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label27.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label27.Location = new System.Drawing.Point(396, 486);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(210, 23);
			this.label27.TabIndex = 59;
			this.label27.Text = "WEB請求書件数";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label28
			// 
			this.label28.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label28.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label28.Location = new System.Drawing.Point(396, 508);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(210, 23);
			this.label28.TabIndex = 63;
			this.label28.Text = "口振請求なし件数（※）";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label29
			// 
			this.label29.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label29.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label29.Location = new System.Drawing.Point(788, 486);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(130, 23);
			this.label29.TabIndex = 61;
			this.label29.Text = "AGREX口振通知書件数";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label30
			// 
			this.label30.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label30.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label30.Location = new System.Drawing.Point(788, 530);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(130, 23);
			this.label30.TabIndex = 67;
			this.label30.Text = "WEB請求書請求金額";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label31
			// 
			this.label31.BackColor = System.Drawing.Color.LightSkyBlue;
			this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label31.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label31.Location = new System.Drawing.Point(396, 530);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(210, 23);
			this.label31.TabIndex = 65;
			this.label31.Text = "請求金額あり件数";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label32.Location = new System.Drawing.Point(388, 556);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(217, 14);
			this.label32.TabIndex = 69;
			this.label32.Text = "（※０円売上明細のWEB請求書作成件数）";
			// 
			// label口座振替請求一覧件数
			// 
			this.label口座振替請求一覧件数.BackColor = System.Drawing.Color.White;
			this.label口座振替請求一覧件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替請求一覧件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替請求一覧件数.Location = new System.Drawing.Point(606, 123);
			this.label口座振替請求一覧件数.Name = "label口座振替請求一覧件数";
			this.label口座振替請求一覧件数.Size = new System.Drawing.Size(180, 23);
			this.label口座振替請求一覧件数.TabIndex = 18;
			this.label口座振替請求一覧件数.Text = "0 件 ";
			this.label口座振替請求一覧件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替不可件数
			// 
			this.label口座振替不可件数.BackColor = System.Drawing.Color.White;
			this.label口座振替不可件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替不可件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替不可件数.Location = new System.Drawing.Point(606, 145);
			this.label口座振替不可件数.Name = "label口座振替不可件数";
			this.label口座振替不可件数.Size = new System.Drawing.Size(180, 23);
			this.label口座振替不可件数.TabIndex = 22;
			this.label口座振替不可件数.Text = "0 件 ";
			this.label口座振替不可件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替不要件数
			// 
			this.label口座振替不要件数.BackColor = System.Drawing.Color.White;
			this.label口座振替不要件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替不要件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替不要件数.Location = new System.Drawing.Point(606, 167);
			this.label口座振替不要件数.Name = "label口座振替不要件数";
			this.label口座振替不要件数.Size = new System.Drawing.Size(180, 23);
			this.label口座振替不要件数.TabIndex = 25;
			this.label口座振替不要件数.Text = "0 件 ";
			this.label口座振替不要件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替請求件数
			// 
			this.label口座振替請求件数.BackColor = System.Drawing.Color.White;
			this.label口座振替請求件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替請求件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替請求件数.Location = new System.Drawing.Point(606, 189);
			this.label口座振替請求件数.Name = "label口座振替請求件数";
			this.label口座振替請求件数.Size = new System.Drawing.Size(180, 23);
			this.label口座振替請求件数.TabIndex = 29;
			this.label口座振替請求件数.Text = "0 件 ";
			this.label口座振替請求件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替請求一覧請求金額
			// 
			this.label口座振替請求一覧請求金額.BackColor = System.Drawing.Color.White;
			this.label口座振替請求一覧請求金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替請求一覧請求金額.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替請求一覧請求金額.Location = new System.Drawing.Point(917, 123);
			this.label口座振替請求一覧請求金額.Name = "label口座振替請求一覧請求金額";
			this.label口座振替請求一覧請求金額.Size = new System.Drawing.Size(160, 23);
			this.label口座振替請求一覧請求金額.TabIndex = 20;
			this.label口座振替請求一覧請求金額.Text = "0 円 ";
			this.label口座振替請求一覧請求金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替不可請求額
			// 
			this.label口座振替不可請求額.BackColor = System.Drawing.Color.White;
			this.label口座振替不可請求額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替不可請求額.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替不可請求額.Location = new System.Drawing.Point(917, 145);
			this.label口座振替不可請求額.Name = "label口座振替不可請求額";
			this.label口座振替不可請求額.Size = new System.Drawing.Size(160, 23);
			this.label口座振替不可請求額.TabIndex = 24;
			this.label口座振替不可請求額.Text = "0 円 ";
			this.label口座振替不可請求額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替不要請求額
			// 
			this.label口座振替不要請求額.BackColor = System.Drawing.Color.White;
			this.label口座振替不要請求額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替不要請求額.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替不要請求額.Location = new System.Drawing.Point(917, 167);
			this.label口座振替不要請求額.Name = "label口座振替不要請求額";
			this.label口座振替不要請求額.Size = new System.Drawing.Size(160, 23);
			this.label口座振替不要請求額.TabIndex = 27;
			this.label口座振替不要請求額.Text = "0 円 ";
			this.label口座振替不要請求額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口座振替請求金額
			// 
			this.label口座振替請求金額.BackColor = System.Drawing.Color.White;
			this.label口座振替請求金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口座振替請求金額.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口座振替請求金額.Location = new System.Drawing.Point(917, 189);
			this.label口座振替請求金額.Name = "label口座振替請求金額";
			this.label口座振替請求金額.Size = new System.Drawing.Size(160, 23);
			this.label口座振替請求金額.TabIndex = 31;
			this.label口座振替請求金額.Text = "0 円 ";
			this.label口座振替請求金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAPLUS送信ファイル出力フォルダ
			// 
			this.labelAPLUS送信ファイル出力フォルダ.BackColor = System.Drawing.Color.White;
			this.labelAPLUS送信ファイル出力フォルダ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAPLUS送信ファイル出力フォルダ.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelAPLUS送信ファイル出力フォルダ.Location = new System.Drawing.Point(606, 79);
			this.labelAPLUS送信ファイル出力フォルダ.Name = "labelAPLUS送信ファイル出力フォルダ";
			this.labelAPLUS送信ファイル出力フォルダ.Size = new System.Drawing.Size(471, 23);
			this.labelAPLUS送信ファイル出力フォルダ.TabIndex = 13;
			this.labelAPLUS送信ファイル出力フォルダ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelWEB請求書ファイル出力フォルダ
			// 
			this.labelWEB請求書ファイル出力フォルダ.BackColor = System.Drawing.Color.White;
			this.labelWEB請求書ファイル出力フォルダ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelWEB請求書ファイル出力フォルダ.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelWEB請求書ファイル出力フォルダ.Location = new System.Drawing.Point(606, 332);
			this.labelWEB請求書ファイル出力フォルダ.Name = "labelWEB請求書ファイル出力フォルダ";
			this.labelWEB請求書ファイル出力フォルダ.Size = new System.Drawing.Size(471, 23);
			this.labelWEB請求書ファイル出力フォルダ.TabIndex = 44;
			this.labelWEB請求書ファイル出力フォルダ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelAGREX口振通知書ファイル出力フォルダ
			// 
			this.labelAGREX口振通知書ファイル出力フォルダ.BackColor = System.Drawing.Color.White;
			this.labelAGREX口振通知書ファイル出力フォルダ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAGREX口振通知書ファイル出力フォルダ.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelAGREX口振通知書ファイル出力フォルダ.Location = new System.Drawing.Point(606, 442);
			this.labelAGREX口振通知書ファイル出力フォルダ.Name = "labelAGREX口振通知書ファイル出力フォルダ";
			this.labelAGREX口振通知書ファイル出力フォルダ.Size = new System.Drawing.Size(471, 23);
			this.labelAGREX口振通知書ファイル出力フォルダ.TabIndex = 55;
			this.labelAGREX口振通知書ファイル出力フォルダ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label請求金額あり件数
			// 
			this.label請求金額あり件数.BackColor = System.Drawing.Color.White;
			this.label請求金額あり件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label請求金額あり件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label請求金額あり件数.Location = new System.Drawing.Point(606, 530);
			this.label請求金額あり件数.Name = "label請求金額あり件数";
			this.label請求金額あり件数.Size = new System.Drawing.Size(180, 23);
			this.label請求金額あり件数.TabIndex = 66;
			this.label請求金額あり件数.Text = "0 件 ";
			this.label請求金額あり件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label口振請求なし件数
			// 
			this.label口振請求なし件数.BackColor = System.Drawing.Color.White;
			this.label口振請求なし件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label口振請求なし件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label口振請求なし件数.Location = new System.Drawing.Point(606, 508);
			this.label口振請求なし件数.Name = "label口振請求なし件数";
			this.label口振請求なし件数.Size = new System.Drawing.Size(180, 23);
			this.label口振請求なし件数.TabIndex = 64;
			this.label口振請求なし件数.Text = "0 件 ";
			this.label口振請求なし件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelWEB請求書件数
			// 
			this.labelWEB請求書件数.BackColor = System.Drawing.Color.White;
			this.labelWEB請求書件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelWEB請求書件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelWEB請求書件数.Location = new System.Drawing.Point(606, 486);
			this.labelWEB請求書件数.Name = "labelWEB請求書件数";
			this.labelWEB請求書件数.Size = new System.Drawing.Size(180, 23);
			this.labelWEB請求書件数.TabIndex = 60;
			this.labelWEB請求書件数.Text = "0 件 ";
			this.labelWEB請求書件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAGREX口振通知書件数
			// 
			this.labelAGREX口振通知書件数.BackColor = System.Drawing.Color.White;
			this.labelAGREX口振通知書件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelAGREX口振通知書件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelAGREX口振通知書件数.Location = new System.Drawing.Point(917, 486);
			this.labelAGREX口振通知書件数.Name = "labelAGREX口振通知書件数";
			this.labelAGREX口振通知書件数.Size = new System.Drawing.Size(160, 23);
			this.labelAGREX口振通知書件数.TabIndex = 62;
			this.labelAGREX口振通知書件数.Text = "0 件 ";
			this.labelAGREX口振通知書件数.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelWEB請求書請求金額
			// 
			this.labelWEB請求書請求金額.BackColor = System.Drawing.Color.White;
			this.labelWEB請求書請求金額.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelWEB請求書請求金額.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelWEB請求書請求金額.Location = new System.Drawing.Point(917, 530);
			this.labelWEB請求書請求金額.Name = "labelWEB請求書請求金額";
			this.labelWEB請求書請求金額.Size = new System.Drawing.Size(160, 23);
			this.labelWEB請求書請求金額.TabIndex = 68;
			this.labelWEB請求書請求金額.Text = "0 円 ";
			this.labelWEB請求書請求金額.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimePicker口座振替日
			// 
			this.dateTimePicker口座振替日.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.dateTimePicker口座振替日.Location = new System.Drawing.Point(606, 35);
			this.dateTimePicker口座振替日.Name = "dateTimePicker口座振替日";
			this.dateTimePicker口座振替日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker口座振替日.TabIndex = 9;
			// 
			// dateTimePicker口座振替請求日
			// 
			this.dateTimePicker口座振替請求日.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.dateTimePicker口座振替請求日.Location = new System.Drawing.Point(606, 266);
			this.dateTimePicker口座振替請求日.Name = "dateTimePicker口座振替請求日";
			this.dateTimePicker口座振替請求日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker口座振替請求日.TabIndex = 36;
			// 
			// dateTimePicker口座振替請求期間開始日
			// 
			this.dateTimePicker口座振替請求期間開始日.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.dateTimePicker口座振替請求期間開始日.Location = new System.Drawing.Point(606, 288);
			this.dateTimePicker口座振替請求期間開始日.Name = "dateTimePicker口座振替請求期間開始日";
			this.dateTimePicker口座振替請求期間開始日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker口座振替請求期間開始日.TabIndex = 38;
			// 
			// dateTimePicker口座振替請求期間終了日
			// 
			this.dateTimePicker口座振替請求期間終了日.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.dateTimePicker口座振替請求期間終了日.Location = new System.Drawing.Point(765, 288);
			this.dateTimePicker口座振替請求期間終了日.Name = "dateTimePicker口座振替請求期間終了日";
			this.dateTimePicker口座振替請求期間終了日.Size = new System.Drawing.Size(131, 23);
			this.dateTimePicker口座振替請求期間終了日.TabIndex = 40;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(742, 292);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(19, 15);
			this.label19.TabIndex = 39;
			this.label19.Text = "～";
			// 
			// buttonAPLUS送信ファイル出力フォルダ
			// 
			this.buttonAPLUS送信ファイル出力フォルダ.Location = new System.Drawing.Point(1077, 79);
			this.buttonAPLUS送信ファイル出力フォルダ.Name = "buttonAPLUS送信ファイル出力フォルダ";
			this.buttonAPLUS送信ファイル出力フォルダ.Size = new System.Drawing.Size(26, 23);
			this.buttonAPLUS送信ファイル出力フォルダ.TabIndex = 14;
			this.buttonAPLUS送信ファイル出力フォルダ.Text = "▼";
			this.buttonAPLUS送信ファイル出力フォルダ.UseVisualStyleBackColor = true;
			this.buttonAPLUS送信ファイル出力フォルダ.Click += new System.EventHandler(this.buttonAPLUS送信ファイル出力フォルダ_Click);
			// 
			// buttonWEB請求書ファイル出力フォルダ
			// 
			this.buttonWEB請求書ファイル出力フォルダ.Location = new System.Drawing.Point(1077, 332);
			this.buttonWEB請求書ファイル出力フォルダ.Name = "buttonWEB請求書ファイル出力フォルダ";
			this.buttonWEB請求書ファイル出力フォルダ.Size = new System.Drawing.Size(26, 23);
			this.buttonWEB請求書ファイル出力フォルダ.TabIndex = 45;
			this.buttonWEB請求書ファイル出力フォルダ.Text = "▼";
			this.buttonWEB請求書ファイル出力フォルダ.UseVisualStyleBackColor = true;
			this.buttonWEB請求書ファイル出力フォルダ.Click += new System.EventHandler(this.buttonWEB請求書ファイル出力フォルダ_Click);
			// 
			// buttonAGREX口振通知書ファイル出力フォルダ
			// 
			this.buttonAGREX口振通知書ファイル出力フォルダ.Location = new System.Drawing.Point(1077, 442);
			this.buttonAGREX口振通知書ファイル出力フォルダ.Name = "buttonAGREX口振通知書ファイル出力フォルダ";
			this.buttonAGREX口振通知書ファイル出力フォルダ.Size = new System.Drawing.Size(26, 23);
			this.buttonAGREX口振通知書ファイル出力フォルダ.TabIndex = 56;
			this.buttonAGREX口振通知書ファイル出力フォルダ.Text = "▼";
			this.buttonAGREX口振通知書ファイル出力フォルダ.UseVisualStyleBackColor = true;
			this.buttonAGREX口振通知書ファイル出力フォルダ.Click += new System.EventHandler(this.buttonAGREX口振通知書ファイル出力フォルダ_Click);
			// 
			// textBoxPCA請求一覧10読込みファイル
			// 
			this.textBoxPCA請求一覧10読込みファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPCA請求一覧10読込みファイル.Location = new System.Drawing.Point(606, 57);
			this.textBoxPCA請求一覧10読込みファイル.Name = "textBoxPCA請求一覧10読込みファイル";
			this.textBoxPCA請求一覧10読込みファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxPCA請求一覧10読込みファイル.TabIndex = 11;
			// 
			// textBoxAPLUS送信ファイル
			// 
			this.textBoxAPLUS送信ファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxAPLUS送信ファイル.Location = new System.Drawing.Point(606, 101);
			this.textBoxAPLUS送信ファイル.Name = "textBoxAPLUS送信ファイル";
			this.textBoxAPLUS送信ファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxAPLUS送信ファイル.TabIndex = 16;
			// 
			// textBoxWEB請求書番号基数
			// 
			this.textBoxWEB請求書番号基数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxWEB請求書番号基数.Location = new System.Drawing.Point(606, 244);
			this.textBoxWEB請求書番号基数.Name = "textBoxWEB請求書番号基数";
			this.textBoxWEB請求書番号基数.Size = new System.Drawing.Size(180, 23);
			this.textBoxWEB請求書番号基数.TabIndex = 34;
			// 
			// textBoxPCA請求明細10読込みファイル
			// 
			this.textBoxPCA請求明細10読込みファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxPCA請求明細10読込みファイル.Location = new System.Drawing.Point(606, 310);
			this.textBoxPCA請求明細10読込みファイル.Name = "textBoxPCA請求明細10読込みファイル";
			this.textBoxPCA請求明細10読込みファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxPCA請求明細10読込みファイル.TabIndex = 42;
			// 
			// textBoxWEB請求書ヘッダファイル
			// 
			this.textBoxWEB請求書ヘッダファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxWEB請求書ヘッダファイル.Location = new System.Drawing.Point(606, 354);
			this.textBoxWEB請求書ヘッダファイル.Name = "textBoxWEB請求書ヘッダファイル";
			this.textBoxWEB請求書ヘッダファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxWEB請求書ヘッダファイル.TabIndex = 47;
			// 
			// textBoxWEB請求書明細売上行ファイル
			// 
			this.textBoxWEB請求書明細売上行ファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxWEB請求書明細売上行ファイル.Location = new System.Drawing.Point(606, 376);
			this.textBoxWEB請求書明細売上行ファイル.Name = "textBoxWEB請求書明細売上行ファイル";
			this.textBoxWEB請求書明細売上行ファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxWEB請求書明細売上行ファイル.TabIndex = 49;
			// 
			// textBoxWEB請求書明細消費税行ファイル
			// 
			this.textBoxWEB請求書明細消費税行ファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxWEB請求書明細消費税行ファイル.Location = new System.Drawing.Point(606, 398);
			this.textBoxWEB請求書明細消費税行ファイル.Name = "textBoxWEB請求書明細消費税行ファイル";
			this.textBoxWEB請求書明細消費税行ファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxWEB請求書明細消費税行ファイル.TabIndex = 51;
			// 
			// textBoxWEB請求書明細記事行ファイル
			// 
			this.textBoxWEB請求書明細記事行ファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxWEB請求書明細記事行ファイル.Location = new System.Drawing.Point(606, 420);
			this.textBoxWEB請求書明細記事行ファイル.Name = "textBoxWEB請求書明細記事行ファイル";
			this.textBoxWEB請求書明細記事行ファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxWEB請求書明細記事行ファイル.TabIndex = 53;
			// 
			// textBoxAGREX口振通知書ファイル
			// 
			this.textBoxAGREX口振通知書ファイル.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxAGREX口振通知書ファイル.Location = new System.Drawing.Point(606, 464);
			this.textBoxAGREX口振通知書ファイル.Name = "textBoxAGREX口振通知書ファイル";
			this.textBoxAGREX口振通知書ファイル.Size = new System.Drawing.Size(180, 23);
			this.textBoxAGREX口振通知書ファイル.TabIndex = 58;
			// 
			// AccountTransferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1142, 584);
			this.Controls.Add(this.textBoxAGREX口振通知書ファイル);
			this.Controls.Add(this.textBoxWEB請求書明細記事行ファイル);
			this.Controls.Add(this.textBoxWEB請求書明細消費税行ファイル);
			this.Controls.Add(this.textBoxWEB請求書明細売上行ファイル);
			this.Controls.Add(this.textBoxWEB請求書ヘッダファイル);
			this.Controls.Add(this.textBoxPCA請求明細10読込みファイル);
			this.Controls.Add(this.textBoxWEB請求書番号基数);
			this.Controls.Add(this.textBoxAPLUS送信ファイル);
			this.Controls.Add(this.textBoxPCA請求一覧10読込みファイル);
			this.Controls.Add(this.buttonAGREX口振通知書ファイル出力フォルダ);
			this.Controls.Add(this.buttonWEB請求書ファイル出力フォルダ);
			this.Controls.Add(this.buttonAPLUS送信ファイル出力フォルダ);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.dateTimePicker口座振替請求期間終了日);
			this.Controls.Add(this.dateTimePicker口座振替請求期間開始日);
			this.Controls.Add(this.dateTimePicker口座振替請求日);
			this.Controls.Add(this.dateTimePicker口座振替日);
			this.Controls.Add(this.labelWEB請求書請求金額);
			this.Controls.Add(this.labelAGREX口振通知書件数);
			this.Controls.Add(this.labelAGREX口振通知書ファイル出力フォルダ);
			this.Controls.Add(this.label請求金額あり件数);
			this.Controls.Add(this.label口振請求なし件数);
			this.Controls.Add(this.labelWEB請求書件数);
			this.Controls.Add(this.labelWEB請求書ファイル出力フォルダ);
			this.Controls.Add(this.labelAPLUS送信ファイル出力フォルダ);
			this.Controls.Add(this.label口座振替請求金額);
			this.Controls.Add(this.label口座振替不要請求額);
			this.Controls.Add(this.label口座振替不可請求額);
			this.Controls.Add(this.label口座振替請求一覧請求金額);
			this.Controls.Add(this.label口座振替請求件数);
			this.Controls.Add(this.label口座振替不要件数);
			this.Controls.Add(this.label口座振替不可件数);
			this.Controls.Add(this.label口座振替請求一覧件数);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label31);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label27);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label52);
			this.Controls.Add(this.label45);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonMakeInvoice);
			this.Controls.Add(this.buttonMakeAccountTransfer);
			this.Controls.Add(this.buttonReadInvoiceDetailData);
			this.Controls.Add(this.buttonReadInvoiceHeaderData);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonReadCustomerInfo);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AccountTransferForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "口座振替データ作成";
			this.Load += new System.EventHandler(this.AccountTransferForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonReadCustomerInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonReadInvoiceHeaderData;
		private System.Windows.Forms.Button buttonReadInvoiceDetailData;
		private System.Windows.Forms.Button buttonMakeAccountTransfer;
		private System.Windows.Forms.Button buttonMakeInvoice;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label口座振替請求一覧件数;
		private System.Windows.Forms.Label label口座振替不可件数;
		private System.Windows.Forms.Label label口座振替不要件数;
		private System.Windows.Forms.Label label口座振替請求件数;
		private System.Windows.Forms.Label label口座振替請求一覧請求金額;
		private System.Windows.Forms.Label label口座振替不可請求額;
		private System.Windows.Forms.Label label口座振替不要請求額;
		private System.Windows.Forms.Label label口座振替請求金額;
		private System.Windows.Forms.Label labelAPLUS送信ファイル出力フォルダ;
		private System.Windows.Forms.Label labelWEB請求書ファイル出力フォルダ;
		private System.Windows.Forms.Label labelAGREX口振通知書ファイル出力フォルダ;
		private System.Windows.Forms.Label label請求金額あり件数;
		private System.Windows.Forms.Label label口振請求なし件数;
		private System.Windows.Forms.Label labelWEB請求書件数;
		private System.Windows.Forms.Label labelAGREX口振通知書件数;
		private System.Windows.Forms.Label labelWEB請求書請求金額;
		private System.Windows.Forms.DateTimePicker dateTimePicker口座振替日;
		private System.Windows.Forms.DateTimePicker dateTimePicker口座振替請求日;
		private System.Windows.Forms.DateTimePicker dateTimePicker口座振替請求期間開始日;
		private System.Windows.Forms.DateTimePicker dateTimePicker口座振替請求期間終了日;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Button buttonAPLUS送信ファイル出力フォルダ;
		private System.Windows.Forms.Button buttonWEB請求書ファイル出力フォルダ;
		private System.Windows.Forms.Button buttonAGREX口振通知書ファイル出力フォルダ;
		private System.Windows.Forms.TextBox textBoxPCA請求一覧10読込みファイル;
		private System.Windows.Forms.TextBox textBoxAPLUS送信ファイル;
		private MwsLib.Component.NumericTextBox textBoxWEB請求書番号基数;
		private System.Windows.Forms.TextBox textBoxPCA請求明細10読込みファイル;
		private System.Windows.Forms.TextBox textBoxWEB請求書ヘッダファイル;
		private System.Windows.Forms.TextBox textBoxWEB請求書明細売上行ファイル;
		private System.Windows.Forms.TextBox textBoxWEB請求書明細消費税行ファイル;
		private System.Windows.Forms.TextBox textBoxWEB請求書明細記事行ファイル;
		private System.Windows.Forms.TextBox textBoxAGREX口振通知書ファイル;
	}
}