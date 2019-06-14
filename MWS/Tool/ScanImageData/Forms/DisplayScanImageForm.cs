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
using System.Text;
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
			string ext = Path.GetExtension(textBoxFilename.Text).ToUpper();
			if (".PDF" == ext)
			{
				axAcroPDF.Visible = true;
				axAcroPDF.LoadFile(textBoxFilename.Text);
			}
			else if (".TXT" == ext)
			{
				textBoxTextLine.Visible = true;
				try
				{
					// ファイルを開く
					using (var sr = new StreamReader(textBoxFilename.Text, Encoding.GetEncoding("Shift_JIS")))
					{
						// ストリームの末尾まで繰り返す
						int i = 0;
						while (!sr.EndOfStream)
						{
							// ファイルから一行読み込む
							var line = sr.ReadLine();
							if (0 < textBoxTextLine.Text.Length)
							{
								textBoxTextLine.Text += "\r\n" + line;
							}
							else
							{
								textBoxTextLine.Text = line;
							}
							i++;
						}
					}
				}
				catch (System.Exception ex)
				{
					// ファイルを開くのに失敗したとき
					MessageBox.Show(string.Format("ファイルオープンエラー({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}			}
			else
			{
				pictureBox.Visible = true;
				pictureBox.Image = Image.FromFile(textBoxFilename.Text);
			}
		}
	}
}
