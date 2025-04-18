﻿namespace MwsServiceCancelTool.Forms
{
	partial class MainForm
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
			this.buttonSearch = new System.Windows.Forms.Button();
			this.labelCustomerName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonMatomeCancel = new System.Windows.Forms.Button();
			this.buttonPcSupportCancel = new System.Windows.Forms.Button();
			this.buttonOnlineDemandCancel = new System.Windows.Forms.Button();
			this.buttonSetServiceCancel = new System.Windows.Forms.Button();
			this.buttonPcSupportEnd = new System.Windows.Forms.Button();
			this.textBoxCustomerNo = new MwsLib.Component.NumericTextBox();
			this.SuspendLayout();
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(223, 8);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(57, 19);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// labelCustomerName
			// 
			this.labelCustomerName.BackColor = System.Drawing.Color.White;
			this.labelCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelCustomerName.Location = new System.Drawing.Point(72, 35);
			this.labelCustomerName.Name = "labelCustomerName";
			this.labelCustomerName.Size = new System.Drawing.Size(450, 20);
			this.labelCustomerName.TabIndex = 4;
			this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 11);
			this.label2.TabIndex = 3;
			this.label2.Text = "■顧客名";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 11);
			this.label1.TabIndex = 0;
			this.label1.Text = "■顧客No";
			// 
			// buttonMatomeCancel
			// 
			this.buttonMatomeCancel.Location = new System.Drawing.Point(14, 68);
			this.buttonMatomeCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonMatomeCancel.Name = "buttonMatomeCancel";
			this.buttonMatomeCancel.Size = new System.Drawing.Size(508, 34);
			this.buttonMatomeCancel.TabIndex = 5;
			this.buttonMatomeCancel.Text = "おまとめプラン 利用申込取消";
			this.buttonMatomeCancel.UseVisualStyleBackColor = true;
			this.buttonMatomeCancel.Click += new System.EventHandler(this.buttonMatomeCancel_Click);
			// 
			// buttonPcSupportCancel
			// 
			this.buttonPcSupportCancel.Location = new System.Drawing.Point(14, 107);
			this.buttonPcSupportCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonPcSupportCancel.Name = "buttonPcSupportCancel";
			this.buttonPcSupportCancel.Size = new System.Drawing.Size(508, 34);
			this.buttonPcSupportCancel.TabIndex = 6;
			this.buttonPcSupportCancel.Text = "PC安心サポート 利用申込取消";
			this.buttonPcSupportCancel.UseVisualStyleBackColor = true;
			this.buttonPcSupportCancel.Click += new System.EventHandler(this.buttonPcSupportCancel_Click);
			// 
			// buttonOnlineDemandCancel
			// 
			this.buttonOnlineDemandCancel.Location = new System.Drawing.Point(14, 186);
			this.buttonOnlineDemandCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonOnlineDemandCancel.Name = "buttonOnlineDemandCancel";
			this.buttonOnlineDemandCancel.Size = new System.Drawing.Size(508, 34);
			this.buttonOnlineDemandCancel.TabIndex = 8;
			this.buttonOnlineDemandCancel.Text = "各種作業料 作業済申請取消";
			this.buttonOnlineDemandCancel.UseVisualStyleBackColor = true;
			this.buttonOnlineDemandCancel.Click += new System.EventHandler(this.buttonOnlineDemandCancel_Click);
			// 
			// buttonSetServiceCancel
			// 
			this.buttonSetServiceCancel.Location = new System.Drawing.Point(16, 225);
			this.buttonSetServiceCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonSetServiceCancel.Name = "buttonSetServiceCancel";
			this.buttonSetServiceCancel.Size = new System.Drawing.Size(506, 34);
			this.buttonSetServiceCancel.TabIndex = 9;
			this.buttonSetServiceCancel.Text = "セット割サービス 利用申込取消";
			this.buttonSetServiceCancel.UseVisualStyleBackColor = true;
			this.buttonSetServiceCancel.Click += new System.EventHandler(this.buttonSetServiceCancel_Click);
			// 
			// buttonPcSupportEnd
			// 
			this.buttonPcSupportEnd.Location = new System.Drawing.Point(14, 146);
			this.buttonPcSupportEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.buttonPcSupportEnd.Name = "buttonPcSupportEnd";
			this.buttonPcSupportEnd.Size = new System.Drawing.Size(508, 34);
			this.buttonPcSupportEnd.TabIndex = 7;
			this.buttonPcSupportEnd.Text = "PC安心サポート 自動更新後の終了処理";
			this.buttonPcSupportEnd.UseVisualStyleBackColor = true;
			this.buttonPcSupportEnd.Click += new System.EventHandler(this.buttonPcSupportEnd_Click);
			// 
			// textBoxCustomerNo
			// 
			this.textBoxCustomerNo.Location = new System.Drawing.Point(72, 8);
			this.textBoxCustomerNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.textBoxCustomerNo.MaxLength = 8;
			this.textBoxCustomerNo.Name = "textBoxCustomerNo";
			this.textBoxCustomerNo.Size = new System.Drawing.Size(145, 19);
			this.textBoxCustomerNo.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 269);
			this.Controls.Add(this.buttonPcSupportEnd);
			this.Controls.Add(this.buttonSetServiceCancel);
			this.Controls.Add(this.buttonOnlineDemandCancel);
			this.Controls.Add(this.buttonPcSupportCancel);
			this.Controls.Add(this.buttonMatomeCancel);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.labelCustomerName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxCustomerNo);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MWSサービス利用申込取消ツール";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.Label labelCustomerName;
		private System.Windows.Forms.Label label2;
		private MwsLib.Component.NumericTextBox textBoxCustomerNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonMatomeCancel;
		private System.Windows.Forms.Button buttonPcSupportCancel;
		private System.Windows.Forms.Button buttonOnlineDemandCancel;
		private System.Windows.Forms.Button buttonSetServiceCancel;
		private System.Windows.Forms.Button buttonPcSupportEnd;
	}
}