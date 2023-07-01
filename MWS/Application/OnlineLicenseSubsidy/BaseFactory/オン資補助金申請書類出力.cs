//
// オン資補助金申請書類出力.cs
// 
// オン資補助金申請書類出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/09/16 勝呂):新規作成
// Ver1.01(2022/11/10 勝呂):経理部動作確認後、要望対応
// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
// Ver1.06(2023/02/07 勝呂):経理部要望対応 NTT以外の案件も作業リストから補助金申請書類を出力できるようにする
// Ver1.11(2023/06/16 勝呂):事業完了報告書、領収内訳書、送付先リストの読込時のエラーメッセージにファイル名を表示
//
using ClosedXML.Excel;
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineLicenseSubsidy.BaseFactory
{
	public static class オン資補助金申請書類出力
	{
		/// <summary>
		/// オン資補助金申請書類作業リスト.xlsx「顧客情報」シート名
		/// </summary>
		private const string WorkListSheetNameCustomer = "顧客情報";

		/// <summary>
		/// オン資補助金申請書類作業リスト.xlsx「領収書内訳書」シート名
		/// </summary>
		private const string WorkListSheetNameReceipt = "領収書内訳書";

		/// <summary>
		/// オン資補助金申請書類作業リスト.xlsx「注文確認書」シート名
		/// </summary>
		// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
		private const string WorkListSheetNameOrder = "注文確認書";

		/// <summary>
		/// オン資補助金申請書類「領収書内訳書」シート名
		/// </summary>
		private const string SubsidySheetNameReceipt = "領収書内訳書";

		/// <summary>
		/// オン資補助金申請書類「事業完了報告書」シート名
		/// </summary>
		private const string SubsidySheetNameReport = "事業完了報告書";

		/// <summary>
		/// オン資補助金申請書類「注文確認書」シート名
		/// </summary>
		// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
		private const string SubsidySheetNameOrder = "注文確認書";

		/// <summary>
		/// 補助金申請書類出力フォルダ名
		/// </summary>
		public const string SubsidyFolderName = "助成金申請書類";

		/// <summary>
		/// オン資補助金申請書類出力作業リスト originファイル名
		/// </summary>
		public const string OrgWorkListFilename = "オン資補助金申請書類作業リスト.xlsx.org";

		/// <summary>
		/// オン資補助金申請書類 originファイル名
		/// </summary>
		public const string OrgSubsidyFilename = "オン資補助金申請書類.xlsx.org";

		/// <summary>
		/// オン資補助金申請書類出力作業リスト 出力ファイル名
		/// </summary>
		/// <param name="east">NTT東日本</param>
		/// <returns>出力ファイル名</returns>
		public static string WorkListFilename(bool east)
		{
			if (east)
			{
				return string.Format("オン資補助金申請書類作業リスト_NTT東日本_{0}.xlsx", Date.Today.GetNumeralString());
			}
			return string.Format("オン資補助金申請書類作業リスト_NTT西日本_{0}.xlsx", Date.Today.GetNumeralString());
		}

		/// <summary>
		/// Double型の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		public static double GetDoubleData(IXLCell cell)
		{
			if (XLDataType.Number == cell.DataType)
			{
				return cell.GetDouble();
			}
			if (XLDataType.Text == cell.DataType)
			{
				return 0;
			}
			return 0;
		}

		/// <summary>
		/// 日付型の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		public static DateTime? GetDateTimeData(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				return cell.GetDateTime();
			}
			return null;
		}

		/// <summary>
		/// 事業完了報告書の読込
		/// </summary>
		/// <param name="ww">vMicユーザーオン資用</param>
		/// <param name="acceptNo">受付通番</param>
		/// <param name="pathname">完了報告書パス名</param>
		/// <param name="east">NTT東日本</param>
		/// <returns>補助金申請情報</returns>
		public static 補助金申請情報 ReadExcel事業完了報告書(vMicユーザーオン資用 ww, string acceptNo, string pathname, bool east)
		{
			if (File.Exists(pathname))
			{
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						// シート名が固定でないので検索する
						IXLWorksheet ws = null;
						foreach (var sheet in wb.Worksheets)
						{
							if (-1 != sheet.Name.IndexOf("別紙様式3"))
							{
								ws = sheet;
								break;
							}
						}
						補助金申請情報 data = new 補助金申請情報();
						data.顧客情報WW = ww;
						data.顧客情報WW.顧客名 = data.顧客情報WW.顧客名.Replace("　", " ");
						data.顧客情報WW.開設者 = data.顧客情報WW.開設者.Replace("　", " ");
						data.顧客情報WW.院長名 = data.顧客情報WW.院長名.Replace("　", " ");
						data.受付通番 = acceptNo;
						string yearStr = ws.Cell(2, 20).GetString().Trim();
						string monthStr = ws.Cell(2, 23).GetString().Trim();
						string dayStr = ws.Cell(2, 25).GetString().Trim();
						int year, month, day;
						int.TryParse(yearStr, out year);
						int.TryParse(monthStr, out month);
						int.TryParse(dayStr, out day);
						if (0 < year && 0 < month && 0 < day)
						{
							data.工事完了日 = new DateTime(year, month, day);
						}
						data.医療機関コード = ws.Cell(8, 19).GetString().Trim();
						data.医療機関コード += ws.Cell(8, 20).GetString().Trim();
						data.医療機関コード += ws.Cell(8, 21).GetString().Trim();
						data.医療機関コード += ws.Cell(8, 22).GetString().Trim();
						data.医療機関コード += ws.Cell(8, 23).GetString().Trim();
						data.医療機関コード += ws.Cell(8, 24).GetString().Trim();
						data.医療機関コード += ws.Cell(8, 25).GetString().Trim();
						data.顧客名 = ws.Cell(9, 19).GetString().Trim();
						data.開設者 = ws.Cell(10, 19).GetString().Trim();
						data.顧客名 = data.顧客名.Replace("　", " ");
						data.開設者 = data.開設者.Replace("　", " ");
						if (east)
						{
							data.郵便番号 = ws.Cell(11, 19).GetString().Trim();
						}
						else
						{
							data.郵便番号 = ws.Cell(11, 18).GetString().Trim();
						}
						data.住所 = ws.Cell(12, 15).GetString().Trim();
						data.電話番号 = ws.Cell(13, 19).GetString().Trim();
						return data;
					}
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("ReadExcel事業完了報告書({0})\r\n{1}", ex.Message, pathname));
				}
			}
			else
			{
				throw new Exception(string.Format("ReadExcel事業完了報告書({0})", string.Format("{0}が見つかりません。", pathname)));
			}
		}

		/// <summary>
		/// 領収書内訳書の読込
		/// </summary>
		/// <param name="pathname">領収書内訳書パス名</param>
		/// <returns>領収内訳情報リスト</returns>
		public static List<領収内訳情報> ReadExcel領収書内訳書(string pathname)
		{
			if (File.Exists(pathname))
			{
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						// シート名が固定でないので検索する
						IXLWorksheet ws = null;
						foreach (var sheet in wb.Worksheets)
						{
							if (-1 != sheet.Name.IndexOf("別紙様式2"))
							{
								ws = sheet;
								break;
							}
						}
						List<領収内訳情報> list = new List<領収内訳情報>();
						for (int i = 0, j = 17; i < 15; i++, j++)
						{
							if ("" == ws.Cell(j, 4).GetString())
							{
								break;
							}
							領収内訳情報 data = new 領収内訳情報();
							data.項目 = ws.Cell(j, 4).GetString().Trim();
							data.内訳 = ws.Cell(j, 14).GetString().Trim();
							data.補助対象金額 = GetDoubleData(ws.Cell(j, 36));
							data.補助対象外金額 = GetDoubleData(ws.Cell(j, 41));
							list.Add(data);
						}
						return list;
					}
				}
				catch (Exception ex)
				{
					// Ver1.11(2023/06/16 勝呂):事業完了報告書、領収内訳書、送付先リストの読込時のエラーメッセージにファイル名を表示
					throw new Exception(string.Format("ReadExcel領収内訳書({0})\r\n{1}", ex.Message, pathname));
				}
			}
			else
			{
				throw new Exception(string.Format("ReadExcel領収内訳書({0})", string.Format("{0}が見つかりません。", pathname)));
			}
		}

		/// <summary>
		/// オン資補助金申請書類作業リスト.xlsxの出力
		/// </summary>
		/// <param name="dataList">補助金申請情報リスト</param>
		/// <param name="pathname">オン資補助金申請書類作業リスト.xlsx</param>
		public static void WriteExcel作業リスト(List<補助金申請情報> dataList, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws1 = wb.Worksheet(WorkListSheetNameCustomer);
					IXLWorksheet ws2 = wb.Worksheet(WorkListSheetNameReceipt);
					IXLWorksheet ws3 = wb.Worksheet(WorkListSheetNameOrder);
					XLColor color = XLColor.Pink;
					for (int i = 0, j = 3, x = 3; i < dataList.Count; i++, j++)
					{
						補助金申請情報 data = dataList[i];

						////////////////////////////////////////
						//「顧客情報」
						////////////////////////////////////////

						// 基本情報
						ws1.Cell(j, 1).SetValue(data.受付通番);
						ws1.Cell(j, 2).SetValue(data.顧客情報WW.得意先No);
						ws1.Cell(j, 3).SetValue(data.顧客情報WW.顧客No);

						// NTT顧客情報
						// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
						ws1.Cell(j, 4).SetValue(data.顧客名);
						ws1.Cell(j, 5).SetValue(data.医療機関コード);
						ws1.Cell(j, 6).SetValue(data.開設者);
						ws1.Cell(j, 7).SetValue(data.郵便番号);
						ws1.Cell(j, 8).SetValue(data.住所);
						ws1.Cell(j, 9).SetValue(data.電話番号);

						// MIC顧客情報
						// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
						ws1.Cell(j, 10).SetValue(data.顧客情報WW.顧客名);
						ws1.Cell(j, 11).SetValue(data.顧客情報WW.GetClinicCodeNumeric());

						// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
						string founder = data.顧客情報WW.開設者;
						if (0 == data.顧客情報WW.開設者.Length)
						{
							founder = data.顧客情報WW.院長名;
						}
						ws1.Cell(j, 12).SetValue(founder);
						ws1.Cell(j, 13).SetValue(data.顧客情報WW.郵便番号);
						ws1.Cell(j, 14).SetValue(data.顧客情報WW.住所);
						ws1.Cell(j, 15).SetValue(data.顧客情報WW.電話番号);

						// 出力する顧客情報  ※NTT顧客情報を設定
						ws1.Cell(j, 16).SetValue(data.顧客情報WW.顧客名);
						ws1.Cell(j, 17).SetValue(data.医療機関コード);
						ws1.Cell(j, 18).SetValue(data.開設者);
						ws1.Cell(j, 19).SetValue(data.郵便番号);
						ws1.Cell(j, 20).SetValue(data.住所);
						ws1.Cell(j, 21).SetValue(data.電話番号);
						ws1.Cell(j, 22).SetValue(data.工事完了日);

						// 相違点は赤で表示する
						// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
						if (data.顧客名 != data.顧客情報WW.顧客名)
						{
							ws1.Cell(j, 16).Style.Font.SetFontColor(XLColor.Red);
						}
						if (data.医療機関コード != data.顧客情報WW.GetClinicCodeNumeric())
						{
							ws1.Cell(j, 17).Style.Font.SetFontColor(XLColor.Red);
						}
						// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
						if (data.開設者 != founder)
						{
							ws1.Cell(j, 18).Style.Font.SetFontColor(XLColor.Red);
						}
						if (data.郵便番号 != data.顧客情報WW.郵便番号)
						{
							ws1.Cell(j, 19).Style.Font.SetFontColor(XLColor.Red);
						}
						if (data.住所 != data.顧客情報WW.住所)
						{
							ws1.Cell(j, 20).Style.Font.SetFontColor(XLColor.Red);
						}
						if (data.電話番号 != data.顧客情報WW.電話番号)
						{
							ws1.Cell(j, 21).Style.Font.SetFontColor(XLColor.Red);
						}

						////////////////////////////////////////
						// 「領収書内訳書」
						////////////////////////////////////////

						// 基本情報
						ws2.Cell(j, 1).SetValue(data.顧客情報WW.得意先No);

						// 領収書内訳書XX行目
						for (int k = 0, c = 2; k < data.領収内訳情報List.Count; k++, c += 4)
						{
							領収内訳情報 item = data.領収内訳情報List[k];
							ws2.Cell(j, c).SetValue(item.項目);
							ws2.Cell(j, c + 1).SetValue(item.内訳);
							ws2.Cell(j, c + 2).SetValue(item.補助対象金額);
							ws2.Cell(j, c + 3).SetValue(item.補助対象外金額);
						}
						// 計
						// 補助対象金額小計
						ws2.Cell(j, 62).SetValue(data.補助対象金額小計());

						// 補助対象外金額小計
						ws2.Cell(j, 63).SetValue(data.補助対象外金額小計());

						// 総額
						ws2.Cell(j, 64).SetValue(data.総額());

						////////////////////////////////////////
						// 「注文確認書」
						////////////////////////////////////////

						// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
						if (data.IsExist注文確認書)
						{
							// 基本情報
							ws3.Cell(x, 1).SetValue(data.顧客情報WW.得意先No);

							// 発送日
							ws3.Cell(x, 2).SetValue(data.発送日);

							// 受注日
							ws3.Cell(x, 3).SetValue(data.受注日);

							// 金額（税込）
							ws3.Cell(x, 4).SetValue(data.金額);
							x++;
						}
					}
					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("WriteExcel作業リスト({0})", ex.Message));
			}
		}

		/// <summary>
		/// 作業リストファイルの読込
		/// </summary>
		/// <param name="pathname">オン資補助金申請書類作業リスト.xlsx</param>
		/// <returns>助成金申請出力情報リスト</returns>
		public static List<補助金申請出力情報> ReadExcel作業リスト(string pathname)
		{
			if (File.Exists(pathname))
			{
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						List<補助金申請出力情報> list = new List<補助金申請出力情報>();

						////////////////////////////////////////
						//「顧客情報」
						////////////////////////////////////////

						IXLWorksheet ws1 = wb.Worksheet(WorkListSheetNameCustomer);
						for (int i = 3; ; i++)
						{
							// Ver1.06(2023/02/01 勝呂):経理部要望対応 NTT以外の案件も作業リストから補助金申請書類を出力できるようにする
							string tokuisakiNo = ws1.Cell(i, 2).GetString();
							if (0 == tokuisakiNo.Length)
							{
								break;
							}
							補助金申請出力情報 data = new 補助金申請出力情報();
							data.得意先番号 = tokuisakiNo;

							// Ver1.06(2023/02/01 勝呂):経理部要望対応 NTT以外の案件も作業リストから補助金申請書類を出力できるようにする
							int customerNo;
							if (int.TryParse(ws1.Cell(i, 3).GetString(), out customerNo))
							{
								data.顧客No = customerNo;
								data.受付通番 = ws1.Cell(i, 1).GetString();
							}
							// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
							data.顧客名 = ws1.Cell(i, 16).GetString();
							data.医療機関コード = ws1.Cell(i, 17).GetString();
							data.開設者 = ws1.Cell(i, 18).GetString();
							data.郵便番号 = ws1.Cell(i, 19).GetString();
							data.住所 = ws1.Cell(i, 20).GetString();
							data.電話番号 = ws1.Cell(i, 21).GetString();
							data.工事完了日 = GetDateTimeData(ws1.Cell(i, 22));

							list.Add(data);
						}

						////////////////////////////////////////
						// 「領収書内訳書」
						////////////////////////////////////////

						IXLWorksheet ws2 = wb.Worksheet(WorkListSheetNameReceipt);
						for (int i = 3; ; i++)
						{
							string tokuisakiNo = ws2.Cell(i, 1).GetString();
							if (0 == tokuisakiNo.Length)
							{
								break;
							}
							補助金申請出力情報 data = list.Find(p => p.得意先番号 == tokuisakiNo);
							if (null != data)
							{
								for (int j = 2; j < 62; j += 4)
								{
									if ("" == ws2.Cell(i, j).GetString())
									{
										break;
									}
									領収内訳情報 meisai = new 領収内訳情報();
									meisai.項目 = ws2.Cell(i, j).GetString();
									meisai.内訳 = ws2.Cell(i, j + 1).GetString();
									meisai.補助対象金額 = GetDoubleData(ws2.Cell(i, j + 2));
									meisai.補助対象外金額 = GetDoubleData(ws2.Cell(i, j + 3));
									data.領収内訳情報List.Add(meisai);
								}
							}
						}

						////////////////////////////////////////
						// 「注文確認書」
						////////////////////////////////////////

						// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
						IXLWorksheet ws3 = wb.Worksheet(WorkListSheetNameOrder);

						for (int i = 3; ; i++)
						{
							string tokuisakiNo = ws3.Cell(i, 1).GetString();
							if (0 == tokuisakiNo.Length)
							{
								break;
							}
							補助金申請出力情報 data = list.Find(p => p.得意先番号 == tokuisakiNo);
							if (null != data)
							{
								data.発送日 = GetDateTimeData(ws3.Cell(i, 2));
								data.受注日 = GetDateTimeData(ws3.Cell(i, 3));
								data.金額 = GetDoubleData(ws3.Cell(i, 4));
							}
						}
						return list;
					}
				}
				catch (Exception ex)
				{
					// Ver1.11(2023/06/16 勝呂):事業完了報告書、領収内訳書、送付先リストの読込時のエラーメッセージにファイル名を表示
					throw new Exception(string.Format("ReadExcel作業リスト({0})\r\n{1}", ex.Message, pathname));
				}
			}
			else
			{
				throw new Exception(string.Format("ReadExcel作業リスト({0})", string.Format("{0}が見つかりません。", pathname)));
			}
		}

		/// <summary>
		/// 補助金申請書類の出力
		/// </summary>
		/// <param name="orgPathname">オン資補助金申請書類.xlsx.org</param>
		/// <param name="outputPathname">オン資補助金申請書類エクセルファイル</param>
		/// <param name="data">助成金申請出力情報</param>
		public static void WriteExcel補助金申請書類(string orgPathname, string outputPathname, 補助金申請出力情報 data)
		{
			try
			{
				File.Copy(orgPathname, outputPathname, true);
				using (XLWorkbook wb = new XLWorkbook(outputPathname, XLEventTracking.Disabled))
				{
					////////////////////////////////////////
					// 「領収書内訳書」
					////////////////////////////////////////

					IXLWorksheet ws1 = wb.Worksheet(SubsidySheetNameReceipt);
					if (data.工事完了日.HasValue)
					{
						ws1.Cell(3, 42).SetValue(string.Format("{0,4}", data.工事完了日.Value.Year));
						ws1.Cell(3, 45).SetValue(string.Format("{0,2}", data.工事完了日.Value.Month));
						ws1.Cell(3, 47).SetValue(string.Format("{0,2}", data.工事完了日.Value.Day));
					}
					string ken = data.GetKenNumberString();
					if (KenNumDef.Length == ken.Length)
					{
						ws1.Cell(6, 9).SetValue(ken.Substring(0, 1));
						ws1.Cell(6, 10).SetValue(ken.Substring(1, 1));
					}
					if (MwsDefine.ClinicCodeLength == data.医療機関コード.Length)
					{
						ws1.Cell(8, 9).SetValue(data.医療機関コード.Substring(0, 1));
						ws1.Cell(8, 10).SetValue(data.医療機関コード.Substring(1, 1));
						ws1.Cell(8, 11).SetValue(data.医療機関コード.Substring(2, 1));
						ws1.Cell(8, 12).SetValue(data.医療機関コード.Substring(3, 1));
						ws1.Cell(8, 13).SetValue(data.医療機関コード.Substring(4, 1));
						ws1.Cell(8, 14).SetValue(data.医療機関コード.Substring(5, 1));
						ws1.Cell(8, 16).SetValue(data.医療機関コード.Substring(6, 1));
					}
					// Ver1.01(2022/11/10 勝呂):経理部動作確認後、要望対応
					//ws1.Cell(10, 9).SetValue(string.Format("{0}  様", data.顧客名));
					ws1.Cell(10, 9).SetValue(data.顧客名);

					//ws1.Cell(8, 40).SetValue(common.社名);
					//ws1.Cell(10, 40).SetValue(common.住所1);
					//ws1.Cell(12, 40).SetValue(common.電話番号);

					for (int i = 0, j = 16; i < data.領収内訳情報List.Count; i++, j++)
					{
						// 項目
						ws1.Cell(j, 4).SetValue(data.領収内訳情報List[i].項目);

						// 内訳
						ws1.Cell(j, 16).SetValue(data.領収内訳情報List[i].内訳);

						// ①補助対象金額
						if (0 < data.領収内訳情報List[i].補助対象金額)
						{
							ws1.Cell(j, 38).SetValue(data.領収内訳情報List[i].補助対象金額);
						}
						if (0 == data.領収内訳情報List[i].補助対象金額 || 0 < data.領収内訳情報List[i].補助対象外金額)
						{
							// ②補助対象外金額
							ws1.Cell(j, 44).SetValue(data.領収内訳情報List[i].補助対象外金額);
						}
					}
					// ①小計
					//ws1.Cell(31, 42).SetValue(total);

					// ②小計
					//;

					// 総額（①＋②）
					//ws1.Cell(12, 9).SetValue(total);

					////////////////////////////////////////
					// 「事業完了報告書」
					////////////////////////////////////////

					IXLWorksheet ws2 = wb.Worksheet(SubsidySheetNameReport);
					if (data.工事完了日.HasValue)
					{
						ws2.Cell(2, 20).SetValue(string.Format("{0,4}", data.工事完了日.Value.Year));
						ws2.Cell(2, 23).SetValue(string.Format("{0,2}", data.工事完了日.Value.Month));
						ws2.Cell(2, 25).SetValue(string.Format("{0,2}", data.工事完了日.Value.Day));
					}
					if (KenNumDef.Length == ken.Length)
					{
						ws2.Cell(7, 19).SetValue(ken.Substring(0, 1));
						ws2.Cell(7, 20).SetValue(ken.Substring(1, 1));
					}
					if (MwsDefine.ClinicCodeLength == data.医療機関コード.Length)
					{
						ws2.Cell(8, 19).SetValue(data.医療機関コード.Substring(0, 1));
						ws2.Cell(8, 20).SetValue(data.医療機関コード.Substring(1, 1));
						ws2.Cell(8, 21).SetValue(data.医療機関コード.Substring(2, 1));
						ws2.Cell(8, 22).SetValue(data.医療機関コード.Substring(3, 1));
						ws2.Cell(8, 23).SetValue(data.医療機関コード.Substring(4, 1));
						ws2.Cell(8, 24).SetValue(data.医療機関コード.Substring(5, 1));
						ws2.Cell(8, 25).SetValue(data.医療機関コード.Substring(6, 1));
					}
					ws2.Cell(9, 19).SetValue(data.顧客名);
					ws2.Cell(11, 18).SetValue(data.郵便番号);
					ws2.Cell(12, 15).SetValue(data.住所);
					ws2.Cell(13, 19).SetValue(data.電話番号);
					ws2.Cell(10, 19).SetValue(data.開設者);

					////////////////////////////////////////
					// 「注文確認書」
					////////////////////////////////////////

					// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
					if (data.IsExist注文確認書)
					{
						IXLWorksheet ws3 = wb.Worksheet(SubsidySheetNameOrder);
						ws3.Cell(1, 5).SetValue(data.発送日);
						ws3.Cell(6, 1).SetValue(data.顧客名);
						ws3.Cell(14, 2).SetValue(data.受注日);
						//ws3.Cell(15, 2).SetValue(data.金額);
						ws3.Cell(18, 4).SetValue(data.金額);
						ws3.Cell(18, 5).SetValue(data.金額);
					}
					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("WriteExcel補助金申請書類({0})", ex.Message));
			}
		}
	}
}
