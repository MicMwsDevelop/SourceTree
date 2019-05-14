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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ScanImageData.Forms
{
	public partial class DisplayScanImageForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private DisplayScanImageForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filename"></param>
		public DisplayScanImageForm(string filename)
		{
			InitializeComponent();

			textBoxFilename.Text = filename;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
