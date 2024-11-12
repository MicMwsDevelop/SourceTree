//
// DocumentOut.cs
// 
// 各種書類出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/04/22):新規作成
// Ver1.02(2021/09/01):Microsoft365利用申込書のFAX番号を本社から消耗品受注センターに変更
// Ver1.03(2021/09/28):5 オンライン請求届出 電子証明書発行等依頼内訳に対応
// Ver1.05(2021/11/12):消耗品FAXオーダーシートの新規追加
// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
// Ver1.08(2022/01/14):5 オンライン請求届出 電子情報処理組織の使用による費用の請求に関する届出 新用紙対応
// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
// Ver1.12(2022/02/22):経理部専用 オンライン資格確認等事業完了報告書 修正依頼対応
// Ver1.13(2022/05/02):Microsoft365利用申込書新フォーム対応
// Ver1.13(2022/05/09):アプラス預金口座振替依頼書・自動払込利用申込書新フォーム対応
// Ver1.14(2022/06/16):8-Microsoft365利用申込書 拠点FAX番号対応
// Ver1.14(2022/06/30):8-Microsoft365利用申込書 新様式対応
// Ver1.15(2023/01/13):19-経理部専用 オンライン資格確認等事業完了報告書 注文確認書の追加、領収証および書類送付状の削除
// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 医療機関コードが7文字でない時にアプリケーションエラーが発生
// Ver1.19(2023/04/13 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 「領収書内訳書」の顧客名の後ろに様を付加しない
// Ver1.19(2023/04/13 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 「領収書内訳書」と「事業完了報告書」の医療機関コードが出力されない
// Ver1.20(2023/06/09 勝呂):2-FAX送付状、3-種類送付状が販売店の時に出力できない
//
using ClosedXML.Excel;
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VariousDocumentOut.Settings;
using Excel = Microsoft.Office.Interop.Excel;

namespace VariousDocumentOut.BaseFactory
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
			OnlineApply,

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
			// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
			//SecondKitting,

			/// <summary>
			/// PC安心サポート加入申込書
			/// </summary>
			PcSupport,

			/// <summary>
			/// アプラス預金口座振替依頼書・自動払込利用申込書
			/// </summary>
			Aplus,

			/// <summary>
			/// 作業報告書
			/// </summary>
			WorkReport,

			/// <summary>
			/// 消耗品FAXオーダーシート
			/// </summary>
			/// Ver1.05(2021/11/12):消耗品FAXオーダーシートの新規追加
			FaxOrderSheet,

			/// <summary>
			/// オンライン資格確認等事業完了報告書(経理部専用)
			/// </summary>
			// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
			OnlineConfirmKeiri,
		}

		/// <summary>
		/// 各種書類出力種別→Excelオリジナルファイル名
		/// </summary>
		public static readonly EnumDictionary<DocumentType, string> OrgFileName = new EnumDictionary<DocumentType, string>()
		{
			{ DocumentType.MwsIDPassword, "1-MWSIDパスワード.xlsx.org" },
			{ DocumentType.FaxLetter, "2-FAX送付状.xlsx.org" },
			{ DocumentType.DocumentLetter, "3-書類送付状.xlsx.org" },
			{ DocumentType.LightDisk, "4-光ディスク請求届出.xlsx.org" },
			{ DocumentType.OnlineApply, "5-オンライン請求届出.xlsx.org" },
			{ DocumentType.Transaction, "6-取引条件確認書.xlsx.org" },
			{ DocumentType.ConfirmCard, "7-登録データ確認カード.xlsx.org" },
			{ DocumentType.Microsoft365, "8-Microsoft365利用申請書.xlsx.org" },
			{ DocumentType.SeikyuChange, "9-請求先変更届.xlsx.org" },
			{ DocumentType.UserFinished, "10-終了届.xlsx.org" },
			{ DocumentType.UserChange, "11-変更届.xlsx.org" },
			{ DocumentType.FirstEngei, "12-第一園芸注文書.xlsx.org" },
			{ DocumentType.Delivery, "13-納品補助作業依頼書.xlsx.org" },
			//{ DocumentType.SecondKitting, "14-2次キッティング依頼書.xlsx.org" },
			{ DocumentType.PcSupport, "15-PC安心サポート加入申込書.xlsx.org" },
			{ DocumentType.Aplus, "16-アプラス預金口座振替依頼書・自動払込利用申込書.xlsx.org" },
			{ DocumentType.WorkReport, "17-作業報告書.xlsx.org" },
			{ DocumentType.FaxOrderSheet, "18-消耗品FAXオーダーシート.xlsx.org" },
			{ DocumentType.OnlineConfirmKeiri, "19-オンライン資格確認等事業完了報告書.xlsx.org" },
		};

		/// <summary>
		/// 各種書類出力種別→Excelファイル名
		/// </summary>
		public static readonly EnumDictionary<DocumentType, string> ExcelFileName = new EnumDictionary<DocumentType, string>()
		{
			{ DocumentType.MwsIDPassword, "1-MWSIDパスワード.xlsx" },
			{ DocumentType.FaxLetter, "2-FAX送付状.xlsx" },
			{ DocumentType.DocumentLetter, "3-書類送付状.xlsx" },
			{ DocumentType.LightDisk, "4-光ディスク請求届出.xlsx" },
			{ DocumentType.OnlineApply, "5-オンライン請求届出.xlsx" },
			{ DocumentType.Transaction, "6-取引条件確認書.xlsx" },
			{ DocumentType.ConfirmCard, "7-登録データ確認カード.xlsx" },
			{ DocumentType.Microsoft365, "8-Microsoft365利用申請書.xlsx" },
			{ DocumentType.SeikyuChange, "9-請求先変更届.xlsx" },
			{ DocumentType.UserFinished, "10-終了届.xlsx" },
			{ DocumentType.UserChange, "11-変更届.xlsx" },
			{ DocumentType.FirstEngei, "12-第一園芸注文書.xlsx" },
			{ DocumentType.Delivery, "13-納品補助作業依頼書.xlsx" },
			//{ DocumentType.SecondKitting, "14-2次キッティング依頼書.xlsx" },
			{ DocumentType.PcSupport, "15-PC安心サポート加入申込書.xlsx" },
			{ DocumentType.Aplus, "16-アプラス預金口座振替依頼書・自動払込利用申込書.xlsx" },
			{ DocumentType.WorkReport, "17-作業報告書.xlsx" },
			{ DocumentType.FaxOrderSheet, "18-消耗品FAXオーダーシート.xlsx" },
			{ DocumentType.OnlineConfirmKeiri, "19-オンライン資格確認等事業完了報告書.xlsx" },
		};

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DocumentOut()
		{
		}

		/// <summary>
		/// EXCEL出力 - 1 MWS IDパスワード
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutMwsIDPassword(DocumentCommon common, string pathname)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["MWS_IDPW"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "郵便番号":
								textFrame.Characters.Text = "〒" + common.顧客情報.郵便番号;
								break;
							case "住所１":
								textFrame.Characters.Text = common.顧客情報.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.顧客情報.住所2;
								break;
							case "顧客名1":
								textFrame.Characters.Text = common.顧客情報.顧客名1;
								break;
							case "顧客名2":
								textFrame.Characters.Text = common.顧客情報.顧客名2;
								break;
							case "御中":
								textFrame.Characters.Text = "御中";
								break;
							case "発行日":
								textFrame.Characters.Text = Date.Today.GetJapaneseString(true, '0', true, true);
								break;
							case "ユーザーID":
								textFrame.Characters.Text = common.顧客情報.MWS_ID;
								break;
							case "初期パスワード":
								textFrame.Characters.Text = common.顧客情報.MWS_パスワード;
								break;
							case "初期パスワード読み":
								textFrame.Characters.Text = common.顧客情報.MWS_パスワード読み;
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 2 FAX送付状
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutFaxLetter(DocumentCommon common, string pathname)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["FAX送付状"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "日付":
								textFrame.Characters.Text = DocumentCommon.DateTodayString();
								break;
							case "送付先":
								textFrame.Characters.Text = string.Format("{0}　御中", common.顧客情報.顧客名);
								break;
							case "FAX":
								textFrame.Characters.Text = string.Format("FAX {0}", common.顧客情報.FAX番号);
								break;
							case "送付元":
								if (common.IsHeadOffice)
								{
									// 本社
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}", common.郵便番号, common.住所1, common.住所2, common.社名);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 3 書類送付状
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutDocumentLetter(DocumentCommon common, string pathname)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["書類送付状"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "日付":
								textFrame.Characters.Text = DocumentCommon.DateTodayString();
								break;
							case "送付先":
								textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}　御中", common.顧客情報.郵便番号, common.顧客情報.住所1, common.顧客情報.住所2, common.顧客情報.顧客名);
								break;
							case "FAX":
								textFrame.Characters.Text = string.Format("FAX {0}", common.顧客情報.FAX番号);
								break;
							case "送付元":
								if (common.IsHeadOffice)
								{
									// 本社
									textFrame.Characters.Text = string.Format("〒{0}\r\n{1}\r\n{2}\r\n{3}", common.郵便番号, common.住所1, common.住所2, common.社名);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 4 光ディスク請求届出
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutLightDisk(DocumentCommon common, string pathname)
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
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet1 = xlSheets["光ディスク請求届出"] as Excel.Worksheet;
				xlShapes1 = xlSheet1.Shapes;

				string clinicCode = common.顧客情報.NumericClinicCode;
				string zipCode = common.顧客情報.NumericZipcode;

				// 光ディスク請求届出-社保用
				foreach (Excel.Shape shape in xlShapes1)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = common.顧客情報.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.顧客情報.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.顧客情報.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.顧客情報.住所2;
								break;
							case "医療機関コード１":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(0, 1);
								}
								break;
							case "医療機関コード２":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(1, 1);
								}
								break;
							case "医療機関コード３":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(2, 1);
								}
								break;
							case "医療機関コード４":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(3, 1);
								}
								break;
							case "医療機関コード５":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(4, 1);
								}
								break;
							case "医療機関コード６":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(5, 1);
								}
								break;
							case "医療機関コード７":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(6, 1);
								}
								break;
							case "顧客名":
								textFrame.Characters.Text = common.顧客情報.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = common.顧客情報.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.顧客情報.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.顧客情報.電話番号;
								break;
							case "郵便番号１":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(0, 1);
								}
								break;
							case "郵便番号２":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(1, 1);
								}
								break;
							case "郵便番号３":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(2, 1);
								}
								break;
							case "郵便番号４":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(3, 1);
								}
								break;
							case "郵便番号５":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(4, 1);
								}
								break;
							case "郵便番号６":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(5, 1);
								}
								break;
							case "郵便番号７":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(6, 1);
								}
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
								textFrame.Characters.Text = common.顧客情報.都道府県名 + "国民健康保険団体連合会";
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
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = common.顧客情報.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.顧客情報.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.顧客情報.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.顧客情報.住所2;
								break;
							case "医療機関コード１":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(0, 1);
								}
								break;
							case "医療機関コード２":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(1, 1);
								}
								break;
							case "医療機関コード３":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(2, 1);
								}
								break;
							case "医療機関コード４":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(3, 1);
								}
								break;
							case "医療機関コード５":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(4, 1);
								}
								break;
							case "医療機関コード６":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(5, 1);
								}
								break;
							case "医療機関コード７":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(6, 1);
								}
								break;
							case "顧客名":
								textFrame.Characters.Text = common.顧客情報.顧客名;
								break;
							case "住所":
								textFrame.Characters.Text = common.顧客情報.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.顧客情報.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.顧客情報.電話番号;
								break;
							case "郵便番号１":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(0, 1);
								}
								break;
							case "郵便番号２":
								if (0 < zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(1, 1);
								}
								break;
							case "郵便番号３":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(2, 1);
								}
								break;
							case "郵便番号４":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(3, 1);
								}
								break;
							case "郵便番号５":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(4, 1);
								}
								break;
							case "郵便番号６":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(5, 1);
								}
								break;
							case "郵便番号７":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(6, 1);
								}
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
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = common.顧客情報.都道府県名 + "国民健康保険団体連合会";
								break;
							case "宛先２":
								textFrame.Characters.Text = string.Empty;
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 5 オンライン請求届出
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="xlsPathname">Excelファイルパス名</param>
		/// <param name="orgPathname">Excelファイルパス名(org)</param>
		public static void ExcelOutOnline(DocumentCommon common, string xlsPathname, string orgPathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(xlsPathname, XLEventTracking.Disabled))
				{
					wb.Worksheet("オンライン請求届出").Delete();

					// 電子証明書発行等依頼書
					IXLWorksheet ws1 = wb.Worksheet("電子証明書発行等依頼書");

					// 基金支部名
					ws1.Cell(6, 2).SetValue(common.顧客情報.支部名);

					// 都道府県
					if (2 == common.顧客情報.県番号.Length)
					{
						ws1.Cell(11, 38).SetValue(common.顧客情報.県番号.Substring(0, 1));
						ws1.Cell(11, 42).SetValue(common.顧客情報.県番号.Substring(1, 1));
					}
					// 機関コード
					string clinicCode2 = common.顧客情報.NumericClinicCode;
					if (7 == clinicCode2.Length)
					{
						ws1.Cell(11, 50).SetValue(clinicCode2.Substring(0, 1));
						ws1.Cell(11, 54).SetValue(clinicCode2.Substring(1, 1));
						ws1.Cell(11, 58).SetValue(clinicCode2.Substring(2, 1));
						ws1.Cell(11, 62).SetValue(clinicCode2.Substring(3, 1));
						ws1.Cell(11, 66).SetValue(clinicCode2.Substring(4, 1));
						ws1.Cell(11, 70).SetValue(clinicCode2.Substring(5, 1));
						ws1.Cell(11, 74).SetValue(clinicCode2.Substring(6, 1));
					}
					ws1.Cell(13, 19).SetValue(common.顧客情報.フリガナ);
					ws1.Cell(14, 19).SetValue(common.顧客情報.顧客名);

					// 郵便番号
					string zipcode = common.顧客情報.NumericZipcode;
					if (7 == zipcode.Length)
					{
						ws1.Cell(16, 14).SetValue(zipcode.Substring(0, 1));
						ws1.Cell(16, 16).SetValue(zipcode.Substring(1, 1));
						ws1.Cell(16, 18).SetValue(zipcode.Substring(2, 1));
						ws1.Cell(16, 22).SetValue(zipcode.Substring(3, 1));
						ws1.Cell(16, 24).SetValue(zipcode.Substring(4, 1));
						ws1.Cell(16, 26).SetValue(zipcode.Substring(5, 1));
						ws1.Cell(16, 28).SetValue(zipcode.Substring(6, 1));
					}
					ws1.Cell(17, 15).SetValue(common.顧客情報.住所);
					ws1.Cell(18, 13).SetValue(common.顧客情報.電話番号);
					ws1.Cell(18, 51).SetValue(common.顧客情報.メールアドレス);

					// 電子証明書発行等依頼内訳
					// Ver1.03(2021/09/28):5 オンライン請求届出 電子証明書発行等依頼内訳に対応
					IXLWorksheet ws2 = wb.Worksheet("電子証明書発行等依頼内訳");

					// 都道府県
					if (2 == common.顧客情報.県番号.Length)
					{
						ws2.Cell(9, 73).SetValue(common.顧客情報.県番号.Substring(0, 1));
						ws2.Cell(9, 80).SetValue(common.顧客情報.県番号.Substring(1, 1));
					}
					// 機関コード
					if (7 == clinicCode2.Length)
					{
						ws2.Cell(9, 96).SetValue(clinicCode2.Substring(0, 1));
						ws2.Cell(9, 103).SetValue(clinicCode2.Substring(1, 1));
						ws2.Cell(9, 110).SetValue(clinicCode2.Substring(2, 1));
						ws2.Cell(9, 117).SetValue(clinicCode2.Substring(3, 1));
						ws2.Cell(9, 124).SetValue(clinicCode2.Substring(4, 1));
						ws2.Cell(9, 131).SetValue(clinicCode2.Substring(5, 1));
						ws2.Cell(9, 138).SetValue(clinicCode2.Substring(6, 1));
					}
					// 機関名称
					ws2.Cell(12, 24).SetValue(common.顧客情報.顧客名);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet1 = null;
			Excel.Workbook xlBookOrg = null;
			Excel.Worksheet xlSheetOrg1 = null;
			Excel.Worksheet xlSheet2 = null;
			Excel.Worksheet xlSheet3 = null;
			Excel.Worksheet xlSheet4 = null;
			Excel.Shapes xlShapes1 = null;
			Excel.Shapes xlShapes2 = null;
			Excel.Shapes xlShapes3 = null;
			Excel.Shapes xlShapes4 = null;
			try
			{
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(xlsPathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet3 = xlSheets["電子証明書発行等依頼書"] as Excel.Worksheet;
				xlShapes3 = xlSheet3.Shapes;

				//「電子証明書発行等依頼書」新規発行丸付け
				Microsoft.Office.Interop.Excel.Shape oval3 = xlShapes3.AddShape(MsoAutoShapeType.msoShapeOval, 215, 122, 75, 20);
				oval3.Fill.Visible = MsoTriState.msoFalse;
				oval3.Line.ForeColor.RGB = System.Drawing.Color.Black.ToArgb();

				//「電子証明書発行等依頼内訳」新規発行丸付け
				xlSheet4 = xlSheets["電子証明書発行等依頼内訳"] as Excel.Worksheet;
				xlShapes4 = xlSheet4.Shapes;
				Microsoft.Office.Interop.Excel.Shape oval4 = xlShapes4.AddShape(MsoAutoShapeType.msoShapeOval, 125, 95, 70, 20);
				oval4.Fill.Visible = MsoTriState.msoFalse;
				oval4.Line.ForeColor.RGB = System.Drawing.Color.Black.ToArgb();

				xlBookOrg = xlBooks.Open(orgPathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheetOrg1 = xlBookOrg.Worksheets["オンライン請求届出"] as Excel.Worksheet;
				xlSheetOrg1.Copy(xlSheet3, Type.Missing);   // 「電子証明書発行等依頼書」の前に「オンライン請求届出」をコピー

				xlSheet1 = xlSheets["オンライン請求届出"] as Excel.Worksheet;
				xlShapes1 = xlSheet1.Shapes;

				string clinicCode = common.顧客情報.NumericClinicCode;
				string zipCode = common.顧客情報.NumericZipcode;

				// オンライン請求届出-社保用
				foreach (Excel.Shape shape in xlShapes1)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = "社会保険診療報酬支払基金";
								break;
							case "宛先２":
								textFrame.Characters.Text = common.顧客情報.支部名;
								break;
							case "院長名":
								textFrame.Characters.Text = common.顧客情報.院長名;
								break;
							case "住所１":
								textFrame.Characters.Text = common.顧客情報.住所1;
								break;
							case "住所２":
								textFrame.Characters.Text = common.顧客情報.住所2;
								break;
							case "医療機関コード１":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(0, 1);
								}
								break;
							case "医療機関コード２":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(1, 1);
								}
								break;
							case "医療機関コード３":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(2, 1);
								}
								break;
							case "医療機関コード４":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(3, 1);
								}
								break;
							case "医療機関コード５":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(4, 1);
								}
								break;
							case "医療機関コード６":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(5, 1);
								}
								break;
							case "医療機関コード７":
								if (7 == clinicCode.Length)
								{
									textFrame.Characters.Text = clinicCode.Substring(6, 1);
								}
								break;
							case "顧客名":
								if (0 < clinicCode.Length)
								{
									textFrame.Characters.Text = common.顧客情報.顧客名;
								}
								break;
							case "住所":
								textFrame.Characters.Text = common.顧客情報.住所;
								break;
							case "システム名称":
								textFrame.Characters.Text = common.顧客情報.システム略称;
								break;
							case "電話番号":
								textFrame.Characters.Text = common.顧客情報.電話番号;
								break;
							case "郵便番号１":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(0, 1);
								}
								break;
							case "郵便番号２":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(1, 1);
								}
								break;
							case "郵便番号３":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(2, 1);
								}
								break;
							case "郵便番号４":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(3, 1);
								}
								break;
							case "郵便番号５":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(4, 1);
								}
								break;
							case "郵便番号６":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(5, 1);
								}
								break;
							case "郵便番号７":
								if (7 == zipCode.Length)
								{
									textFrame.Characters.Text = zipCode.Substring(6, 1);
								}
								break;
							case "メーカー名":
								textFrame.Characters.Text = common.社名;
								break;
						}
					}
				}
				// オンライン請求届出-国保用
				xlSheet1.Copy(Type.Missing, xlSheet1);  //「オンライン請求届出」の後に同シートをコピー
				xlSheet2 = xlSheets["オンライン請求届出 (2)"] as Excel.Worksheet;
				xlSheet1.Name = "オンライン請求届出-社保";
				xlSheet2.Name = "オンライン請求届出-国保";
				xlShapes2 = xlSheet2.Shapes;
				foreach (Excel.Shape shape in xlShapes2)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "宛先１":
								textFrame.Characters.Text = common.顧客情報.都道府県名 + "国民健康保険団体連合会";
								break;
							case "宛先２":
								textFrame.Characters.Text = string.Empty;
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
				if (null != xlSheet1)
				{
					Marshal.ReleaseComObject(xlSheet1);
				}
				if (null != xlSheetOrg1)
				{
					Marshal.ReleaseComObject(xlSheetOrg1);
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
				if (null != xlBookOrg)
				{
					xlBookOrg.Close();
					Marshal.ReleaseComObject(xlBookOrg);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 6 取引条件確認書
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutTransaction(DocumentCommon common, string pathname)
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
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["取引条件確認書"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
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
								textFrame.Characters.Text = common.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.顧客情報.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.顧客情報.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[36, 6] = common.顧客情報.フリガナ;
				xlCells[37, 6] = common.顧客情報.顧客名;
				xlCells[39, 6] = common.顧客情報.住所フリガナ;
				xlCells[40, 6] = "〒" + common.顧客情報.郵便番号 + "\r\n" + common.顧客情報.住所;
				xlCells[40, 22] = common.顧客情報.電話番号;
				xlCells[41, 22] = common.顧客情報.FAX番号;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 7 登録データ確認カード
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutConfirmCard(DocumentCommon common, string pathname)
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

					ws.Cell(8, 7).SetValue(common.顧客情報.顧客No);
					ws.Cell(8, 21).SetValue(common.顧客情報.得意先No);
					ws.Cell(10, 7).SetValue(common.顧客情報.フリガナ);
					ws.Cell(12, 7).SetValue(common.顧客情報.顧客名);
					ws.Cell(14, 7).SetValue("〒" + common.顧客情報.郵便番号);
					ws.Cell(16, 7).SetValue(common.顧客情報.住所フリガナ);
					ws.Cell(18, 7).SetValue(common.顧客情報.住所);
					ws.Cell(20, 7).SetValue(common.顧客情報.電話番号);
					ws.Cell(22, 7).SetValue(common.顧客情報.医保医療コード);
					ws.Cell(24, 7).SetValue(common.顧客情報.院長名フリガナ);
					ws.Cell(26, 7).SetValue(common.顧客情報.院長名);
					ws.Cell(20, 19).SetValue(common.顧客情報.FAX番号);
					ws.Cell(22, 19).SetValue(common.顧客情報.休診日);
					ws.Cell(24, 19).SetValue(common.顧客情報.診療時間);
					ws.Cell(26, 19).SetValue(common.顧客情報.メールアドレス);
					ws.Cell(29, 7).SetValue(common.顧客情報.システム名称);
					ws.Cell(31, 7).SetValue(common.顧客情報.備考);
					if (0 < common.顧客情報.単体)
					{
						ws.Cell(33, 7).SetValue(common.顧客情報.単体);
					}
					if (0 < common.顧客情報.サーバー)
					{
						ws.Cell(33, 15).SetValue(common.顧客情報.サーバー);
					}
					if (0 < common.顧客情報.クライアント)
					{
						ws.Cell(33, 23).SetValue(common.顧客情報.クライアント);
					}
					if (0 < common.顧客情報.販売店ID)
					{
						ws.Cell(35, 7).SetValue(common.顧客情報.販売店ID + " " + common.顧客情報.販売店名称);
					}
					ws.Cell(37, 7).SetValue(common.顧客情報.納品月);
					ws.Cell(39, 7).SetValue(common.顧客情報.運用サポート情報);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 8 Microsoft365利用申請書
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		/// Ver1.13(2022/05/02):Microsoft365利用申込書新フォーム対応
		/// Ver1.14(2022/06/30):8-Microsoft365利用申込書 新様式対応
		public static void ExcelOutMicrosoft365(DocumentCommon common, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("Microsoft365利用申請書");
					ws.Cell(7, 10).SetValue(common.顧客情報.顧客No);
					ws.Cell(9, 10).SetValue(common.顧客情報.顧客名);
					ws.Cell(11, 10).SetValue(common.顧客情報.院長名);
					ws.Cell(11, 27).SetValue(common.顧客情報.電話番号);
					ws.Cell(13, 11).SetValue(common.顧客情報.郵便番号);
					ws.Cell(14, 10).SetValue(common.顧客情報.住所);
					ws.Cell(16, 10).SetValue(common.顧客情報.メールアドレス);

					// 本社情報
					// Ver1.02(2021/09/01):Microsoft365利用申込書のFAX番号を本社から消耗品受注センターに変更
					//ws.Cell(58, 19).SetValue(common.HeadOffice.Fax);

					// Ver1.14(2022/06/16):8-Microsoft365利用申込書 拠点FAX番号対応
					//ws.Cell(47, 20).SetValue(Program.gSettings.HeadOffice.FaxExpendables);
					ws.Cell(50, 20).SetValue(common.FAX番号);

					ws.Cell(54, 19).SetValue(common.社名);
					ws.Cell(55, 19).SetValue(common.本社郵便番号);
					ws.Cell(56, 19).SetValue(common.本社住所);
					//ws.Cell(55, 21).SetValue(string.Format("e-mail {0}", common.メールアドレス));
					ws.Cell(57, 19).SetValue(common.URL);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 9 請求先変更届
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutSeikyuChange(DocumentCommon common, string pathname)
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
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["請求先変更届"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
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
								textFrame.Characters.Text = common.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.顧客情報.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.顧客情報.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", Date.Today.GetJapaneseString(true, '0', true, true));
				xlCells[12, 23] = common.顧客情報.得意先No;
				xlCells[15, 5] = common.顧客情報.フリガナ;
				xlCells[16, 5] = common.顧客情報.顧客名;
				xlCells[17, 5] = common.顧客情報.院長名フリガナ;
				xlCells[18, 5] = common.顧客情報.院長名;
				xlCells[20, 5] = "〒" + common.顧客情報.郵便番号 + "\r\n" + common.顧客情報.住所1 + "\r\n" + common.顧客情報.住所2;
				xlCells[20, 21] = common.顧客情報.電話番号;
				xlCells[22, 21] = common.顧客情報.FAX番号;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 10 終了届
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutUserFinished(DocumentCommon common, string pathname)
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
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["終了届"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
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
								textFrame.Characters.Text = common.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.顧客情報.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.顧客情報.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", DocumentCommon.DateTodayString());
				xlCells[13, 23] = common.顧客情報.得意先No;
				xlCells[16, 5] = common.顧客情報.フリガナ;
				xlCells[17, 5] = common.顧客情報.顧客名;
				xlCells[18, 5] = common.顧客情報.院長名フリガナ;
				xlCells[19, 5] = common.顧客情報.院長名;
				xlCells[21, 5] = "〒" + common.顧客情報.郵便番号 + "\r\n" + common.顧客情報.住所1 + "\r\n" + common.顧客情報.住所2;
				xlCells[21, 21] = common.顧客情報.電話番号;
				xlCells[23, 21] = common.顧客情報.FAX番号;
				xlCells[25, 5] = common.顧客情報.医保医療コード;
				xlCells[27, 5] = common.顧客情報.システム名称;
				xlCells[27, 17] = common.顧客情報.クライアント;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 11 変更届
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutUserChange(DocumentCommon common, string pathname)
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
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["変更届"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;
				xlCells = xlSheet.Cells;
				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
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
								textFrame.Characters.Text = common.住所2;
								break;
							case "送付先":
								textFrame.Characters.Text = common.送付先;
								break;
							case "FAX番号":
								textFrame.Characters.Text = string.Format("FAX {0}", common.FAX番号);
								break;
							case "顧客No":
								textFrame.Characters.Text = common.顧客情報.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.顧客情報.得意先No;
								break;
						}
					}
				}
				xlCells[1, 19] = string.Format("作成日　　{0}", DocumentCommon.DateTodayString());
				xlCells[12, 23] = common.顧客情報.得意先No;
				xlCells[15, 5] = common.顧客情報.フリガナ;
				xlCells[16, 5] = common.顧客情報.顧客名;
				xlCells[17, 5] = common.顧客情報.院長名フリガナ;
				xlCells[18, 5] = common.顧客情報.院長名;
				xlCells[20, 5] = "〒" + common.顧客情報.郵便番号 + "\r\n" + common.顧客情報.住所1 + "\r\n" + common.顧客情報.住所2;
				xlCells[20, 21] = common.顧客情報.電話番号;
				xlCells[22, 21] = common.顧客情報.FAX番号;
				xlCells[24, 5] = common.顧客情報.医保医療コード;
				xlCells[26, 5] = common.顧客情報.システム名称;
				xlCells[26, 17] = common.顧客情報.クライアント;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 12-第一園芸注文書
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutFirstEngei(DocumentCommon common, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// 第一園芸医院向け
					IXLWorksheet ws1 = wb.Worksheet("第一園芸医院向け");
					ws1.Cell(6, 4).SetValue(common.顧客情報.郵便番号);
					ws1.Cell(7, 3).SetValue(common.顧客情報.住所1);
					ws1.Cell(8, 3).SetValue(common.顧客情報.住所2);
					ws1.Cell(9, 5).SetValue(common.顧客情報.顧客名);
					ws1.Cell(13, 5).SetValue(common.顧客情報.電話番号);

					ws1.Cell(6, 18).SetValue(common.郵便番号);
					ws1.Cell(7, 17).SetValue(common.住所1);
					ws1.Cell(8, 17).SetValue(common.住所2);
					ws1.Cell(9, 19).SetValue((common.IsHeadOffice) ? common.社名 : string.Format("{0} {1}", common.社名, common.Satellite.Branch));
					ws1.Cell(13, 19).SetValue(common.電話番号);
					ws1.Cell(13, 26).SetValue(common.FAX番号);
					ws1.Cell(20, 12).SetValue(common.社名);
					ws1.Cell(37, 6).SetValue(common.本社郵便番号);
					ws1.Cell(38, 5).SetValue(common.本社住所);
					// 経理部
					ws1.Cell(40, 5).SetValue(Program.gSettings.HeadOffice.TelKeiri);
					ws1.Cell(40, 15).SetValue(common.本社FAX番号);

					// 第一園芸販売店向け
					IXLWorksheet ws2 = wb.Worksheet("第一園芸販売店向け");
					ws2.Cell(6, 18).SetValue(common.郵便番号);
					ws2.Cell(7, 17).SetValue(common.住所1);
					ws2.Cell(8, 17).SetValue(common.住所2);
					ws2.Cell(9, 19).SetValue((common.IsHeadOffice) ? common.社名 : string.Format("{0} {1}", common.社名, common.Satellite.Branch));
					ws2.Cell(13, 19).SetValue(common.電話番号);
					ws2.Cell(13, 26).SetValue(common.FAX番号);
					ws2.Cell(20, 12).SetValue(common.社名);
					ws2.Cell(37, 6).SetValue(common.本社郵便番号);
					ws2.Cell(38, 5).SetValue(common.本社住所);
					// 経理部
					ws2.Cell(40, 5).SetValue(Program.gSettings.HeadOffice.TelKeiri);
					ws2.Cell(40, 15).SetValue(common.本社FAX番号);

					// Excelファイルの保存
					wb.Save();
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
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutDelivery(DocumentCommon common, string pathname)
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
					ws.Cell(7, 27).SetValue(common.顧客情報.顧客No);
					ws.Cell(7, 10).SetValue(common.営業部名);
					ws.Cell(9, 10).SetValue(common.顧客情報.顧客名);
					ws.Cell(11, 10).SetValue(string.Format("〒{0}", common.顧客情報.郵便番号));
					ws.Cell(12, 10).SetValue(common.顧客情報.住所);
					ws.Cell(15, 10).SetValue(common.顧客情報.院長名);
					ws.Cell(15, 27).SetValue(common.顧客情報.電話番号);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		///// <summary>
		///// EXCEL出力 - 14-2次キッティング依頼書
		///// </summary>
		///// <param name="common">各種書類出力 共通情報</param>
		///// <param name="pathname">Excelファイルパス名</param>
		//public static void ExcelOutSecondKitting(DocumentCommon common, string pathname, string saleDepartment, tMih支店情報 branch)
		//{
		//	try
		//	{
		//		using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
		//		{
		//			// 2次キッティング依頼書（医院）
		//			IXLWorksheet ws1 = wb.Worksheet("2次キッティング依頼書（医院）");
		//			// 依頼日
		//			ws1.Cell(2, 29).SetValue(string.Format("{0:D4}", Date.Today.Year));
		//			ws1.Cell(2, 32).SetValue(string.Format("{0:D2}", Date.Today.Month));
		//			ws1.Cell(2, 34).SetValue(string.Format("{0:D2}", Date.Today.Day));
		//			ws1.Cell(6, 3).SetValue("入力必須");					// ＷＷ受注番号
		//			//【依頼者情報】
		//			ws1.Cell(10, 11).SetValue((common.IsHeadOffice) ? "選択してください" : saleDepartment);	// MIC担当拠点
		//			ws1.Cell(10, 28).SetValue("-");							// MIC担当者
		//			ws1.Cell(11, 11).SetValue("-");							// 希望納期
		//			ws1.Cell(11, 16).SetValue("-");
		//			ws1.Cell(11, 19).SetValue("-");
		//			ws1.Cell(11, 28).SetValue("医院直送");					// 納品先
		//			ws1.Cell(12, 12).SetValue(common.Customer.郵便番号);	// 納品先郵便番号
		//			ws1.Cell(12, 28).SetValue(common.Customer.電話番号);	// 納品先電話番号
		//			ws1.Cell(13, 11).SetValue(common.Customer.住所);		// 納品先住所
		//			//【歯科医院情報】
		//			ws1.Cell(18, 11).SetValue(common.Customer.顧客No);
		//			ws1.Cell(19, 11).SetValue(common.Customer.顧客名);
		//			if (0 == common.Customer.院長名.Length)
		//			{
		//				ws1.Cell(21, 11).SetValue("入力必須");
		//				ws1.Cell(21, 28).SetValue("入力必須");
		//			}
		//			else
		//			{
		//				ws1.Cell(21, 11).SetValue(common.Customer.院長名);
		//				ws1.Cell(21, 28).SetValue(common.Customer.院長名);
		//			}
		//			ws1.Cell(22, 12).SetValue(common.Customer.郵便番号);
		//			ws1.Cell(23, 11).SetValue(common.Customer.住所);
		//			ws1.Cell(26, 11).SetValue(common.Customer.電話番号);
		//			ws1.Cell(26, 28).SetValue(common.Customer.FAX番号);
		//			ws1.Cell(27, 11).SetValue("入力必須");
		//			ws1.Cell(27, 28).SetValue(common.Customer.医保医療コード);
		//			ws1.Cell(28, 11).SetValue("-");
		//			//【システム構成情報】
		//			ws1.Cell(31, 11).SetValue("-");
		//			ws1.Cell(31, 28).SetValue("-");
		//			//【システム構成詳細】
		//			ws1.Cell(36, 10).SetValue("-");
		//			ws1.Cell(36, 15).SetValue("-");
		//			ws1.Cell(36, 20).SetValue("-");
		//			ws1.Cell(36, 25).SetValue("-");
		//			ws1.Cell(36, 30).SetValue("-");
		//			ws1.Cell(37, 10).SetValue("-");
		//			ws1.Cell(37, 15).SetValue("-");
		//			ws1.Cell(37, 20).SetValue("-");
		//			ws1.Cell(37, 25).SetValue("-");
		//			ws1.Cell(37, 30).SetValue("-");
		//			ws1.Cell(38, 10).SetValue("-");
		//			ws1.Cell(38, 15).SetValue("-");
		//			ws1.Cell(38, 20).SetValue("-");
		//			ws1.Cell(38, 25).SetValue("-");
		//			ws1.Cell(38, 30).SetValue("-");
		//			ws1.Cell(39, 10).SetValue("-");
		//			ws1.Cell(39, 15).SetValue("-");
		//			ws1.Cell(39, 20).SetValue("-");
		//			ws1.Cell(39, 25).SetValue("-");
		//			ws1.Cell(39, 30).SetValue("-");
		//			ws1.Cell(40, 10).SetValue("-");
		//			ws1.Cell(40, 15).SetValue("-");
		//			ws1.Cell(40, 20).SetValue("-");
		//			ws1.Cell(40, 25).SetValue("-");
		//			ws1.Cell(40, 30).SetValue("-");
		//			ws1.Cell(41, 10).SetValue("-");
		//			ws1.Cell(41, 15).SetValue("-");
		//			ws1.Cell(41, 20).SetValue("-");
		//			ws1.Cell(41, 25).SetValue("-");
		//			ws1.Cell(41, 30).SetValue("-");
		//			ws1.Cell(42, 10).SetValue("-");
		//			ws1.Cell(42, 15).SetValue("-");
		//			ws1.Cell(42, 20).SetValue("-");
		//			ws1.Cell(42, 25).SetValue("-");
		//			ws1.Cell(42, 30).SetValue("-");
		//			ws1.Cell(43, 10).SetValue("-");
		//			ws1.Cell(43, 15).SetValue("-");
		//			ws1.Cell(43, 20).SetValue("-");
		//			ws1.Cell(43, 25).SetValue("-");
		//			ws1.Cell(43, 30).SetValue("-");
		//			//【IPアドレス(固定の場合のみ記載)】
		//			ws1.Cell(47, 10).SetValue("-");
		//			ws1.Cell(47, 15).SetValue("-");
		//			ws1.Cell(47, 20).SetValue("-");
		//			ws1.Cell(47, 25).SetValue("-");
		//			ws1.Cell(47, 30).SetValue("-");
		//			ws1.Cell(48, 10).SetValue("-");
		//			ws1.Cell(48, 15).SetValue("-");
		//			ws1.Cell(48, 20).SetValue("-");
		//			ws1.Cell(48, 25).SetValue("-");
		//			ws1.Cell(48, 30).SetValue("-");
		//			ws1.Cell(49, 10).SetValue("-");
		//			ws1.Cell(49, 15).SetValue("-");
		//			ws1.Cell(49, 20).SetValue("-");
		//			ws1.Cell(49, 25).SetValue("-");
		//			ws1.Cell(49, 30).SetValue("-");
		//			ws1.Cell(50, 10).SetValue("-");
		//			ws1.Cell(50, 15).SetValue("-");
		//			ws1.Cell(50, 20).SetValue("-");
		//			ws1.Cell(50, 25).SetValue("-");
		//			ws1.Cell(50, 30).SetValue("-");
		//			ws1.Cell(51, 10).SetValue("-");
		//			ws1.Cell(51, 15).SetValue("-");
		//			ws1.Cell(51, 20).SetValue("-");
		//			ws1.Cell(51, 25).SetValue("-");
		//			ws1.Cell(51, 30).SetValue("-");
		//			//【PLM・訪問診療使用時のパラメ-ター】
		//			ws1.Cell(55, 9).SetValue("PC-00");
		//			ws1.Cell(55, 23).SetValue(@"\\PC-00\MIC_PALETTE\");
		//			//【備考】
		//			ws1.Cell(59, 3).SetValue("");

		//			// 2次キッティング依頼書（拠点）
		//			IXLWorksheet ws2 = wb.Worksheet("2次キッティング依頼書（拠点）");
		//			// 依頼日
		//			ws2.Cell(2, 29).SetValue(string.Format("{0:D4}", Date.Today.Year));
		//			ws2.Cell(2, 32).SetValue(string.Format("{0:D2}", Date.Today.Month));
		//			ws2.Cell(2, 34).SetValue(string.Format("{0:D2}", Date.Today.Day));
		//			ws2.Cell(6, 3).SetValue("入力必須");			// ＷＷ受注番号
		//			//【依頼者情報】
		//			ws2.Cell(10, 11).SetValue((common.IsHeadOffice) ? "選択してください" : saleDepartment);	// MIC担当拠点
		//			ws2.Cell(10, 28).SetValue("-");					// MIC担当者
		//			ws2.Cell(11, 11).SetValue("-");					// 希望納期
		//			ws2.Cell(11, 16).SetValue("-");
		//			ws2.Cell(11, 19).SetValue("-");
		//			if (false == common.IsHeadOffice)
		//			{
		//				// 営業部
		//				ws2.Cell(11, 28).SetValue(branch.f支店名);		// 納品先
		//				ws2.Cell(12, 12).SetValue(branch.f郵便番号);	// 納品先郵便番号
		//				ws2.Cell(12, 28).SetValue(branch.f電話番号);	// 納品先電話番号
		//				ws2.Cell(13, 11).SetValue(branch.住所);			// 納品先住所
		//			}
		//			else
		//			{
		//				// 本社
		//				ws2.Cell(11, 28).SetValue(common.社名);								// 納品先
		//				ws2.Cell(12, 12).SetValue(Program.gSettings.HeadOffice.Zipcode);	// 納品先郵便番号
		//				ws2.Cell(12, 28).SetValue(Program.gSettings.HeadOffice.Tel);		// 納品先電話番号
		//				ws2.Cell(13, 11).SetValue(Program.gSettings.HeadOffice.住所);		// 納品先住所
		//			}
		//			//【歯科医院情報】
		//			ws2.Cell(18, 11).SetValue(common.Customer.顧客No);
		//			ws2.Cell(19, 11).SetValue(common.Customer.顧客名);
		//			if (0 == common.Customer.院長名.Length)
		//			{
		//				ws2.Cell(21, 11).SetValue("入力必須");
		//				ws2.Cell(21, 28).SetValue("入力必須");
		//			}
		//			else
		//			{
		//				ws2.Cell(21, 11).SetValue(common.Customer.院長名);
		//				ws2.Cell(21, 28).SetValue(common.Customer.院長名);
		//			}
		//			ws2.Cell(22, 12).SetValue(common.Customer.郵便番号);
		//			ws2.Cell(23, 11).SetValue(common.Customer.住所);
		//			ws2.Cell(26, 11).SetValue(common.Customer.電話番号);
		//			ws2.Cell(26, 28).SetValue(common.Customer.FAX番号);
		//			ws2.Cell(27, 11).SetValue("入力必須");
		//			ws2.Cell(27, 28).SetValue(common.Customer.医保医療コード);
		//			ws2.Cell(28, 11).SetValue("-");
		//			//【システム構成情報】
		//			ws2.Cell(31, 11).SetValue("-");
		//			ws2.Cell(31, 28).SetValue("-");
		//			//【システム構成詳細】
		//			ws2.Cell(36, 10).SetValue("-");
		//			ws2.Cell(36, 15).SetValue("-");
		//			ws2.Cell(36, 20).SetValue("-");
		//			ws2.Cell(36, 25).SetValue("-");
		//			ws2.Cell(36, 30).SetValue("-");
		//			ws2.Cell(37, 10).SetValue("-");
		//			ws2.Cell(37, 15).SetValue("-");
		//			ws2.Cell(37, 20).SetValue("-");
		//			ws2.Cell(37, 25).SetValue("-");
		//			ws2.Cell(37, 30).SetValue("-");
		//			ws2.Cell(38, 10).SetValue("-");
		//			ws2.Cell(38, 15).SetValue("-");
		//			ws2.Cell(38, 20).SetValue("-");
		//			ws2.Cell(38, 25).SetValue("-");
		//			ws2.Cell(38, 30).SetValue("-");
		//			ws2.Cell(39, 10).SetValue("-");
		//			ws2.Cell(39, 15).SetValue("-");
		//			ws2.Cell(39, 20).SetValue("-");
		//			ws2.Cell(39, 25).SetValue("-");
		//			ws2.Cell(39, 30).SetValue("-");
		//			ws2.Cell(40, 10).SetValue("-");
		//			ws2.Cell(40, 15).SetValue("-");
		//			ws2.Cell(40, 20).SetValue("-");
		//			ws2.Cell(40, 25).SetValue("-");
		//			ws2.Cell(40, 30).SetValue("-");
		//			ws2.Cell(41, 10).SetValue("-");
		//			ws2.Cell(41, 15).SetValue("-");
		//			ws2.Cell(41, 20).SetValue("-");
		//			ws2.Cell(41, 25).SetValue("-");
		//			ws2.Cell(41, 30).SetValue("-");
		//			ws2.Cell(42, 10).SetValue("-");
		//			ws2.Cell(42, 15).SetValue("-");
		//			ws2.Cell(42, 20).SetValue("-");
		//			ws2.Cell(42, 25).SetValue("-");
		//			ws2.Cell(42, 30).SetValue("-");
		//			ws2.Cell(43, 10).SetValue("-");
		//			ws2.Cell(43, 15).SetValue("-");
		//			ws2.Cell(43, 20).SetValue("-");
		//			ws2.Cell(43, 25).SetValue("-");
		//			ws2.Cell(43, 30).SetValue("-");
		//			//【IPアドレス(固定の場合のみ記載)】
		//			ws2.Cell(47, 10).SetValue("-");
		//			ws2.Cell(47, 15).SetValue("-");
		//			ws2.Cell(47, 20).SetValue("-");
		//			ws2.Cell(47, 25).SetValue("-");
		//			ws2.Cell(47, 30).SetValue("-");
		//			ws2.Cell(48, 10).SetValue("-");
		//			ws2.Cell(48, 15).SetValue("-");
		//			ws2.Cell(48, 20).SetValue("-");
		//			ws2.Cell(48, 25).SetValue("-");
		//			ws2.Cell(48, 30).SetValue("-");
		//			ws2.Cell(49, 10).SetValue("-");
		//			ws2.Cell(49, 15).SetValue("-");
		//			ws2.Cell(49, 20).SetValue("-");
		//			ws2.Cell(49, 25).SetValue("-");
		//			ws2.Cell(49, 30).SetValue("-");
		//			ws2.Cell(50, 10).SetValue("-");
		//			ws2.Cell(50, 15).SetValue("-");
		//			ws2.Cell(50, 20).SetValue("-");
		//			ws2.Cell(50, 25).SetValue("-");
		//			ws2.Cell(50, 30).SetValue("-");
		//			ws2.Cell(51, 10).SetValue("-");
		//			ws2.Cell(51, 15).SetValue("-");
		//			ws2.Cell(51, 20).SetValue("-");
		//			ws2.Cell(51, 25).SetValue("-");
		//			ws2.Cell(51, 30).SetValue("-");
		//			//【PLM・訪問診療使用時のパラメ-ター】
		//			ws2.Cell(55, 9).SetValue("PC-00");
		//			ws2.Cell(55, 23).SetValue(@"\\PC-00\MIC_PALETTE\");
		//			//【備考】
		//			ws2.Cell(59, 3).SetValue("");

		//			// Excelファイルの保存
		//			wb.Save();
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		throw new Exception(e.Message);
		//	}
		//}

		/// <summary>
		/// EXCEL出力 - 15 PC安心サポート加入申込書 or PC安心サポートPlus加入申込書
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutPcSupport(DocumentCommon common, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// PC安心サポート加入申込書
					IXLWorksheet ws1 = wb.Worksheet("PC安心サポート加入申込書");
					ws1.Cell(11, 11).SetValue(common.顧客情報.得意先No);
					ws1.Cell(12, 11).SetValue(common.顧客情報.顧客No);
					ws1.Cell(13, 11).SetValue(common.顧客情報.顧客名);
					ws1.Cell(15, 11).SetValue(common.顧客情報.電話番号);
					ws1.Cell(16, 12).SetValue(common.顧客情報.郵便番号);
					ws1.Cell(17, 11).SetValue(common.顧客情報.住所1 + "\r\n" + common.顧客情報.住所2);
					ws1.Cell(20, 11).SetValue(common.顧客情報.メールアドレス);
					ws1.Cell(21, 11).SetValue(common.顧客情報.支店名);
					ws1.Cell(21, 25).SetValue(common.顧客情報.営業担当者名);
					ws1.Cell(48, 12).SetValue(common.社名);

					// PC安心サポートPlus加入申込書
					IXLWorksheet ws2 = wb.Worksheet("PC安心サポートPlus加入申込書");
					ws2.Cell(11, 11).SetValue(common.顧客情報.得意先No);
					ws2.Cell(12, 11).SetValue(common.顧客情報.顧客No);
					ws2.Cell(13, 11).SetValue(common.顧客情報.顧客名);
					ws2.Cell(15, 11).SetValue(common.顧客情報.電話番号);
					ws2.Cell(16, 12).SetValue(common.顧客情報.郵便番号);
					ws2.Cell(17, 11).SetValue(common.顧客情報.住所1 + "\r\n" + common.顧客情報.住所2);
					ws2.Cell(20, 11).SetValue(common.顧客情報.メールアドレス);
					ws2.Cell(21, 11).SetValue(common.顧客情報.支店名);
					ws2.Cell(21, 25).SetValue(common.顧客情報.営業担当者名);
					ws2.Cell(48, 12).SetValue(common.社名);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 16 アプラス預金口座振替依頼書・自動払込利用申込書
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		/// Ver1.13(2022/05/09):アプラス預金口座振替依頼書・自動払込利用申込書新フォーム対応
		public static void ExcelOutAplus(DocumentCommon common, string pathname)
		{
			Excel.Application xlApp = null;
			Excel.Workbooks xlBooks = null;
			Excel.Workbook xlBook = null;
			Excel.Sheets xlSheets = null;
			Excel.Worksheet xlSheet = null;
			Excel.Shapes xlShapes = null;
			try
			{
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBooks = xlApp.Workbooks;
				xlBook = xlBooks.Open(pathname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				xlSheets = xlBook.Worksheets;
				xlSheet = xlSheets["アプラス預金口座振替依頼書_自動払込利用申込書"] as Excel.Worksheet;
				xlShapes = xlSheet.Shapes;

				// シート内コントロール初期化
				// ※チェックボックスはクリックすると容易にチェックがついてしまうため
				// 0x800A03EC のエラーが発生するので、下記の処理に修正
				//xlSheet.OLEObjects("新規").Object.Value = 0;
				//xlSheet.OLEObjects("変更").Object.Value = 0;
				//xlSheet.OLEObjects("不備").Object.Value = 0;
				Excel.CheckBox chkBoxNew = (Excel.CheckBox)xlSheet.CheckBoxes(1);
				chkBoxNew.Value = 0;
				Excel.CheckBox chkBoxModify = (Excel.CheckBox)xlSheet.CheckBoxes(2);
				chkBoxModify.Value = 0;
				Excel.CheckBox chkBoxDefect = (Excel.CheckBox)xlSheet.CheckBoxes(3);
				chkBoxDefect.Value = 0;

				foreach (Excel.Shape shape in xlShapes)
				{
					if (MsoShapeType.msoTextBox == shape.Type)
					{
						dynamic textFrame = shape.TextFrame;
						switch (shape.Name)
						{
							case "顧客No":
								textFrame.Characters.Text = common.顧客情報.顧客No;
								break;
							case "得意先No":
								textFrame.Characters.Text = common.顧客情報.得意先No;
								break;
							case "契約者名フリガナ":
								textFrame.Characters.Text = common.顧客情報.フリガナ;
								break;
							case "契約者名":
								textFrame.Characters.Text = common.顧客情報.顧客名;
								break;
							case "契約者郵便番号":
								textFrame.Characters.Text = common.顧客情報.郵便番号;
								break;
							case "契約者住所":
								textFrame.Characters.Text = common.顧客情報.住所;
								break;
							case "契約者電話番号":
								textFrame.Characters.Text = common.顧客情報.電話番号;
								break;
							case "APLUSコード":
								// Ver1.21(2024/09/17 勝呂):16-アプラス預金口座振替依頼書・自動払込利用申込書 でAPLUSコードが発番されていない時にExcel出力する際にアプリケーションエラーが発生
								if (0 < common.顧客情報.代行回収APLUSコード.Length)
								{
									textFrame.Characters.Text = common.顧客情報.代行回収APLUSコード.Substring(8);
								}
								break;
						}
					}
				}
				if ("予約" == common.顧客情報.代行回収状態)
				{
					chkBoxNew.Value = 1;
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
				// ガベージコレクションを直ちに強制実行する
				GC.Collect();
			}
		}

		/// <summary>
		/// EXCEL出力 - 17 作業報告書
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		public static void ExcelOutWorkReport(DocumentCommon common, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("作業報告書");
					ws.Cell(10, 3).SetValue(common.顧客情報.得意先No);
					ws.Cell(12, 3).SetValue(common.顧客情報.顧客名);
					ws.Cell(18, 3).SetValue(string.Format("〒{0}\r\n{1}\r\n{2}", common.顧客情報.郵便番号, common.顧客情報.住所1, common.顧客情報.住所2));
					ws.Cell(34, 3).SetValue(string.Format("TEL {0}", common.顧客情報.電話番号));

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 消耗品FAXオーダーシート
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		/// Ver1.05(2021/11/12):消耗品FAXオーダーシートの新規追加
		public static void ExcelOutFaxOrderSheet(DocumentCommon common, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("消耗品FAXオーダーシート");
					ws.Cell(1, 36).SetValue(Program.gSettings.HeadOffice.FaxExpendables);
					ws.Cell(9, 9).SetValue(common.顧客情報.得意先No);
					ws.Cell(10, 9).SetValue(common.顧客情報.顧客名);
					ws.Cell(11, 9).SetValue(common.顧客情報.FAX番号);

					List<tMikユーザ> userList = JunpDatabaseAccess.Select_tMikユーザ(string.Format("fusCliMicID = {0}", common.顧客情報.顧客No), "", Program.gSettings.Connect.Junp.ConnectionString);
					if (null != userList && 0 < userList.Count)
					{
						// 当日の消費税率
						int taxRate = JunpDatabaseAccess.GetTaxRate(Date.Today, Program.gSettings.Connect.Junp.ConnectionString);

						// オーダーリスト
						List< FaxOrderSheetGoods> orderList = new List<FaxOrderSheetGoods>();

						// 電子レセプト請求用CD-Rの取得
						List<tMikOS明細印字> cdrList = JunpDatabaseAccess.Select_tMikOS明細印字(string.Format("fmsコード種別 = '10' AND fmsコード = '{0}'", userList[0].fus連単), "", Program.gSettings.Connect.Junp.ConnectionString);
						if (null != cdrList && 0 < cdrList.Count)
						{
							for (int i = 0; i < 8; i++)
							{
								string code = string.Empty;
								switch (i)
								{
									case 0: code = cdrList[0].fms商品コード1; break;
									case 1: code = cdrList[0].fms商品コード2; break;
									case 2: code = cdrList[0].fms商品コード3; break;
									case 3: code = cdrList[0].fms商品コード4; break;
									case 4: code = cdrList[0].fms商品コード5; break;
									case 5: code = cdrList[0].fms商品コード6; break;
									case 6: code = cdrList[0].fms商品コード7; break;
									case 7: code = cdrList[0].fms商品コード8; break;
								}
								if (0 == code.Length)
								{
									break;
								}
								vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(code, Program.gSettings.Connect.Junp.ConnectionString);
								if (null != pca)
								{
									FaxOrderSheetGoods order = new FaxOrderSheetGoods();
									order.GoodsCode = pca.sms_scd;
									order.GoodsName = pca.sms_mei;
									order.Price = pca.sms_hyo;
									order.Unit = cdrList[0].fms発注単位;
									orderList.Add(order);
								}
							}
						}
						// カルテ用紙の取得
						List<tMikOS明細印字> chartList = JunpDatabaseAccess.Select_tMikOS明細印字(string.Format("fmsコード種別 = '03' AND fmsコード = '{0}'", userList[0].fusカルテ用紙), "", Program.gSettings.Connect.Junp.ConnectionString);
						if (null != chartList && 0 < chartList.Count)
						{
							for (int i = 0; i < 8; i++)
							{
								string code = string.Empty;
								switch (i)
								{
									case 0: code = chartList[0].fms商品コード1; break;
									case 1: code = chartList[0].fms商品コード2; break;
									case 2: code = chartList[0].fms商品コード3; break;
									case 3: code = chartList[0].fms商品コード4; break;
									case 4: code = chartList[0].fms商品コード5; break;
									case 5: code = chartList[0].fms商品コード6; break;
									case 6: code = chartList[0].fms商品コード7; break;
									case 7: code = chartList[0].fms商品コード8; break;
								}
								if (0 == code.Length)
								{
									break;
								}
								vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(code, Program.gSettings.Connect.Junp.ConnectionString);
								if (null != pca)
								{
									FaxOrderSheetGoods order = new FaxOrderSheetGoods();
									order.GoodsCode = pca.sms_scd;
									order.GoodsName = pca.sms_mei;
									order.Price = pca.sms_hyo;
									order.Unit = chartList[0].fms発注単位;
									orderList.Add(order);
								}
							}
						}
						// 領収証用紙の取得
						List<tMikOS明細印字> receiptList = JunpDatabaseAccess.Select_tMikOS明細印字(string.Format("fmsコード種別 = '05' AND fmsコード = '{0}'", userList[0].fus領収書用紙), "", Program.gSettings.Connect.Junp.ConnectionString);
						if (null != receiptList && 0 < receiptList.Count)
						{
							for (int i = 0; i < 8; i++)
							{
								string code = string.Empty;
								switch (i)
								{
									case 0: code = receiptList[0].fms商品コード1; break;
									case 1: code = receiptList[0].fms商品コード2; break;
									case 2: code = receiptList[0].fms商品コード3; break;
									case 3: code = receiptList[0].fms商品コード4; break;
									case 4: code = receiptList[0].fms商品コード5; break;
									case 5: code = receiptList[0].fms商品コード6; break;
									case 6: code = receiptList[0].fms商品コード7; break;
									case 7: code = receiptList[0].fms商品コード8; break;
								}
								if (0 == code.Length)
								{
									break;
								}
								vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(code, Program.gSettings.Connect.Junp.ConnectionString);
								if (null != pca)
								{
									FaxOrderSheetGoods order = new FaxOrderSheetGoods();
									order.GoodsCode = pca.sms_scd;
									order.GoodsName = pca.sms_mei;
									order.Price = pca.sms_hyo;
									order.Unit = receiptList[0].fms発注単位;
									orderList.Add(order);
								}
							}
						}
						// トナータイプの取得
						List<tMikOS明細印字> tonerList = JunpDatabaseAccess.Select_tMikOS明細印字(string.Format("fmsコード種別 = '05' AND fmsコード = '{0}'", userList[0].fus領収書用紙2), "", Program.gSettings.Connect.Junp.ConnectionString);
						if (null != tonerList && 0 < tonerList.Count)
						{
							for (int i = 0; i < 8; i++)
							{
								string code = string.Empty;
								switch (i)
								{
									case 0: code = tonerList[0].fms商品コード1; break;
									case 1: code = tonerList[0].fms商品コード2; break;
									case 2: code = tonerList[0].fms商品コード3; break;
									case 3: code = tonerList[0].fms商品コード4; break;
									case 4: code = tonerList[0].fms商品コード5; break;
									case 5: code = tonerList[0].fms商品コード6; break;
									case 6: code = tonerList[0].fms商品コード7; break;
									case 7: code = tonerList[0].fms商品コード8; break;
								}
								if (0 == code.Length)
								{
									break;
								}
								vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(code, Program.gSettings.Connect.Junp.ConnectionString);
								if (null != pca)
								{
									FaxOrderSheetGoods order = new FaxOrderSheetGoods();
									order.GoodsCode = pca.sms_scd;
									order.GoodsName = pca.sms_mei;
									order.Price = pca.sms_hyo;
									order.Unit = tonerList[0].fms発注単位;
									orderList.Add(order);
								}
							}
						}
						// 注文欄の設定
						for (int i = 0; i < orderList.Count; i++)
						{
							ws.Cell(16 + i, 1).SetValue(orderList[i].GoodsCode);
							ws.Cell(16 + i, 7).SetValue(orderList[i].GoodsName);
							ws.Cell(16 + i, 27).SetValue(string.Format(@"\{0}―", StringUtil.CommaEdit(orderList[i].税込単価(taxRate))));
							ws.Cell(16 + i, 35).SetValue(orderList[i].Unit);
						}
					}
					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// EXCEL出力 - 19-オンライン資格確認等事業完了報告書(経理部専用)
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="pathname">Excelファイルパス名</param>
		/// <param name="orgPathname">Excelファイルパス名(org)</param>
		/// <param name="detailList">領収書内訳書明細リスト</param>
		/// <param name="soft">vMicオンライン資格確認ソフト改修費</param>
		/// <param name="destination">送付先リスト</param>
		/// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
		/// Ver1.12(2022/02/22):経理部専用 オンライン資格確認等事業完了報告書 修正依頼対応
		/// Ver1.15(2023/01/13):19-経理部専用 オンライン資格確認等事業完了報告書 注文確認書の追加、領収証および書類送付状の削除
		/// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
		public static void ExcelOutOnlineConfirm(DocumentCommon common, string pathname, string orgPathname, List<オンライン資格確認対象商品売上明細> detailList, vMicオンライン資格確認ソフト改修費 soft, 送付先リスト destination)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					string clinicCode = common.顧客情報.NumericClinicCode;
/*
					// Ver1.15(2023/01/13):19-経理部専用 オンライン資格確認等事業完了報告書 注文確認書の追加、領収証および書類送付状の削除
					////////////////////////////////////////
					//「領収証」
					////////////////////////////////////////

					IXLWorksheet ws1 = wb.Worksheet("領収証");
					ws1.Cell(5, 2).SetValue(common.Customer.顧客名);
					ws1.Cell(13, 3).SetValue(string.Format("{0,4} 年 {1,2} 月 {2,2} 日", Date.Today.Year, Date.Today.Month, Date.Today.Day));
					ws1.Cell(16, 7).SetValue(common.社名);
					ws1.Cell(18, 7).SetValue(common.住所1);
					ws1.Cell(19, 7).SetValue(common.住所2);
					ws1.Cell(20, 7).SetValue(string.Format("TEL {0}", common.電話番号));
*/

					////////////////////////////////////////
					//「領収書内訳書」
					////////////////////////////////////////

					IXLWorksheet ws2 = wb.Worksheet("領収書内訳書");
					ws2.Cell(3, 42).SetValue(string.Format("{0,4}", Date.Today.Year));
					ws2.Cell(3, 45).SetValue(string.Format("{0,2}", Date.Today.Month));
					ws2.Cell(3, 47).SetValue(string.Format("{0,2}", Date.Today.Day));
					if (2 == common.顧客情報.県番号.Length)
					{
						ws2.Cell(6, 9).SetValue(common.顧客情報.県番号.Substring(0, 1));
						ws2.Cell(6, 10).SetValue(common.顧客情報.県番号.Substring(1, 1));
					}
					// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 医療機関コードが7文字でない時にアプリケーションエラーが発生
					// Ver1.19(2023/04/13 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 「領収書内訳書」と「事業完了報告書」の医療機関コードが出力されない
					//if (MwsDefine.TokuisakiNoLength == clinicCode.Length)
					if (MwsDefine.ClinicCodeLength == clinicCode.Length)
					{
							ws2.Cell(8, 9).SetValue(clinicCode.Substring(0, 1));
						ws2.Cell(8, 10).SetValue(clinicCode.Substring(1, 1));
						ws2.Cell(8, 11).SetValue(clinicCode.Substring(2, 1));
						ws2.Cell(8, 12).SetValue(clinicCode.Substring(3, 1));
						ws2.Cell(8, 13).SetValue(clinicCode.Substring(4, 1));
						ws2.Cell(8, 14).SetValue(clinicCode.Substring(5, 1));
						ws2.Cell(8, 16).SetValue(clinicCode.Substring(6, 1));
					}
					// Ver1.19(2023/04/12 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 「領収書内訳書」の顧客名の後ろに様を付加しない
					//ws2.Cell(10, 9).SetValue(string.Format("{0}  様", common.Customer.顧客名));
					ws2.Cell(10, 9).SetValue(common.顧客情報.顧客名);

					// Ver1.12(2022/02/22):経理部専用 オンライン資格確認等事業完了報告書 修正依頼対応
					//ws2.Cell(8, 40).SetValue(common.社名);
					//ws2.Cell(10, 40).SetValue(common.住所1);
					//ws2.Cell(12, 40).SetValue(common.電話番号);

					if (null != detailList && 0 < detailList.Count)
					{
						//int total = 0;
						for (int i = 0; i < detailList.Count; i++)
						{
							オンライン資格確認対象商品売上明細 detail = detailList[i];
							OnlineGoods online = Program.gSettings.OnlineGoodsList.Find(p => p.商品コード == detail.商品コード);
							if (null != online)
							{
								// 項目
								ws2.Cell(16 + i, 4).SetValue(online.項目);

								// 内訳
								ws2.Cell(16 + i, 16).SetValue(online.内訳);
							}
							else
							{
								// 項目
								ws2.Cell(16 + i, 4).SetValue(detail.商品コード);

								// 内訳
								ws2.Cell(16 + i, 16).SetValue(detail.商品名);
							}
							// ①補助対象金額
							ws2.Cell(16 + i, 38).SetValue(detail.補助対象金額);
							//total += online.補助対象金額;

							// ②補助対象外金額
							//;
						}
						// ①小計
						//ws2.Cell(31, 42).SetValue(total);

						// ②小計
						//;

						// 総額（①＋②）
						//ws2.Cell(12, 9).SetValue(total);
					}

					////////////////////////////////////////
					//「事業完了報告書」
					////////////////////////////////////////

					IXLWorksheet ws3 = wb.Worksheet("事業完了報告書");
					ws3.Cell(2, 20).SetValue(string.Format("{0,4}", Date.Today.Year));
					ws3.Cell(2, 23).SetValue(string.Format("{0,2}", Date.Today.Month));
					ws3.Cell(2, 25).SetValue(string.Format("{0,2}", Date.Today.Day));
					if (2 == common.顧客情報.県番号.Length)
					{
						ws3.Cell(7, 19).SetValue(common.顧客情報.県番号.Substring(0, 1));
						ws3.Cell(7, 20).SetValue(common.顧客情報.県番号.Substring(1, 1));
					}
					// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 医療機関コードが7文字でない時にアプリケーションエラーが発生
					// Ver1.19(2023/04/13 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 「領収書内訳書」と「事業完了報告書」の医療機関コードが出力されない
					//if (MwsDefine.TokuisakiNoLength == clinicCode.Length)
					if (MwsDefine.ClinicCodeLength == clinicCode.Length)
					{
						ws3.Cell(8, 19).SetValue(clinicCode.Substring(0, 1));
						ws3.Cell(8, 20).SetValue(clinicCode.Substring(1, 1));
						ws3.Cell(8, 21).SetValue(clinicCode.Substring(2, 1));
						ws3.Cell(8, 22).SetValue(clinicCode.Substring(3, 1));
						ws3.Cell(8, 23).SetValue(clinicCode.Substring(4, 1));
						ws3.Cell(8, 24).SetValue(clinicCode.Substring(5, 1));
						ws3.Cell(8, 25).SetValue(clinicCode.Substring(6, 1));
					}
					ws3.Cell(9, 19).SetValue(common.顧客情報.顧客名);
					ws3.Cell(11, 18).SetValue(common.顧客情報.郵便番号);
					ws3.Cell(12, 15).SetValue(common.顧客情報.住所1);
					ws3.Cell(13, 19).SetValue(common.顧客情報.電話番号);

					// Ver1.12(2022/02/22):経理部専用 オンライン資格確認等事業完了報告書 修正依頼対応
					if (0 == common.顧客情報.開設者名.Length)
					{
						// 開設者がいない時は院長を印字
						ws3.Cell(10, 19).SetValue(common.顧客情報.院長名);
					}
					else
					{
						ws3.Cell(10, 19).SetValue(common.顧客情報.開設者名);
					}

					// Ver1.15(2023/01/13):19-経理部専用 オンライン資格確認等事業完了報告書 注文確認書の追加、領収証および書類送付状の削除
					////////////////////////////////////////
					//「注文確認書」
					////////////////////////////////////////

					IXLWorksheet ws4 = wb.Worksheet("注文確認書");
					ws4.Cell(1, 5).SetValue(DateTime.Today);
					ws4.Cell(6, 1).SetValue(common.顧客情報.顧客名);

					int price = 0;
					if (null != soft)
					{
						ws4.Cell(6, 1).SetValue(soft.顧客名);
						ws4.Cell(14, 2).SetValue(soft.受注日);
						price = (int)soft.受注金額税込;
					}
					// Ver1.18(2023/04/04 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
					if (null != destination)
					{
						ws4.Cell(14, 2).SetValue(destination.受注日);
						if (0 == price)
						{
							price = destination.金額;
						}
					}
					ws4.Cell(18, 4).SetValue(price);
					ws4.Cell(18, 5).SetValue(price);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
