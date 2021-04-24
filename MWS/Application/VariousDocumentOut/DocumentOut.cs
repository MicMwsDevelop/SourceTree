//
// DocumentOut.cs
// 
// 各種書類出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using ClosedXML.Excel;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace VariousDocumentOut
{
	/// <summary>
	/// 各種書類出力
	/// </summary>
	public class DocumentOut
	{
		/// <summary>
		/// 各種書類出力種別
		/// </summary>
		public enum DocumentType
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
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DocumentOut()
		{
		}

		/// <summary>
		/// EXCEL出力 - MWS IDパスワード
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutMwsIDPassword(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = "〒" + common.Customer.郵便番号;
								break;
							case "住所１":
								textFrame.Characters.Text = common.Customer.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.Customer.住所2;
								break;
							case "顧客名1":
								textFrame.Characters.Text = common.Customer.顧客名1;
								break;
							case "顧客名2":
								textFrame.Characters.Text = common.Customer.顧客名2;
								break;
							case "御中":
								textFrame.Characters.Text = "御中";
								break;
							case "発行日":
								textFrame.Characters.Text = Date.Today.GetJapaneseString(true, '0', true, true);
								break;
							case "ユーザーID":
								textFrame.Characters.Text = common.Customer.MWS_ID;
								break;
							case "初期パスワード":
								textFrame.Characters.Text = common.Customer.MWS_パスワード;
								break;
							case "初期パスワード読み":
								textFrame.Characters.Text = common.Customer.MWS_パスワード読み;
								break;
						}
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
			}
		}

		/// <summary>
		/// EXCEL出力 - FAX送付状
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutFaxLetter(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = DocumentCommon.DateTodayString();
								break;
							case "送付先":
								textFrame.Characters.Text = string.Format("{0}　御中", common.Customer.顧客名);
								break;
							case "FAX":
								textFrame.Characters.Text = string.Format("FAX {0}", common.Customer.FAX番号);
								break;
							case "送付元":
								if (common.IsHeadOffice)
								{
									// 本社
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}", common.郵便番号, common.住所1, common.社名);
								}
								else
								{
									// 拠点
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3} {4}\r\nTEL {5} FAX {6}", common.郵便番号, common.住所1, common.住所2, common.社名, common.営業部名, common.電話番号, common.FAX番号);
								}
								break;
						}
					}
				}
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 書類送付状
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutDocumentLetter(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = DocumentCommon.DateTodayString();
								break;
							case "送付先":
								textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}　御中", common.Customer.郵便番号, common.Customer.住所1, common.Customer.住所2, common.Customer.顧客名);
								break;
							case "FAX":
								textFrame.Characters.Text = string.Format("FAX {0}", common.Customer.FAX番号);
								break;
							case "送付元":
								if (common.IsHeadOffice)
								{
									// 本社
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}", common.郵便番号, common.住所1, common.社名);
								}
								else
								{
									// 拠点
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3} {4}\r\nTEL {5} FAX {6}", common.郵便番号, common.住所1, common.住所2, common.社名, common.営業部名, common.電話番号, common.FAX番号);
								}
								break;
						}
					}
				}
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 光ディスク請求届出
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutLightDisk(string pathname, DocumentCommon common)
		{
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
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet1 = xlSheets["光ディスク請求届出"] as Excel.Worksheet;
				xlShapes1 = xlSheet1.Shapes;

				string clinicCode = common.Customer.NumericClinicCode;
				string zipCode = common.Customer.NumericZipcode;

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
								textFrame.Characters.Text = common.Customer.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.Customer.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.Customer.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.Customer.住所2;
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
								textFrame.Characters.Text = common.Customer.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = common.Customer.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.Customer.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.Customer.電話番号;
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
								textFrame.Characters.Text = common.社名;
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
								textFrame.Characters.Text = common.Customer.都道府県名 + "国民健康保険団体連合会";
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
								textFrame.Characters.Text = common.Customer.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.Customer.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.Customer.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.Customer.住所2;
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
								textFrame.Characters.Text = common.Customer.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = common.Customer.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.Customer.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.Customer.電話番号;
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
								textFrame.Characters.Text = common.社名;
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
								textFrame.Characters.Text = common.Customer.都道府県名 + "国民健康保険団体連合会";
								break;
							case "宛先２":
								textFrame.Characters.Text = string.Empty;
								break;
						}
					}
				}
				// [光ディスク請求届出-社保]を選択状態
				xlSheet1.Select();
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
			}
		}

		/// <summary>
		/// EXCEL出力 - オンライン請求届出
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutOnline(string pathname, DocumentCommon common)
		{
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
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet1 = xlSheets["オンライン請求届出"] as Excel.Worksheet;
				xlShapes1 = xlSheet1.Shapes;

				string clinicCode = common.Customer.NumericClinicCode;
				string zipCode = common.Customer.NumericZipcode;

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
								textFrame.Characters.Text = common.Customer.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.Customer.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.Customer.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.Customer.住所2;
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
								textFrame.Characters.Text = common.Customer.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = common.Customer.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.Customer.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.Customer.電話番号;
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
								textFrame.Characters.Text = common.社名;
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
								textFrame.Characters.Text = common.Customer.都道府県名 + "国民健康保険団体連合会";
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
								textFrame.Characters.Text = common.Customer.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.Customer.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.Customer.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.Customer.住所2;
								break;
							case "県番号１":
								textFrame.Characters.Text = common.Customer.県番号.Substring(0, 1);
								break;
							case "県番号２":
								textFrame.Characters.Text = common.Customer.県番号.Substring(1, 1);
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
								textFrame.Characters.Text = common.Customer.フリガナ;
								break;
							case "顧客名":
								textFrame.Characters.Text = common.Customer.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = common.Customer.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.Customer.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.Customer.電話番号;
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
				// [オンライン請求届出-社保]を選択状態
				xlSheet1.Select();
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 取引条件確認書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutTransaction(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = string.Format("〒{0}", common.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = common.住所1;
								break;
							case "住所２":
								if (false == common.IsHeadOffice)
								{
									// 拠点
									textFrame.Characters.Text = common.住所2;
								}
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.Customer.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.Customer.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[36, 6] = common.Customer.フリガナ;
				xlCells[37, 6] = common.Customer.顧客名;
				xlCells[40, 6] = "〒" + common.Customer.郵便番号 + "\r\n" + common.Customer.住所;
				xlCells[40, 22] = common.Customer.電話番号;
				xlCells[41, 22] = common.Customer.FAX番号;
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 登録データ確認カード
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutConfirmCard(string pathname, DocumentCommon common)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("登録データ確認カード");
					ws.Cell(8, 7).SetValue("");
					ws.Cell(8, 21).SetValue("");
					ws.Cell(10, 7).SetValue("");
					ws.Cell(12, 7).SetValue("");
					ws.Cell(14, 7).SetValue("");
					ws.Cell(16, 7).SetValue("");
					ws.Cell(18, 7).SetValue("");
					ws.Cell(20, 7).SetValue("");
					ws.Cell(22, 7).SetValue("");
					ws.Cell(24, 7).SetValue("");
					ws.Cell(26, 7).SetValue("");
					ws.Cell(20, 19).SetValue("");
					ws.Cell(22, 19).SetValue("");
					ws.Cell(24, 19).SetValue("");
					ws.Cell(26, 19).SetValue("");
					ws.Cell(29, 7).SetValue("");
					ws.Cell(31, 7).SetValue("");
					ws.Cell(33, 7).SetValue("");
					ws.Cell(33, 15).SetValue("");
					ws.Cell(33, 23).SetValue("");
					ws.Cell(35, 7).SetValue("");
					ws.Cell(37, 7).SetValue("");

					ws.Cell(8, 7).SetValue(common.Customer.顧客No);
					ws.Cell(8, 21).SetValue(common.Customer.得意先No);
					ws.Cell(10, 7).SetValue(common.Customer.フリガナ);
					ws.Cell(12, 7).SetValue(common.Customer.顧客名);
					ws.Cell(14, 7).SetValue("〒" + common.Customer.郵便番号);
					ws.Cell(16, 7).SetValue(common.Customer.住所フリガナ);
					ws.Cell(18, 7).SetValue(common.Customer.住所);
					ws.Cell(20, 7).SetValue(common.Customer.電話番号);
					ws.Cell(22, 7).SetValue(common.Customer.医保医療コード);
					ws.Cell(24, 7).SetValue(common.Customer.院長名フリガナ);
					ws.Cell(26, 7).SetValue(common.Customer.院長名);
					ws.Cell(20, 19).SetValue(common.Customer.FAX番号);
					ws.Cell(22, 19).SetValue(common.Customer.休診日);
					ws.Cell(24, 19).SetValue(common.Customer.診療時間);
					ws.Cell(26, 19).SetValue(common.Customer.メールアドレス);
					ws.Cell(29, 7).SetValue(common.Customer.システム名称);
					ws.Cell(31, 7).SetValue(common.Customer.備考);
					ws.Cell(33, 7).SetValue(common.Customer.単体);
					ws.Cell(33, 15).SetValue(common.Customer.サーバー);
					ws.Cell(33, 23).SetValue(common.Customer.クライアント);
					ws.Cell(35, 7).SetValue(common.Customer.販売店ID + " " + common.Customer.販売店名称);
					ws.Cell(37, 7).SetValue(common.Customer.納品月);
					ws.Cell(39, 7).SetValue(common.運用サポート情報);

					// Excelファイルの保存
					wb.SaveAs(pathname);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - Microsoft365利用申請書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutMicrosoft365(string pathname, DocumentCommon common)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("Microsoft365利用申請書");
					ws.Cell(10, 11).SetValue(common.Customer.顧客No);
					ws.Cell(11, 11).SetValue(common.Customer.顧客名);
					ws.Cell(13, 11).SetValue(common.Customer.院長名);
					ws.Cell(13, 28).SetValue(common.Customer.電話番号);
					ws.Cell(14, 12).SetValue(common.Customer.郵便番号);
					ws.Cell(15, 11).SetValue(common.Customer.住所);
					ws.Cell(18, 11).SetValue(common.Customer.メールアドレス);

					// 本社情報
					ws.Cell(58, 19).SetValue(common.HeadOffice.Fax);
					ws.Cell(63, 21).SetValue(common.社名);
					ws.Cell(64, 21).SetValue(common.HeadOffice.Zipcode);
					ws.Cell(65, 21).SetValue(common.HeadOffice.Address1);
					ws.Cell(66, 21).SetValue(string.Format("e-mail {0}", common.HeadOffice.Email));
					ws.Cell(67, 21).SetValue(common.HeadOffice.Url);

					// Excelファイルの保存
					wb.SaveAs(pathname);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 請求先変更届
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutSeikyuChange(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = string.Format("〒{0}", common.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = common.住所1;
								break;
							case "住所２":
								if (false == common.IsHeadOffice)
								{
									// 拠点
									textFrame.Characters.Text = common.住所2;
								}
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.Customer.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.Customer.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[12, 23] = common.Customer.得意先No;
				xlCells[15, 5] = common.Customer.フリガナ;
				xlCells[16, 5] = common.Customer.顧客名;
				xlCells[17, 5] = common.Customer.院長名フリガナ;
				xlCells[18, 5] = common.Customer.院長名;
				xlCells[20, 5] = "〒" + common.Customer.郵便番号 + "\r\n" + common.Customer.住所1 + "\r\n" + common.Customer.住所2;
				xlCells[20, 21] = common.Customer.電話番号;
				xlCells[22, 21] = common.Customer.FAX番号;
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 終了届
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutUserFinished(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = string.Format("〒{0}", common.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = common.住所1;
								break;
							case "住所２":
								if (false == common.IsHeadOffice)
								{
									// 拠点
									textFrame.Characters.Text = common.住所2;
								}
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.Customer.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.Customer.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", DocumentCommon.DateTodayString());
				xlCells[13, 23] = common.Customer.得意先No;
				xlCells[16, 5] = common.Customer.フリガナ;
				xlCells[17, 5] = common.Customer.顧客名;
				xlCells[18, 5] = common.Customer.院長名フリガナ;
				xlCells[19, 5] = common.Customer.院長名;
				xlCells[21, 5] = "〒" + common.Customer.郵便番号 + "\r\n" + common.Customer.住所1 + "\r\n" + common.Customer.住所2;
				xlCells[21, 21] = common.Customer.電話番号;
				xlCells[23, 21] = common.Customer.FAX番号;
				xlCells[25, 5] = common.Customer.医保医療コード;
				xlCells[27, 5] = common.Customer.システム名称;
				xlCells[27, 17] = common.Customer.クライアント;
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 変更届
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutUserChange(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			Excel.Range xlCells = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = string.Format("〒{0}", common.郵便番号);
								break;
							case "住所１":
								textFrame.Characters.Text = common.住所1;
								break;
							case "住所２":
								if (false == common.IsHeadOffice)
								{
									// 拠点
									textFrame.Characters.Text = common.住所2;
								}
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.Customer.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.Customer.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", DocumentCommon.DateTodayString());
				xlCells[12, 23] = common.Customer.得意先No;
				xlCells[15, 5] = common.Customer.フリガナ;
				xlCells[16, 5] = common.Customer.顧客名;
				xlCells[17, 5] = common.Customer.院長名フリガナ;
				xlCells[18, 5] = common.Customer.院長名;
				xlCells[20, 5] = "〒" + common.Customer.郵便番号 + "\r\n" + common.Customer.住所1 + "\r\n" + common.Customer.住所2;
				xlCells[20, 21] = common.Customer.電話番号;
				xlCells[22, 21] = common.Customer.FAX番号;
				xlCells[24, 5] = common.Customer.医保医療コード;
				xlCells[26, 5] = common.Customer.システム名称;
				xlCells[26, 17] = common.Customer.クライアント;
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
			}
		}

		/// <summary>
		/// EXCEL出力 - 12-第一園芸注文書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutFirstEngei(string pathname, DocumentCommon common)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// 第一園芸医院向け
					IXLWorksheet ws1 = wb.Worksheet("第一園芸医院向け");
					ws1.Cell(6, 4).SetValue(common.Customer.郵便番号);
					ws1.Cell(7, 3).SetValue(common.Customer.住所1);
					ws1.Cell(8, 3).SetValue(common.Customer.住所2);
					ws1.Cell(9, 5).SetValue(common.Customer.顧客名);
					ws1.Cell(13, 5).SetValue(common.Customer.電話番号);

					ws1.Cell(6, 18).SetValue(common.郵便番号);
					ws1.Cell(7, 17).SetValue(common.住所1);
					ws1.Cell(8, 17).SetValue(common.住所2);
					ws1.Cell(9, 19).SetValue((common.IsHeadOffice) ? common.社名 : string.Format("{0} {1}", common.社名, common.Satellite.Branch));
					ws1.Cell(13, 19).SetValue(common.電話番号);
					ws1.Cell(13, 26).SetValue(common.FAX番号);
					ws1.Cell(20, 12).SetValue(common.社名);
					ws1.Cell(37, 6).SetValue(common.HeadOffice.Zipcode);
					ws1.Cell(38, 5).SetValue(common.HeadOffice.住所);
					// 経理部
					//ws1.Cell(40, 5).SetValue(Tel);
					//ws1.Cell(40, 15).SetValue(Fax);

					// 第一園芸販売店向け
					IXLWorksheet ws2 = wb.Worksheet("第一園芸販売店向け");
					ws2.Cell(6, 18).SetValue(common.郵便番号);
					ws2.Cell(7, 17).SetValue(common.住所1);
					ws2.Cell(8, 17).SetValue(common.住所2);
					ws2.Cell(9, 19).SetValue((common.IsHeadOffice) ? common.社名 : string.Format("{0} {1}", common.社名, common.Satellite.Branch));
					ws2.Cell(13, 19).SetValue(common.電話番号);
					ws2.Cell(13, 26).SetValue(common.FAX番号);
					ws2.Cell(20, 12).SetValue(common.社名);
					ws2.Cell(37, 6).SetValue(common.HeadOffice.Zipcode);
					ws2.Cell(38, 5).SetValue(common.HeadOffice.住所);
					// 経理部
					//ws2.Cell(40, 5).SetValue(Tel);
					//ws2.Cell(40, 15).SetValue(Fax);

					// Excelファイルの保存
					wb.SaveAs(pathname);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 13-納品補助作業依頼書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutDelivery(string pathname, DocumentCommon common)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("新興サービス注文書");
					ws.Cell(4, 24).SetValue(common.社名);
					ws.Cell(1, 27).SetValue(string.Format("{0:D4}", Date.Today.Year));
					ws.Cell(1, 30).SetValue(string.Format("{0:D2}", Date.Today.Month));
					ws.Cell(1, 32).SetValue(string.Format("{0:D2}", Date.Today.Day));
					ws.Cell(7, 27).SetValue(common.Customer.顧客No);
					ws.Cell(7, 10).SetValue(common.営業部名);
					ws.Cell(9, 10).SetValue(common.Customer.顧客名);
					ws.Cell(11, 10).SetValue(string.Format("〒{0}", common.Customer.郵便番号));
					ws.Cell(12, 10).SetValue(common.Customer.住所);
					ws.Cell(15, 10).SetValue(common.Customer.院長名);
					ws.Cell(15, 27).SetValue(common.Customer.電話番号);

					// Excelファイルの保存
					wb.SaveAs(pathname);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 14-2次キッティング依頼書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutSecondKitting(string pathname, DocumentCommon common, string saleDepartment, tMih支店情報 branch)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// 2次キッティング依頼書（医院）
					IXLWorksheet ws1 = wb.Worksheet("2次キッティング依頼書（医院）");
					// 依頼日
					ws1.Cell(2, 29).SetValue(string.Format("{0:D4}", Date.Today.Year));
					ws1.Cell(2, 32).SetValue(string.Format("{0:D2}", Date.Today.Month));
					ws1.Cell(2, 34).SetValue(string.Format("{0:D2}", Date.Today.Day));
					ws1.Cell(6, 3).SetValue("入力必須");					// ＷＷ受注番号
					//【依頼者情報】
					ws1.Cell(10, 11).SetValue((common.IsHeadOffice) ? "選択してください" : saleDepartment);	// MIC担当拠点
					ws1.Cell(10, 28).SetValue("-");							// MIC担当者
					ws1.Cell(11, 11).SetValue("-");							// 希望納期
					ws1.Cell(11, 16).SetValue("-");
					ws1.Cell(11, 19).SetValue("-");
					ws1.Cell(11, 28).SetValue("医院直送");					// 納品先
					ws1.Cell(12, 12).SetValue(common.Customer.郵便番号);	// 納品先郵便番号
					ws1.Cell(12, 28).SetValue(common.Customer.電話番号);	// 納品先電話番号
					ws1.Cell(13, 11).SetValue(common.Customer.住所);		// 納品先住所
					//【歯科医院情報】
					ws1.Cell(18, 11).SetValue(common.Customer.顧客No);
					ws1.Cell(19, 11).SetValue(common.Customer.顧客名);
					if (0 == common.Customer.院長名.Length)
					{
						ws1.Cell(21, 11).SetValue("入力必須");
						ws1.Cell(21, 28).SetValue("入力必須");
					}
					else
					{
						ws1.Cell(21, 11).SetValue(common.Customer.院長名);
						ws1.Cell(21, 28).SetValue(common.Customer.院長名);
					}
					ws1.Cell(22, 12).SetValue(common.Customer.郵便番号);
					ws1.Cell(23, 11).SetValue(common.Customer.住所);
					ws1.Cell(26, 11).SetValue(common.Customer.電話番号);
					ws1.Cell(26, 28).SetValue(common.Customer.FAX番号);
					ws1.Cell(27, 11).SetValue("入力必須");
					ws1.Cell(27, 28).SetValue(common.Customer.医保医療コード);
					ws1.Cell(28, 11).SetValue("-");
					//【システム構成情報】
					ws1.Cell(31, 11).SetValue("-");
					ws1.Cell(31, 28).SetValue("-");
					//【システム構成詳細】
					ws1.Cell(36, 10).SetValue("-");
					ws1.Cell(36, 15).SetValue("-");
					ws1.Cell(36, 20).SetValue("-");
					ws1.Cell(36, 25).SetValue("-");
					ws1.Cell(36, 30).SetValue("-");
					ws1.Cell(37, 10).SetValue("-");
					ws1.Cell(37, 15).SetValue("-");
					ws1.Cell(37, 20).SetValue("-");
					ws1.Cell(37, 25).SetValue("-");
					ws1.Cell(37, 30).SetValue("-");
					ws1.Cell(38, 10).SetValue("-");
					ws1.Cell(38, 15).SetValue("-");
					ws1.Cell(38, 20).SetValue("-");
					ws1.Cell(38, 25).SetValue("-");
					ws1.Cell(38, 30).SetValue("-");
					ws1.Cell(39, 10).SetValue("-");
					ws1.Cell(39, 15).SetValue("-");
					ws1.Cell(39, 20).SetValue("-");
					ws1.Cell(39, 25).SetValue("-");
					ws1.Cell(39, 30).SetValue("-");
					ws1.Cell(40, 10).SetValue("-");
					ws1.Cell(40, 15).SetValue("-");
					ws1.Cell(40, 20).SetValue("-");
					ws1.Cell(40, 25).SetValue("-");
					ws1.Cell(40, 30).SetValue("-");
					ws1.Cell(41, 10).SetValue("-");
					ws1.Cell(41, 15).SetValue("-");
					ws1.Cell(41, 20).SetValue("-");
					ws1.Cell(41, 25).SetValue("-");
					ws1.Cell(41, 30).SetValue("-");
					ws1.Cell(42, 10).SetValue("-");
					ws1.Cell(42, 15).SetValue("-");
					ws1.Cell(42, 20).SetValue("-");
					ws1.Cell(42, 25).SetValue("-");
					ws1.Cell(42, 30).SetValue("-");
					ws1.Cell(43, 10).SetValue("-");
					ws1.Cell(43, 15).SetValue("-");
					ws1.Cell(43, 20).SetValue("-");
					ws1.Cell(43, 25).SetValue("-");
					ws1.Cell(43, 30).SetValue("-");
					//【IPアドレス(固定の場合のみ記載)】
					ws1.Cell(47, 10).SetValue("-");
					ws1.Cell(47, 15).SetValue("-");
					ws1.Cell(47, 20).SetValue("-");
					ws1.Cell(47, 25).SetValue("-");
					ws1.Cell(47, 30).SetValue("-");
					ws1.Cell(48, 10).SetValue("-");
					ws1.Cell(48, 15).SetValue("-");
					ws1.Cell(48, 20).SetValue("-");
					ws1.Cell(48, 25).SetValue("-");
					ws1.Cell(48, 30).SetValue("-");
					ws1.Cell(49, 10).SetValue("-");
					ws1.Cell(49, 15).SetValue("-");
					ws1.Cell(49, 20).SetValue("-");
					ws1.Cell(49, 25).SetValue("-");
					ws1.Cell(49, 30).SetValue("-");
					ws1.Cell(50, 10).SetValue("-");
					ws1.Cell(50, 15).SetValue("-");
					ws1.Cell(50, 20).SetValue("-");
					ws1.Cell(50, 25).SetValue("-");
					ws1.Cell(50, 30).SetValue("-");
					ws1.Cell(51, 10).SetValue("-");
					ws1.Cell(51, 15).SetValue("-");
					ws1.Cell(51, 20).SetValue("-");
					ws1.Cell(51, 25).SetValue("-");
					ws1.Cell(51, 30).SetValue("-");
					//【PLM・訪問診療使用時のパラメ-ター】
					ws1.Cell(55, 9).SetValue("PC-00");
					ws1.Cell(55, 23).SetValue(@"\\PC-00\MIC_PALETTE\");
					//【備考】
					ws1.Cell(59, 3).SetValue("");

					// 2次キッティング依頼書（拠点）
					IXLWorksheet ws2 = wb.Worksheet("2次キッティング依頼書（拠点）");
					// 依頼日
					ws2.Cell(2, 29).SetValue(string.Format("{0:D4}", Date.Today.Year));
					ws2.Cell(2, 32).SetValue(string.Format("{0:D2}", Date.Today.Month));
					ws2.Cell(2, 34).SetValue(string.Format("{0:D2}", Date.Today.Day));
					ws2.Cell(6, 3).SetValue("入力必須");			// ＷＷ受注番号
					//【依頼者情報】
					ws2.Cell(10, 11).SetValue((common.IsHeadOffice) ? "選択してください" : saleDepartment);	// MIC担当拠点
					ws2.Cell(10, 28).SetValue("-");					// MIC担当者
					ws2.Cell(11, 11).SetValue("-");					// 希望納期
					ws2.Cell(11, 16).SetValue("-");
					ws2.Cell(11, 19).SetValue("-");
					if (false == common.IsHeadOffice)
					{
						// 営業部
						ws2.Cell(11, 28).SetValue(string.Format("{0} {1}", common.社名, branch.f支店名));	// 納品先
						ws2.Cell(12, 12).SetValue(branch.f郵便番号);	// 納品先郵便番号
						ws2.Cell(12, 28).SetValue(branch.f電話番号);	// 納品先電話番号
						ws2.Cell(13, 11).SetValue(branch.住所);			// 納品先住所
					}
					else
					{
						// 本社
						ws2.Cell(11, 28).SetValue(common.社名);					// 納品先
						ws2.Cell(12, 12).SetValue(common.HeadOffice.Zipcode);	// 納品先郵便番号
						ws2.Cell(12, 28).SetValue(common.HeadOffice.Tel);		// 納品先電話番号
						ws2.Cell(13, 11).SetValue(common.HeadOffice.住所);		// 納品先住所
					}
					//【歯科医院情報】
					ws2.Cell(18, 11).SetValue(common.Customer.顧客No);
					ws2.Cell(19, 11).SetValue(common.Customer.顧客名);
					if (0 == common.Customer.院長名.Length)
					{
						ws2.Cell(21, 11).SetValue("入力必須");
						ws2.Cell(21, 28).SetValue("入力必須");
					}
					else
					{
						ws2.Cell(21, 11).SetValue(common.Customer.院長名);
						ws2.Cell(21, 28).SetValue(common.Customer.院長名);
					}
					ws2.Cell(22, 12).SetValue(common.Customer.郵便番号);
					ws2.Cell(23, 11).SetValue(common.Customer.住所);
					ws2.Cell(26, 11).SetValue(common.Customer.電話番号);
					ws2.Cell(26, 28).SetValue(common.Customer.FAX番号);
					ws2.Cell(27, 11).SetValue("入力必須");
					ws2.Cell(27, 28).SetValue(common.Customer.医保医療コード);
					ws2.Cell(28, 11).SetValue("-");
					//【システム構成情報】
					ws2.Cell(31, 11).SetValue("-");
					ws2.Cell(31, 28).SetValue("-");
					//【システム構成詳細】
					ws2.Cell(36, 10).SetValue("-");
					ws2.Cell(36, 15).SetValue("-");
					ws2.Cell(36, 20).SetValue("-");
					ws2.Cell(36, 25).SetValue("-");
					ws2.Cell(36, 30).SetValue("-");
					ws2.Cell(37, 10).SetValue("-");
					ws2.Cell(37, 15).SetValue("-");
					ws2.Cell(37, 20).SetValue("-");
					ws2.Cell(37, 25).SetValue("-");
					ws2.Cell(37, 30).SetValue("-");
					ws2.Cell(38, 10).SetValue("-");
					ws2.Cell(38, 15).SetValue("-");
					ws2.Cell(38, 20).SetValue("-");
					ws2.Cell(38, 25).SetValue("-");
					ws2.Cell(38, 30).SetValue("-");
					ws2.Cell(39, 10).SetValue("-");
					ws2.Cell(39, 15).SetValue("-");
					ws2.Cell(39, 20).SetValue("-");
					ws2.Cell(39, 25).SetValue("-");
					ws2.Cell(39, 30).SetValue("-");
					ws2.Cell(40, 10).SetValue("-");
					ws2.Cell(40, 15).SetValue("-");
					ws2.Cell(40, 20).SetValue("-");
					ws2.Cell(40, 25).SetValue("-");
					ws2.Cell(40, 30).SetValue("-");
					ws2.Cell(41, 10).SetValue("-");
					ws2.Cell(41, 15).SetValue("-");
					ws2.Cell(41, 20).SetValue("-");
					ws2.Cell(41, 25).SetValue("-");
					ws2.Cell(41, 30).SetValue("-");
					ws2.Cell(42, 10).SetValue("-");
					ws2.Cell(42, 15).SetValue("-");
					ws2.Cell(42, 20).SetValue("-");
					ws2.Cell(42, 25).SetValue("-");
					ws2.Cell(42, 30).SetValue("-");
					ws2.Cell(43, 10).SetValue("-");
					ws2.Cell(43, 15).SetValue("-");
					ws2.Cell(43, 20).SetValue("-");
					ws2.Cell(43, 25).SetValue("-");
					ws2.Cell(43, 30).SetValue("-");
					//【IPアドレス(固定の場合のみ記載)】
					ws2.Cell(47, 10).SetValue("-");
					ws2.Cell(47, 15).SetValue("-");
					ws2.Cell(47, 20).SetValue("-");
					ws2.Cell(47, 25).SetValue("-");
					ws2.Cell(47, 30).SetValue("-");
					ws2.Cell(48, 10).SetValue("-");
					ws2.Cell(48, 15).SetValue("-");
					ws2.Cell(48, 20).SetValue("-");
					ws2.Cell(48, 25).SetValue("-");
					ws2.Cell(48, 30).SetValue("-");
					ws2.Cell(49, 10).SetValue("-");
					ws2.Cell(49, 15).SetValue("-");
					ws2.Cell(49, 20).SetValue("-");
					ws2.Cell(49, 25).SetValue("-");
					ws2.Cell(49, 30).SetValue("-");
					ws2.Cell(50, 10).SetValue("-");
					ws2.Cell(50, 15).SetValue("-");
					ws2.Cell(50, 20).SetValue("-");
					ws2.Cell(50, 25).SetValue("-");
					ws2.Cell(50, 30).SetValue("-");
					ws2.Cell(51, 10).SetValue("-");
					ws2.Cell(51, 15).SetValue("-");
					ws2.Cell(51, 20).SetValue("-");
					ws2.Cell(51, 25).SetValue("-");
					ws2.Cell(51, 30).SetValue("-");
					//【PLM・訪問診療使用時のパラメ-ター】
					ws2.Cell(55, 9).SetValue("PC-00");
					ws2.Cell(55, 23).SetValue(@"\\PC-00\MIC_PALETTE\");
					//【備考】
					ws2.Cell(59, 3).SetValue("");

					// Excelファイルの保存
					wb.SaveAs(pathname);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - PC安心サポート加入申込書 or PC安心サポートPlus加入申込書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutPcSupport(string pathname, DocumentCommon common)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// PC安心サポート加入申込書
					IXLWorksheet ws1 = wb.Worksheet("PC安心サポート加入申込書");
					ws1.Cell(11, 11).SetValue(common.Customer.得意先No);
					ws1.Cell(12, 11).SetValue(common.Customer.顧客No);
					ws1.Cell(13, 11).SetValue(common.Customer.顧客名);
					ws1.Cell(15, 11).SetValue(common.Customer.電話番号);
					ws1.Cell(16, 12).SetValue(common.Customer.郵便番号);
					ws1.Cell(17, 11).SetValue(common.Customer.住所1 + "\r\n" + common.Customer.住所2);
					ws1.Cell(20, 11).SetValue(common.Customer.メールアドレス);
					ws1.Cell(21, 11).SetValue(common.Customer.支店名);
					ws1.Cell(21, 25).SetValue(common.Customer.営業担当者名);
					ws1.Cell(48, 12).SetValue(common.社名);

					// PC安心サポートPlus加入申込書
					IXLWorksheet ws2 = wb.Worksheet("PC安心サポートPlus加入申込書");
					ws2.Cell(11, 11).SetValue(common.Customer.得意先No);
					ws2.Cell(12, 11).SetValue(common.Customer.顧客No);
					ws2.Cell(13, 11).SetValue(common.Customer.顧客名);
					ws2.Cell(15, 11).SetValue(common.Customer.電話番号);
					ws2.Cell(16, 12).SetValue(common.Customer.郵便番号);
					ws2.Cell(17, 11).SetValue(common.Customer.住所1 + "\r\n" + common.Customer.住所2);
					ws2.Cell(20, 11).SetValue(common.Customer.メールアドレス);
					ws2.Cell(21, 11).SetValue(common.Customer.支店名);
					ws2.Cell(21, 25).SetValue(common.Customer.営業担当者名);
					ws2.Cell(48, 12).SetValue(common.社名);

					// Excelファイルの保存
					wb.SaveAs(pathname);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - アプラス預金口座振替依頼書・自動払込利用申込書
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="common">各種書類出力 共通情報</param>
		public static void ExcelOutAplus(string pathname, DocumentCommon common)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
								textFrame.Characters.Text = common.Customer.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.Customer.得意先No;
								break;
							case "契約者名フリガナ":
								textFrame.Characters.Text = common.Customer.フリガナ;
								break;
							case "契約者名":
								textFrame.Characters.Text = common.Customer.顧客名;
								break;
							case "契約者郵便番号":
								textFrame.Characters.Text = common.Customer.郵便番号;
								break;
							case "契約者住所":
								textFrame.Characters.Text = common.Customer.住所;
								break;
							case "契約者電話番号":
								textFrame.Characters.Text = common.Customer.電話番号;
								break;
							case "APLUSコード":
								textFrame.Characters.Text = common.Customer.代行回収APLUSコード.Substring(8);
								break;
						}
					}
				}
				if ("予約" == common.Customer.代行回収状態)
				{
					xlSheet.OLEObjects("新規登録CheckBox").Object.Value = 1;
				}
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
			}
		}
	}
}
