//
// NoticeResearch.cs
//
// 現調通知クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.11 NTT現調プランに対応(2022/08/29 勝呂)
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
	/// <summary>
	/// 現地調査通知処理
	/// </summary>
	public static class NoticeResearch
	{
		/// <summary>
		/// オンライン資格確認通知結果「現調通知1(東)」
		/// </summary>
		public const string SheetNameResearch1East = "現調通知1(東)";

		/// <summary>
		/// オンライン資格確認通知結果「現調通知1(西)」
		/// </summary>
		public const string SheetNameResearch1West = "現調通知1(西)";

		/// <summary>
		/// オンライン資格確認通知結果「現調通知2」
		/// </summary>
		public const string SheetNameResearch2 = "現調通知2";

		/// <summary>
		/// オンライン資格確認通知結果「現調通知3(東)」
		/// </summary>
		public const string SheetNameResearch3East = "現調通知3(東)";

		/// <summary>
		/// オンライン資格確認通知結果「現調通知3(西)」
		/// </summary>
		public const string SheetNameResearch3West = "現調通知3(西)";

		/// <summary>
		/// オンライン資格確認通知結果「現調通知4(東)」
		/// </summary>
		public const string SheetNameResearch4East = "現調通知4(東)";

		/// <summary>
		/// オンライン資格確認通知結果「現調通知4(西)」
		/// </summary>
		public const string SheetNameResearch4West = "現調通知4(西)";

		/// <summary>
		/// 現調通知１：現地調査確定日の連絡（NTT東日本）
		///   現地調査確定日(M列)と現地調査確定時間(L列)が設定されたら担当者へ通知
		/// 抽出条件
		/// (1)現地調査確定日(M列)と現地調査確定時間(L列)が設定済み かつ 現地調査結果(O列)が未設定
		/// (2)現地調査確定日(M列)が翌日以降
		/// (3)[進捗管理表_作業情報] が存在しない もしくは 現調未設定
		/// (4)[進捗管理表_作業情報].現地調査確定日と現地調査確定日(M列)が違う
		/// </summary>
		/// <param name="prgressFilename">NTT東日本 進捗管理表ファイル名</param>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知数</returns>
		public static int Notice1East(string prgressFilename, List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, XLWorkbook wb, string connectStr)
		{
			int ret = 0;
			if (null != eastList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", connectStr);

				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch1East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					// (1)現地調査確定日(M列)と現地調査確定時間(L列)が設定済み かつ 現地調査結果(O列)が未設定
					if (east.現地調査確定日付.HasValue && 0 < east.現地調査確定時間.Length && 0 == east.現地調査結果.Length)
					{
						// (2)現地調査確定日(M列)が翌日以降
						if (Date.Today < east.現地調査確定日付.Value)
						{
							進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == east.病院ID);

							// (3)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
							if (null == db)
							{
								// MIC連絡担当者の通知情報を取得
								NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID, connectStr);
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
								
								// レコード追加：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日→通知１
								進捗管理表_作業情報 data = new 進捗管理表_作業情報();
								data.顧客No = east.病院ID;
								data.受付通番 = east.受付通番;
								data.進捗管理表ファイル名 = prgressFilename;
								data.現地調査確定日 = east.現地調査確定日付.Value.ToDateTime();
								data.現地調査確定日格納日時 = DateTime.Now;
								進捗管理表_作業情報.WriteProgressDatabase(data, false, connectStr);
							}
							// (3)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
							else if (db.Is現調未設定)
							{
								// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日→通知１
								db.受付通番 = east.受付通番;
								db.進捗管理表ファイル名 = prgressFilename;
								db.現地調査確定日 = east.現地調査確定日付.Value.ToDateTime();
								db.現地調査確定日格納日時 = DateTime.Now;
								進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
							}
							// (4) [進捗管理表_作業情報].現地調査確定日と現地調査確定日(M列)が違う
							else if (db.現地調査確定日.HasValue && east.現地調査確定日付 != db.現地調査確定日.Value.ToDate())
							{
								// MIC連絡担当者の通知情報を取得
								NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID, connectStr);
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

								// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=null、現地調査結果格納日時db=null→通知１
								db.受付通番 = east.受付通番;
								db.進捗管理表ファイル名 = prgressFilename;
								db.現地調査確定日 = east.現地調査確定日付.Value.ToDateTime();
								db.現地調査確定日格納日時 = DateTime.Now;
								db.現地調査結果 = string.Empty;
								db.現地調査結果格納日時 = null;
								進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 現調通知１：現地調査確定日の連絡（NTT西日本）
		///   訪問日(CH列)と訪問時間(CI列)が設定されたら担当者へ通知
		/// 抽出条件
		/// (1)訪問日(CH列)と訪問時間(CI列)が設定済み かつ 完了報告日(CJ列)が未設定
		/// (2)[進捗管理表_作業情報] が存在しない もしくは 現調未設定
		/// (3)訪問日(CH列)と[進捗管理表_作業情報].現地調査確定日が違う
		/// </summary>
		/// <param name="prgressFilename">NTT西日本 進捗管理表ファイル名</param>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知数</returns>
		public static int Notice1West(string prgressFilename, List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT西日本> westList, XLWorkbook wb, string connectStr)
		{
			int ret = 0;
			if (null != westList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", connectStr);

				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch1East);
				int row = 6;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					// (1)訪問日(CH列)と訪問時間(CI列)が設定済み かつ 完了報告日(CJ列)が未設定
					if (west.現調プラン_訪問日付.HasValue && 0 < west.現調プラン_訪問時間.Length && false == west.現調プラン_完了報告日付.HasValue)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == west.病院ID);

						// (2)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
						if (null == db)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID, connectStr);
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

							// レコード追加：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日→通知１
							進捗管理表_作業情報 data = new 進捗管理表_作業情報();
							data.顧客No = west.病院ID;
							data.受付通番 = west.受付通番;
							data.進捗管理表ファイル名 = prgressFilename;
							data.現地調査確定日 = west.現調プラン_訪問日付.Value.ToDateTime();
							data.現地調査確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(data, false, connectStr);
						}
						// (2)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
						else if (db.Is現調未設定)
						{
							// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日→通知１
							db.受付通番 = west.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.現地調査確定日 = west.現調プラン_訪問日付.Value.ToDateTime();
							db.現地調査確定日格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
						}
						// (3) 訪問日(CH列)と[進捗管理表_作業情報].現地調査確定日が違う
						else if (db.現地調査確定日.HasValue && west.現調プラン_訪問日付.Value != db.現地調査確定日.Value.ToDate())
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID, connectStr);
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

							// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=null、現地調査結果格納日時db=null→通知１
							db.受付通番 = west.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.現地調査確定日 = west.現調プラン_訪問日付.Value.ToDateTime();
							db.現地調査確定日格納日時 = DateTime.Now;
							db.現地調査結果 = string.Empty;
							db.現地調査結果格納日時 = null;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 現調通知２：提出漏れ（NTT東日本／NTT西日本）
		///   WEBヒアリングシートで現地調査依頼の送信後、進捗管理表に存在しない場合に、NTTへの提出漏れとして営業管理部に通知
		/// 抽出条件
		/// (1)WEBヒアリングシートの現調送信履歴に設定済み
		/// (2)進捗管理表に該当する医院が存在しない
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <returns>通知数</returns>
		public static int Notice2(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, List<進捗管理表_NTT西日本> westList, XLWorkbook wb)
		{
			List<vオンライン資格確認ユーザー> hsList = webHS.FindAll(p => p.現調送信履歴 != "");
			if (null != hsList)
			{
				int ret = 0;
				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch2);

				int row = 4;
				if (null != eastList)
				{
					// 進捗管理表（NTT東日本）
					// (1)WEBヒアリングシートの現調送信履歴に設定済み
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
					// (1)WEBヒアリングシートの現調送信履歴に設定済み
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
		/// 現調通知３：現調結果の連絡（NTT東日本）
		///   現地調査結果(O列)にOK or NG が記載されたら、担当者に通知
		/// 抽出条件
		/// (1)現地調査確定日(M列)と現地調査確定時間(L列)が設定済み かつ 現地調査結果(O列)にOK or NGが設定済み
		/// (2)[進捗管理表_作業情報] が存在しない もしくは 現調未設定
		/// (3)現地調査確定日(M列)と[進捗管理表_作業情報].現地調査確定日が同日 かつ 現地調査結果(O列)と[進捗管理表_作業情報].現地調査結果が違う
		/// (4)[進捗管理表_作業情報].現地調査結果格納日時が当日より過去の日
		/// </summary>
		/// <param name="prgressFilename">NTT東日本 進捗管理表ファイル名</param>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知数</returns>
		public static int Notice3East(string prgressFilename, List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, XLWorkbook wb, string connectStr)
		{
			int ret = 0;
			if (null != eastList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", connectStr);

				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch3East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					// (1)現地調査確定日(M列)と現地調査確定時間(L列)が設定済み かつ 現地調査結果(O列)にOK or NGが設定済み
					if (east.現地調査確定日付.HasValue && 0 < east.現地調査確定時間.Length && (east.現地調査結果_OK || east.現地調査結果_NG))
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == east.病院ID);

						// (2)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
						if (null == db)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID, connectStr);
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

							// レコード追加：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=現地調査結果、現地調査結果格納日時db=当日→通知３
							進捗管理表_作業情報 data = new 進捗管理表_作業情報();
							data.顧客No = east.病院ID;
							data.受付通番 = east.受付通番;
							data.進捗管理表ファイル名 = prgressFilename;
							data.現地調査確定日 = east.現地調査確定日付.Value.ToDateTime();
							data.現地調査確定日格納日時 = DateTime.Now;
							data.現地調査結果 = east.現地調査結果;
							data.現地調査結果格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(data, false, connectStr);
						}
						// (2)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
						else if (db.Is現調未設定)
						{
							// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=現地調査結果、現地調査結果格納日時db=当日→通知３
							db.受付通番 = east.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.現地調査確定日 = east.現地調査確定日付.Value.ToDateTime();
							db.現地調査確定日格納日時 = DateTime.Now;
							db.現地調査結果 = east.現地調査結果;
							db.現地調査結果格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
						}
						else if (db.現地調査確定日.HasValue)
						{
							// (3)現地調査確定日(M列)と[進捗管理表_作業情報].現地調査確定日が同日 かつ 現地調査結果(O列)と[進捗管理表_作業情報].現地調査結果が違う
							if (east.現地調査確定日付 == db.現地調査確定日.Value.ToDate() && east.現地調査結果 != db.現地調査結果)
							{
								if (0 == db.現地調査結果.Length)
								{
									// MIC連絡担当者の通知情報を取得
									NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID, connectStr);
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

									// レコード更新：現地調査結果db=現地調査結果、現地調査結果格納日時db=当日→通知３
									db.受付通番 = east.受付通番;
									db.進捗管理表ファイル名 = prgressFilename;
									db.現地調査結果 = east.現地調査結果;
									db.現地調査結果格納日時 = DateTime.Now;
									進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
								}
							}
							// (4)[進捗管理表_作業情報].現地調査結果格納日時が現地調査確定日(M列)より過去の日
							else if (db.現地調査確定日.Value.ToDate() < east.現地調査確定日付)
							{
								// MIC連絡担当者の通知情報を取得
								NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID, connectStr);
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

								// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=現地調査結果、現地調査結果格納日時db=当日→通知３
								db.受付通番 = east.受付通番;
								db.進捗管理表ファイル名 = prgressFilename;
								db.現地調査確定日 = east.現地調査確定日付.Value.ToDateTime();
								db.現地調査確定日格納日時 = DateTime.Now;
								db.現地調査結果 = east.現地調査結果;
								db.現地調査結果格納日時 = DateTime.Now;
								進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 現調通知３：現調結果の連絡（NTT西日本）
		///   完了報告日(CJ列)が設定されたら担当者に通知
		///   ※NTT東日本と同じ現地調査結果でなく、完了報告日のため、NGは通知できない
		/// 抽出条件
		/// (1)訪問日(CH列)、訪問時間(CI列)、完了報告日(CJ列)が設定済み
		/// (2)[進捗管理表_作業情報] が存在しない もしくは 現調未設定
		/// (3)訪問日(CH列)と[進捗管理表_作業情報].現地調査確定日が同日 かつ[進捗管理表_作業情報].現地調査結果が未設定
		/// </summary>
		/// <param name="prgressFilename">NTT西日本 進捗管理表ファイル名</param>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT西日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知数</returns>
		public static int Notice3West(string prgressFilename, List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT西日本> westList, XLWorkbook wb, string connectStr)
		{
			int ret = 0;
			if (null != westList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", connectStr);

				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch3East);
				int row = 6;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					// (1)訪問日(CH列)、訪問時間(CI列)、完了報告日(CJ列)が設定済み
					if (west.現調プラン_訪問日付.HasValue && 0 < west.現調プラン_訪問時間.Length && west.現調プラン_完了報告日付.HasValue)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == west.病院ID);

						// (2)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
						if (null == db)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID, connectStr);
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

							// レコード追加：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=OK、現地調査結果格納日時db=当日→通知３
							進捗管理表_作業情報 data = new 進捗管理表_作業情報();
							data.顧客No = west.病院ID;
							data.受付通番 = west.受付通番;
							data.進捗管理表ファイル名 = prgressFilename;
							data.現地調査確定日 = west.現調プラン_訪問日付.Value.ToDateTime();
							data.現地調査確定日格納日時 = DateTime.Now;
							data.現地調査結果 = "OK";
							data.現地調査結果格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(data, false, connectStr);
						}
						// (2)[進捗管理表_作業情報]が存在しない もしくは 現調未設定
						else if (db.Is現調未設定)
						{
							// レコード更新：現地調査確定日db=現地調査確定日、現地調査確定日格納日時db=当日、現地調査結果db=現地調査結果、現地調査結果格納日時db=当日→通知３
							db.受付通番 = west.受付通番;
							db.進捗管理表ファイル名 = prgressFilename;
							db.現地調査確定日 = west.現調プラン_訪問日付.Value.ToDateTime();
							db.現地調査確定日格納日時 = DateTime.Now;
							db.現地調査結果 = "OK";
							db.現地調査結果格納日時 = DateTime.Now;
							進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
						}
						else if (db.現地調査確定日.HasValue)
						{
							// (3)訪問日(CH列)と[進捗管理表_作業情報].現地調査確定日が同日 かつ [進捗管理表_作業情報].現地調査結果が未設定
							if (west.現調プラン_訪問日付 == db.現地調査確定日.Value.ToDate() && 0 == db.現地調査結果.Length)
							{
								if (0 == db.現地調査結果.Length)
								{
									// MIC連絡担当者の通知情報を取得
									NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID, connectStr);
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

									// レコード更新：現地調査結果db=現地調査結果、現地調査結果格納日時db=当日→通知３
									db.受付通番 = west.受付通番;
									db.進捗管理表ファイル名 = prgressFilename;
									db.現地調査結果 = "OK";
									db.現地調査結果格納日時 = DateTime.Now;
									進捗管理表_作業情報.WriteProgressDatabase(db, true, connectStr);
								}
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 現調通知４：新規案件出し忘れの連絡（NTT東日本）
		///   現地調査がOKにも関わらず、14日間が経過している場合に担当者に通知
		/// 抽出条件
		/// (1)現地調査結果(O列)にOKが格納
		/// (2)[進捗管理表_作業情報].現地調査確定日より14日間が経過している
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知数</returns>
		public static int Notice4East(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT東日本> eastList, XLWorkbook wb, string connectStr)
		{
			int ret = 0;
			if (null != eastList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", connectStr);
				List<vオンライン資格確認ユーザー> hearingSheet = webHS.FindAll(p => p.現調送信履歴 != "");

				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch4East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					// (1)現地調査結果(O列)にOKが格納
					if (east.現地調査結果_OK)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == east.病院ID);
						if (null != db)
						{
							if (db.現地調査結果格納日時.HasValue)
							{
								// (2)[進捗管理表_作業情報].現地調査確定日より14日間が経過している
								if (14 < (Date.Today - db.現地調査結果格納日時.Value.ToDate()))
								{
									// vオンライン資格確認ユーザー.現調送信履歴=null
									if (null == hearingSheet.Find(p => p.顧客No == db.顧客No))
									{
										// MIC連絡担当者の通知情報を取得
										NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, east.病院ID, connectStr);
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
			}
			return ret;
		}

		/// <summary>
		/// 現調通知４：新規案件出し忘れの連絡（NTT西日本）
		///   現地調査がOKにも関わらず、14日間が経過している場合に担当者に通知
		/// 抽出条件
		/// (1)完了報告日(CJ列)が設定済み
		/// (2)[進捗管理表_作業情報].現地調査確定日より14日間が経過している
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="wb">オンライン資格確認通知結果.xlsx</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知数</returns>
		public static int Notice4West(List<vオンライン資格確認ユーザー> webHS, List<進捗管理表_NTT西日本> westList, XLWorkbook wb, string connectStr)
		{
			int ret = 0;
			if (null != westList)
			{
				// 進捗管理表_作業情報の読込
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", connectStr);
				List<vオンライン資格確認ユーザー> hearingSheet = webHS.FindAll(p => p.現調送信履歴 != "");

				IXLWorksheet ws = wb.Worksheet(NoticeResearch.SheetNameResearch4East);
				int row = 6;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					// (1)完了報告日(CJ列)が設定済み
					if (west.現調プラン_完了報告日付.HasValue)
					{
						進捗管理表_作業情報 db = progressList.Find(p => p.顧客No == west.病院ID);
						if (null != db)
						{
							if (db.現地調査結果格納日時.HasValue)
							{
								// (2) [進捗管理表_作業情報].現地調査確定日より14日間が経過している
								if (14 < (Date.Today - db.現地調査結果格納日時.Value.ToDate()))
								{
									// vオンライン資格確認ユーザー.現調送信履歴=null
									if (null == hearingSheet.Find(p => p.顧客No == db.顧客No))
									{
										// MIC連絡担当者の通知情報を取得
										NoticeInfo notice = NoticeInfo.GetNoticeInfo(webHS, west.病院ID, connectStr);
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
									}
								}
							}
						}
					}
				}
			}
			return ret;
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
			IXLWorksheet ws = wb.Worksheet(sheetName);
			for (int i = 6; ; i++)
			{
				if ("" == ws.Cell(i, 5).GetString())
				{
					break;
				}
				進捗管理表_NTT東日本 data = new 進捗管理表_NTT東日本();
				data.SetWorksheetByオンライン資格確認通知結果(ws, i);
				list.Add(data);
			}
			return list.Count;
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
			IXLWorksheet ws = wb.Worksheet(sheetName);
			for (int i = 7; ; i++)
			{
				if ("" == ws.Cell(i, 5).GetString())
				{
					break;
				}
				進捗管理表_NTT西日本 data = new 進捗管理表_NTT西日本();
				data.SetWorksheetByオンライン資格確認通知結果(ws, i);
				list.Add(data);
			}
			return list.Count;
		}
	}
}
