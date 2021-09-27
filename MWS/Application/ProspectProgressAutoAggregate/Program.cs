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
		private static SqlServerConnectSettings gSettings { get; set; }

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.03 (2021/09/27)";

		/// <summary>
		/// 部門コード
		/// </summary>
		private static string[] gBumonCodes = { "50", "70", "60", "75", "76", "80" };

		/// <summary>
		/// PCA部門コード
		/// </summary>
		private static string[] gPcaBumonCoeds = { "081", "082", "083", "086", "087", "085" };

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
		private static List<売上実績> 売上実績List { get; set; }

		/// <summary>
		/// 売上予想リスト
		/// </summary>
		private static List<売上予想> 売上予想List { get; set; }

		/// <summary>
		/// paletteES 売上予想リスト
		/// </summary>
		private static List<ES売上予想> ES売上予想List { get; set; }

		/// <summary>
		/// palette ES 予測連絡用リスト
		/// </summary>
		private static List<予測連絡用ES> 予測連絡用_ESList { get; set; }

		/// <summary>
		/// おまとめプラン 予測連絡用リスト
		/// </summary>
		private static List<予測連絡用まとめ> 予測連絡用_まとめList { get; set; }

		/// <summary>
		/// 起動日
		/// </summary>
		private static Date BootDate { get; set; }

		/// <summary>
		/// 起動日に対する今期期間
		/// </summary>
		private static Span Term { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = SqlServerConnectSettingsIF.GetSettings();

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg;
					return AutoAggregate(Date.Today, out msg);
				}
			}
			Application.Run(new Forms.MainForm());
			return 0;
		}

		/// <summary>
		/// 今期期間の取得
		/// </summary>
		/// <param name="today"></param>
		/// <returns></returns>
		private static Span GetTerm(Date today)
		{
			Date start = new Date((today.Month < 8) ? today.Year - 1 : today.Year, 8, 1);
			Date end = start.PlusYears(1).LastDayOfLasMonth();
			return new Span(start, end);
		}

		/// <summary>
		/// 期の取得
		/// </summary>
		/// <returns></returns>
		private static int GetPeriod()
		{
			int year = BootDate.Year;
			if (8 <= BootDate.Month)
			{
				year++;
			}
			return year - 1975;
		}

		/// <summary>
		/// Excelオリジナルファイルパス名の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <returns></returns>
		private static string GetExcelOriginalPathname(Date today)
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format("見込進捗_{0:D2}期.xlsx.org", today.Year - 1974));
		}

		/// <summary>
		/// Excel出力ファイルパス名の取得
		/// </summary>
		/// <param name="today"></param>
		/// <returns></returns>
		private static string GetExcelPathname(Date today)
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format("見込進捗_{0:D2}期_{1}.xlsx", today.Year - 1974, Date.Today.GetNumeralString()));
		}

		/// <summary>
		/// 自動進捗Excelファイル出力
		/// </summary>
		/// <param name="today">集計日</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>判定</returns>
		public static int AutoAggregate(Date today, out string msg)
		{
			msg = string.Empty;

			売上実績List = null;
			売上予想List = new List<売上予想>();
			ES売上予想List = new List<ES売上予想>();
			予測連絡用_ESList = new List<予測連絡用ES>();
			予測連絡用_まとめList = new List<予測連絡用まとめ>();

			// 起動日の設定
			BootDate = today;

			// 上期８月～来期８月
			Term = GetTerm(BootDate);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				売上実績List = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 >= {0} AND 実績日 <= {1}", Term.Start.ToIntYMD(), Term.End.ToIntYMD()), "実績日, 営業部コード", gSettings.Charlie.ConnectionString);
				if (null == 売上実績List)
				{
					msg = "当日の売上実績が登録されていないため見込進捗を出力できません。";
					return 1;
				}
				Date start = Term.Start;
				for (int i = 0; i < 13; i++)
				{
					List<売上予想> work = ProspectProgressAutoAggregateAccess.Select_売上予想(start, gSettings.Junp.ConnectionString);
					if (null != work)
					{
						売上予想List.AddRange(work);
					}
					start = start.PlusMonths(1);
				}
				ES売上予想List = ProspectProgressAutoAggregateAccess.Select_ES売上予想(Term.Start, Term.End, gSettings.Junp.ConnectionString);
				予測連絡用_ESList = ProspectProgressAutoAggregateAccess.Select_予測連絡用ES(gSettings.Junp.ConnectionString);
				予測連絡用_まとめList = ProspectProgressAutoAggregateAccess.Select_予測連絡用まとめ(gSettings.Junp.ConnectionString);

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
				string pathname = GetExcelPathname(Term.Start);
				File.Copy(GetExcelOriginalPathname(Term.Start), pathname, true);
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					//「予測連絡用」
					予測連絡用(wb);

					//「売上実績」
					売上実績(wb);

					//「見込進捗_詳細」
					見込進捗詳細(wb);

					// Excelファイルの保存
					wb.SaveAs(pathname);

					// カーソルを元に戻す
					Cursor.Current = preCursor;

					// Excelの起動
					ProcessStartInfo pInfo = new ProcessStartInfo();
					pInfo.FileName = pathname;
					Process.Start(pInfo);
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
		private static void 予測連絡用(XLWorkbook wb)
		{
			IXLWorksheet ws = wb.Worksheet("予測連絡用");

			// 当月予算予測進捗値の設定
			予測連絡用_当月予算予測進捗値設定(ws);

			// 翌月予算予測進捗値の設定
			予測連絡用_翌月予算進捗値設定(ws);

			// バージョン情報
			ws.Cell(21, 25).SetValue(VersionStr);

			// 更新日
			ws.Cell(2, 25).SetValue(string.Format("更新日：{0}", DateTime.Now.ToString()));
		}

		/// <summary>
		/// 「予測連絡用」当月予算予測進捗値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		private static void 予測連絡用_当月予算予測進捗値設定(IXLWorksheet ws)
		{
			// 当月初日
			int date = BootDate.FirstDayOfTheMonth().ToIntYMD();
			YearMonth thisYM = BootDate.ToYearMonth();
			for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(6, 5 + j).SetValue(result.予算ES);
					ws.Cell(6, 6 + j).SetValue(result.予算まとめ);
					ws.Cell(6, 7 + j).SetValue(result.予算売上);
					ws.Cell(7, 5 + j).SetValue(result.予測ES);
					ws.Cell(7, 6 + j).SetValue(result.予測まとめ);
					ws.Cell(7, 7 + j).SetValue(result.予測売上);
					ws.Cell(11, 5 + j).SetValue(result.予算営業損益);
					ws.Cell(12, 5 + j).SetValue(result.予測営業損益);
				}
				// 進捗
				int price = 0;
				List<売上予想> sale = 売上予想List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<ES売上予想> soft = ES売上予想List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.計上月 == thisYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				ws.Cell(8, 7 + j).SetValue(To金額千円単位(price)); // 金額千円単位

				List<予測連絡用ES> es = 予測連絡用_ESList.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == thisYM.ToString());
				if (null != es)
				{
					ws.Cell(8, 5 + j).SetValue(es.Count());
				}
				List<予測連絡用まとめ> matome = 予測連絡用_まとめList.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == thisYM.ToString());
				if (null != matome)
				{
					ws.Cell(8, 6 + j).SetValue(matome.Count);
				}
			}
		}

		/// <summary>
		/// 「予測連絡用」翌月予算進捗値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		private static void 予測連絡用_翌月予算進捗値設定(IXLWorksheet ws)
		{
			// 来月初日
			int date = BootDate.FirstDayOfNextMonth().ToIntYMD();
			YearMonth nextYM = BootDate.FirstDayOfNextMonth().ToYearMonth();
			for (int i = 0, j = 0; i < gBumonCodes.Length; i++, j += 3)
			{
				// 予算・予測
				売上実績 result = 売上実績List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == date);
				if (null != result)
				{
					ws.Cell(15, 5 + j).SetValue(result.予算ES);
					ws.Cell(15, 6 + j).SetValue(result.予算まとめ);
					ws.Cell(15, 7 + j).SetValue(result.予算売上);
					ws.Cell(19, 5 + j).SetValue(result.予算営業損益);
				}
				// 進捗
				int price = 0;
				List<売上予想> sale = 売上予想List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.集計月 == nextYM);
				if (null != sale)
				{
					price = sale.Sum(p => p.金額);
				}
				// ソフト保守の売上を加算
				// Ver1.03 予測連絡用の進捗値と見込進捗詳細の順売上高との数値が合わない(2021/09/27 勝呂)
				List<ES売上予想> soft = ES売上予想List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.計上月 == nextYM.ToString());
				if (null != soft)
				{
					price += soft.Sum(p => p.売上金額);
				}
				// 翌月分のpalette売上分には、まとめ分が含まれていないので、「予測連絡用-まとめ」の金額を加算する
				List<予測連絡用まとめ> matome = 予測連絡用_まとめList.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != matome)
				{
					price += matome.Sum(p => p.金額);
					ws.Cell(16, 6 + j).SetValue(matome.Count);
				}
				ws.Cell(16, 7 + j).SetValue(To金額千円単位(price));

				List<予測連絡用ES> es = 予測連絡用_ESList.FindAll(p => p.BshCode2 == gBumonCodes[i] && p.売上月 == nextYM.ToString());
				if (null != es)
				{
					ws.Cell(16, 5 + j).SetValue(es.Count());
				}
			}
		}

		/// <summary>
		/// 「売上実績」
		/// </summary>
		/// <param name="wb">workbool</param>
		private static void 売上実績(XLWorkbook wb)
		{
			IXLWorksheet ws = wb.Worksheet("売上実績");
			Date date = Term.Start;

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
				売上実績 result = 売上実績List.Find(p => p.営業部コード == gBumonCodes[i] && p.実績日 == date);
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
		private static void 見込進捗詳細(XLWorkbook wb)
		{
			IXLWorksheet ws = wb.Worksheet("見込進捗_詳細");
			Date start = Term.Start;

			// 上期８月～来期８月
			for (int i = 0, j = 6; i < 13; i++, j += 4)
			{
				見込進捗詳細_実績値設定(ws, start.ToYearMonth(), j);
				start = start.PlusMonths(1);
			}
			// 更新日
			ws.Cell(2, 2).SetValue(DateTime.Now.ToString());
		}

		/// <summary>
		/// 「見込進捗_詳細」実績値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		/// <param name="ym">該当月</param>
		/// <param name="col">カラム</param>
		private static void 見込進捗詳細_実績値設定(IXLWorksheet ws, YearMonth ym, int col)
		{
			string[] bumon1 = { "081", "082", "083", "086", "087", "085" };					// 東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部
			string[] bumon2 = { "075", "081", "082", "083", "086", "087", "085" };          // ヘルスケア営業部、東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部
			string[] bumon3 = { "081", "082", "083", "086", "087", "085", "011" };			// 東日本営業部、首都圏営業部、関東営業部、中部営業部、関西営業部、西日本営業部、営業管理部

			// 期の取得
			int period = GetPeriod();

			List<売上予想> result = 売上予想List.FindAll(p => p.集計月 == ym);

			// palette売上
			// Ver1.01 新設の商品区分2(0030) paletteおまとめをpalette売上に含める(2021/09/06 勝呂)
			List<売上予想> pca = result.FindAll(p => p.商品区分コード == "28" || p.商品区分コード == "30");
			YearMonth nextYM = BootDate.PlusMonths(1).ToYearMonth();
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
					// 翌月分のpalette売上分には、まとめ分が含まれていないので、「予測連絡用-まとめ」の金額を加算する
					List<予測連絡用まとめ> matome = 予測連絡用_まとめList.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == nextYM.ToString());
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
			List<ES売上予想> soft = ES売上予想List.FindAll(p => p.計上月 == ym.ToString());
			for (int i = 0, j = ROW_ソフト保守; i < bumon1.Length; i++, j++)
			{
				List<ES売上予想> sale = soft.FindAll(p => p.部門コード == bumon1[i]);
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
