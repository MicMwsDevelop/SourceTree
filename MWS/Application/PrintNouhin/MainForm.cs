using CommonDialog.PrintPreview;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace PrintNouhin
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static PrintNouhinSettings gSettings { get; set; }

		/// <summary>
		/// 納品書データリスト
		/// </summary>
		private NouhinDataList DataList;

		/// <summary>
		/// 納品書印刷制御クラス
		/// </summary>
		private PrintNouhinControl PrintControl;

		/// <summary>
		/// 印刷設定保持用
		/// </summary>
		private PrintDocument PrintDocument { get; set; }

		/// <summary>
		/// 印刷 総ページ数
		/// </summary>
		private int MaxPage { get; set; }

		/// <summary>
		/// カレント印刷部
		/// </summary>
		private int CurrentPrintIndex { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			gSettings = null;
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			CurrentPrintIndex = 0;

			DataList = new NouhinDataList();
			PrintControl = new PrintNouhinControl();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			//PrintNouhinSettings set = new PrintNouhinSettings();
			//set.PaperOffset = new Point(50, 50);
			//set.NouhinDir = @"C:\PrintNouhin\納品書用データ";
			//set.NouhinFile = "Nouhin.csv";
			//PrintNouhinSettingsIF.SetPrintNouhinSettings(set);

			// 環境設定の読み込み
			gSettings = PrintNouhinSettingsIF.GetPrintNouhinSettings();

		}

		/// <summary>
		/// 納品書印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrint_Click(object sender, EventArgs e)
		{
            // 納品書CSVファイルの読み込み
            string msg;
			DataList = NouhinDataList.ReadNohinCsvFile(Path.Combine(gSettings.NouhinDir, gSettings.NouhinFile), out msg);
			if (null != DataList)
			{
				int printCount = DataList.GetPrintCount();
				for (int i = 0; i < printCount; i++)
				{
					CurrentPrintIndex = i + 1;
					NouhinDataList list = DataList.GetPrintDataList(CurrentPrintIndex);
					MaxPage = list.GetMaxPage();
					PrintNouhin(false);
				}
			}
			else
			{
				MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// 環境設定の保存
			PrintNouhinSettingsIF.SetPrintNouhinSettings(gSettings);
		}


		////////////////////////////////////////////////////////////////////
		// 印刷メソッド

		/// <summary>
		/// 見積書の印刷
		/// </summary>
		/// <param name="isPrint">印刷かどうか？</param>
		private void PrintNouhin(bool isPrint)
		{
			string msg;
			if (-1 != PrintControl.ReadParameterFile(Directory.GetCurrentDirectory(), PrintNouhinControl.PARAMETER_FILENAME, out msg))
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

					if (isPrint)
					{
						// 印刷
						if (pf.Document.PrinterSettings.IsValid)
						{
							pf.Print();
						}
						else
						{
							MessageBox.Show(this, "有効なプリンタが指定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					else
					{
						// プレビュー
						if (pf.Document.PrinterSettings.IsValid)
						{
							// 印刷プレビューダイアログを表示する
							if (pf.ShowDialog() == DialogResult.OK)
							{
								// PrintPreviewFormで印刷を実行
								;
							}
						}
						else
						{
							MessageBox.Show(this, "有効なプリンタが指定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}
			else
			{
				MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
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
					pdlg.PrinterSettings.MaximumPage = MaxPage;
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
			//printDocument.DocumentName = PrintEstimateDef.GetDocumentName(PrintControl.PaperType, PrintInfo.PrintData.Destination);
		}

		/// <summary>
		/// PrintPreviewForm PrintPageイベント
		/// </summary>
		private void PrintPreviewForm_PrintPage(object sender, PrintPageEventArgs e, int page)
		{
			PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			e.Graphics.PageUnit = GraphicsUnit.Display;

			//Point offset = ClientIniIO.GetClientIntroduceOffset(PrintIntroduce.PaperName);

			//if (printDocument.PrintController.IsPreview)
			//{
			//	// プレビューのハードマージン分のずれを補正する

			//	// 印字不可領域を、1/100inchから0.1mm単位に変換する
			//	float x = PrintPara.ToMillimeter(printDocument.DefaultPageSettings.HardMarginX);
			//	float y = PrintPara.ToMillimeter(printDocument.DefaultPageSettings.HardMarginY);

			//	offset.X += (int)x;
			//	offset.Y += (int)y;
			//}

			//// 基底引数
			//var args = this.PaletteSystemInfo as VisitArgs;

			// 印刷処理
			//PrintControl.PrintNouhinData(DataList.GetPrintDataList(CurrentPrintIndex), PrintIndex, e.Graphics, gSettings.PaperOffset, page, false);
			PrintControl.PrintNouhinData(DataList.GetPrintDataList(CurrentPrintIndex), CurrentPrintIndex + 1, e.Graphics, new Point(50, 50), page, true);
		}

		/// <summary>
		/// PrintPreviewForm EndPrintイベント
		/// </summary>
		private void PrintPreviewForm_EndPrint(object sender, PrintEventArgs e)
		{
			//PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			//if (!printDocument.PrintController.IsPreview)
			//{
			//	if (!e.Cancel)
			//	{
			//		// プリンタ発行情報をINIファイルに記憶する
			//		ClientPrinterInfoIni pi = new ClientPrinterInfoIni();
			//		pi.ReadPrintDocument(printDocument);
			//		ClientIniIO.SetClientIntroducePrinterInfo(PrintIntroduce.PaperName, pi);
			//	}
			//}
		}
	}
}
