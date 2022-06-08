//
// MainForm.cs
// 
// 配送センター納品書印刷 メイン画面フォームクラス
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DeliveryCenterPrintNouhin.Forms
{
	public partial class MainForm : Form
	{
		[StructLayout(LayoutKind.Sequential)]
		struct MENUITEMINFO
		{
			public uint cbSize;
			public uint fMask;
			public uint fType;
			public uint fState;
			public uint wID;
			public IntPtr hSubMenu;
			public IntPtr hbmpChecked;
			public IntPtr hbmpUnchecked;
			public IntPtr dwItemData;
			public string dwTypeData;
			public uint cch;
			public IntPtr hbmpItem;

			// return the size of the structure
			public static uint sizeOf
			{
				get { return (uint)Marshal.SizeOf(typeof(MENUITEMINFO)); }
			}
		}

		[DllImport("user32.dll")]
		static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll")]
		static extern bool InsertMenuItem(IntPtr hMenu, uint uItem, bool fByPosition,
		  [In] ref MENUITEMINFO lpmii);

		private const uint MENU_ID_01 = 0x0001;
		private const uint MENU_ID_02 = 0x0002;

		private const uint MFT_BITMAP = 0x00000004;
		private const uint MFT_MENUBARBREAK = 0x00000020;
		private const uint MFT_MENUBREAK = 0x00000040;
		private const uint MFT_OWNERDRAW = 0x00000100;
		private const uint MFT_RADIOCHECK = 0x00000200;
		private const uint MFT_RIGHTJUSTIFY = 0x00004000;
		private const uint MFT_RIGHTORDER = 0x000002000;

		private const uint MFT_SEPARATOR = 0x00000800;
		private const uint MFT_STRING = 0x00000000;

		private const uint MIIM_FTYPE = 0x00000100;
		private const uint MIIM_STRING = 0x00000040;
		private const uint MIIM_ID = 0x00000002;

		private const uint WM_SYSCOMMAND = 0x0112;

		/// <summary>
		/// 環境設定
		/// </summary>
		public DeliveryCenterPrintNouhinSettings Settings { get; set; }

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
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			PrintDocument = new PrintDocument();
			MaxPage = 0;
			DataList = new NouhinDataList();
			PrintControl = new PrintNouhinControl();

			IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

			MENUITEMINFO item1 = new MENUITEMINFO();
			item1.cbSize = (uint)Marshal.SizeOf(item1);
			item1.fMask = MIIM_FTYPE;
			item1.fType = MFT_SEPARATOR;
			InsertMenuItem(hSysMenu, 5, true, ref item1);

			MENUITEMINFO item2 = new MENUITEMINFO();
			item2.cbSize = (uint)Marshal.SizeOf(item2);
			item2.fMask = MIIM_STRING | MIIM_ID;
			item2.wID = MENU_ID_01;
			item2.dwTypeData = "環境設定";
			InsertMenuItem(hSysMenu, 6, true, ref item2);
		}

		/// <summary>
		/// システムメニュー
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == WM_SYSCOMMAND)
			{
				uint menuid = (uint)(m.WParam.ToInt32() & 0xffff);
				if (MENU_ID_01 == menuid)
				{
					// 環境設定
					using (EnvironmentForm dlg = new EnvironmentForm())
					{
						dlg.Settings = Settings.DeepCopy();
						if (DialogResult.OK == dlg.ShowDialog())
						{
							Settings = dlg.Settings.DeepCopy();

							// 環境設定の保存
							DeliveryCenterPrintNouhinSettingsIF.SetSettings(Settings);
						}
					}
				}
			}
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			//gSettings = new DeliveryCenterPrintNouhinSettings();
			//gSettings.PaperOffset = new Point(50, 50);
			//gSettings.NouhinDir = @"C:\PrintNouhin\納品書用データ";
			//gSettings.NouhinFile = "Nouhin.csv";
			//DeliveryCenterPrintNouhinSettingsIF.SetSettings(gSettings);

			this.Text = this.Text + "   " + Program.ProgramVersion;

			// 環境設定の読み込み
			Settings = DeliveryCenterPrintNouhinSettingsIF.GetSettings();
		}

		/// <summary>
		/// 納品書印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrint_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Settings.NouhinDir;
				dlg.FileName = Settings.NouhinFile;
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
				dlg.Title = "納品書CSVファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					Settings.NouhinDir = Path.GetDirectoryName(dlg.FileName);
					Settings.NouhinFile = Path.GetFileName(dlg.FileName);

					// 納品書CSVファイルの読み込み
					string msg;
					DataList = NouhinDataList.ReadNohinCsvFile(Settings.NouhinFilePathname, out msg);
					if (null != DataList)
					{
						MaxPage = DataList.GetMaxPage();

						if (false == Settings.印刷プレビュー)
						{
							progressBar.Minimum = 0;
							progressBar.Maximum = MaxPage;
							progressBar.Value = 0;
						}
						PrintNouhin();
					}
					else
					{
						MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
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
			DeliveryCenterPrintNouhinSettingsIF.SetSettings(Settings);
		}


		////////////////////////////////////////////////////////////////////
		// 印刷メソッド

		/// <summary>
		/// 納品書の印刷
		/// </summary>
		private void PrintNouhin()
		{
			string msg;
			if (-1 != PrintControl.ReadParameterFile(Directory.GetCurrentDirectory(), Settings.ParamFile, out msg))
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

					if (false == Settings.印刷プレビュー)
					{
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
							MessageBox.Show("有効なプリンタが指定されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}
			else
			{
				MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
			PrintControl.PrintNouhinData(e.Graphics, Settings.PaperOffset, DataList, page, Settings.矩形印刷);

			if (false == Settings.印刷プレビュー)
			{
				progressBar.Value = page;
			}
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
