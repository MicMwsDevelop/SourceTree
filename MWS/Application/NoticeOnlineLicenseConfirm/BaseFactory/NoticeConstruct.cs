//
// NoticeConstruct.cs
//
// 工事通知クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.11 NTT現調プランに対応(2022/08/29 勝呂)
// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
// Ver1.14 [進捗管理表_作業情報]に現調情報が登録されている場合、工事通知１(東西)を検出してもエクセル出力されなかった(2022/12/07 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.BaseFactory.Sales.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Sales;
using System;
using System.Collections.Generic;

namespace NoticeOnlineLicenseConfirm.BaseFactory
{
	public static class NoticeConstruct
	{
		/// <summary>
		/// オンライン資格確認通知結果「工事通知1(東)」
		/// </summary>
		public const string SheetNameConstruct1East = "工事通知1(東)";

		/// <summary>
		/// オンライン資格確認通知結果「工事通知1(西)」
		/// </summary>
		public const string SheetNameConstruct1West = "工事通知1(西)";

		/// <summary>
		/// オンライン資格確認通知結果「工事通知2」
		/// </summary>
		public const string SheetNameConstruct2 = "工事通知2";

		/// <summary>
		/// オンライン資格確認通知結果「工事通知3(東)」
		/// </summary>
		public const string SheetNameConstruct3East = "工事通知3(東)";

		/// <summary>
		/// オンライン資格確認通知結果「工事通知3(西)」
		/// </summary>
		public const string SheetNameConstruct3West = "工事通知3(西)";

		/// <summary>
		/// オンライン資格確認通知結果「工事通知4(東)」
		/// </summary>
		public const string SheetNameConstruct4East = "工事通知4(東)";

		/// <summary>
		/// オンライン資格確認通知結果「工事通知4(西)」
		/// </summary>
		public const string SheetNameConstruct4West = "工事通知4(西)";

		/// <summary>
		/// 工事通知１：工事確定日の連絡（NTT東日本）
		///   工事確定日(S列)が設定されたら担当者へ通知
		/// 抽出条件
		/// (1)工事確定日(S列)が設定済み
		/// (2)[進捗管理表_作業情報] が存在しない もしくは 工事未設定
		/// (3)工事確定日(S列)より[進捗管理表_作業情報].工事確定日が過去日
		/// </summary>
		/// <param name="prgressFilename">NTT東日本 進捗管理表ファイル名</param>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice1East(string prgressFilename, List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, XLWorkbook wb)
		{
			int ret = 0;
			if (null != eastList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", Program.gSettings.ConnectSales.ConnectionString);

				IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct1East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					// (1)工事確定日(S列)が設定済み
					if (east.工事確定日付.HasValue)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == east.病院ID);

						// (2)[進捗管理表_作業情報] が存在しない もしくは 工事未設定
						if (null == db)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;

							// レコード追加：工事確定日db=工事確定日、工事確定日格納日時db=当日→通知１
							進捗管理表_作業情報 data = new 進捗管理表_作業情報();
							data.顧客No = east.病院ID;
							data.受付通番 = east.受付通番;
							data.進捗管理表ファイル名 = prgressFilename;
							data.工事確定日 = east.工事確定日付.Value.ToDateTime();
							data.工事確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(data, false, Program.gSettings.ConnectSales.ConnectionString);
						}
						// (2)[進捗管理表_作業情報] が存在しない もしくは 工事未設定
						else if (db.Is工事未設定)
						{
							// Ver1.14 [進捗管理表_作業情報]に現調情報が登録されている場合、工事通知１(東西)を検出してもエクセル出力されなかった(2022/12/07 勝呂)
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;

							// レコード更新：工事確定日db=工事確定日、工事確定日格納日時db=当日→通知１
							db.受付通番 = east.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.工事確定日 = east.工事確定日付.Value.ToDateTime();
							db.工事確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, Program.gSettings.ConnectSales.ConnectionString);
						}
						// (3)工事確定日(S列)より[進捗管理表_作業情報].工事確定日が過去日
						else if (db.工事確定日.HasValue && db.工事確定日.Value.ToDate() < east.工事確定日付.Value)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;

							// レコード更新：工事確定日db=工事確定日、工事確定日格納日時db=当日→通知１
							db.受付通番 = east.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.工事確定日 = east.工事確定日付.Value.ToDateTime();
							db.工事確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, Program.gSettings.ConnectSales.ConnectionString);
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 工事通知１：工事確定日の連絡（NTT西日本）
		///   工事確定日(I列)が設定されたら担当者へ通知
		/// 抽出条件
		/// (1)工事確定日(I列)が設定済み
		/// (2)[進捗管理表_作業情報] が存在しない もしくは 工事未設定
		/// (3)工事確定日(I列)より[進捗管理表_作業情報].工事確定日が過去日
		/// </summary>
		/// <param name="prgressFilename">NTT西日本 進捗管理表ファイル名</param>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT西日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice1West(string prgressFilename, List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT西日本> westList, XLWorkbook wb)
		{
			int ret = 0;
			if (null != westList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", Program.gSettings.ConnectSales.ConnectionString);

				IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct1West);
				int row = 7;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					// (1)工事確定日(I列)が設定済み
					if (west.工事確定日付.HasValue)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == west.病院ID);

						// (2)[進捗管理表_作業情報] が存在しない もしくは 工事未設定
						if (null == db)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID);
							if (null != notice)
							{
								west.Notice = notice;
							}
							// シートに追加
							string[] record = west.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;

							// レコード追加：工事確定日db=工事確定日、工事確定日格納日時db=当日→通知１
							進捗管理表_作業情報 data = new 進捗管理表_作業情報();
							data.受付通番 = west.受付通番;
							data.顧客No = west.病院ID;
							data.進捗管理表ファイル名 = prgressFilename;
							data.工事確定日 = west.工事確定日付.Value.ToDateTime();
							data.工事確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(data, false, Program.gSettings.ConnectSales.ConnectionString);
						}
						// (2)[進捗管理表_作業情報] が存在しない もしくは 工事未設定
						else if (db.Is工事未設定)
						{
							// Ver1.14 [進捗管理表_作業情報]に現調情報が登録されている場合、工事通知１(東西)を検出してもエクセル出力されなかった(2022/12/07 勝呂)
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID);
							if (null != notice)
							{
								west.Notice = notice;
							}
							// シートに追加
							string[] record = west.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;

							// レコード更新：工事確定日db=工事確定日、工事確定日格納日時db=当日→通知１
							db.受付通番 = west.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.工事確定日 = west.工事確定日付.Value.ToDateTime();
							db.工事確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, Program.gSettings.ConnectSales.ConnectionString);
						}
						// (3)工事確定日(I列)より[進捗管理表_作業情報].工事確定日が過去日
						else if (db.工事確定日.HasValue && db.工事確定日.Value.ToDate() < west.工事確定日付.Value)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID);
							if (null != notice)
							{
								west.Notice = notice;
							}
							// シートに追加
							string[] record = west.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;

							// レコード更新：工事確定日db=工事確定日、工事確定日格納日時db=当日→通知１
							db.受付通番 = west.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.工事確定日 = west.工事確定日付.Value.ToDateTime();
							db.工事確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, Program.gSettings.ConnectSales.ConnectionString);
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 工事通知２：提出漏れ通知（NTT東日本／NTT西日本）
		///   WEBヒアリングシートでヒアリングシートの提出後、進捗管理表に存在しない場合に、NTTへの提出漏れとして営業管理部に通知
		/// 抽出条件
		/// (1)WEBヒアリングシートの送信履歴に設定済み
		/// (2)進捗管理表に該当する医院が存在しない
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice2(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, List<進捗管理表_NTT西日本> westList, XLWorkbook wb)
		{
			List<vオンライン資格確認ユーザー> hsList = webHS.FindAll(p => p.送信履歴 != "");
			if (null != hsList)
			{
				int ret = 0;
				IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct2);
				int row = 4;

				if (null != eastList)
				{
					// 進捗管理表（NTT東日本）
					// (1)WEBヒアリングシートの送信履歴に設定済み
					foreach (vオンライン資格確認ユーザー hs in hsList)
					{
						if (hs.IsNTT東日本管轄)
						{
							// (2)進捗管理表に該当する医院が存在しない
							if (-1 == eastList.FindIndex(p => p.病院ID == hs.顧客No))
							{
								string[] record = hs.GetData();
								for (int i = 0; i < record.Length; i++)
								{
									ws.Cell(row, i + 1).SetValue(record[i]);
								}
								row++;
								ret++;
							}
						}
					}
				}
				if (null != westList)
				{
					// 進捗管理表（NTT西日本）
					// (1)WEBヒアリングシートの送信履歴に設定済み
					foreach (vオンライン資格確認ユーザー hs in hsList)
					{
						if (false == hs.IsNTT東日本管轄)
						{
							// (2)進捗管理表に該当する医院が存在しない
							if (-1 == westList.FindIndex(p => p.病院ID == hs.顧客No))
							{
								string[] record = hs.GetData();
								for (int i = 0; i < record.Length; i++)
								{
									ws.Cell(row, i + 1).SetValue(record[i]);
								}
								row++;
								ret++;
							}
						}
					}
				}
				return ret;
			}
			return 0;
		}

		/// <summary>
		/// 工事通知３：ヒアリングシート不備の連絡（NTT東日本）
		///   NTTからヒアリングシートに不備があった場合、NTT東日本進捗管理表の本日の更新列に日付が格納され、かつ回答結果(BJ列、BL列)にNGが設定されたら
		///   修正箇所を担当者に通知
		/// 抽出条件
		/// (1)本日の更新分(BI列)に進捗管理表のファイル名と同じ日付が設定
		/// (2)回答結果(BJ列、BL列)のいずれかにNGが設定
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="eastFileDate">NTT東日本 進捗管理表ファイル作成日</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice3East(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, Date? eastFileDate, XLWorkbook wb)
		{
			int ret = 0;
			IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct3East);
			int row = 6;
			foreach (進捗管理表_NTT東日本 east in eastList)
			{
				if (east.本日の更新分日付.HasValue)
				{
					// (1)本日の更新分(BI列)に進捗管理表のファイル名と同じ日付が設定
					if (eastFileDate.Value == east.本日の更新分日付.Value)
					{
						// (2)回答結果(BJ列、BL列)のいずれかにNGが設定
						if (east.回答結果1_NG || east.回答結果2_NG)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 工事通知３：ヒアリングシート不備の連絡（NTT西日本）
		///   NTTからヒアリングシートに不備があった場合、ヒアリングシート修正依頼日(AP列)に日付が格納される。連絡票の連絡内容を担当者に通知
		/// 抽出条件
		/// (1)ヒアリングシート修正依頼日(AP列)に進捗管理表のファイル名と同じ日付が設定
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT西日本 進捗管理表</param>
		/// <param name="eastFileDate">NTT西日本 進捗管理表ファイル作成日</param>
		/// <param name="contractList">NTT西日本 連絡票</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice3West(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT西日本> westList, Date? westFileDate, List<連絡票_NTT西日本> contractList, XLWorkbook wb)
		{
			int ret = 0;
			IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct3West);
			int row = 7;
			foreach (進捗管理表_NTT西日本 west in westList)
			{
				if (west.ヒアリングシート修正依頼日付.HasValue)
				{
					// (1)ヒアリングシート修正依頼日(AP列)に進捗管理表のファイル名と同じ日付が設定
					if (westFileDate.Value == west.ヒアリングシート修正依頼日付.Value)
					{
						// MIC連絡担当者の通知情報を取得
						NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID);
						if (null != notice)
						{
							west.Notice = notice;
						}
						// シートに追加
						string[] record = west.GetData();
						for (int i = 0; i < record.Length; i++)
						{
							ws.Cell(row, i + 1).SetValue(record[i]);
						}
						if (null != contractList)
						{
							// NTT西日本進捗管理表のヒアリングシート修正依頼日とNTT西日本連絡票の依頼日が違う場合があり、NTT通番がユニークでないため、正しくマッチングできない
							//連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番 && p.依頼日付.Value == west.ヒアリングシート修正依頼日付.Value);
							連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番);
							if (null != contract)
							{
								ws.Cell(row, record.Length - 1).SetValue(contract.連絡項目);
								ws.Cell(row, record.Length).SetValue(contract.連絡内容);
							}
						}
						row++;
						ret++;
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT東日本）
		///   工事確定日まで１４日を切っているにも関わらずヒアリングシートが未完成の場合、担当者に通知
		/// 抽出条件
		/// (1)工事確定日(S列)が当日以降
		/// (2)回答結果(BJ列) または 回答結果(BL列)のいずれかにNGが設定
		/// (3)当日が工事確定日(S列)の１４日以内
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice4East(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, XLWorkbook wb)
		{
			int ret = 0;
			if (null != eastList)
			{
				IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct4East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					if (east.工事確定日付.HasValue)
					{
						// (2)回答結果(BJ列) または 回答結果(BL列)のいずれかにNGが設定
						if (east.回答結果1_NG || east.回答結果2_NG)
						{
							// Ver1.06 通知５の判定を本日以降の工事確定日付のみ検索するように抽出条件を変更(2022/05/17 勝呂)
							if (Date.Today <= east.工事確定日付.Value)
							{
								// (3)当日が工事確定日(S列)の１４日以内
								if (14 >= (east.工事確定日付.Value - Date.Today))
								{
									// MIC連絡担当者の通知情報を取得
									NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID);
									if (null != notice)
									{
										east.Notice = notice;
									}
									// シートに追加
									string[] record = east.GetData();
									for (int i = 0; i < record.Length; i++)
									{
										ws.Cell(row, i + 1).SetValue(record[i]);
									}
									row++;
									ret++;
								}
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT西日本）
		///   工事確定日まで１４日を切っているにも関わらずヒアリングシートが未完成の場合、担当者に通知
		/// 抽出条件
		/// (1)工事確定日(I列)が当日以降
		/// (2)ヒアリングシートチェック結果(AO列)がNG
		/// (3)当日が工事確定日(I列)の１４日以内
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="contractList">NTT西日本 連絡票</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice4West(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT西日本> westList, List<連絡票_NTT西日本> contractList, XLWorkbook wb)
		{
			int ret = 0;
			if (null != westList)
			{
				IXLWorksheet ws = wb.Worksheet(NoticeConstruct.SheetNameConstruct4West);
				int row = 7;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					// (1)工事確定日(I列)が当日以降
					if (west.工事確定日付.HasValue)
					{
						// (2)ヒアリングシートチェック結果(AO列)がNG
						if (west.ヒアリングシートチェック結果_NG)
						{
							// Ver1.06 通知５の判定を本日以降の工事確定日付のみ検索するように抽出条件を変更(2022/05/17 勝呂)
							if (Date.Today <= west.工事確定日付.Value)
							{
								// (3)当日が工事確定日(I列)の１４日以内
								if (14 >= (west.工事確定日付.Value - Date.Today))
								{
									// MIC連絡担当者の通知情報を取得
									NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID);
									if (null != notice)
									{
										west.Notice = notice;
									}
									// シートに追加
									string[] record = west.GetData();
									for (int i = 0; i < record.Length; i++)
									{
										ws.Cell(row, i + 1).SetValue(record[i]);
									}
									if (null != contractList)
									{
										// NTT西日本進捗管理表のヒアリングシート修正依頼日とNTT西日本連絡票の依頼日が違う場合があり、NTT通番がユニークでないため、正しくマッチングできない
										//連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番 && p.依頼日付.Value == west.ヒアリングシート修正依頼日付.Value);
										連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番);
										if (null != contract)
										{
											ws.Cell(row, record.Length - 1).SetValue(contract.連絡項目);
											ws.Cell(row, record.Length).SetValue(contract.連絡内容);
										}
									}
									row++;
									ret++;
								}
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// NTT東日本 工事結果の設定
		/// </summary>
		/// <param name="prgressFilename">NTT東日本 進捗管理表ファイル名</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
		public static void SetEastConstrctionResult(string prgressFilename, List<進捗管理表_NTT東日本> eastList)
		{
			if (null != eastList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", Program.gSettings.ConnectSales.ConnectionString);

				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					if (east.工事結果_OK)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == east.病院ID);
						if (null != db)
						{
							if ("OK" != db.工事結果)
							{
								db.受付通番 = east.受付通番;
								db.進捗管理表ファイル名 = prgressFilename;
								db.工事結果 = "OK";
								db.工事結果格納日時 = DateTime.Now;
								進捗管理表_作業情報.WriteProgressDatabase(db, true, Program.gSettings.ConnectSales.ConnectionString);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// NTT西日本 工事結果の設定
		/// </summary>
		/// <param name="prgressFilename">NTT西日本 進捗管理表ファイル名</param>
		/// <param name="eastList">NTT西日本 進捗管理表</param>
		/// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
		public static void SetWestConstrctionResult(string prgressFilename, List<進捗管理表_NTT西日本> westList)
		{
			if (null != westList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", Program.gSettings.ConnectSales.ConnectionString);

				foreach (進捗管理表_NTT西日本 west in westList)
				{
					if (west.工事結果_OK)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == west.病院ID);
						if (null != db)
						{
							if ("OK" != db.工事結果)
							{
								db.受付通番 = west.受付通番;
								db.進捗管理表ファイル名 = prgressFilename;
								db.工事結果 = "OK";
								db.工事結果格納日時 = DateTime.Now;
								進捗管理表_作業情報.WriteProgressDatabase(db, true, Program.gSettings.ConnectSales.ConnectionString);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// NTT東日本 出力レコードリストの取得
		/// </summary>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="sheetName">シート名</param>
		/// <param name="list">出力レコードリスト</param>
		/// <returns>出力レコード数</returns>
		public static int GetEastOutputRecordList(XLWorkbook wb, string sheetName, List<進捗管理表_NTT東日本> list)
		{
			return NoticeResearch.GetEastOutputRecordList(wb, sheetName, list);
		}

		/// <summary>
		/// NTT西日本 出力レコードリストの取得
		/// </summary>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="sheetName">シート名</param>
		/// <param name="list">出力レコードリスト</param>
		/// <returns>出力レコード数</returns>
		public static int GetWestOutputRecordList(XLWorkbook wb, string sheetName, List<進捗管理表_NTT西日本> list)
		{
			return NoticeResearch.GetWestOutputRecordList(wb, sheetName, list);
		}
	}
}
