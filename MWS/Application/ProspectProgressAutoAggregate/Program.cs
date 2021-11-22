//
// Program.cs
// 
// 見込進捗自動集計 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
// Ver1.01 新設の商品区分2(0030) paletteおまとめをpalette売上に含める(2021/09/06 勝呂)
// Ver1.02 見込進捗詳細をPCA勘定科目の変更に対応(2021/09/15 勝呂)
// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
// Ver1.04 来期追加と売上実績設定機能を追加（管理者用）(2021/10/20 勝呂)
// Ver1.05 予測連絡用シート名と売上進捗シートの変更(2021/11/10 勝呂)
// Ver1.06 予測連絡用の内容を元の見込進捗に合わせる(2021/11/16 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.ProspectProgressAutoAggregate;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.ProspectProgressAutoAggregate;
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProspectProgressAutoAggregate
{
	static class Program
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static SqlServerConnectSettings gSettings { get; set; }

		/// <summary>
		/// バージョン情報
		/// </summary>
		public static readonly string VersionStr = "Ver1.06 (2021/11/16)";

		/// <summary>
		/// 起動日
		/// </summary>
		public static Date BootDate { get; set; }

		/// <summary>
		/// MIC創立年
		/// </summary>
		private static readonly int gMicStartYear = 1974;

		/// <summary>
		/// カレント決算期
		/// </summary>
		public static readonly int gCurrentPeriod = 47;

		/// <summary>
		/// 部門コード
		/// </summary>
		public static readonly string[] gBumonCodes = { "50", "70", "60", "75", "76", "80" };

		/// <summary>
		/// PCA部門コード
		/// </summary>
		private static readonly string[] gPcaBumonCoeds = { "081", "082", "083", "086", "087", "085" };

		/// <summary>
		/// palette売上 行番号
		/// </summary>
		private static readonly int ROW_palette売上 = 6;

		/// <summary>
		/// paletteES売上 行番号
		/// </summary>
		private static readonly int ROW_paletteES売上 = 13;

		/// <summary>
		/// その他ｿﾌﾄ売上 行番号
		/// </summary>
		private static readonly int ROW_その他ｿﾌﾄ売上 = 20;

		/// <summary>
		/// ハード売上 行番号
		/// </summary>
		private static readonly int ROW_ハード売上 = 28;

		/// <summary>
		/// 技術指導売上 行番号
		/// </summary>
		private static readonly int ROW_技術指導売上 = 35;

		/// <summary>
		/// ハード保守 行番号
		/// </summary>
		private static readonly int ROW_ハード保守 = 49;

		/// <summary>
		/// ソフト保守 行番号
		/// </summary>
		private static readonly int ROW_ソフト保守 = 56;

		/// <summary>
		/// 周辺機器売上 行番号
		/// </summary>
		private static readonly int ROW_周辺機器売上 = 63;

		/// <summary>
		/// その他売上 行番号
		/// </summary>
		private static readonly int ROW_その他売上 = 70;

		/// <summary>
		/// ｵﾝ資格導入売上 行番号
		/// </summary>
		private static readonly int ROW_ｵﾝ資格導入売上 = 78;

		/// <summary>
		/// Curline本体 行番号
		/// </summary>
		private static readonly int ROW_Curline本体_46 = 78;

		/// <summary>
		/// Curline替ﾌﾞﾗｼ 行番号
		/// </summary>
		private static readonly int ROW_Curline替ﾌﾞﾗｼ_46 = 86;

		/// <summary>
		/// Curline本体 行番号
		/// </summary>
		private static readonly int ROW_Curline本体_47 = 85;

		/// <summary>
		/// Curline替ﾌﾞﾗｼ 行番号
		/// </summary>
		private static readonly int ROW_Curline替ﾌﾞﾗｼ_47 = 93;

		/// <summary>
		/// 売上実績リスト
		/// </summary>
		private static List<売上実績> 売上実績_List { get; set; }

		/// <summary>
		/// 売上予想リスト
		/// </summary>
		private static List<売上予想> 売上予想_List { get; set; }

		/// <summary>
		/// paletteES 売上予想リスト
		/// </summary>
		private static List<売上予想ES> 売上予想ES_List { get; set; }

		/// <summary>
		/// palette ES 売上進捗リスト
		/// </summary>
		private static List<売上進捗ES> 売上進捗ES_List { get; set; }

		/// <summary>
		/// おまとめプラン 売上進捗リスト
		/// </summary>
		private static List<売上進捗まとめ> 売上進捗まとめ_List { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = SqlServerConnectSettingsIF.GetSettings();

#if DEBUG
			//BootDate = new Date(2020, 8, 1);
			BootDate = Date.Today;
#else
			BootDate = Date.Today;
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					int period = GetPeriod(BootDate);
					string msg;
					return AutoAggregate(period, out msg);
				}
			}
			Application.Run(new Forms.MainForm());
			return 0;
		}

		/// <summary>
		/// 決算期間の取得
		/// </summary>
		/// <param name="period">決算期</param>
		/// <returns></returns>
		private static Span GetTerm(int period)
		{
			Date start = new Date(gMicStartYear + period, 8, 1);
			Date end = start.PlusYears(1).LastDayOfLasMonth();
			return new Span(start, end);
		}

		/// <summary>
		/// 期の取得
		/// </summary>
		/// <returns></returns>
		public static int GetPeriod(Date date)
		{
			int year = date.Year;
			if (date.Month < 8)
			{
				year--;
			}
			return year - gMicStartYear;
		}

		/// <summary>
		/// 期→開始日付
		/// </summary>
		/// <param name="period">期</param>
		/// <returns>開始日付</returns>
		public static Date PeriodToDate(int period)
		{
			return new Date(gMicStartYear + period, 8, 1);
		}

		/// <summary>
		/// Excelオリジナルファイルパス名の取得
		/// </summary>
		/// <param name="period">決算期</param>
		/// <returns></returns>
		private static string GetExcelOriginalPathname(int period)
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format("見込進捗_{0:D2}期.xlsx.org", period));
		}

		/// <summary>
		/// Excel出力ファイルパス名の取得
		/// </summary>
		/// <param name="period">決算期</param>
		/// <returns></returns>
		private static string GetExcelPathname(int period)
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format("見込進捗_{0:D2}期_{1}.xlsx", period, Date.Today.GetNumeralString()));
		}

		/// <summary>
		/// 自動進捗Excelファイル出力
		/// </summary>
		/// <param name="period">決算期</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>判定</returns>
		public static int AutoAggregate(int period, out string msg)
		{
			msg = string.Empty;

			売上実績_List = null;
			売上予想_List = new List<売上予想>();
			売上予想ES_List = new List<売上予想ES>();
			売上進捗ES_List = new List<売上進捗ES>();
			売上進捗まとめ_List = new List<売上進捗まとめ>();

			// 上期８月～来期８月
			Span term = GetTerm(period);
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				売上実績_List = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 >= {0} AND 実績日 <= {1}", term.Start.ToIntYMD(), term.End.ToIntYMD()), "実績日, 営業部コード", gSettings.Charlie.ConnectionString);
				if (null == 売上実績_List)
				{
					msg = "当日の売上実績が登録されていないため見込進捗を出力できません。";
					return 1;
				}
				Date start = term.Start;
				for (int i = 0; i < 13; i++)
				{
					List<売上予想> work = ProspectProgressAutoAggregateAccess.Select_売上予想(start, gSettings.Junp.ConnectionString);
					if (null != work)
					{
						売上予想_List.AddRange(work);
					}
					start = start.PlusMonths(1);
				}
				売上予想ES_List = ProspectProgressAutoAggregateAccess.Select_売上予想ES(term.Start, term.End, gSettings.Junp.ConnectionString);
				売上進捗ES_List = ProspectProgressAutoAggregateAccess.Select_売上進捗ES(gSettings.Junp.ConnectionString);
				売上進捗まとめ_List = ProspectProgressAutoAggregateAccess.Select_売上進捗まとめ(gSettings.Junp.ConnectionString);

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				return 1;
			}
			try
			{
				string pathname = GetExcelPathname(period);
				File.Copy(GetExcelOriginalPathname(period), pathname, true);
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					//「予測連絡用」
					予測連絡用(wb, period);

					//「売上実績」
					売上実績(wb, term);

					//「見込進捗_詳細」
					見込進捗詳細(wb, period, term);

					// Excelファイルの保存
					wb.SaveAs(pathname);

					// カーソルを元に戻す
					Cursor.Current = preCursor;

					// Excelの起動
					using (Process process = new Process())
					{
						process.StartInfo.FileName = pathname;
						process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
						process.Start();
					}
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				return 1;
			}
			return 0;
		}

		/// <summary>
		/// 「予測連絡用」
		/// </summary>
		/// <param name="wb">workbook</param>
		/// <param name="period">決算期</param>
		private static void 予測連絡用(XLWorkbook wb, int period)
		{
			IXLWorksheet ws = wb.Worksheet("予測連絡用");

			if (period != gCurrentPeriod)
			{
				// カレント決算期でない時は8/1とする
				BootDate = GetTerm(period).Start;
			}
			// 前月実績値の設定
			予測連絡用_前月実績値設定(ws, period, BootDate);

			// 当月予算予測進捗値の設定
			予測連絡用_当月予算予測進捗値設定(ws, period, BootDate);

			// 翌月予算予測進捗値の設定
			予測連絡用_翌月予算進捗値設定(ws, period, BootDate);

			// バージョン情報
			ws.Cell(21, 25).SetValue(VersionStr);

			// 更新日
			ws.Cell(2, 25).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));
		}

		/// <summary>
		/// 「予測連絡用」前月実績値の設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="bootDate">カレント日</param>
		private static void 予測連絡用_前月実績値設定(IXLWorksheet ws, int period, Date bootDate)
		{
			// 先月初日
			YearMonth lastYM = bootDate.FirstDayOfLasMonth().ToYearMonth();
			ws.Cell(6, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(実績)", lastYM.Year, lastYM.Month));

			int lastFirstDay = bootDate.FirstDayOfLasMonth().ToIntYMD();
			if (7 == lastYM.Month)
			{
				// 前期
				Span term = GetTerm(period - 1);
				List<売上実績> list = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 >= {0} AND 実績日 <= {1}", term.Start.ToIntYMD(), term.End.ToIntYMD()), "実績日, 営業部コード", gSettings.Charlie.ConnectionString);

				// 前期７月の実績値の設定
				for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
				{
					売上実績 result = list.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(6, 5 + j).SetValue(result.予算ES);
						ws.Cell(6, 6 + j).SetValue(result.予算まとめ);
						ws.Cell(6, 7 + j).SetValue(result.予算売上);
						ws.Cell(7, 5 + j).SetValue(result.実績ES);
						ws.Cell(7, 6 + j).SetValue(result.実績まとめ);
						ws.Cell(7, 7 + j).SetValue(result.実績売上);
						ws.Cell(10, 5 + j).SetValue(result.予算営業損益);
						ws.Cell(11, 5 + j).SetValue(result.実績営業損益);
					}
				}
				// 前期累計実績値の設定
				for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
				{
					List<売上実績> result = list.FindAll(p => p.営業部コード == gBumonCodes[i]);
					if (null != result)
					{
						ws.Cell(14, 5 + j).SetValue(result.Sum(p =>p.予算ES));
						ws.Cell(14, 6 + j).SetValue(result.Sum(p => p.予算まとめ));
						ws.Cell(14, 7 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(15, 5 + j).SetValue(result.Sum(p => p.実績ES));
						ws.Cell(15, 6 + j).SetValue(result.Sum(p => p.実績まとめ));
						ws.Cell(15, 7 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(18, 5 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(19, 5 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				ws.Cell(14, 2).SetValue(string.Format("{0}期累計\r\n(実績)", period - 1));
			}
			else
			{
				// 前月の実績値の設定
				for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
				{
					売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(6, 5 + j).SetValue(result.予算ES);
						ws.Cell(6, 6 + j).SetValue(result.予算まとめ);
						ws.Cell(6, 7 + j).SetValue(result.予算売上);
						ws.Cell(7, 5 + j).SetValue(result.実績ES);
						ws.Cell(7, 6 + j).SetValue(result.実績まとめ);
						ws.Cell(7, 7 + j).SetValue(result.実績売上);
						ws.Cell(10, 5 + j).SetValue(result.予算営業損益);
						ws.Cell(11, 5 + j).SetValue(result.実績営業損益);
					}
				}
				// 今期累計実績値の設定
				Span term = GetTerm(period);
				term = new Span(term.Start, bootDate.LastDayOfLasMonth());
				for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
				{
					List<売上実績> result = 売上実績_List.FindAll(p => p.営業部コード == gBumonCodes[i] && p.実績日 >=term.Start.ToIntYMD() && p.実績日 <= term.End.ToIntYMD());
					if (null != result)
					{
						ws.Cell(14, 5 + j).SetValue(result.Sum(p => p.予算ES));
						ws.Cell(14, 6 + j).SetValue(result.Sum(p => p.予算まとめ));
						ws.Cell(14, 7 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(15, 5 + j).SetValue(result.Sum(p => p.実績ES));
						ws.Cell(15, 6 + j).SetValue(result.Sum(p => p.実績まとめ));
						ws.Cell(15, 7 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(18, 5 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(19, 5 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				ws.Cell(14, 2).SetValue(string.Format("{0}期累計\r\n(実績)", period));
			}
		}

		/// <summary>
		/// 「予測連絡用」当月予算予測進捗値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="bootDate">カレント日</param>
		private static void 予測連絡用_当月予算予測進捗値設定(IXLWorksheet ws, int period, Date bootDate)
		{
			// 当月初日
			int thisFirstDay = bootDate.FirstDayOfTheMonth().ToIntYMD();
			YearMonth thisYM = bootDate.ToYearMonth();
			ws.Cell(22, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(進捗)", thisYM.Year, thisYM.Month));

			for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == thisFirstDay);
				if (null != result)
				{
					ws.Cell(22, 5 + j).SetValue(result.予算ES);
					ws.Cell(22, 6 + j).SetValue(result.予算まとめ);
					ws.Cell(22, 7 + j).SetValue(result.予算売上);
					ws.Cell(23, 5 + j).SetValue(result.予測ES);
					ws.Cell(23, 6 + j).SetValue(result.予測まとめ);
					ws.Cell(23, 7 + j).SetValue(result.予測売上);
					ws.Cell(27, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(28, 5 + j).SetValue(result.予測営業損益);
				}
				// 進捗
				int price = 0;
				List<売上予想> sale = 売上予想_List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<売上予想ES> soft = 売上予想ES_List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.計上月 == thisYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				ws.Cell(24, 7 + j).SetValue(To金額千円単位(price)); // 金額千円単位

				List<売上進捗ES> es = 売上進捗ES_List.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == thisYM.ToString());
				if (null != es)
				{
					ws.Cell(24, 5 + j).SetValue(es.Count());
				}
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == thisYM.ToString());
				if (null != matome)
				{
					ws.Cell(24, 6 + j).SetValue(matome.Count);
				}
			}
		}

		/// <summary>
		/// 「予測連絡用」翌月予算進捗値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="bootDate">カレント日</param>
		private static void 予測連絡用_翌月予算進捗値設定(IXLWorksheet ws, int period, Date bootDate)
		{
			// 来月初日
			int nextFirstDay = bootDate.FirstDayOfNextMonth().ToIntYMD();
			YearMonth nextYM = bootDate.FirstDayOfNextMonth().ToYearMonth();
			ws.Cell(31, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(進捗)", nextYM.Year, nextYM.Month));

			for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == nextFirstDay);
				if (null != result)
				{
					ws.Cell(31, 5 + j).SetValue(result.予算ES);
					ws.Cell(31, 6 + j).SetValue(result.予算まとめ);
					ws.Cell(31, 7 + j).SetValue(result.予算売上);
					ws.Cell(35, 5 + j).SetValue(result.予算営業損益);
				}
				// 進捗
				int price = 0;
				List<売上予想> sale = 売上予想_List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.集計月 == nextYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<売上予想ES> soft = 売上予想ES_List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.計上月 == nextYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != matome)
				{
					price += matome.Sum(p => p.金額);
					ws.Cell(32, 6 + j).SetValue(matome.Count);
				}
				ws.Cell(32, 7 + j).SetValue(To金額千円単位(price));

				List<売上進捗ES> es = 売上進捗ES_List.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != es)
				{
					ws.Cell(32, 5 + j).SetValue(es.Count());
				}
			}
		}

		/// <summary>
		/// 「売上実績」
		/// </summary>
		/// <param name="wb">workbool</param>
		/// <param name="term">決算期間</param>
		private static void 売上実績(XLWorkbook wb, Span term)
		{
			IXLWorksheet ws = wb.Worksheet("売上実績");
			Date date = term.Start;

			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 6);  // 上期8月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 16);  // 上期9月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 26);  // 上期10月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 44);  // 上期11月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 54);  // 上期12月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 64);  // 上期1月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 90);  // 下期2月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 100);  // 下期3月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 110);  // 下期4月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 128);  // 下期5月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 138);  // 下期6月実績
			date = date.PlusMonths(1);
			売上実績_予算予測実績値設定(ws, date.ToIntYMD(), 148);  // 下期7月実績

			// 更新日
			ws.Cell(2, 25).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));
		}

		/// <summary>
		/// 「売上実績」予算予測実績値設定
		/// </summary>
		/// <param name="ws"></param>
		/// <param name="date"></param>
		/// <param name="row"></param>
		private static void 売上実績_予算予測実績値設定(IXLWorksheet ws, int date, int row)
		{
			for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
			{
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(row, 5 + j).SetValue(result.予算ES);
					ws.Cell(row + 1, 5 + j).SetValue(result.予測ES);
					ws.Cell(row + 2, 5 + j).SetValue(result.実績ES);
					ws.Cell(row, 6 + j).SetValue(result.予算まとめ);
					ws.Cell(row + 1, 6 + j).SetValue(result.予測まとめ);
					ws.Cell(row + 2, 6 + j).SetValue(result.実績まとめ);
					ws.Cell(row, 7 + j).SetValue(result.予算売上);
					ws.Cell(row + 1, 7 + j).SetValue(result.予測売上);
					ws.Cell(row + 2, 7 + j).SetValue(result.実績売上);
					ws.Cell(row + 5, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(row + 6, 5 + j).SetValue(result.予測営業損益);
					ws.Cell(row + 7, 5 + j).SetValue(result.実績営業損益);
				}
			}
		}

		/// <summary>
		/// 「見込進捗_詳細」
		/// </summary>
		/// <param name="wb">workbook</param>
		/// <param name="period">決算期</param>
		/// <param name="term">決算期間</param>
		private static void 見込進捗詳細(XLWorkbook wb, int period, Span term)
		{
			IXLWorksheet ws = wb.Worksheet("見込進捗_詳細");
			Date start = term.Start;

			// 上期８月～来期８月
			for (int i = 0, j = 6; i < 13; i++, j += 4)
			{
				見込進捗詳細_実績値設定(ws, period, start.ToYearMonth(), j);
				start = start.PlusMonths(1);
			}
			// 更新日
			ws.Cell(2, 2).SetValue(DateTime.Now.ToString());
		}

		/// <summary>
		/// 「見込進捗_詳細」実績値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="ym">該当月</param>
		/// <param name="col">カラム</param>
		private static void 見込進捗詳細_実績値設定(IXLWorksheet ws, int period, YearMonth ym, int col)
		{
			string[] bumon1 = { "081", "082", "083", "086", "087", "085" };					// 東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部
			string[] bumon2 = { "075", "081", "082", "083", "086", "087", "085" };          // ヘルスケア営業部、東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部
			string[] bumon3 = { "081", "082", "083", "086", "087", "085", "011" };			// 東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部、営業管理部

			List<売上予想> result = 売上予想_List.FindAll(p => p.集計月 == ym);

			// palette売上
			// Ver1.01 新設の商品区分2(0030) paletteおまとめをpalette売上に含める(2021/09/06 勝呂)
			List<売上予想> pca = result.FindAll(p => p.商品区分コード == "28" || p.商品区分コード == "30");
			if (gCurrentPeriod == period)
			{
				// カレント決算期
				YearMonth nextYM = Date.Today.PlusMonths(1).ToYearMonth();
				if (ym == nextYM)
				{
					// 翌月
					for (int i = 0, j = ROW_palette売上; i < bumon1.Length; i++, j++)
					{
						int price = 0;
						List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
						if (null != sale)
						{
							price = sale.Sum(p => p.金額);
						}
						// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
						List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == nextYM.ToString());
						if (null != matome)
						{
							price += matome.Sum(p => p.金額);
						}
						ws.Cell(j, col).SetValue(To金額千円単位(price));
					}
				}
				else
				{
					// 翌月以外
					for (int i = 0, j = ROW_palette売上; i < bumon1.Length; i++, j++)
					{
						List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
						if (null != sale)
						{
							ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
						}
					}
				}
			}
			else
			{
				for (int i = 0, j = ROW_palette売上; i < bumon1.Length; i++, j++)
				{
					List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
					if (null != sale)
					{
						ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
					}
				}
			}
			// paletteES
			pca = result.FindAll(p => p.商品区分コード == "27");
			for (int i = 0, j = ROW_paletteES売上; i < bumon1.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// その他ｿﾌﾄ売上
			pca = result.FindAll(p => p.商品区分コード == "1");
			for (int i = 0, j = ROW_その他ｿﾌﾄ売上; i < bumon2.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon2[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// ハード売上
			pca = result.FindAll(p => p.商品区分コード == "2");
			for (int i = 0, j = ROW_ハード売上; i < bumon1.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 技術指導売上
			pca = result.FindAll(p => p.商品区分コード == "40");
			for (int i = 0, j = ROW_技術指導売上; i < bumon1.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// ハード保守
			pca = result.FindAll(p => p.商品区分コード == "7");
			for (int i = 0, j = ROW_ハード保守; i < bumon1.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// ソフト保守
			// ソフト保守はES売上予想Listから金額を取得
			List<売上予想ES> soft = 売上予想ES_List.FindAll(p => p.計上月 == ym.ToString());
			for (int i = 0, j = ROW_ソフト保守; i < bumon1.Length; i++, j++)
			{
				List<売上予想ES> sale = soft.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.売上金額)));
				}
			}
			// 周辺機器売上
			pca = result.FindAll(p => p.商品区分コード == "97");
			for (int i = 0, j = ROW_周辺機器売上; i < bumon1.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			if (46 < period)
			{
				// 47期以降
				// その他売上
				pca = result.FindAll(p => p.商品区分コード == "99");
				for (int i = 0, j = ROW_その他売上; i < bumon3.Length; i++, j++)
				{
					List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon3[i]);
					if (null != sale)
					{
						ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
					}
				}
				// ｵﾝ資格導入売上
				pca = result.FindAll(p => p.商品区分コード == "50");
				for (int i = 0, j = ROW_ｵﾝ資格導入売上; i < bumon1.Length; i++, j++)
				{
					List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
					if (null != sale)
					{
						ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
					}
				}
			}
			else
			{
				// 46期
				// その他売上
				// ※50:ｵﾝﾗｲﾝ資格確認導入をその他に含める
				pca = result.FindAll(p => p.商品区分コード == "99" || p.商品区分コード == "50");
				for (int i = 0, j = ROW_その他売上; i < bumon3.Length; i++, j++)
				{
					List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon3[i]);
					if (null != sale)
					{
						ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
					}
				}
			}
			int rowCurlineBody = ROW_Curline本体_47;
			int rowCurlineBrush = ROW_Curline替ﾌﾞﾗｼ_47;
			if (46 == period)
			{
				// 46期はｵﾝ資格導入売上がないので行数を変更
				rowCurlineBody = ROW_Curline本体_46;
				rowCurlineBrush = ROW_Curline替ﾌﾞﾗｼ_46;
			}
			// Curline本体
			pca = result.FindAll(p => p.商品区分コード == "201");
			for (int i = 0, j = rowCurlineBody; i < bumon3.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon3[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// Curline替ﾌﾞﾗｼ
			pca = result.FindAll(p => p.商品区分コード == "202");
			for (int i = 0, j = rowCurlineBrush; i < bumon3.Length; i++, j++)
			{
				List<売上予想> sale = pca.FindAll(p => p.部門コード == bumon3[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
		}

		/// <summary>
		/// 金額千円単位
		/// </summary>
		/// <param name="price">金額</param>
		/// <returns>金額千円単位</returns>
		public static int To金額千円単位(int price)
		{
			return string.Format("{0:#,}", price).ToInt();
		}
	}
}
