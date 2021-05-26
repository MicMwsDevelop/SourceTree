using ClosedXML.Excel;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.ProspectProgressAutoAggregate;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.ProspectProgressAutoAggregate;
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
		/// データベース接続先
		/// </summary>
		private const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.00 (2021/05/20)";

		/// <summary>
		/// 部門コード
		/// </summary>
		private static string[] gBumonCodes = { "50", "70", "60", "75", "76", "80" };

		/// <summary>
		/// PCA部門コード
		/// </summary>
		private static string[] gPcaBumonCoeds = { "081", "082", "083", "086", "087", "085" };

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
		/// 期間の取得
		/// </summary>
		private static Tuple<Date, Date> 期間
		{
			get
			{
				Date today = Date.Today;
				Date start = new Date((today.Month < 8) ? today.Year - 1 : today.Year, 8, 1);
				Date end = new Date((today.Month < 8) ? today.Year : today.Year - 1, 7, 1);
				end = new Date(end.Year, end.Month, end.GetDaysInMonth());
				return new Tuple<Date, Date>(start, end);
			}
		}

		/// <summary>
		/// Excelファイルパス名の取得
		/// </summary>
		private static string GetExcelPathname
		{
			get
			{
				return Path.Combine(Directory.GetCurrentDirectory(), string.Format("見込進捗_{0:D2}期.xlsx", 期間.Item1.Year - 1974));
			}
		}

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg;
					return AutoAggregate(out msg);
				}
			}
			Application.Run(new MainForm());
			return 0;
		}

		/// <summary>
		/// 自動進捗Excelファイル出力
		/// </summary>
		public static int AutoAggregate(out string msg)
		{
			msg = string.Empty;

			売上実績List = null;
			売上予想List = new List<売上予想>();
			ES売上予想List = new List<ES売上予想>();
			予測連絡用_ESList = new List<予測連絡用ES>();
			予測連絡用_まとめList = new List<予測連絡用まとめ>();

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 上期８月～来期８月
				売上実績List = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 >= {0} and 実績日 <= {1}", 期間.Item1.ToIntYMD(), 期間.Item2.ToIntYMD()), "実績日, 営業部コード", DATABASE_ACCESS_CT);
				Date start = 期間.Item1;
				for (int i = 0; i < 13; i++)
				{
					List<売上予想> work = ProspectProgressAutoAggregateAccess.Select_売上予想(start, DATABASE_ACCESS_CT);
					if (null != work)
					{
						売上予想List.AddRange(work);
					}
					start = start.PlusMonths(1);
				}
				ES売上予想List = ProspectProgressAutoAggregateAccess.Select_ES売上予想(期間.Item1, 期間.Item2, DATABASE_ACCESS_CT);
				予測連絡用_ESList = ProspectProgressAutoAggregateAccess.Select_予測連絡用ES(DATABASE_ACCESS_CT);
				予測連絡用_まとめList = ProspectProgressAutoAggregateAccess.Select_予測連絡用まとめ(DATABASE_ACCESS_CT);

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
				string pathname = GetExcelPathname;
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
			ws.Cell(21, 23).SetValue(VersionStr);

			// 更新日
			ws.Cell(2, 23).SetValue(DateTime.Now.ToString());
		}

		/// <summary>
		/// 「予測連絡用」当月予算予測進捗値設定
		/// </summary>
		/// <param name="ws">worksheet</param>
		private static void 予測連絡用_当月予算予測進捗値設定(IXLWorksheet ws)
		{
			// 当月初日
			int date = Date.Today.FirstDayOfTheMonth().ToIntYMD();
			YearMonth thisYM = Date.Today.ToYearMonth();
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
				List<売上予想> sale = 売上予想List.FindAll(p => p.部門コード == gPcaBumonCoeds[i] && p.集計月 == thisYM);
				if (null != sale)
				{
					ws.Cell(8, 7 + j).SetValue(sale.Sum(p => p.金額千円単位));
				}
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
			int date = Date.Today.FirstDayOfNextMonth().ToIntYMD();
			YearMonth nextYM = Date.Today.FirstDayOfNextMonth().ToYearMonth();
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
			Date date = 期間.Item1;

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
			ws.Cell(2, 23).SetValue(DateTime.Now.ToString());
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

		/// <summary>
		/// 「見込進捗_詳細」
		/// </summary>
		/// <param name="wb">workbook</param>
		private static void 見込進捗詳細(XLWorkbook wb)
		{
			IXLWorksheet ws = wb.Worksheet("見込進捗_詳細");
			Date start = 期間.Item1;

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
			string[] bumon1 = { "081", "082", "083", "086", "087", "085" };
			string[] bumon2 = { "081", "082", "083", "086", "087", "085", "075" };
			string[] bumon3 = { "081", "082", "083", "086", "087", "085", "011", "075" };

			List<売上予想> result = 売上予想List.FindAll(p => p.集計月 == ym);

			// palette売上
			List<売上予想> work = result.FindAll(p => p.商品区分コード == "28");
			YearMonth nextYM = Date.Today.PlusMonths(1).ToYearMonth();
			if (ym == nextYM)
			{
				// 翌月
				for (int i = 0, j = 6; i < bumon1.Length; i++, j++)
				{
					int palette = 0;
					List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
					if (null != price)
					{
						palette = price.Sum(p => p.金額);
					}
					// 翌月分のpalette売上分には、まとめ分が含まれていないので、「予測連絡用-まとめ」の金額を加算する
					List<予測連絡用まとめ> matome = 予測連絡用_まとめList.FindAll(p => p.営業部コード == gBumonCodes[i] && p.売上月 == nextYM.ToString());
					if (null != matome)
					{
						palette += matome.Sum(p => p.金額);
					}
					ws.Cell(j, col).SetValue(To金額千円単位(palette));
				}
			}
			else
			{
				// 翌月以外
				for (int i = 0, j = 6; i < bumon1.Length; i++, j++)
				{

					List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
					if (null != price)
					{
						ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
					}
				}
			}
			// paletteES
			work = result.FindAll(p => p.商品区分コード == "27");
			for (int i = 0, j = 13; i < bumon1.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// その他ｿﾌﾄ売上
			work = result.FindAll(p => p.商品区分コード == "1");
			for (int i = 0, j = 20; i < bumon2.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon2[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// ハード売上
			work = result.FindAll(p => p.商品区分コード == "2");
			for (int i = 0, j = 28; i < bumon1.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// 技術指導売上
			work = result.FindAll(p => p.商品区分コード == "40");
			for (int i = 0, j = 35; i < bumon1.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// ハード保守
			work = result.FindAll(p => p.商品区分コード == "7");
			for (int i = 0, j = 49; i < bumon1.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// ソフト保守
			// ソフト保守はES売上予想Listから金額を取得
			List<ES売上予想> soft = ES売上予想List.FindAll(p => p.計上月 == ym.ToString());
			for (int i = 0, j = 56; i < bumon1.Length; i++, j++)
			{
				List<ES売上予想> price = soft.FindAll(p => p.部門コード == bumon1[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.売上金額)));
				}
			}
			// 周辺機器売上
			work = result.FindAll(p => p.商品区分コード == "97");
			for (int i = 0, j = 63; i < bumon1.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon1[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// その他売上
			// ※50:ｵﾝﾗｲﾝ資格確認導入をその他に含める
			work = result.FindAll(p => p.商品区分コード == "99" || p.商品区分コード == "50");
			for (int i = 0, j = 70; i < bumon3.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon3[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// Curline本体
			work = result.FindAll(p => p.商品区分コード == "201");
			for (int i = 0, j = 79; i < bumon3.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon3[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
				}
			}
			// Curline替ﾌﾞﾗｼ
			work = result.FindAll(p => p.商品区分コード == "202");
			for (int i = 0, j = 88; i < bumon3.Length; i++, j++)
			{
				List<売上予想> price = work.FindAll(p => p.部門コード == bumon3[i]);
				if (null != price)
				{
					ws.Cell(j, col).SetValue(To金額千円単位(price.Sum(p => p.金額)));
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
