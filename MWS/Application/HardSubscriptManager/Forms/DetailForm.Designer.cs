﻿namespace HardSubscriptManager.Forms
{
	partial class DetailForm
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
			this.buttonSearch = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.labelGoodsName = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxCategory = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxAssetsCode = new System.Windows.Forms.TextBox();
			this.textBoxSerialNo = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.numericTextBoxQuantity = new MwsLib.Component.NumericTextBox();
			this.numericTextBoxGoodsCode = new MwsLib.Component.NumericTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Moccasin;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(13, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "商品コード ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(205, 9);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(63, 25);
			this.buttonSearch.TabIndex = 2;
			this.buttonSearch.Text = "検索";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Moccasin;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(13, 35);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "機器名 ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelGoodsName
			// 
			this.labelGoodsName.BackColor = System.Drawing.Color.White;
			this.labelGoodsName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelGoodsName.Location = new System.Drawing.Point(113, 35);
			this.labelGoodsName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelGoodsName.Name = "labelGoodsName";
			this.labelGoodsName.Size = new System.Drawing.Size(602, 25);
			this.labelGoodsName.TabIndex = 4;
			this.labelGoodsName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Moccasin;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(13, 61);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 25);
			this.label3.TabIndex = 5;
			this.label3.Text = "機器区分 ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxCategory
			// 
			this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCategory.FormattingEnabled = true;
			this.comboBoxCategory.Location = new System.Drawing.Point(113, 65);
			this.comboBoxCategory.Name = "comboBoxCategory";
			this.comboBoxCategory.Size = new System.Drawing.Size(121, 21);
			this.comboBoxCategory.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Moccasin;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(13, 87);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 25);
			this.label4.TabIndex = 7;
			this.label4.Text = "数量 ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Moccasin;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(13, 139);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 25);
			this.label5.TabIndex = 11;
			this.label5.Text = "資産コード ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Moccasin;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(13, 113);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(99, 25);
			this.label6.TabIndex = 9;
			this.label6.Text = "シリアルNo ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxAssetsCode
			// 
			this.textBoxAssetsCode.Location = new System.Drawing.Point(113, 144);
			this.textBoxAssetsCode.Name = "textBoxAssetsCode";
			this.textBoxAssetsCode.Size = new System.Drawing.Size(121, 20);
			this.textBoxAssetsCode.TabIndex = 12;
			// 
			// textBoxSerialNo
			// 
			this.textBoxSerialNo.Location = new System.Drawing.Point(113, 118);
			this.textBoxSerialNo.Name = "textBoxSerialNo";
			this.textBoxSerialNo.Size = new System.Drawing.Size(270, 20);
			this.textBoxSerialNo.TabIndex = 10;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(503, 133);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(102, 31);
			this.buttonOK.TabIndex = 13;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(613, 133);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(102, 31);
			this.buttonCancel.TabIndex = 14;
			this.buttonCancel.Text = "キャンセル";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// numericTextBoxQuantity
			// 
			this.numericTextBoxQuantity.Location = new System.Drawing.Point(113, 92);
			this.numericTextBoxQuantity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxQuantity.MaxLength = 2;
			this.numericTextBoxQuantity.Name = "numericTextBoxQuantity";
			this.numericTextBoxQuantity.Size = new System.Drawing.Size(50, 20);
			this.numericTextBoxQuantity.TabIndex = 8;
			// 
			// numericTextBoxGoodsCode
			// 
			this.numericTextBoxGoodsCode.Location = new System.Drawing.Point(113, 14);
			this.numericTextBoxGoodsCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericTextBoxGoodsCode.MaxLength = 13;
			this.numericTextBoxGoodsCode.Name = "numericTextBoxGoodsCode";
			this.numericTextBoxGoodsCode.Size = new System.Drawing.Size(93, 20);
			this.numericTextBoxGoodsCode.TabIndex = 1;
			// 
			// DetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(730, 172);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.textBoxSerialNo);
			this.Controls.Add(this.textBoxAssetsCode);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericTextBoxQuantity);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBoxCategory);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelGoodsName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.numericTextBoxGoodsCode);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DetailForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "機器情報の入力";
			this.Load += new System.EventHandler(this.DetailForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSearch;
		private MwsLib.Component.NumericTextBox numericTextBoxGoodsCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelGoodsName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxCategory;
		private System.Windows.Forms.Label label4;
		private MwsLib.Component.NumericTextBox numericTextBoxQuantity;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxAssetsCode;
		private System.Windows.Forms.TextBox textBoxSerialNo;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
	}
}