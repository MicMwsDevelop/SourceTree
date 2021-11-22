//
// EnvironmentForm.cs
// 
// 環境設定画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
//
using CommonDialog.PrintPreview;
using DeliveryCenterPrintNouhin.Print;
using DeliveryCenterPrintNouhin.Settings;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace DeliveryCenterPrintNouhin.Forms
{
	public partial class EnvironmentForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public DeliveryCenterPrintNouhinSettings Settings { get; set; }

		/// <summary>
		/// 印刷設定保持用
		/// </summary>
		private PrintDocument PrintDocument { get; set; }

		/// <summary>
		/// 納品書印刷制御クラス
		/// </summary>
		private PrintNouhinControl PrintControl;

		/// <summary>
		/// 印刷 総ページ数
		/// </summary>
		private int MaxPage { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EnvironmentForm()
		{
			InitializeComponent();

			PrintDocument = new PrintDocument();
			PrintControl = new PrintNouhinControl();
			MaxPage = 1;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EnvironmentForm_Load(object sender, EventArgs e)
		{
			textBoxNouhinDir.Text = Settings.NouhinDir;
			textBoxNouhinFile.Text = Settings.NouhinFile;
			numericUpDownOffsetX.Value = Settings.PaperOffset.X;
			numericUpDownOffsetY.Value = Settings.PaperOffset.Y;
			textBoxParamFile.Text = Settings.ParamFile;

#if DEBUG
			checkBoxPrintRectangle.Visible = true;
			checkBoxPrintPreview.Visible = true;
			if (0 != Settings.PrintRectangle)
			{
				checkBoxPrintRectangle.Checked = true;
			}
			if (0 != Settings.PrintPreview)
			{
				checkBoxPrintPreview.Checked = true;
			}
#endif
		}

		/// <summary>
		/// 納品書ファイルフォルダの設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputNouhinDir_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dlg = new FolderBrowserDialog())
			{
				dlg.Description = "納品書ファイルフォルダを指定してください。";
				dlg.SelectedPath = textBoxNouhinDir.Text.Trim();
				dlg.ShowNewFolderButton = false;
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxNouhinDir.Text = dlg.SelectedPath;
				}
			}
		}

		/// <summary>
		/// 左 GotFocus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownOffsetX_Enter(object sender, EventArgs e)
		{
			// イメージの差し替え
			pictureBoxOffsetX.Visible = true;
			pictureBoxOffsetY.Visible = false;
		}

		/// <summary>
		/// 上 GotFocus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownOffsetY_Enter(object sender, EventArgs e)
		{
			// イメージの差し替え
			pictureBoxOffsetX.Visible = false;
			pictureBoxOffsetY.Visible = true;
		}

		/// <summary>
		/// テスト印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrintTest_Click(object sender, EventArgs e)
		{
			if (0 == textBoxParamFile.Text.Trim().Length)
			{
				MessageBox.Show("パラメタファイル名を正しく指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == File.Exists(textBoxParamFile.Text.Trim()))
			{
				MessageBox.Show("パラメタファイル名が存在しません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			NouhinData test = NouhinData.GetTestData();
			string msg;
			if (-1 != PrintControl.ReadParameterFile(Directory.GetCurrentDirectory(), textBoxParamFile.Text.Trim(), out msg))
			{
				// 印刷プレビューダイアログ生成
				using (PrintPreviewForm pf = new PrintPreviewForm())
				{
					// 印刷処理開始イベントハンドラの追加
					pf.BeginPrint += new PrintPreviewForm.PrintEventHandler(PrintPreviewForm_BeginPrint);
					// ページ枚の印刷処理イベントハンドラの追加
					pf.PrintPage += new PrintPreviewForm.PrintPageEventHandler(PrintPreviewForm_PrintPage);
					// 印刷終了イベントハンドラの追加
					pf.EndPrint += new PrintPreviewForm.PrintEventHandler(PrintPreviewForm_EndPrint);
					// 印刷ドキュメント                        
					pf.Document = PrintDocument;
					// ページ数
					pf.MaxPage = MaxPage;
					// 表示を最大化
					pf.WindowState = FormWindowState.Maximized;

					// 印刷
					if (pf.Document.PrinterSettings.IsValid)
					{
						pf.Print();
					}
					else
					{
						MessageBox.Show("有効なプリンタが指定されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			else
			{
				MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == textBoxNouhinDir.Text.Trim().Length)
			{
				MessageBox.Show("納品書ファイルフォルダを正しく指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == textBoxNouhinFile.Text.Trim().Length)
			{
				MessageBox.Show("納品書ファイル名を正しく指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == textBoxParamFile.Text.Trim().Length)
			{
				MessageBox.Show("パラメタファイル名を正しく指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == File.Exists(textBoxParamFile.Text.Trim()))
			{
				MessageBox.Show("パラメタファイル名が存在しません。パラメタファイル名を正しく指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Settings.NouhinDir = textBoxNouhinDir.Text.Trim();
			Settings.NouhinFile = textBoxNouhinFile.Text.Trim();
			Settings.PaperOffset.X = (int)numericUpDownOffsetX.Value;
			Settings.PaperOffset.Y = (int)numericUpDownOffsetY.Value;
			Settings.ParamFile = textBoxParamFile.Text.Trim();

#if DEBUG
			Settings.PrintRectangle = (false == checkBoxPrintRectangle.Checked) ? 0 : 1;
			Settings.PrintPreview = (false == checkBoxPrintPreview.Checked) ? 0 : 1;
#else
			Settings.PrintRectangle = 0;
			Settings.PrintPreview = 0;
#endif

			DialogResult = DialogResult.OK;
		}

		////////////////////////////////////////////////////////////////////
		// 印刷メソッド

		/// <summary>
		/// PrintPreviewForm BeginPrintイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintPreviewForm_BeginPrint(object sender, PrintEventArgs e)
		{
			PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			if (!printDocument.PrintController.IsPreview)
			{
				// 印刷の選択ダイアログを表示
				using (PrintDialog pdlg = new PrintDialog())
				{
					PrintDocument pd = ((PrintPreviewForm)sender).Document;
					pdlg.Document = pd;
					pdlg.AllowSomePages = true;
					pdlg.PrinterSettings.MinimumPage = 1;
					pdlg.PrinterSettings.MaximumPage = 1;
					pdlg.PrinterSettings.FromPage = pdlg.PrinterSettings.MinimumPage;
					pdlg.PrinterSettings.ToPage = pdlg.PrinterSettings.MaximumPage;
					if (pdlg.ShowDialog() == DialogResult.Cancel)
					{
						// 印刷のキャンセル
						e.Cancel = true;
						return;
					}
					pd.PrinterSettings = pdlg.PrinterSettings;
					MaxPage = pdlg.PrinterSettings.ToPage;
				}
			}
		}

		/// <summary>
		/// PrintPreviewForm PrintPageイベント
		/// </summary>
		private void PrintPreviewForm_PrintPage(object sender, PrintPageEventArgs e, int page)
		{
			PrintDocument printDocument = ((PrintPreviewForm)sender).Document;
			e.Graphics.PageUnit = GraphicsUnit.Display;

			// 印刷処理
			Point offset = new Point((int)numericUpDownOffsetX.Value, (int)numericUpDownOffsetY.Value);
#if DEBUG
			PrintControl.PrintNouhinData(e.Graphics, offset, NouhinDataList.GetTestData(), 1, checkBoxPrintRectangle.Checked);
#else
			PrintControl.PrintNouhinData(e.Graphics, offset, NouhinDataList.GetTestData(), 1, false);
#endif
		}

		/// <summary>
		/// PrintPreviewForm EndPrintイベント
		/// </summary>
		private void PrintPreviewForm_EndPrint(object sender, PrintEventArgs e)
		{
			;
		}
	}
}
