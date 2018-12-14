//
// DisplayScanImageForm.cs
// 
// スキャナーイメージの表示画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ScanImageData.Forms
{
	public partial class DisplayScanImageForm : Form
	{
		//public string ScanImagePathname { get; set; }

		private DisplayScanImageForm()
		{
			InitializeComponent();
		}

		public DisplayScanImageForm(string filename)
		{
			InitializeComponent();

			textBoxFilename.Text = filename;
		}

		private void DisplayScanImageForm_Load(object sender, EventArgs e)
		{
			string ext = Path.GetExtension(textBoxFilename.Text);
			if (".PDF" == ext.ToUpper())
			{
				axAcroPDF.Visible = true;
				axAcroPDF.LoadFile(textBoxFilename.Text);
			}
			else
			{
				pictureBox.Visible = true;
				pictureBox.Image = Image.FromFile(textBoxFilename.Text);
			}
		}
	}
}
