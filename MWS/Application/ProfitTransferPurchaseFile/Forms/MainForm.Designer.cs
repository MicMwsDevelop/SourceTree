namespace ProfitTransferPurchaseFile
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
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this.textBoxPcaVersion = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelversion = new System.Windows.Forms.Label();
			this.buttonExit = new System.Windows.Forms.Button();
			this.buttonStart = new System.Windows.Forms.Button();
			this.dateTimePickerTarget = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxProfitTransferPurchaseFilename = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxPcaVersion
			// 
			this.textBoxPcaVersion.BackColor = System.Drawing.Color.White;
			this.textBoxPcaVersion.Location = new System.Drawing.Point(695, 170);
			this.textBoxPcaVersion.Name = "textBoxPcaVersion";
			this.textBoxPcaVersion.ReadOnly = true;
			this.textBoxPcaVersion.Size = new System.Drawing.Size(37, 27);
			this.textBoxPcaVersion.TabIndex = 42;
			this.textBoxPcaVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(565, 173);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124, 19);
			this.label2.TabIndex = 41;
			this.label2.Text = "PCAバージョン番号";
			// 
			// textBoxOutputFolder
			// 
			this.textBoxOutputFolder.BackColor = System.Drawing.Color.White;
			this.textBoxOutputFolder.Location = new System.Drawing.Point(124, 59);
			this.textBoxOutputFolder.Name = "textBoxOutputFolder";
			this.textBoxOutputFolder.ReadOnly = true;
			this.textBoxOutputFolder.Size = new System.Drawing.Size(348, 27);
			this.textBoxOutputFolder.TabIndex = 38;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 19);
			this.label3.TabIndex = 37;
			this.label3.Text = "出力先フォルダ";
			// 
			// labelversion
			// 
			this.labelversion.Location = new System.Drawing.Point(473, 12);
			this.labelversion.Name = "labelversion";
			this.labelversion.Size = new System.Drawing.Size(259, 26);
			this.labelversion.TabIndex = 36;
			this.labelversion.Text = "Ver?.??(????/????/????)";
			this.labelversion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(610, 217);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(122, 49);
			this.buttonExit.TabIndex = 43;
			this.buttonExit.Text = "終了";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(500, 59);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(232, 107);
			this.buttonStart.TabIndex = 40;
			this.buttonStart.Text = "START";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// dateTimePickerTarget
			// 
			this.dateTimePickerTarget.CustomFormat = "yyyy年MM月";
			this.dateTimePickerTarget.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerTarget.Location = new System.Drawing.Point(96, 12);
			this.dateTimePickerTarget.Name = "dateTimePickerTarget";
			this.dateTimePickerTarget.Size = new System.Drawing.Size(160, 27);
			this.dateTimePickerTarget.TabIndex = 35;
			this.dateTimePickerTarget.DropDown += new System.EventHandler(this.dateTimePickerTarget_DropDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 16);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 19);
			this.label1.TabIndex = 34;
			this.label1.Text = "対象年月";
			// 
			// textBoxProfitTransferPurchaseFilename
			// 
			this.textBoxProfitTransferPurchaseFilename.BackColor = System.Drawing.Color.White;
			this.textBoxProfitTransferPurchaseFilename.Location = new System.Drawing.Point(21, 139);
			this.textBoxProfitTransferPurchaseFilename.Name = "textBoxProfitTransferPurchaseFilename";
			this.textBoxProfitTransferPurchaseFilename.ReadOnly = true;
			this.textBoxProfitTransferPurchaseFilename.Size = new System.Drawing.Size(447, 27);
			this.textBoxProfitTransferPurchaseFilename.TabIndex = 45;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(21, 117);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(259, 19);
			this.label5.TabIndex = 44;
			this.label5.Text = "部署間利益付け替え仕入データファイル名";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(756, 284);
			this.Controls.Add(this.textBoxProfitTransferPurchaseFilename);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxPcaVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxOutputFolder);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelversion);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.dateTimePickerTarget);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "部署間利益付け替え仕入データ作成";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxPcaVersion;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxOutputFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelversion;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.DateTimePicker dateTimePickerTarget;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxProfitTransferPurchaseFilename;
		private System.Windows.Forms.Label label5;
	}
}

