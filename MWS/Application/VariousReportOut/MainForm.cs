using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MwsLib.BaseFactory.VariousReportOut;
using MwsLib.DB.SqlServer.VariousReportOut;
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.Common;

namespace VariousReportOut
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 各種帳票種別
		/// </summary>
		private enum ReportType
		{
			/// <summary>
			/// MWS IDパスワード
			/// </summary>
			MwsIDPassword,

			/// <summary>
			/// FAX送付状
			/// </summary>
			FaxLetter,

			/// <summary>
			/// 書類送付状
			/// </summary>
			DocumentLetter,

			/// <summary>
			/// 光ディスク請求届出
			/// </summary>
			LightDisk,

			/// <summary>
			/// オンライン請求届出
			/// </summary>
			Online,

			/// <summary>
			/// 取引条件確認書
			/// </summary>
			Transaction,

			/// <summary>
			/// 登録データ確認カード
			/// </summary>
			ConfirmCard,

			/// <summary>
			/// Microsoft365利用申請書
			/// </summary>
			Microsoft365,

			/// <summary>
			/// 請求先変更届
			/// </summary>
			SeikyuChange,

			/// <summary>
			/// 終了届
			/// </summary>
			UserFinished,

			/// <summary>
			/// 変更届
			/// </summary>
			UserChange,

			/// <summary>
			/// 第一園芸注文書
			/// </summary>
			FirstEngei,

			/// <summary>
			/// 納品補助作業依頼書
			/// </summary>
			Delivery,

			/// <summary>
			/// 二次キッティング依頼書
			/// </summary>
			SecondKitting,

			/// <summary>
			/// PC安心サポート加入申込書
			/// </summary>
			PcSupport,

			/// <summary>
			/// アプラス預金口座振替依頼書・自動払込利用申込書
			/// </summary>
			Aplus,

			/// <summary>
			/// PC安心サポートPlus加入申込書
			/// </summary>
			PcSupportPlus,
		}

		/// <summary>
		/// 各種帳票種別
		/// </summary>
		private ReportType RepType { get; set;}

		/// <summary>
		/// 事業所情報
		/// </summary>
		private OfficeInfo Office { get; set;}

		/// <summary>
		/// 顧客情報
		/// </summary>
		private CustomerInfo Customer { get; set;}

		/// <summary>
		/// 顧客詳細情報
		/// </summary>
		private vMic全ユーザー2 CustomerDetail { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			Office = null;
			Customer = null;
			CustomerDetail = null;
		}

		/// <summary>
		/// Load Form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
#if DEBUG
			textBoxTokuisakiNo.Text = "010223";
#endif
			RepType = ReportType.MwsIDPassword;
			List<OfficeInfo> satelliteList = VariousReportOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.DATABASE_ACCESS_CT);
			List<OfficeInfo> headOfficeList = VariousReportOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.DATABASE_ACCESS_CT);
			if (null == satelliteList && null == headOfficeList)
			{
				// 本社所属
				Office = new OfficeInfo();
				Office.SetHeadOfficeInfo(Environment.UserName);
			}
			else if (0 < satelliteList.Count && null == headOfficeList)
			{
				// 営業部所属
				Office = satelliteList.First();
			}
			else
			{
				// 営業部以外所属
				Office = headOfficeList.First();
			}
		}

		/// <summary>
		/// 各種書類出力管理
		/// </summary>
		private void ReportOutEnable(bool enable)
		{
			radioアプラス預金口座振替依頼書.Enabled = enable;
			radioPC安心サポート加入申込書.Enabled = enable;
			radio二次キッティング依頼書.Enabled = enable;
			radio納品補助作業依頼書.Enabled = enable;
			radio第一園芸注文書.Enabled = enable;
			radio変更届.Enabled = enable;
			radio終了届.Enabled = enable;
			radio請求先変更届.Enabled = enable;
			radioMicrosoft365利用申請書.Enabled = enable;
			radio登録データ確認カード.Enabled = enable;
			radio取引条件確認書.Enabled = enable;
			radioオンライン請求届出.Enabled = enable;
			radio光ディスク請求届出.Enabled = enable;
			radio書類送付状.Enabled = enable;
			radioFAX送付状.Enabled = enable;
			radioMWSIDパスワード.Enabled = enable;
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (MwsDefine.TokuisakiNoLength == textBoxTokuisakiNo.Text.Length)
			{
				List<CustomerInfo> result = VariousReportOutAccess.Select_CustomerInfo(textBoxTokuisakiNo.Text, Program.DATABASE_ACCESS_CT);
				if (0 < result.Count)
				{
					Customer = result.First();
					textBoxCustomerName.Text = Customer.顧客名;

					if (Customer.Enable)
					{
						// 出力可能
						ReportOutEnable(true);

						// 顧客詳細情報の読込
						string whereStr = string.Format("得意先No = '{0}'", textBoxTokuisakiNo.Text);
						List<vMic全ユーザー2 > work = JunpDatabaseAccess.Select_vMic全ユーザー2(whereStr, "", Program.DATABASE_ACCESS_CT);
						if (null != work)
						{
							CustomerDetail = work.First();
						}
					}
					else
					{
						// 出力不可
						ReportOutEnable(false);
					}
					return;
				}
			}
			MessageBox.Show(this, "該当顧客が見つかりません", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// クリア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void buttonClear_Click(object sender, EventArgs e)
		{
			Customer.Empty();
		}

		/// <summary>
		/// MWS IDパスワード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioMWSIDパスワード_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.MwsIDPassword;
		}

		/// <summary>
		/// FAX送付状
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioFAX送付状_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.FaxLetter;
		}

		/// <summary>
		/// 書類送付状
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio書類送付状_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.DocumentLetter;
		}

		/// <summary>
		/// 光ディスク請求届出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio光ディスク請求届出_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.LightDisk;
		}

		/// <summary>
		/// オンライン請求届出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioオンライン請求届出_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.Online;
		}

		/// <summary>
		/// 取引条件確認書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio取引条件確認書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.Transaction;
		}

		/// <summary>
		/// 登録データ確認カード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio登録データ確認カード_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.ConfirmCard;
		}

		/// <summary>
		/// Microsoft365利用申請書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioMicrosoft365利用申請書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.Microsoft365;
		}

		/// <summary>
		/// 請求先変更届
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio請求先変更届_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.SeikyuChange;
		}

		/// <summary>
		/// 終了届
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio終了届_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.UserFinished;
		}

		/// <summary>
		/// 変更届
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio変更届_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.UserChange;
		}

		/// <summary>
		/// 第一園芸注文書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio第一園芸注文書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.FirstEngei;
		}

		/// <summary>
		/// 納品補助作業依頼書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio納品補助作業依頼書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.Delivery;
		}

		/// <summary>
		/// 二次キッティング依頼書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio二次キッティング依頼書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.SecondKitting;
		}

		/// <summary>
		/// PC安心サポート加入申込書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioPC安心サポート加入申込書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.PcSupport;
		}

		/// <summary>
		/// アプラス預金口座振替依頼書・自動払込利用申込書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioアプラス預金口座振替依頼書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.Aplus;
		}

		/// <summary>
		/// PC安心サポートPlus加入申込書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioPC安心サポートPlus加入申込書_CheckedChanged(object sender, EventArgs e)
		{
			RepType = ReportType.PcSupportPlus;
		}

		/// <summary>
		/// シート入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// シート入力有無
		/// MWSIDパスワード	×
		/// FAX送付状	〇
		/// 書類送付状	〇
		/// 光ディスク請求届出	×
		/// オンライン請求届出	×
		/// 取引条件確認書	×
		/// 登録データ確認カード	×
		/// Microsoft365利用申請書	〇
		/// 請求先変更届	×
		/// 終了届	×
		/// 変更届	×
		/// 第一園芸注文書	〇
		/// 納品補助作業依頼書	〇
		/// 二次キッティング依頼書	〇
		/// PC安心サポート加入申込書	×
		/// アプラス預金口座振替依頼書・自動払込利用申込書	×
		/// PC安心サポートPlus加入申込書	×

		/// <summary>
		/// EXCEL出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputExcel_Click(object sender, EventArgs e)
		{
			if (null == CustomerDetail)
			{
				MessageBox.Show(this, "得意先Noを指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			switch (RepType)
			{
				/// <summary>
				/// MWS IDパスワード
				/// </summary>
				case ReportType.MwsIDPassword:
					ExcelOutMwsIDPassword("1-MWSIDパスワード.xlsx");
					break;
				/// <summary>
				/// FAX送付状
				/// </summary>
				case ReportType.FaxLetter:
					ExcelOutFaxLetter("2-FAX送付状.xlsx");
					break;
				/// <summary>
				/// 書類送付状
				/// </summary>
				case ReportType.DocumentLetter:
					ExcelOutDocumentLetter("3-書類送付状.xlsx");
					break;
				/// <summary>
				/// 光ディスク請求届出
				/// </summary>
				case ReportType.LightDisk:
					ExcelOutLightDisk("4-光ディスク請求届出.xlsx");
					break;
				/// <summary>
				/// オンライン請求届出
				/// </summary>
				case ReportType.Online:
					ExcelOutOnline("5-オンライン請求届出.xlsx");
					break;
				/// <summary>
				/// 取引条件確認書
				/// </summary>
				case ReportType.Transaction:
					ExcelOutTransaction("6-取引条件確認書.xlsx");
					break;
				/// <summary>
				/// 登録データ確認カード
				/// </summary>
				case ReportType.ConfirmCard:
					ExcelOutConfirmCard("7-登録データ確認カード.xlsx");
					break;
				/// <summary>
				/// Microsoft365利用申請書
				/// </summary>
				case ReportType.Microsoft365:
					ExcelOutConfirmCard("8-Microsoft365利用申請書.xlsx");
					break;
				/// <summary>
				/// 請求先変更届
				/// </summary>
				case ReportType.SeikyuChange:
					ExcelOutSeikyuChange("9-請求先変更届.xlsx");
					break;
				/// <summary>
				/// 終了届
				/// </summary>
				case ReportType.UserFinished:
					ExcelOutUserFinished("10-終了届.xlsx");
					break;
				/// <summary>
				/// 変更届
				/// </summary>
				case ReportType.UserChange:
					ExcelOutUserChange("11-変更届.xlsx");
					break;
				/// <summary>
				/// 第一園芸注文書
				/// </summary>
				case ReportType.FirstEngei:
					ExcelOutFirstEngei("12-第一園芸注文書.xlsx");
					break;
				/// <summary>
				/// 納品補助作業依頼書
				/// </summary>
				case ReportType.Delivery:
					ExcelOutDelivery("13-納品補助作業依頼書.xlsx");
					break;
				/// <summary>
				/// 二次キッティング依頼書
				/// </summary>
				case ReportType.SecondKitting:
					break;
				/// <summary>
				/// PC安心サポート加入申込書
				/// </summary>
				case ReportType.PcSupport:
					ExcelOutPcSupport("15-PC安心サポート加入申込書.xlsx", false);
					break;
				/// <summary>
				/// アプラス預金口座振替依頼書・自動払込利用申込書
				/// </summary>
				case ReportType.Aplus:
					ExcelOutAplus("16-アプラス預金口座振替依頼書・自動払込利用申込書.xlsx");
					break;
				/// <summary>
				/// PC安心サポートPlus加入申込書
				/// </summary>
				case ReportType.PcSupportPlus:
					ExcelOutPcSupport("17-PC安心サポートPlus加入申込書.xlsx", true);
					break;
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// EXCEL出力 - MWS IDパスワード
		/// </summary>
		private void ExcelOutMwsIDPassword(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["MWS_IDPW"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "郵便番号":
								textFrame.Characters.Text = CustomerDetail.郵便番号;
								break;
							case "住所１":
								textFrame.Characters.Text = CustomerDetail.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = CustomerDetail.住所2;
								break;
							case "顧客名1":
								textFrame.Characters.Text = CustomerDetail.顧客名1;
								break;
							case "顧客名2":
								textFrame.Characters.Text = CustomerDetail.顧客名2;
								break;
							case "御中":
								textFrame.Characters.Text = "御中";
								break;
							case "発行日":
								textFrame.Characters.Text = Date.Today.GetJapaneseString(true, '0', true, true);
								break;
							case "ユーザーID":
								textFrame.Characters.Text = CustomerDetail.MWS_ID;
								break;
							case "初期パスワード":
								textFrame.Characters.Text = CustomerDetail.MWS_パスワード;
								break;
							case "初期パスワード読み":
								textFrame.Characters.Text = CustomerDetail.MWS_パスワード読み;
								break;
						}
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - FAX送付状
		/// </summary>
		private void ExcelOutFaxLetter(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["FAX送付状"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "日付":
								textFrame.Characters.Text = Date.Today.GetJapaneseString(true, '0', true, true);
								break;
							case "送付先":
								textFrame.Characters.Text = string.Format("{0}　御中", CustomerDetail.顧客名);
								break;
							case "FAX":
								textFrame.Characters.Text = string.Format("FAX {0}", CustomerDetail.FAX番号);
								break;
							case "送付元":
								if (Office.HeadOffice)
								{
									// 本社
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}", OfficeInfo.CompanyZipcode, OfficeInfo.CompanyAddress, OfficeInfo.CompanyName);
								}
								else
								{
									// 拠点
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4} {5}\r\nTEL {6} FAX {7}", Office.郵便番号, Office.住所1, Office.住所2, OfficeInfo.CompanyName, Office.部署名, Office.電話番号, Office.FAX番号);
								}
								break;
						}
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 書類送付状
		/// </summary>
		private void ExcelOutDocumentLetter(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["書類送付状"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "日付":
								textFrame.Characters.Text = Date.Today.GetJapaneseString(true, '0', true, true);
								break;
							case "送付先":
								textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}　御中", CustomerDetail.郵便番号, CustomerDetail.住所1, CustomerDetail.住所2, CustomerDetail.顧客名);
								break;
							case "FAX":
								textFrame.Characters.Text = string.Format("FAX {0}", CustomerDetail.FAX番号);
								break;
							case "送付元":
								if (Office.HeadOffice)
								{
									// 本社
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}", OfficeInfo.CompanyZipcode, OfficeInfo.CompanyAddress, OfficeInfo.CompanyName);
								}
								else
								{
									// 拠点
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4} {5}\r\nTEL {6} FAX {7}", Office.郵便番号, Office.住所1, Office.住所2, OfficeInfo.CompanyName, Office.部署名, Office.電話番号, Office.FAX番号);
								}
								break;
						}
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 光ディスク請求届出
		/// </summary>
		private void ExcelOutLightDisk(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet1 = null;
			Excel.Worksheet xlSheet2 = null;
			Excel.Shapes xlShapes1 = null;
			Excel.Shapes xlShapes2 = null;
			Excel.Worksheet xlSheet3 = null;
			Excel.Worksheet xlSheet4 = null;
			Excel.Shapes xlShapes3 = null;
			Excel.Shapes xlShapes4 = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet1 = xlSheets["光ディスク請求届出"] as Excel.Worksheet;
				xlShapes1 = xlSheet1.Shapes;

				string clinicCode = CustomerDetail.NumericClinicCode;
				string zipCode = CustomerDetail.NumericZipcode;

				// 光ディスク請求届出-社保用
				foreach (Excel.Shape shape in xlShapes1)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = CustomerDetail.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = CustomerDetail.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = CustomerDetail.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = CustomerDetail.住所2;
								break;
							case "医療機関コード１":
								textFrame.Characters.Text = clinicCode.Substring(0, 1);
								break;
							case "医療機関コード２":
								textFrame.Characters.Text = clinicCode.Substring(1, 1);
								break;
							case "医療機関コード３":
								textFrame.Characters.Text = clinicCode.Substring(2, 1);
								break;
							case "医療機関コード４":
								textFrame.Characters.Text = clinicCode.Substring(3, 1);
								break;
							case "医療機関コード５":
								textFrame.Characters.Text = clinicCode.Substring(4, 1);
								break;
							case "医療機関コード６":
								textFrame.Characters.Text = clinicCode.Substring(5, 1);
								break;
							case "医療機関コード７":
								textFrame.Characters.Text = clinicCode.Substring(6, 1);
								break;
							case "顧客名":
								textFrame.Characters.Text = CustomerDetail.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = CustomerDetail.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = CustomerDetail.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = CustomerDetail.電話番号;
								break;
							case "郵便番号１":
								textFrame.Characters.Text = zipCode.Substring(0, 1);
								break;
							case "郵便番号２":
								textFrame.Characters.Text = zipCode.Substring(1, 1);
								break;
							case "郵便番号３":
								textFrame.Characters.Text = zipCode.Substring(2, 1);
								break;
							case "郵便番号４":
								textFrame.Characters.Text = zipCode.Substring(3, 1);
								break;
							case "郵便番号５":
								textFrame.Characters.Text = zipCode.Substring(4, 1);
								break;
							case "郵便番号６":
								textFrame.Characters.Text = zipCode.Substring(5, 1);
								break;
							case "郵便番号７":
								textFrame.Characters.Text = zipCode.Substring(6, 1);
								break;
							case "メーカー名":
								textFrame.Characters.Text = OfficeInfo.CompanyName;
								break;
						}
					}
				}
				// 光ディスク請求届出-国保用
				xlSheet1.Copy(Type.Missing, xlSheet1);
				xlSheet2 = xlSheets["光ディスク請求届出 (2)"] as Excel.Worksheet;
				xlSheet1.Name = "光ディスク請求届出-社保";
				xlSheet2.Name = "光ディスク請求届出-国保";
				xlShapes2 = xlSheet2.Shapes;
				foreach (Excel.Shape shape in xlShapes2)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = CustomerDetail.都道府県名 + "国民健康保険団体連合会";
								break;
							case "宛先２":
								textFrame.Characters.Text = string.Empty;
								break;
						}
					}
				}
				// 光ディスク請求確認試験依頼書-社保用
				xlSheet3 = xlSheets["光ディスク請求確認試験依頼書"] as Excel.Worksheet;
				xlShapes3 = xlSheet3.Shapes;
				foreach (Excel.Shape shape in xlShapes3)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = CustomerDetail.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = CustomerDetail.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = CustomerDetail.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = CustomerDetail.住所2;
								break;
							case "医療機関コード１":
								textFrame.Characters.Text = clinicCode.Substring(0, 1);
								break;
							case "医療機関コード２":
								textFrame.Characters.Text = clinicCode.Substring(1, 1);
								break;
							case "医療機関コード３":
								textFrame.Characters.Text = clinicCode.Substring(2, 1);
								break;
							case "医療機関コード４":
								textFrame.Characters.Text = clinicCode.Substring(3, 1);
								break;
							case "医療機関コード５":
								textFrame.Characters.Text = clinicCode.Substring(4, 1);
								break;
							case "医療機関コード６":
								textFrame.Characters.Text = clinicCode.Substring(5, 1);
								break;
							case "医療機関コード７":
								textFrame.Characters.Text = clinicCode.Substring(6, 1);
								break;
							case "顧客名":
								textFrame.Characters.Text = CustomerDetail.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = CustomerDetail.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = CustomerDetail.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = CustomerDetail.電話番号;
								break;
							case "郵便番号１":
								textFrame.Characters.Text = zipCode.Substring(0, 1);
								break;
							case "郵便番号２":
								textFrame.Characters.Text = zipCode.Substring(1, 1);
								break;
							case "郵便番号３":
								textFrame.Characters.Text = zipCode.Substring(2, 1);
								break;
							case "郵便番号４":
								textFrame.Characters.Text = zipCode.Substring(3, 1);
								break;
							case "郵便番号５":
								textFrame.Characters.Text = zipCode.Substring(4, 1);
								break;
							case "郵便番号６":
								textFrame.Characters.Text = zipCode.Substring(5, 1);
								break;
							case "郵便番号７":
								textFrame.Characters.Text = zipCode.Substring(6, 1);
								break;
							case "メーカー名":
								textFrame.Characters.Text = OfficeInfo.CompanyName;
								break;
							case "御中":
								textFrame.Characters.Text = "御中";
								break;
						}
					}
				}
				// 光ディスク請求確認試験依頼書-国保用
				xlSheet3.Copy(Type.Missing, xlSheet3);
				xlSheet4 = xlSheets["光ディスク請求確認試験依頼書 (2)"] as Excel.Worksheet;
				xlSheet3.Name = "光ディスク請求確認試験依頼書-社保";
				xlSheet4.Name = "光ディスク請求確認試験依頼書-国保";
				xlShapes4 = xlSheet4.Shapes;
				foreach (Excel.Shape shape in xlShapes4)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = CustomerDetail.都道府県名 + "国民健康保険団体連合会";
								break;
							case "宛先２":
								textFrame.Characters.Text = string.Empty;
								break;
						}
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlSheet1)
				{
					Marshal.ReleaseComObject(xlSheet1);
				}
				if (null != xlSheet2)
				{
					Marshal.ReleaseComObject(xlSheet2);
				}
				if (null != xlSheet3)
				{
					Marshal.ReleaseComObject(xlSheet3);
				}
				if (null != xlSheet4)
				{
					Marshal.ReleaseComObject(xlSheet4);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - オンライン請求届出
		/// </summary>
		private void ExcelOutOnline(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet1 = null;
			Excel.Worksheet xlSheet2 = null;
			Excel.Shapes xlShapes1 = null;
			Excel.Shapes xlShapes2 = null;
			Excel.Worksheet xlSheet3 = null;
			Excel.Shapes xlShapes3 = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet1 = xlSheets["オンライン請求届出"] as Excel.Worksheet;
				xlShapes1 = xlSheet1.Shapes;

				string clinicCode = CustomerDetail.NumericClinicCode;
				string zipCode = CustomerDetail.NumericZipcode;

				// オンライン請求届出-社保用
				foreach (Excel.Shape shape in xlShapes1)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = CustomerDetail.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = CustomerDetail.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = CustomerDetail.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = CustomerDetail.住所2;
								break;
							case "医療機関コード１":
								textFrame.Characters.Text = clinicCode.Substring(0, 1);
								break;
							case "医療機関コード２":
								textFrame.Characters.Text = clinicCode.Substring(1, 1);
								break;
							case "医療機関コード３":
								textFrame.Characters.Text = clinicCode.Substring(2, 1);
								break;
							case "医療機関コード４":
								textFrame.Characters.Text = clinicCode.Substring(3, 1);
								break;
							case "医療機関コード５":
								textFrame.Characters.Text = clinicCode.Substring(4, 1);
								break;
							case "医療機関コード６":
								textFrame.Characters.Text = clinicCode.Substring(5, 1);
								break;
							case "医療機関コード７":
								textFrame.Characters.Text = clinicCode.Substring(6, 1);
								break;
							case "顧客名":
								textFrame.Characters.Text = CustomerDetail.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = CustomerDetail.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = CustomerDetail.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = CustomerDetail.電話番号;
								break;
							case "郵便番号１":
								textFrame.Characters.Text = zipCode.Substring(0, 1);
								break;
							case "郵便番号２":
								textFrame.Characters.Text = zipCode.Substring(1, 1);
								break;
							case "郵便番号３":
								textFrame.Characters.Text = zipCode.Substring(2, 1);
								break;
							case "郵便番号４":
								textFrame.Characters.Text = zipCode.Substring(3, 1);
								break;
							case "郵便番号５":
								textFrame.Characters.Text = zipCode.Substring(4, 1);
								break;
							case "郵便番号６":
								textFrame.Characters.Text = zipCode.Substring(5, 1);
								break;
							case "郵便番号７":
								textFrame.Characters.Text = zipCode.Substring(6, 1);
								break;
							case "メーカー名":
								textFrame.Characters.Text = OfficeInfo.CompanyName;
								break;
						}
					}
				}
				// オンライン請求届出-国保用
				xlSheet1.Copy(Type.Missing, xlSheet1);
				xlSheet2 = xlSheets["オンライン請求届出 (2)"] as Excel.Worksheet;
				xlSheet1.Name = "オンライン請求届出-社保";
				xlSheet2.Name = "オンライン請求届出-国保";
				xlShapes2 = xlSheet2.Shapes;
				foreach (Excel.Shape shape in xlShapes2)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = CustomerDetail.都道府県名 + "国民健康保険団体連合会";
								break;
							case "宛先２":
								textFrame.Characters.Text = string.Empty;
								break;
						}
					}
				}
				// オンライン証明書発行依頼書
				xlSheet3 = xlSheets["オンライン証明書発行依頼書"] as Excel.Worksheet;
				xlShapes3 = xlSheet3.Shapes;
				foreach (Excel.Shape shape in xlShapes3)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = CustomerDetail.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = CustomerDetail.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = CustomerDetail.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = CustomerDetail.住所2;
								break;
							case "県番号１":
								textFrame.Characters.Text = CustomerDetail.県番号.Substring(0, 1);
								break;
							case "県番号２":
								textFrame.Characters.Text = CustomerDetail.県番号.Substring(1, 1);
								break;
							case "医療機関コード１":
								textFrame.Characters.Text = clinicCode.Substring(0, 1);
								break;
							case "医療機関コード２":
								textFrame.Characters.Text = clinicCode.Substring(1, 1);
								break;
							case "医療機関コード３":
								textFrame.Characters.Text = clinicCode.Substring(2, 1);
								break;
							case "医療機関コード４":
								textFrame.Characters.Text = clinicCode.Substring(3, 1);
								break;
							case "医療機関コード５":
								textFrame.Characters.Text = clinicCode.Substring(4, 1);
								break;
							case "医療機関コード６":
								textFrame.Characters.Text = clinicCode.Substring(5, 1);
								break;
							case "医療機関コード７":
								textFrame.Characters.Text = clinicCode.Substring(6, 1);
								break;
							case "ﾌﾘｶﾞﾅ":
								textFrame.Characters.Text = CustomerDetail.フリガナ;
								break;
							case "顧客名":
								textFrame.Characters.Text = CustomerDetail.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = CustomerDetail.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = CustomerDetail.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = CustomerDetail.電話番号;
								break;
							case "郵便番号１":
								textFrame.Characters.Text = zipCode.Substring(0, 1);
								break;
							case "郵便番号２":
								textFrame.Characters.Text = zipCode.Substring(1, 1);
								break;
							case "郵便番号３":
								textFrame.Characters.Text = zipCode.Substring(2, 1);
								break;
							case "郵便番号４":
								textFrame.Characters.Text = zipCode.Substring(3, 1);
								break;
							case "郵便番号５":
								textFrame.Characters.Text = zipCode.Substring(4, 1);
								break;
							case "郵便番号６":
								textFrame.Characters.Text = zipCode.Substring(5, 1);
								break;
							case "郵便番号７":
								textFrame.Characters.Text = zipCode.Substring(6, 1);
								break;
						}
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlSheet1)
				{
					Marshal.ReleaseComObject(xlSheet1);
				}
				if (null != xlSheet2)
				{
					Marshal.ReleaseComObject(xlSheet2);
				}
				if (null != xlSheet3)
				{
					Marshal.ReleaseComObject(xlSheet3);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 取引条件確認書
		/// </summary>
		private void ExcelOutTransaction(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["取引条件確認書"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "郵便番号":
								textFrame.Characters.Text = string.Format("〒{0}", Office.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = Office.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = Office.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = Office.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("ＦＡＸ{0}", Office.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = CustomerDetail.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = CustomerDetail.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[36, 6] = CustomerDetail.フリガナ;
				xlCells[37, 6] = CustomerDetail.顧客名;
				xlCells[40, 6] = "〒" + CustomerDetail.郵便番号 + "\r\n" + CustomerDetail.住所1 + "\r\n" + CustomerDetail.住所2;
				xlCells[40, 22] = CustomerDetail.電話番号;
				xlCells[41, 22] = CustomerDetail.FAX番号;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 登録データ確認カード
		/// </summary>
		private void ExcelOutConfirmCard(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["登録データ確認カード"] as Excel.Worksheet;
				xlCells = xlSheet.Cells;

				xlCells[8, 7] = "";
				xlCells[8, 21] = "";
				xlCells[10, 7] = "";
				xlCells[12, 7] = "";
				xlCells[14, 7] = "";
				xlCells[16, 7] = "";
				xlCells[18, 7] = "";
				xlCells[20, 7] = "";
				xlCells[22, 7] = "";
				xlCells[24, 7] = "";
				xlCells[26, 7] = "";
				xlCells[20, 19] = "";
				xlCells[22, 19] = "";
				xlCells[24, 19] = "";
				xlCells[26, 19] = "";
				xlCells[29, 7] = "";
				xlCells[31, 7] = "";
				xlCells[33, 7] = "";
				xlCells[33, 15] = "";
				xlCells[33, 23] = "";
				xlCells[35, 7] = "";
				xlCells[37, 7] = "";
        
				xlCells[8, 7] = CustomerDetail.顧客No;
				xlCells[8, 21] = CustomerDetail.得意先No;
				xlCells[10, 7] = CustomerDetail.フリガナ;
				xlCells[12, 7] = CustomerDetail.顧客名;
				xlCells[14, 7] = "〒" + CustomerDetail.郵便番号;
				xlCells[16, 7] = CustomerDetail.住所フリガナ;
				xlCells[18, 7] = CustomerDetail.住所;
				xlCells[20, 7] = CustomerDetail.電話番号;
				xlCells[22, 7] = CustomerDetail.医保医療コード;
				xlCells[24, 7] = CustomerDetail.院長名フリガナ;
				xlCells[26, 7] = CustomerDetail.院長名;
				xlCells[20, 19] = CustomerDetail.FAX番号;
				xlCells[22, 19] = CustomerDetail.休診日;
				xlCells[24, 19] = CustomerDetail.診療時間;
				xlCells[26, 19] = CustomerDetail.メールアドレス;
				xlCells[29, 7] = CustomerDetail.システム名称;
				xlCells[31, 7] = CustomerDetail.備考;
				xlCells[33, 7] = CustomerDetail.単体;
				xlCells[33, 15] = CustomerDetail.サーバー;
				xlCells[33, 23] = CustomerDetail.クライアント;
				xlCells[35, 7] = CustomerDetail.販売店ID + " " + CustomerDetail.販売店名称;
				xlCells[37, 7] = CustomerDetail.納品月;
				xlCells[39, 7] = Customer.運用サポート情報;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - Microsoft365利用申請書
		/// </summary>
		private void ExcelOutMicrosoft365(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["Microsoft365利用申請書"] as Excel.Worksheet;
				xlCells = xlSheet.Cells;

				xlCells[10, 11] = CustomerDetail.顧客No;
				xlCells[11, 11] = CustomerDetail.顧客名;
				xlCells[13, 11] = CustomerDetail.院長名;
				xlCells[13, 28] = CustomerDetail.電話番号;
				xlCells[14, 12] = CustomerDetail.郵便番号;
				xlCells[15, 11] = CustomerDetail.住所;
				xlCells[18, 11] = CustomerDetail.メールアドレス;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 請求先変更届
		/// </summary>
		private void ExcelOutSeikyuChange(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["請求先変更届"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "郵便番号":
								textFrame.Characters.Text = string.Format("〒{0}", Office.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = Office.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = Office.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = Office.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("ＦＡＸ{0}", Office.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = CustomerDetail.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = CustomerDetail.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[12, 23] = CustomerDetail.得意先No;
				xlCells[15, 5] = CustomerDetail.フリガナ;
				xlCells[16, 5] = CustomerDetail.顧客名;
				xlCells[17, 5] = CustomerDetail.院長名フリガナ;
				xlCells[18, 5] = CustomerDetail.院長名;
				xlCells[20, 5] = "〒" + CustomerDetail.郵便番号 + "\r\n" + CustomerDetail.住所1 + "\r\n" + CustomerDetail.住所2;
				xlCells[20, 21] = CustomerDetail.電話番号;
				xlCells[22, 21] = CustomerDetail.FAX番号;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 終了届
		/// </summary>
		private void ExcelOutUserFinished(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["終了届"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "郵便番号":
								textFrame.Characters.Text = string.Format("〒{0}", Office.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = Office.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = Office.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = Office.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("ＦＡＸ{0}", Office.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = CustomerDetail.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = CustomerDetail.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[13, 23] = CustomerDetail.得意先No;
				xlCells[16, 5] = CustomerDetail.フリガナ;
				xlCells[17, 5] = CustomerDetail.顧客名;
				xlCells[18, 5] = CustomerDetail.院長名フリガナ;
				xlCells[19, 5] = CustomerDetail.院長名;
				xlCells[21, 5] = "〒" + CustomerDetail.郵便番号 + "\r\n" + CustomerDetail.住所1 + "\r\n" + CustomerDetail.住所2;
				xlCells[21, 21] = CustomerDetail.電話番号;
				xlCells[23, 21] = CustomerDetail.FAX番号;
				xlCells[25, 5] = CustomerDetail.医保医療コード;
				xlCells[27, 5] = CustomerDetail.システム名称;
				xlCells[27, 17] = CustomerDetail.クライアント;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 変更届
		/// </summary>
		private void ExcelOutUserChange(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["変更届"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "郵便番号":
								textFrame.Characters.Text = string.Format("〒{0}", Office.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = Office.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = Office.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = Office.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("ＦＡＸ{0}", Office.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = CustomerDetail.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = CustomerDetail.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[12, 23] = CustomerDetail.得意先No;
				xlCells[15, 5] = CustomerDetail.フリガナ;
				xlCells[16, 5] = CustomerDetail.顧客名;
				xlCells[17, 5] = CustomerDetail.院長名フリガナ;
				xlCells[18, 5] = CustomerDetail.院長名;
				xlCells[20, 5] = "〒" + CustomerDetail.郵便番号 + "\r\n" + CustomerDetail.住所1 + "\r\n" + CustomerDetail.住所2;
				xlCells[20, 21] = CustomerDetail.電話番号;
				xlCells[22, 21] = CustomerDetail.FAX番号;
				xlCells[24, 5] = CustomerDetail.医保医療コード;
				xlCells[26, 5] = CustomerDetail.システム名称;
				xlCells[26, 17] = CustomerDetail.クライアント;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 12-第一園芸注文書
		/// </summary>
		private void ExcelOutFirstEngei(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet1 = null;
			Excel.Worksheet xlSheet2 = null;
			Excel.Range xlCells1 = null;
			Excel.Range xlCells2 = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;

				// 第一園芸医院向け
				xlSheet1 = xlSheets["第一園芸医院向け"] as Excel.Worksheet;
				xlCells1 = xlSheet1.Cells;
				xlCells1[6, 4] = CustomerDetail.郵便番号;
				xlCells1[7, 3] = CustomerDetail.住所1;
				xlCells1[8, 3] = CustomerDetail.住所2;
				xlCells1[9, 5] = CustomerDetail.顧客名;
				xlCells1[13, 5] = CustomerDetail.電話番号;

				// 第一園芸販売店向け
				xlSheet2 = xlSheets["第一園芸販売店向け"] as Excel.Worksheet;
				xlCells2 = xlSheet2.Cells;
				xlCells2[6, 18] = CustomerDetail.郵便番号;
				xlCells2[7, 17] = CustomerDetail.住所1;
				xlCells2[8, 17] = CustomerDetail.住所2;
				xlCells2[9, 19] = CustomerDetail.顧客名;
				xlCells2[13, 19] = CustomerDetail.電話番号;
				xlCells2[13, 26] = CustomerDetail.FAX番号;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells1)
				{
					Marshal.ReleaseComObject(xlCells1);
				}
				if (null != xlCells2)
				{
					Marshal.ReleaseComObject(xlCells2);
				}
				if (null != xlSheet1)
				{
					Marshal.ReleaseComObject(xlSheet1);
				}
				if (null != xlSheet2)
				{
					Marshal.ReleaseComObject(xlSheet2);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - 13-納品補助作業依頼書
		/// </summary>
		private void ExcelOutDelivery(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;

				xlSheet = xlSheets["新興サービス注文書"] as Excel.Worksheet;
				xlCells = xlSheet.Cells;
				xlCells[1, 27] = string.Format("{0:D4}", Date.Today.Year);
				xlCells[1, 30] = string.Format("{0:D2}", Date.Today.Month);
				xlCells[1, 32] = string.Format("{0:D2}", Date.Today.Day);
				xlCells[7, 27] = CustomerDetail.顧客No;
				xlCells[7, 10] = Office.拠点名;
				xlCells[9, 10] = CustomerDetail.顧客名;
				xlCells[11, 10] = string.Format("〒{0}", CustomerDetail.郵便番号);
				xlCells[12, 10] = CustomerDetail.住所;
				xlCells[15, 10] = CustomerDetail.院長名;
				xlCells[15, 27] = CustomerDetail.電話番号;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - PC安心サポート加入申込書 or PC安心サポートPlus加入申込書
		/// </summary>
		private void ExcelOutPcSupport(string filename, bool plus)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Range xlCells = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				if (plus)
				{
					xlSheet = xlSheets["PC安心サポートPlus加入申込書"] as Excel.Worksheet;
				}
				else
				{
					xlSheet = xlSheets["PC安心サポート加入申込書"] as Excel.Worksheet;
				}
				xlCells = xlSheet.Cells;

				xlCells[11, 11] = CustomerDetail.得意先No;
				xlCells[12, 11] = CustomerDetail.顧客No;
				xlCells[13, 11] = CustomerDetail.顧客名;
				xlCells[15, 11] = CustomerDetail.電話番号;
				xlCells[16, 12] = CustomerDetail.郵便番号;
				xlCells[17, 11] = CustomerDetail.住所1 + "\r\n" + CustomerDetail.住所2;
				xlCells[20, 11] = CustomerDetail.メールアドレス;
				xlCells[21, 11] = CustomerDetail.支店名;
				xlCells[21, 25] = CustomerDetail.営業担当者名;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlCells)
				{
					Marshal.ReleaseComObject(xlCells);
				}
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// EXCEL出力 - アプラス預金口座振替依頼書・自動払込利用申込書
		/// </summary>
		private void ExcelOutAplus(string filename)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook  = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["アプラス預金口座振替依頼書_自動払込利用申込書"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;

				// シート内コントロール初期化
				// ※チェックボックスはクリックすると容易にチェックがついてしまうため
				xlSheet.OLEObjects("新規登録CheckBox").Object.Value = 0;
				xlSheet.OLEObjects("申込書不備CheckBox").Object.Value = 0;
				xlSheet.OLEObjects("変更_引落CheckBox").Object.Value = 0;
				xlSheet.OLEObjects("変更_振込CheckBox").Object.Value = 0;

				foreach (Excel.Shape shape in xlShapes)
				{
					if (Microsoft.Office.Core.MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "顧客No":
								textFrame.Characters.Text = CustomerDetail.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = CustomerDetail.得意先No;
								break;
							case "契約者名フリガナ":
								textFrame.Characters.Text = CustomerDetail.フリガナ;
								break;
							case "契約者名":
								textFrame.Characters.Text = CustomerDetail.顧客名;
								break;
							case "契約者郵便番号":
								textFrame.Characters.Text = CustomerDetail.郵便番号;
								break;
							case "契約者住所":
								textFrame.Characters.Text = CustomerDetail.住所;
								break;
							case "契約者電話番号":
								textFrame.Characters.Text = CustomerDetail.電話番号;
								break;
							case "APLUSコード":
								textFrame.Characters.Text = CustomerDetail.代行回収APLUSコード.Substring(8);
								break;
						}
					}
				}
				if ("予約" == CustomerDetail.代行回収状態)
				{
					xlSheet.OLEObjects("新規登録CheckBox").Object.Value = 1;
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			finally
			{
				if (null != xlSheet)
				{
					Marshal.ReleaseComObject(xlSheet);
				}
				if (null != xlSheets)
				{
					Marshal.ReleaseComObject(xlSheets);
				}
				if (null != xlBook)
				{
					xlBook.Save();
					Marshal.ReleaseComObject(xlBook);
				}
				if (null != xlBooks)
				{
					Marshal.ReleaseComObject(xlBooks);
				}
				if (null != xlApp)
				{
					xlApp.Quit();
					Marshal.ReleaseComObject(xlApp);
				}
				MessageBox.Show(this, string.Format("{0}を保存しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
