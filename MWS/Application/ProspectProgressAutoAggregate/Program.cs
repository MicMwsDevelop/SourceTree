//
// Program.cs
// 
// 見込進捗自動集計 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：見込進捗自動集計
// 処理概要：営業部上長が日々確認する見込進捗の自動集計版
// 入力ファイル：無
// 出力ファイル：見込進捗_47期_yyyyMMdd.xlsx
// 印刷物：無
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2021/08/04 勝呂)
// Ver1.01 新設の商品区分2(0030) paletteおまとめをpalette売上に含める(2021/09/06 勝呂)
// Ver1.02 見込進捗詳細をPCA勘定科目の変更に対応(2021/09/15 勝呂)
// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
// Ver1.04 来期追加と売上実績設定機能を追加（管理者用）(2021/10/20 勝呂)
// Ver1.05 予測連絡用シート名と売上進捗シートの変更(2021/11/10 勝呂)
// Ver1.06 予測連絡用の内容を元の見込進捗に合わせる(2021/11/16 勝呂)
// Ver1.07 2022/02組織変更に伴うフォーム変更(2022/02/09 勝呂)
// Ver1.08 2022/02組織変更に伴うフォーム見直し(2022/04/12 勝呂)
// Ver1.09 48期に対応(2023/01/12 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
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
		public static readonly string VersionStr = "Ver1.09 (2023/01/12)";

		/// <summary>
		/// MIC創立年
		/// </summary>
		private const int MIC_START_YEAR = 1974;

		/// <summary>
		/// 起動日
		/// </summary>
		public static Date gBootDate { get; set; }

		/// <summary>
		/// 部門コード
		/// 47期2月組織変更前
		/// </summary>
		public static readonly string[] gBumonCodes = { "50", "70", "60", "75", "76", "80" };

		/// <summary>
		/// 部門コード
		/// 47期2月組織変更後
		/// </summary>
		public static readonly string[] gBumonCodes202002 = { "41", "42", "50", "70", "75", "76", "80" };

		/// <summary>
		/// PCA部門コード
		/// 47期2月組織変更前
		/// </summary>
		private static readonly string[] gPcaBumonCoeds = { "081", "082", "083", "086", "087", "085" };

		/// <summary>
		/// PCA部門コード
		/// 47期2月組織変更後
		/// </summary>
		private static readonly string[] gPcaBumonCoeds202202 = { "045", "046", "053", "054", "055", "057", "058" };

		/// <summary>
		/// 見込進捗詳細行番号 47上期
		/// </summary>
		private enum RowType47First
		{
			palette売上 = 6,
			paletteES売上 = 13,
			その他ソフト売上 = 20,
			ハード売上 = 28,
			技術指導売上 = 35,
			ハード保守 = 49,
			ソフト保守 = 56,
			周辺機器売上 = 63,
			その他売上 = 70,
			オン資格導入売上 = 78,
			Curline本体 = 85,
			Curline替ブラシ = 93,
		}

		/// <summary>
		/// 見込進捗詳細行番号 47下期
		/// </summary>
		private enum RowType47Second
		{
			palette売上 = 6,
			paletteES売上 = 14,
			その他ソフト売上 = 22,
			ハード売上 = 31,
			技術指導売上 = 39,
			ハード保守 = 55,
			ソフト保守 = 63,
			周辺機器売上 = 71,
			その他売上 = 79,
			オン資格導入売上 = 88,
			Curline本体 = 96,
			Curline替ブラシ = 105,
		}

		/// <summary>
		/// 売上実績リスト
		/// </summary>
		private static List<売上実績> 売上実績_List { get; set; }

		/// <summary>
		/// 売上進捗リスト
		/// </summary>
		private static List<売上進捗> 売上進捗_List { get; set; }

		/// <summary>
		/// paletteES ソフトウェア保守料売上予測リスト
		/// </summary>
		private static List<vMicソフトウェア保守料売上予測> ソフトウェア保守料売上予測_List { get; set; }

		/// <summary>
		/// palette ES 売上進捗リスト
		/// </summary>
		private static List<売上進捗ES> 売上進捗ES_List { get; set; }

		/// <summary>
		/// 月額課金 売上進捗リスト
		/// </summary>
		private static List<売上進捗ES> 売上進捗課金_List { get; set; }

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
			//gBootDate = new Date(2023, 9, 1);
			gBootDate = Date.Today;
#else
			gBootDate = Date.Today;
#endif

			if (gBootDate.ToIntYMD() < 20220201)
			{
				// 47期上期以前は集計対象外とする
				return 0;
			}
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					int period = GetPeriod(gBootDate);
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
			Date start = new Date(MIC_START_YEAR + period, 8, 1);
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
			return year - MIC_START_YEAR;
		}

		/// <summary>
		/// 期→開始日付
		/// </summary>
		/// <param name="period">期</param>
		/// <returns>開始日付</returns>
		public static Date PeriodToDate(int period)
		{
			return new Date(MIC_START_YEAR + period, 8, 1);
		}

		/// <summary>
		/// 47期組織変更後かどうか？
		/// </summary>
		/// <param name="today">当日</param>
		/// <returns>判定</returns>
		public static bool IsAfter202202(Date today)
		{
			return (202202 <= today.ToYearMonth().ToIntYM()) ? true : false;
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
		/// 見込進捗詳細 palette売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_palette売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.palette売上;
			}
			return (int)RowType47First.palette売上;
		}

		/// <summary>
		/// 見込進捗詳細 paletteES売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_paletteES売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.paletteES売上;
			}
			return (int)RowType47First.paletteES売上;
		}

		/// <summary>
		/// 見込進捗詳細 その他ソフト売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_その他ソフト売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.その他ソフト売上;
			}
			return (int)RowType47First.その他ソフト売上;
		}

		/// <summary>
		/// 見込進捗詳細 ハード売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_ハード売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.ハード売上;
			}
			return (int)RowType47First.ハード売上;
		}

		/// <summary>
		/// 見込進捗詳細 技術指導売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_技術指導売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.技術指導売上;
			}
			return (int)RowType47First.技術指導売上;
		}

		/// <summary>
		/// 見込進捗詳細 ハード保守 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_ハード保守(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.ハード保守;
			}
			return (int)RowType47First.ハード保守;
		}

		/// <summary>
		/// 見込進捗詳細 ソフト保守 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_ソフト保守(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.ソフト保守;
			}
			return (int)RowType47First.ソフト保守;
		}

		/// <summary>
		/// 見込進捗詳細 周辺機器売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_周辺機器売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.周辺機器売上;
			}
			return (int)RowType47First.周辺機器売上;
		}

		/// <summary>
		/// 見込進捗詳細 その他売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_その他売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.その他売上;
			}
			return (int)RowType47First.その他売上;
		}

		/// <summary>
		/// 見込進捗詳細 オン資格導入売上 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_オン資格導入売上(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.オン資格導入売上;
			}
			return (int)RowType47First.オン資格導入売上;
		}

		/// <summary>
		/// 見込進捗詳細 Curline本体 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_Curline本体(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.Curline本体;
			}
			return (int)RowType47First.Curline本体;
		}

		/// <summary>
		/// 見込進捗詳細 Curline本体 行番号の取得
		/// </summary>
		/// <param name="ym"></param>
		/// <returns>行番号</returns>
		private static int GetRow_Curline替ブラシ(YearMonth ym)
		{
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 2022/02以降
				return (int)RowType47Second.Curline替ブラシ;
			}
			return (int)RowType47First.Curline替ブラシ;
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
			売上進捗_List = new List<売上進捗>();
			ソフトウェア保守料売上予測_List = new List<vMicソフトウェア保守料売上予測>();
			売上進捗ES_List = new List<売上進捗ES>();
			売上進捗課金_List = new List<売上進捗ES>();
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
					List<売上進捗> work = ProspectProgressAutoAggregateAccess.Select_売上進捗(start, gSettings.Junp.ConnectionString);
					if (null != work && 0 < work.Count)
					{
						売上進捗_List.AddRange(work);
					}
					start = start.PlusMonths(1);
				}
				ソフトウェア保守料売上予測_List = ProspectProgressAutoAggregateAccess.Select_ソフトウェア保守料売上予測(term.Start, term.End, gSettings.Junp.ConnectionString);
				売上進捗ES_List = ProspectProgressAutoAggregateAccess.Select_売上進捗ES(gSettings.Junp.ConnectionString);
				売上進捗課金_List = ProspectProgressAutoAggregateAccess.Select_売上進捗課金(gSettings.Junp.ConnectionString);
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
					予測連絡用(wb, period, gBootDate);

					//「売上実績」
					売上実績(wb, period, gBootDate, term);

					//「見込進捗詳細」
					見込進捗詳細(wb, period, gBootDate, term);

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
		/// <param name="today">当日</param>
		private static void 予測連絡用(XLWorkbook wb, int period, Date today)
		{
			IXLWorksheet ws	= wb.Worksheet("予測連絡用");

			if (period < 48)
			{
				// 47期以前
				// 2022/02の前月は旧組織でフォームが合わないために前月実績値は設定しない
				if (202202 != today.ToYearMonth().ToIntYM())
				{
					// 前月実績値の設定
					予測連絡用_前月実績値設定_47(ws, period, today);
				}
				// 当月予算予測進捗値の設定
				予測連絡用_当月予算予測進捗値設定_47(ws, today);

				// 翌月予算予測進捗値の設定
				予測連絡用_翌月予算進捗値設定_47(ws, today);

				// 更新日
				ws.Cell(2, 28).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));

				// バージョン情報
				ws.Cell(37, 28).SetValue(VersionStr);
			}
			else
			{
				// 48期以降
				// 前月実績値の設定
				予測連絡用_前月実績値設定_48(ws, period, today);

				// 当月予算予測進捗値の設定
				予測連絡用_当月予算予測進捗値設定_48(ws, today);

				// 翌月予算予測進捗値の設定
				予測連絡用_翌月予算進捗値設定_48(ws, today);

				// 更新日
				ws.Cell(2, 24).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));

				// バージョン情報
				ws.Cell(36, 24).SetValue(VersionStr);
			}
		}

		/// <summary>
		/// 「予測連絡用」前月実績値の設定（47期以前）
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="today">当日</param>
		private static void 予測連絡用_前月実績値設定_47(IXLWorksheet ws, int period, Date today)
		{
			// 先月初日
			YearMonth lastYM = today.FirstDayOfLasMonth().ToYearMonth();
			ws.Cell(7, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(実績)", lastYM.Year, lastYM.Month));

			int lastFirstDay = today.FirstDayOfLasMonth().ToIntYMD();
			if (7 == lastYM.Month)
			{
				// 前期
				Span term = GetTerm(period - 1);
				List<売上実績> list = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 >= {0} AND 実績日 <= {1}", term.Start.ToIntYMD(), term.End.ToIntYMD()), "実績日, 営業部コード", gSettings.Charlie.ConnectionString);

				// 前期７月の実績値の設定
				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					売上実績 result = list.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(7, 5 + j).SetValue(result.予算ES);
						ws.Cell(7, 6 + j).SetValue(result.予算課金);
						ws.Cell(7, 7 + j).SetValue(result.予算売上);
						ws.Cell(8, 5 + j).SetValue(result.実績ES);
						ws.Cell(8, 6 + j).SetValue(result.実績課金);
						ws.Cell(8, 7 + j).SetValue(result.実績売上);
						ws.Cell(11, 5 + j).SetValue(result.予算営業損益);
						ws.Cell(12, 5 + j).SetValue(result.実績営業損益);
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					売上実績 result = list.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(7, 14 + j).SetValue(result.予算まとめ);
						ws.Cell(7, 15 + j).SetValue(result.予算売上);
						ws.Cell(8, 14 + j).SetValue(result.実績まとめ);
						ws.Cell(8, 15 + j).SetValue(result.実績売上);
						ws.Cell(11, 14 + j).SetValue(result.予算営業損益);
						ws.Cell(12, 14 + j).SetValue(result.実績営業損益);
					}
				}
				// 前期累計実績値の設定
				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					List<売上実績> result = list.FindAll(p => p.営業部コード == gBumonCodes202002[i]);
					if (null != result)
					{
						ws.Cell(15, 5 + j).SetValue(result.Sum(p =>p.予算ES));
						ws.Cell(15, 6 + j).SetValue(result.Sum(p => p.予算課金));
						ws.Cell(15, 7 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(16, 5 + j).SetValue(result.Sum(p => p.実績ES));
						ws.Cell(16, 6 + j).SetValue(result.Sum(p => p.実績課金));
						ws.Cell(16, 7 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(19, 5 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(20, 5 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					List<売上実績> result = list.FindAll(p => p.営業部コード == gBumonCodes202002[i]);
					if (null != result)
					{
						ws.Cell(15, 11 + j).SetValue(result.Sum(p => p.予算まとめ));
						ws.Cell(15, 12 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(16, 11 + j).SetValue(result.Sum(p => p.実績まとめ));
						ws.Cell(16, 12 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(19, 11 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(20, 11 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				ws.Cell(15, 2).SetValue(string.Format("{0}期累計\r\n(実績)", period - 1));
			}
			else
			{
				// 前月の実績値の設定
				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(7, 5 + j).SetValue(result.予算ES);
						ws.Cell(7, 6 + j).SetValue(result.予算課金);
						ws.Cell(7, 7 + j).SetValue(result.予算売上);
						ws.Cell(8, 5 + j).SetValue(result.実績ES);
						ws.Cell(8, 6 + j).SetValue(result.実績課金);
						ws.Cell(8, 7 + j).SetValue(result.実績売上);
						ws.Cell(11, 5 + j).SetValue(result.予算営業損益);
						ws.Cell(12, 5 + j).SetValue(result.実績営業損益);
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(7, 14 + j).SetValue(result.予算まとめ);
						ws.Cell(7, 15 + j).SetValue(result.予算売上);
						ws.Cell(8, 14 + j).SetValue(result.実績まとめ);
						ws.Cell(8, 15 + j).SetValue(result.実績売上);
						ws.Cell(11, 14 + j).SetValue(result.予算営業損益);
						ws.Cell(12, 14 + j).SetValue(result.実績営業損益);
					}
				}

				////////////////////////////////////////
				// とりあえず４７期専用で作成
				// 上期と下期で組織が違うので累計が出せないので、下期累計とする
				///////////////////////////////////////

				// 今期累計実績値の設定
				Span term = GetTerm(period);
				//term = new Span(term.Start, today.LastDayOfLasMonth());
				term = new Span(term.Start.PlusMonths(6), today.LastDayOfLasMonth());

				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					List<売上実績> result = 売上実績_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 >= term.Start.ToIntYMD() && p.実績日 <= term.End.ToIntYMD());
					if (null != result)
					{
						ws.Cell(15, 5 + j).SetValue(result.Sum(p => p.予算ES));
						ws.Cell(15, 6 + j).SetValue(result.Sum(p => p.予算課金));
						ws.Cell(15, 7 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(16, 5 + j).SetValue(result.Sum(p => p.実績ES));
						ws.Cell(16, 6 + j).SetValue(result.Sum(p => p.実績課金));
						ws.Cell(16, 7 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(19, 5 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(20, 5 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					List<売上実績> result = 売上実績_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 >= term.Start.ToIntYMD() && p.実績日 <= term.End.ToIntYMD());
					if (null != result)
					{
						ws.Cell(15, 14 + j).SetValue(result.Sum(p => p.予算まとめ));
						ws.Cell(15, 15 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(16, 14 + j).SetValue(result.Sum(p => p.実績まとめ));
						ws.Cell(16, 15 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(19, 14 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(20, 14 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				ws.Cell(15, 2).SetValue(string.Format("{0}下期累計\r\n(実績)", period));
			}
		}

		/// <summary>
		/// 「予測連絡用」前月実績値の設定（48期以降）
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="today">当日</param>
		private static void 予測連絡用_前月実績値設定_48(IXLWorksheet ws, int period, Date today)
		{
			// 先月初日
			YearMonth lastYM = today.FirstDayOfLasMonth().ToYearMonth();
			ws.Cell(6, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(実績)", lastYM.Year, lastYM.Month));
			int lastFirstDay = today.FirstDayOfLasMonth().ToIntYMD();

			if (7 == lastYM.Month)
			{
				// 前期
				Span term = GetTerm(period - 1);
				List<売上実績> list = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 >= {0} AND 実績日 <= {1}", term.Start.ToIntYMD(), term.End.ToIntYMD()), "実績日, 営業部コード", gSettings.Charlie.ConnectionString);

				// 前期７月の実績値の設定
				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					売上実績 result = list.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(6, 5 + j).SetValue(result.予算ES);
						ws.Cell(6, 6 + j).SetValue(result.予算課金);
						ws.Cell(6, 7 + j).SetValue(result.予算売上);
						ws.Cell(7, 5 + j).SetValue(result.実績ES);
						ws.Cell(7, 6 + j).SetValue(result.実績課金);
						ws.Cell(7, 7 + j).SetValue(result.実績売上);
						ws.Cell(10, 5 + j).SetValue(result.予算営業損益);
						ws.Cell(11, 5 + j).SetValue(result.実績営業損益);
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					売上実績 result = list.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(6, 11 + j).SetValue(result.予算まとめ);
						ws.Cell(6, 12 + j).SetValue(result.予算売上);
						ws.Cell(7, 11 + j).SetValue(result.実績まとめ);
						ws.Cell(7, 12 + j).SetValue(result.実績売上);
						ws.Cell(10, 11 + j).SetValue(result.予算営業損益);
						ws.Cell(11, 11 + j).SetValue(result.実績営業損益);
					}
				}
				// 前期累計実績値の設定
				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					List<売上実績> result = list.FindAll(p => p.営業部コード == gBumonCodes202002[i]);
					if (null != result)
					{
						ws.Cell(14, 5 + j).SetValue(result.Sum(p => p.予算ES));
						ws.Cell(14, 6 + j).SetValue(result.Sum(p => p.予算課金));
						ws.Cell(14, 7 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(15, 5 + j).SetValue(result.Sum(p => p.実績ES));
						ws.Cell(15, 6 + j).SetValue(result.Sum(p => p.実績課金));
						ws.Cell(15, 7 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(18, 5 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(19, 5 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					List<売上実績> result = list.FindAll(p => p.営業部コード == gBumonCodes202002[i]);
					if (null != result)
					{
						ws.Cell(14, 11 + j).SetValue(result.Sum(p => p.予算まとめ));
						ws.Cell(14, 12 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(15, 11 + j).SetValue(result.Sum(p => p.実績まとめ));
						ws.Cell(15, 12 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(18, 11 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(19, 11 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				ws.Cell(14, 2).SetValue(string.Format("{0}期累計\r\n(実績)", period - 1));
			}
			else
			{
				// 前月の実績値の設定
				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(6, 5 + j).SetValue(result.予算ES);
						ws.Cell(6, 6 + j).SetValue(result.予算課金);
						ws.Cell(6, 7 + j).SetValue(result.予算売上);
						ws.Cell(7, 5 + j).SetValue(result.実績ES);
						ws.Cell(7, 6 + j).SetValue(result.実績課金);
						ws.Cell(7, 7 + j).SetValue(result.実績売上);
						ws.Cell(10, 5 + j).SetValue(result.予算営業損益);
						ws.Cell(11, 5 + j).SetValue(result.実績営業損益);
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == lastFirstDay);
					if (null != result)
					{
						ws.Cell(6, 11 + j).SetValue(result.予算まとめ);
						ws.Cell(6, 12 + j).SetValue(result.予算売上);
						ws.Cell(7, 11 + j).SetValue(result.実績まとめ);
						ws.Cell(7, 12 + j).SetValue(result.実績売上);
						ws.Cell(10, 11 + j).SetValue(result.予算営業損益);
						ws.Cell(11, 11 + j).SetValue(result.実績営業損益);
					}
				}

				// 今期累計実績値の設定
				Span term = GetTerm(period);
				term = new Span(term.Start, today.LastDayOfLasMonth());

				// 営業部
				for (int i = 0, j = 0; i < 2; i++, j += 3)
				{
					List<売上実績> result = 売上実績_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 >= term.Start.ToIntYMD() && p.実績日 <= term.End.ToIntYMD());
					if (null != result)
					{
						ws.Cell(14, 5 + j).SetValue(result.Sum(p => p.予算ES));
						ws.Cell(14, 6 + j).SetValue(result.Sum(p => p.予算課金));
						ws.Cell(14, 7 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(15, 5 + j).SetValue(result.Sum(p => p.実績ES));
						ws.Cell(15, 6 + j).SetValue(result.Sum(p => p.実績課金));
						ws.Cell(15, 7 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(18, 5 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(19, 5 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				// サポートセンター
				for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
				{
					List<売上実績> result = 売上実績_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 >= term.Start.ToIntYMD() && p.実績日 <= term.End.ToIntYMD());
					if (null != result)
					{
						ws.Cell(14, 11 + j).SetValue(result.Sum(p => p.予算まとめ));
						ws.Cell(14, 12 + j).SetValue(result.Sum(p => p.予算売上));
						ws.Cell(15, 11 + j).SetValue(result.Sum(p => p.実績まとめ));
						ws.Cell(15, 12 + j).SetValue(result.Sum(p => p.実績売上));
						ws.Cell(18, 11 + j).SetValue(result.Sum(p => p.予算営業損益));
						ws.Cell(19, 11 + j).SetValue(result.Sum(p => p.実績営業損益));
					}
				}
				ws.Cell(14, 2).SetValue(string.Format("{0}下期累計\r\n(実績)", period));
			}
		}

		/// <summary>
		/// 「予測連絡用」当月予算予測進捗値設定（47期以前）
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">期</param>
		/// <param name="today">当日</param>
		private static void 予測連絡用_当月予算予測進捗値設定_47(IXLWorksheet ws/*, int period*/, Date today)
		{
			// 当月初日
			int thisFirstDay = today.FirstDayOfTheMonth().ToIntYMD();
			YearMonth thisYM = today.ToYearMonth();
			ws.Cell(23, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(進捗)", thisYM.Year, thisYM.Month));

			// 営業部
			for (int i = 0, j = 0; i < 2; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == thisFirstDay);
				if (null != result)
				{
					ws.Cell(23, 5 + j).SetValue(result.予算ES);
					ws.Cell(23, 6 + j).SetValue(result.予算課金);
					ws.Cell(23, 7 + j).SetValue(result.予算売上);
					ws.Cell(28, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(24, 5 + j).SetValue(result.予測ES);
					ws.Cell(24, 6 + j).SetValue(result.予測課金);
					ws.Cell(24, 7 + j).SetValue(result.予測売上);
					ws.Cell(29, 5 + j).SetValue(result.予測営業損益);
				}
				// 進捗
				List<売上進捗ES> es = 売上進捗ES_List.FindAll(p => p.BshCode2 == gBumonCodes202002[i] && p.売上月 == thisYM.ToString());
				if (null != es)
				{
					ws.Cell(25, 5 + j).SetValue(es.Count());
				}
				List<売上進捗ES> 課金 = 売上進捗課金_List.FindAll(p => p.BshCode2 == gBumonCodes202002[i] && p.売上月 == thisYM.ToString());
				if (null != 課金)
				{
					ws.Cell(25, 6 + j).SetValue(課金.Count());
				}
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == thisYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				ws.Cell(25, 7 + j).SetValue(To金額千円単位(price)); // 金額千円単位
			}
			// サポートセンター
			for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == thisFirstDay);
				if (null != result)
				{
					ws.Cell(23, 14 + j).SetValue(result.予算まとめ);
					ws.Cell(23, 15 + j).SetValue(result.予算売上);
					ws.Cell(28, 14 + j).SetValue(result.予算営業損益);
					ws.Cell(24, 14 + j).SetValue(result.予測まとめ);
					ws.Cell(24, 15 + j).SetValue(result.予測売上);
					ws.Cell(29, 14 + j).SetValue(result.予測営業損益);
				}
				// 進捗
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.売上月 == thisYM.ToString());
				if (null != matome)
				{
					ws.Cell(25, 14 + j).SetValue(matome.Count);
				}
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == thisYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				ws.Cell(25, 15 + j).SetValue(To金額千円単位(price)); // 金額千円単位
			}
		}

		/// <summary>
		/// 「予測連絡用」当月予算予測進捗値設定（48期以降）
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">期</param>
		/// <param name="today">当日</param>
		private static void 予測連絡用_当月予算予測進捗値設定_48(IXLWorksheet ws/*, int period*/, Date today)
		{
			// 当月初日
			int thisFirstDay = today.FirstDayOfTheMonth().ToIntYMD();
			YearMonth thisYM = today.ToYearMonth();
			ws.Cell(22, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(進捗)", thisYM.Year, thisYM.Month));

			// 営業部
			for (int i = 0, j = 0; i < 2; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == thisFirstDay);
				if (null != result)
				{
					ws.Cell(22, 5 + j).SetValue(result.予算ES);
					ws.Cell(22, 6 + j).SetValue(result.予算課金);
					ws.Cell(22, 7 + j).SetValue(result.予算売上);
					ws.Cell(27, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(23, 5 + j).SetValue(result.予測ES);
					ws.Cell(23, 6 + j).SetValue(result.予測課金);
					ws.Cell(23, 7 + j).SetValue(result.予測売上);
					ws.Cell(28, 5 + j).SetValue(result.予測営業損益);
				}
				// 進捗
				List<売上進捗ES> es = 売上進捗ES_List.FindAll(p => p.BshCode2 == gBumonCodes202002[i] && p.売上月 == thisYM.ToString());
				if (null != es)
				{
					ws.Cell(24, 5 + j).SetValue(es.Count());
				}
				List<売上進捗ES> 課金 = 売上進捗課金_List.FindAll(p => p.BshCode2 == gBumonCodes202002[i] && p.売上月 == thisYM.ToString());
				if (null != 課金)
				{
					ws.Cell(24, 6 + j).SetValue(課金.Count());
				}
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == thisYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				ws.Cell(24, 7 + j).SetValue(To金額千円単位(price)); // 金額千円単位
			}
			// サポートセンター
			for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == thisFirstDay);
				if (null != result)
				{
					ws.Cell(22, 11 + j).SetValue(result.予算まとめ);
					ws.Cell(22, 12 + j).SetValue(result.予算売上);
					ws.Cell(27, 11 + j).SetValue(result.予算営業損益);
					ws.Cell(23, 11 + j).SetValue(result.予測まとめ);
					ws.Cell(23, 12 + j).SetValue(result.予測売上);
					ws.Cell(28, 11 + j).SetValue(result.予測営業損益);
				}
				// 進捗
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.売上月 == thisYM.ToString());
				if (null != matome)
				{
					ws.Cell(24, 11 + j).SetValue(matome.Count);
				}
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == thisYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				ws.Cell(24, 12 + j).SetValue(To金額千円単位(price)); // 金額千円単位
			}
		}

		/// <summary>
		/// 「予測連絡用」翌月予算進捗値設定（47期以前）
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="today">当日</param>
		private static void 予測連絡用_翌月予算進捗値設定_47(IXLWorksheet ws/*, int period*/, Date today)
		{
			// 来月初日
			int nextFirstDay = today.FirstDayOfNextMonth().ToIntYMD();
			YearMonth nextYM = today.FirstDayOfNextMonth().ToYearMonth();
			ws.Cell(32, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(進捗)", nextYM.Year, nextYM.Month));

			// 営業部
			for (int i = 0, j = 0; i < 2; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == nextFirstDay);
				if (null != result)
				{
					ws.Cell(32, 5 + j).SetValue(result.予算ES);
					ws.Cell(32, 6 + j).SetValue(result.予算課金);
					ws.Cell(32, 7 + j).SetValue(result.予算売上);
					ws.Cell(36, 5 + j).SetValue(result.予算営業損益);
				}
				// 進捗
				List<売上進捗ES> es = 売上進捗ES_List.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != es)
				{
					ws.Cell(33, 5 + j).SetValue(es.Count());
				}
				List<売上進捗ES> 課金 = 売上進捗課金_List.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != 課金)
				{
					ws.Cell(33, 6 + j).SetValue(es.Count());
				}
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == nextYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == nextYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.売上月 == nextYM.ToString());
				if (null != matome)
				{
					price += matome.Sum(p => p.金額);
					//ws.Cell(32, 6 + j).SetValue(matome.Count);
				}
				ws.Cell(33, 7 + j).SetValue(To金額千円単位(price));
			}
			// サポートセンター
			for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == nextFirstDay);
				if (null != result)
				{
					ws.Cell(32, 14 + j).SetValue(result.予算まとめ);
					ws.Cell(32, 15 + j).SetValue(result.予算売上);
					ws.Cell(36, 14 + j).SetValue(result.予算営業損益);
				}
				// 進捗
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == nextYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == nextYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.売上月 == nextYM.ToString());
				if (null != matome)
				{
					price += matome.Sum(p => p.金額);
					ws.Cell(33, 14 + j).SetValue(matome.Count);
				}
				ws.Cell(33, 15 + j).SetValue(To金額千円単位(price));
			}
		}

		/// <summary>
		/// 「予測連絡用」翌月予算進捗値設定（48期以降）
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="period">決算期</param>
		/// <param name="today">当日</param>
		private static void 予測連絡用_翌月予算進捗値設定_48(IXLWorksheet ws/*, int period*/, Date today)
		{
			// 来月初日
			int nextFirstDay = today.FirstDayOfNextMonth().ToIntYMD();
			YearMonth nextYM = today.FirstDayOfNextMonth().ToYearMonth();
			ws.Cell(31, 2).SetValue(string.Format("{0:D4}年{1:D2}月\r\n(進捗)", nextYM.Year, nextYM.Month));

			// 営業部
			for (int i = 0, j = 0; i < 2; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == nextFirstDay);
				if (null != result)
				{
					ws.Cell(31, 5 + j).SetValue(result.予算ES);
					ws.Cell(31, 6 + j).SetValue(result.予算課金);
					ws.Cell(31, 7 + j).SetValue(result.予算売上);
					ws.Cell(35, 5 + j).SetValue(result.予算営業損益);
				}
				// 進捗
				List<売上進捗ES> es = 売上進捗ES_List.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != es)
				{
					ws.Cell(32, 5 + j).SetValue(es.Count());
				}
				List<売上進捗ES> 課金 = 売上進捗課金_List.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != 課金)
				{
					ws.Cell(32, 6 + j).SetValue(es.Count());
				}
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == nextYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == nextYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.売上月 == nextYM.ToString());
				if (null != matome)
				{
					price += matome.Sum(p => p.金額);
				}
				ws.Cell(32, 7 + j).SetValue(To金額千円単位(price));
			}
			// サポートセンター
			for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
			{
				// 予算・予測
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == nextFirstDay);
				if (null != result)
				{
					ws.Cell(31, 11 + j).SetValue(result.予算まとめ);
					ws.Cell(31, 12 + j).SetValue(result.予算売上);
					ws.Cell(35, 11 + j).SetValue(result.予算営業損益);
				}
				// 進捗
				int price = 0;
				List<売上進捗> sale = 売上進捗_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.集計月 == nextYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.部門コード == gPcaBumonCoeds202202[i] && p.計上月 == nextYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
				List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == gBumonCodes202002[i] && p.売上月 == nextYM.ToString());
				if (null != matome)
				{
					price += matome.Sum(p => p.金額);
					ws.Cell(32, 11 + j).SetValue(matome.Count);
				}
				ws.Cell(32, 12 + j).SetValue(To金額千円単位(price));
			}
		}

		/// <summary>
		/// 「売上実績」
		/// </summary>
		/// <param name="wb">workbool</param>
		/// <param name="period">期</param>
		/// <param name="today">当日</param>
		/// <param name="term">決算期間</param>
		private static void 売上実績(XLWorkbook wb, int period, Date today, Span term)
		{
			if (47 == period)
			{
				// 47期
				IXLWorksheet ws1 = wb.Worksheet("売上実績上期");

				Date date = term.Start;
				売上実績_予算予測実績値設定(ws1, date.ToIntYMD(), 6);  // 上期8月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定(ws1, date.ToIntYMD(), 16);  // 上期9月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定(ws1, date.ToIntYMD(), 26);  // 上期10月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定(ws1, date.ToIntYMD(), 44);  // 上期11月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定(ws1, date.ToIntYMD(), 54);  // 上期12月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定(ws1, date.ToIntYMD(), 64);  // 上期1月実績

				// 更新日
				ws1.Cell(2, 27).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));

				if (IsAfter202202(today))
				{
					// 当日が2022/02以降
					IXLWorksheet ws2 = wb.Worksheet("売上実績下期");
					date = date.PlusMonths(1);
					売上実績_予算予測実績値設定_202202(ws2, date.ToIntYMD(), 7);  // 下期2月実績
					date = date.PlusMonths(1);
					売上実績_予算予測実績値設定_202202(ws2, date.ToIntYMD(), 17);  // 下期3月実績
					date = date.PlusMonths(1);
					売上実績_予算予測実績値設定_202202(ws2, date.ToIntYMD(), 27);  // 下期4月実績
					date = date.PlusMonths(1);
					売上実績_予算予測実績値設定_202202(ws2, date.ToIntYMD(), 45);  // 下期5月実績
					date = date.PlusMonths(1);
					売上実績_予算予測実績値設定_202202(ws2, date.ToIntYMD(), 55);  // 下期6月実績
					date = date.PlusMonths(1);
					売上実績_予算予測実績値設定_202202(ws2, date.ToIntYMD(), 65);  // 下期7月実績

					// 更新日
					ws2.Cell(2, 28).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));
				}
			}
			else
			{
				// 48期以降
				// Ver1.09 48期に対応(2023/01/12 勝呂)
				IXLWorksheet ws = wb.Worksheet("売上実績");

				Date date = term.Start;
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 6);  // 上期8月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 16);  // 上期9月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 26);  // 上期10月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 44);  // 上期11月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 54);  // 上期12月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 64);  // 上期1月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 90);  // 下期2月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 100);  // 下期3月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 110);  // 下期4月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 128);  // 下期5月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 128);  // 下期6月実績
				date = date.PlusMonths(1);
				売上実績_予算予測実績値設定_202208(ws, date.ToIntYMD(), 148);  // 下期7月実績

				// 更新日
				ws.Cell(2, 24).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));
			}
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
					ws.Cell(row, 6 + j).SetValue(result.予算まとめ);
					ws.Cell(row, 7 + j).SetValue(result.予算売上);
					ws.Cell(row + 1, 5 + j).SetValue(result.予測ES);
					ws.Cell(row + 1, 6 + j).SetValue(result.予測まとめ);
					ws.Cell(row + 1, 7 + j).SetValue(result.予測売上);
					ws.Cell(row + 2, 5 + j).SetValue(result.実績ES);
					ws.Cell(row + 2, 6 + j).SetValue(result.実績まとめ);
					ws.Cell(row + 2, 7 + j).SetValue(result.実績売上);
					ws.Cell(row + 5, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(row + 6, 5 + j).SetValue(result.予測営業損益);
					ws.Cell(row + 7, 5 + j).SetValue(result.実績営業損益);
				}
			}
		}

		/// <summary>
		/// 「売上実績」予算予測実績値設定（47期 2022/02以降）
		/// </summary>
		/// <param name="ws"></param>
		/// <param name="date"></param>
		/// <param name="row"></param>
		private static void 売上実績_予算予測実績値設定_202202(IXLWorksheet ws, int date, int row)
		{
			// 営業部
			for (int i = 0, j = 0; i < 2; i++, j += 3)
			{
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(row, 5 + j).SetValue(result.予算ES);
					ws.Cell(row, 6 + j).SetValue(result.予算課金);
					ws.Cell(row, 7 + j).SetValue(result.予算売上);
					ws.Cell(row + 1, 5 + j).SetValue(result.予測ES);
					ws.Cell(row + 1, 6 + j).SetValue(result.予測課金);
					ws.Cell(row + 1, 7 + j).SetValue(result.予測売上);
					ws.Cell(row + 2, 5 + j).SetValue(result.実績ES);
					ws.Cell(row + 2, 6 + j).SetValue(result.実績課金);
					ws.Cell(row + 2, 7 + j).SetValue(result.実績売上);
					ws.Cell(row + 5, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(row + 6, 5 + j).SetValue(result.予測営業損益);
					ws.Cell(row + 7, 5 + j).SetValue(result.実績営業損益);
				}
			}
			// サポートセンター
			for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
			{
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(row, 14 + j).SetValue(result.予算まとめ);
					ws.Cell(row, 15 + j).SetValue(result.予算売上);
					ws.Cell(row + 1, 14 + j).SetValue(result.予測まとめ);
					ws.Cell(row + 1, 15 + j).SetValue(result.予測売上);
					ws.Cell(row + 2, 14 + j).SetValue(result.実績まとめ);
					ws.Cell(row + 2, 15 + j).SetValue(result.実績売上);
					ws.Cell(row + 5, 14 + j).SetValue(result.予算営業損益);
					ws.Cell(row + 6, 14 + j).SetValue(result.予測営業損益);
					ws.Cell(row + 7, 14 + j).SetValue(result.実績営業損益);
				}
			}
		}

		/// <summary>
		/// 「売上実績」予算予測実績値設定（48期以降）
		/// </summary>
		/// <param name="ws"></param>
		/// <param name="date"></param>
		/// <param name="row"></param>
		/// Ver1.09 48期に対応(2023/01/12 勝呂)
		private static void 売上実績_予算予測実績値設定_202208(IXLWorksheet ws, int date, int row)
		{
			// 営業部
			for (int i = 0, j = 0; i < 2; i++, j += 3)
			{
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(row, 5 + j).SetValue(result.予算ES);
					ws.Cell(row, 6 + j).SetValue(result.予算課金);
					ws.Cell(row, 7 + j).SetValue(result.予算売上);
					ws.Cell(row + 1, 5 + j).SetValue(result.予測ES);
					ws.Cell(row + 1, 6 + j).SetValue(result.予測課金);
					ws.Cell(row + 1, 7 + j).SetValue(result.予測売上);
					ws.Cell(row + 2, 5 + j).SetValue(result.実績ES);
					ws.Cell(row + 2, 6 + j).SetValue(result.実績課金);
					ws.Cell(row + 2, 7 + j).SetValue(result.実績売上);
					ws.Cell(row + 5, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(row + 6, 5 + j).SetValue(result.予測営業損益);
					ws.Cell(row + 7, 5 + j).SetValue(result.実績営業損益);
				}
			}
			// サポートセンター
			for (int i = 2, j = 0; i < gBumonCodes202002.Length; i++, j += 2)
			{
				売上実績 result = 売上実績_List.Find(p => p.営業部コード == gBumonCodes202002[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(row, 11 + j).SetValue(result.予算まとめ);
					ws.Cell(row, 12 + j).SetValue(result.予算売上);
					ws.Cell(row + 1, 11 + j).SetValue(result.予測まとめ);
					ws.Cell(row + 1, 12 + j).SetValue(result.予測売上);
					ws.Cell(row + 2, 11 + j).SetValue(result.実績まとめ);
					ws.Cell(row + 2, 12 + j).SetValue(result.実績売上);
					ws.Cell(row + 5, 11 + j).SetValue(result.予算営業損益);
					ws.Cell(row + 6, 11 + j).SetValue(result.予測営業損益);
					ws.Cell(row + 7, 11 + j).SetValue(result.実績営業損益);
				}
			}
		}

		/// <summary>
		/// 「見込進捗詳細」
		/// </summary>
		/// <param name="wb">workbook</param>
		/// <param name="period">決算期</param>
		/// <param name="today">当日</param>
		/// <param name="term">決算期間</param>
		private static void 見込進捗詳細(XLWorkbook wb, int period, Date today, Span term)
		{
			Date start = term.Start;
			if (47 == period)
			{
				// 47期
				// 上期8月～上期1月
				IXLWorksheet ws1 = wb.Worksheet("見込進捗詳細上期");
				for (int i = 0, j = 6; i < 6; i++, j += 4)
				{
					見込進捗詳細_実績値設定(ws1, period, start.ToYearMonth(), j);
					start = start.PlusMonths(1);
				}
				// 更新日
				ws1.Cell(2, 2).SetValue(DateTime.Now.ToString());

				if (IsAfter202202(today))
				{
					// 当日が2022/02以降
					// 下期2月～来期８月
					IXLWorksheet ws2 = wb.Worksheet("見込進捗詳細下期");
					for (int i = 0, j = 6; i < 6; i++, j += 4)
					{
						見込進捗詳細_実績値設定(ws2, period, start.ToYearMonth(), j);
						start = start.PlusMonths(1);
					}
					// 更新日
					ws2.Cell(2, 2).SetValue(DateTime.Now.ToString());
				}
			}
			else
			{
				// 48期以降
				// 上期8月～来期8月
				IXLWorksheet ws = wb.Worksheet("見込進捗詳細");
				for (int i = 0, j = 6; i < 12; i++, j += 4)
				{
					見込進捗詳細_実績値設定(ws, period, start.ToYearMonth(), j);
					start = start.PlusMonths(1);
				}
				// 更新日
				ws.Cell(2, 2).SetValue(DateTime.Now.ToString());
			}
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
			string[] bumon1_Before = { "081", "082", "083", "086", "087", "085" };			// 東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部
			string[] bumon2_Before = { "081", "082", "083", "086", "087", "085", "011" };     // 東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部、営業管理部
			string[] bumon3_Before = { "075", "081", "082", "083", "086", "087", "085" };     // ヘルスケア営業部、東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部

			string[] bumon1_After = { "045", "046", "053", "054", "055", "057", "058" };			// 東日本営業部、西日本営業部、東日本サポートセンター、首都圏サポートセンター、中日本サポートセンター、関西サポートセンター、西日本サポートセンター
			string[] bumon2_After = { "045", "046", "053", "054", "055", "057", "058", "011" };  // 東日本営業部、西日本営業部、東日本サポートセンター、首都圏サポートセンター、中日本サポートセンター、関西サポートセンター、西日本サポートセンター、営業管理部
			string[] bumon3_After = { "075", "045", "046", "053", "054", "055", "057", "058" };  // 企画推進部、東日本営業部、西日本営業部、東日本サポートセンター、首都圏サポートセンター、中日本サポートセンター、関西サポートセンター、西日本サポートセンター

			string[] bumon1, bumon2, bumon3;
			if (IsAfter202202(ym.ToDate(1)))
			{
				// 47期下期以降
				bumon1 = new string[bumon1_After.Length];
				Array.Copy(bumon1_After, bumon1, bumon1_After.Length);
				bumon2 = new string[bumon2_After.Length];
				Array.Copy(bumon2_After, bumon2, bumon2_After.Length);
				bumon3 = new string[bumon3_After.Length];
				Array.Copy(bumon3_After, bumon3, bumon3_After.Length);
			}
			else
			{
				// 47上期以前
				bumon1 = new string[bumon1_Before.Length];
				Array.Copy(bumon1_Before, bumon1, bumon1_Before.Length);
				bumon2 = new string[bumon2_Before.Length];
				Array.Copy(bumon2_Before, bumon2, bumon2_Before.Length);
				bumon3 = new string[bumon3_Before.Length];
				Array.Copy(bumon3_Before, bumon3, bumon3_Before.Length);
			}
			List<売上進捗> result = 売上進捗_List.FindAll(p => p.集計月 == ym);

			// 5041 palette売上
			// Ver1.01 新設の商品区分2(0030) paletteおまとめをpalette売上に含める(2021/09/06 勝呂)
			List<売上進捗> pca = result.FindAll(p => p.商品区分コード == "28" || p.商品区分コード == "30");

			int currnetPeriod = GetPeriod(gBootDate);
			int thisPeriod = GetPeriod(Date.Today);
			if (currnetPeriod == thisPeriod)
			{
				// 今期
				YearMonth nextYM = Date.Today.PlusMonths(1).ToYearMonth();
				if (ym == nextYM)
				{
					// 翌月
					for (int i = 0, j = GetRow_palette売上(ym); i < bumon1.Length; i++, j++)
					{
						int price = 0;
						List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
						if (null != sale)
						{
							price = sale.Sum(p => p.金額);
						}
						// 翌月分のpalette売上分には、まとめ分が含まれていないので、「売上進捗-まとめ」の金額を加算する
						List<売上進捗まとめ> matome = 売上進捗まとめ_List.FindAll(p => p.営業部コード == bumon1[i] && p.売上月 == nextYM.ToString());
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
					for (int i = 0, j = GetRow_palette売上(ym); i < bumon1.Length; i++, j++)
					{
						List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
						if (null != sale)
						{
							ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
						}
					}
				}
			}
			else
			{
				// 今期以外
				for (int i = 0, j = GetRow_palette売上(ym); i < bumon1.Length; i++, j++)
				{
					List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
					if (null != sale)
					{
						ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
					}
				}
			}
			// 5051 paletteES
			pca = result.FindAll(p => p.商品区分コード == "27");
			for (int i = 0, j = GetRow_paletteES売上(ym); i < bumon1.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5114 その他ｿﾌﾄ売上
			pca = result.FindAll(p => p.商品区分コード == "1");
			for (int i = 0, j = GetRow_その他ソフト売上(ym); i < bumon3.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon3[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5115 ハード売上
			pca = result.FindAll(p => p.商品区分コード == "2");
			for (int i = 0, j = GetRow_ハード売上(ym); i < bumon1.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5117 技術指導売上
			pca = result.FindAll(p => p.商品区分コード == "40");
			for (int i = 0, j = GetRow_技術指導売上(ym); i < bumon1.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5120 ハード保守
			pca = result.FindAll(p => p.商品区分コード == "7");
			for (int i = 0, j = GetRow_ハード保守(ym); i < bumon1.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// ソフト保守
			// ソフト保守はES売上予想Listから金額を取得
			List<vMicソフトウェア保守料売上予測> soft = ソフトウェア保守料売上予測_List.FindAll(p => p.計上月 == ym.ToString());
			for (int i = 0, j = GetRow_ソフト保守(ym); i < bumon1.Length; i++, j++)
			{
				List<vMicソフトウェア保守料売上予測> sale = soft.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.売上金額)));
				}
			}
			// 5133 周辺機器売上
			pca = result.FindAll(p => p.商品区分コード == "97");
			for (int i = 0, j = GetRow_周辺機器売上(ym); i < bumon1.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5135 その他売上
			pca = result.FindAll(p => p.商品区分コード == "99");
			for (int i = 0, j = GetRow_その他売上(ym); i < bumon2.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon2[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5145 ｵﾝ資格導入売上
			pca = result.FindAll(p => p.商品区分コード == "50");
			for (int i = 0, j = GetRow_オン資格導入売上(ym); i < bumon1.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon1[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5151 Curline本体
			pca = result.FindAll(p => p.商品区分コード == "201");
			for (int i = 0, j = GetRow_Curline本体(ym); i < bumon2.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon2[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
			// 5152 Curline替ﾌﾞﾗｼ
			pca = result.FindAll(p => p.商品区分コード == "202");
			for (int i = 0, j = GetRow_Curline替ブラシ(ym); i < bumon2.Length; i++, j++)
			{
				List<売上進捗> sale = pca.FindAll(p => p.部門コード == bumon2[i]);
				if (null != sale)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(sale.Sum(p => p.金額)));
				}
			}
		}
	}
}
