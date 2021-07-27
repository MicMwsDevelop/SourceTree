//
// DisplayScanImageForm.cs
// 
// 得意先番号登録画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
//
using MwsLib.BaseFactory.ScanImageManager;
using MwsLib.DB.SqlServer.ScanImageManager;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ScanImageManager.Forms
{
	public partial class DisplayScanImageForm : Form
	{
		/// <summary>
		/// スキャナーファイル
		/// </summary>
		public ScanImageFile ImageFile { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DisplayScanImageForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DisplayScanImageForm_Load(object sender, EventArgs e)
		{
			labelFilename.Text = ImageFile.FileName;

			if (0 < ImageFile.TokuisakiNo.Length)
			{
				textBoxTokuisaki.Text = ImageFile.TokuisakiNo;
			}
			else
			{
				textBoxTokuisaki.Text = ImageFile.GetToluisakiNo();
			}
			string ext = Path.GetExtension(ImageFile.FileName).ToUpper();
			if (".PDF" == ext)
			{
				// PDFファイル
				axAcroPDF_Image.Visible = true;
				axAcroPDF_Image.LoadFile(ImageFile.Pathname);
			}
			else if (".TXT" == ext)
			{
				// テキストファイル
				textBoxTextLine.Visible = true;
				try
				{
					using (var sr = new StreamReader(ImageFile.Pathname, Encoding.GetEncoding("Shift_JIS")))
					{
						while (!sr.EndOfStream)
						{
							string line = sr.ReadLine();
							if (0 < textBoxTextLine.Text.Length)
							{
								textBoxTextLine.Text += "\r\n" + line;
							}
							else
							{
								textBoxTextLine.Text = line;
							}
						}
					}
				}
				catch (Exception ex)
				{
					// ファイルを開くのに失敗したとき
					MessageBox.Show(string.Format("ファイルオープンエラー({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			else
			{
				// tiffなど他画像ファイル
				pictureBoxImage.Visible = true;
				pictureBoxImage.Image = Image.FromFile(ImageFile.Pathname);
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (6 == textBoxTokuisaki.Text.Length)
			{
				ScanImageFile scan = ScanImageManagerAccess.GetCustomerInfo(textBoxTokuisaki.Text, Program.DATABACE_ACCEPT_CT);
				if (null != scan)
				{
					ImageFile.ClinicName = scan.ClinicName;
					ImageFile.CustomerNo = scan.CustomerNo;
					ImageFile.TokuisakiNo = scan.TokuisakiNo;
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageBox.Show("得意先番号に対する顧客情報がありません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MessageBox.Show("得意先番号を正しく入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
