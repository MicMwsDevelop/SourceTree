﻿
namespace WonderEstimateExcel
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
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.buttonEstimate = new System.Windows.Forms.Button();
			this.pictureBoxDropZone = new System.Windows.Forms.PictureBox();
			this.labelFilename = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOrderSheet = new System.Windows.Forms.Button();
			this.buttonOrderConfirm = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDropZone)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "■見積書CSVファイル";
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(410, 62);
			this.buttonSelectFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(156, 48);
			this.buttonSelectFile.TabIndex = 3;
			this.buttonSelectFile.Text = "ファイル追加";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// buttonEstimate
			// 
			this.buttonEstimate.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonEstimate.Location = new System.Drawing.Point(410, 118);
			this.buttonEstimate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonEstimate.Name = "buttonEstimate";
			this.buttonEstimate.Size = new System.Drawing.Size(156, 48);
			this.buttonEstimate.TabIndex = 4;
			this.buttonEstimate.Text = "見積書出力";
			this.buttonEstimate.UseVisualStyleBackColor = true;
			this.buttonEstimate.Click += new System.EventHandler(this.buttonEstimate_Click);
			// 
			// pictureBoxDropZone
			// 
			this.pictureBoxDropZone.BackColor = System.Drawing.Color.White;
			this.pictureBoxDropZone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxDropZone.ErrorImage = null;
			this.pictureBoxDropZone.Image = global::WonderEstimateExcel.Properties.Resources.DropMassage;
			this.pictureBoxDropZone.Location = new System.Drawing.Point(12, 62);
			this.pictureBoxDropZone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBoxDropZone.Name = "pictureBoxDropZone";
			this.pictureBoxDropZone.Size = new System.Drawing.Size(392, 217);
			this.pictureBoxDropZone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxDropZone.TabIndex = 4;
			this.pictureBoxDropZone.TabStop = false;
			this.pictureBoxDropZone.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxDropZone_DragDrop);
			this.pictureBoxDropZone.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxDropZone_DragEnter);
			// 
			// labelFilename
			// 
			this.labelFilename.BackColor = System.Drawing.Color.White;
			this.labelFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelFilename.Location = new System.Drawing.Point(12, 25);
			this.labelFilename.Name = "labelFilename";
			this.labelFilename.Size = new System.Drawing.Size(554, 30);
			this.labelFilename.TabIndex = 1;
			this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(10, 284);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(347, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "※WonderWeb見積書CSVファイルを指定してください。";
			// 
			// buttonOrderSheet
			// 
			this.buttonOrderSheet.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOrderSheet.Location = new System.Drawing.Point(410, 174);
			this.buttonOrderSheet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOrderSheet.Name = "buttonOrderSheet";
			this.buttonOrderSheet.Size = new System.Drawing.Size(156, 48);
			this.buttonOrderSheet.TabIndex = 5;
			this.buttonOrderSheet.Text = "注文書出力";
			this.buttonOrderSheet.UseVisualStyleBackColor = true;
			this.buttonOrderSheet.Click += new System.EventHandler(this.buttonOrderSheet_Click);
			// 
			// buttonOrderConfirm
			// 
			this.buttonOrderConfirm.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOrderConfirm.Location = new System.Drawing.Point(410, 231);
			this.buttonOrderConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOrderConfirm.Name = "buttonOrderConfirm";
			this.buttonOrderConfirm.Size = new System.Drawing.Size(156, 48);
			this.buttonOrderConfirm.TabIndex = 6;
			this.buttonOrderConfirm.Text = "注文請書出力";
			this.buttonOrderConfirm.UseVisualStyleBackColor = true;
			this.buttonOrderConfirm.Click += new System.EventHandler(this.buttonOrderConfirm_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(581, 317);
			this.Controls.Add(this.buttonOrderConfirm);
			this.Controls.Add(this.buttonOrderSheet);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelFilename);
			this.Controls.Add(this.pictureBoxDropZone);
			this.Controls.Add(this.buttonEstimate);
			this.Controls.Add(this.buttonSelectFile);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "WonderWeb見積書 Excel出力（Ver1.05 2021/06/11）";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDropZone)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSelectFile;
		private System.Windows.Forms.Button buttonEstimate;
		private System.Windows.Forms.PictureBox pictureBoxDropZone;
		private System.Windows.Forms.Label labelFilename;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOrderSheet;
		private System.Windows.Forms.Button buttonOrderConfirm;
	}
}

